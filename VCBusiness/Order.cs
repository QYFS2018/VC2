using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCBusiness.Model;
using WComm;
using DotNetOpenMail;



namespace VCBusiness
{
    public class Order : BaseOrder
    {
        #region Download

        public override ReturnValue Download()
        {
            ReturnValue _result = new ReturnValue();

            Common.Connect();

            #region get order list

            Model.TOrder _tOrder = Common.CreateObject(this.Owner, "TOrder") as Model.TOrder;
            _result = _tOrder.getDownloadOrderList();
            if (_result.Success == false)
            {
                _result.Success = false;
                _result.ErrMessage = "getDownloadOrderList failed. \r\n " + _result.ErrMessage;

                Common.Log("getDownloadOrderList---ER \r\n" + _result.ErrMessage);

                return _result;
            }

            EntityList orderList = _result.ObjectList;

            #endregion

            #region post order to VerCore

            foreach (TOrder order in orderList)
            {
                #region get order line

                TOrder_Line_Item _tOrder_Line_Item = new TOrder_Line_Item();
                _result = _tOrder_Line_Item.getOrderLineByOrderId(order.OrderId);
                if (_result.Success == false)
                {
                    errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                    failedRecord++;

                    Common.Log("Order : " + order.OrderId + "  getOrderLineByOrderId---ER \r\n" + _result.ErrMessage);

                    continue;
                }

                EntityList orderItemList = _result.ObjectList;

                if (orderItemList.Count() == 0)
                {
                    _result.ErrMessage = "Can't find Line Item";
                    errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                    failedRecord++;

                    Common.Log("Order : " + order.OrderId + "  getOrderLineByOrderId---ER \r\n" + _result.ErrMessage);

                    continue;
                }


                #endregion

                #region fill data

                order.ProgramID = int.Parse(Owner.OwnerInfo["ProgramID"].ToString());


                if (Owner.OwnerInfo.ContainsKey("FreightService") == true)  // VeraCore Default Shipmethod
                {
                   ((TOrder_Line_Item) orderItemList[0] ).ShipMethod= Owner.OwnerInfo["FreightService"].ToString();
                }

                _result = this.customerEventPreOrderDownload(order, orderItemList);
                if (_result.Success == false)
                {
                    errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                    failedRecord++;

                    Common.Log("Order : " + order.OrderId + "  customerData---ER \r\n" + _result.ErrMessage);

                    continue;
                }

                #endregion

                #region ExpectedShipDate

                if (this.Owner.OwnerInfo.ContainsKey("ExpectedShipDate") == true && this.Owner.OwnerInfo["ExpectedShipDate"].ToString() == "Y")
                {
                    _result = this.updateExpectedShipDate(order, orderItemList);
                    if (_result.Success == false)
                    {
                        errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                        failedRecord++;

                        Common.Log("Order : " + order.OrderId + "  updateExpectedShipDate---ER \r\n" + _result.ErrMessage);

                        continue;
                    }

                }

                #endregion

                #region post order to VeraCore

                _result = VeraCore.PostOrder(order, orderItemList);
                if (_result.Success == false)
                {
                    if (_result.ErrMessage.IndexOf("already exists") > -1)
                    {
                        _result.ErrMessage = "The Order Already Exists";
                    }

                    errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                    failedRecord++;

                    Common.Log("Order : " + order.OrderId + "  PostOrder---ER \r\n" + _result.ErrMessage);

                    continue;
                }

                 #endregion

                #region release  order line

                _result = _tOrder_Line_Item.ReleaseOrderLineByOrderId(order.OrderId);
                if (_result.Success == false)
                {
                    errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                    failedRecord++;

                    Common.Log("Order : " + order.OrderId + "  ReleaseOrderLineByOrderId---ER \r\n" + _result.ErrMessage);

                    continue;
                }

                #endregion

                #region ImportDMOrderDetail

                if (this.Owner.OwnerInfo["ImportDM"].ToString() == "Y")
                {
                    _result = this.ImportDMOrderDetail(order.OrderId);
                    if (_result.Success == false)
                    {
                        errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                        failedRecord++;

                        Common.Log("Order : " + order.OrderId + "  ImportDMOrderDetail---ER \r\n" + _result.ErrMessage);

                        continue;
                    }
                }
                #endregion

                successfulRecord++;
                Common.Log("Order : " + order.OrderId + "---OK");
            }

            #endregion

            #region update stock

            Model.TProduct _tProduct = Common.CreateObject(this.Owner, "TProduct") as Model.TProduct;
            _result = _tProduct.resetProductEstcommitted();
            if (_result.Success == false)
            {
                _result.Success = false;
                _result.ErrMessage = "resetProductEstcommitted failed. \r\n " + _result.ErrMessage;

                Common.Log("resetProductEstcommitted---ER \r\n" + _result.ErrMessage);

                return _result;
            }



            #endregion

            Common.SentAlterEmail(failedRecord, errorNotes);

            _result.Success = true;

            return _result;
        }

        protected virtual ReturnValue updateExpectedShipDate(TOrder order, EntityList orderline)
        {
            ReturnValue _result = new ReturnValue();

            if (orderline.Count == 0)
            {
                return _result;
            }


            Model.TOrder_Line_Item _line0 = orderline[0] as Model.TOrder_Line_Item;

            #region calcuate ExpectedShipDate
     
            TOrder _tOrderExpectedShipDate = new TOrder();
            _tOrderExpectedShipDate.DataConnectProviders = "ZoytoCommon";
            _result = _tOrderExpectedShipDate.getOrderExpectedShipDate(order.ProgramID, System.DateTime.Now, _line0.ShipMethod);
            if (_result.Success == false)
            {
                return _result;
            }
            _tOrderExpectedShipDate = _result.Object as TOrder;

            if (Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["IsTestMode"].ToString()) == true)
            {
                _tOrderExpectedShipDate.ExpectedShipDate = System.DateTime.Now.AddDays(5);
            }


            if (_tOrderExpectedShipDate.ExpectedShipDate==null || _tOrderExpectedShipDate.ExpectedShipDate.Value.Year == 1)
            {
                _result.Success = false;
                _result.ErrMessage = "expected ship date is invalid :" + _tOrderExpectedShipDate.ExpectedShipDate.ToString();
                return _result;
            }
            else
            {
                order.ExpectedShipDate = _tOrderExpectedShipDate.ExpectedShipDate;
                _result = order.updateOrderExpectedShipDate(order.OrderId, _tOrderExpectedShipDate.ExpectedShipDate.Value);
                if (_result.Success == false)
                {
                    return _result;
                }
            }

            #endregion


            return _result;
        }

        protected virtual ReturnValue customerEventPreOrderDownload(TOrder order, EntityList orderline)
        {
            ReturnValue _result = new ReturnValue();

            return _result;
        }

        public override ReturnValue ImportDMOrderDetail(int orderId)
        {
            ReturnValue _result = new ReturnValue();

            #region get order line


            Model.TDM_Order_Detail _tDM_Order_Detail = Common.CreateObject(this.Owner, "TDM_Order_Detail") as Model.TDM_Order_Detail;
            _result = _tDM_Order_Detail.getOrderForZoytoPH(orderId);

            if (_result.Success == false)
            {
                return _result;
            }


            Common.Log("getOrderForZoytoPH---OK \r\n" + _result.ObjectList.Count().ToString ());


            EntityList _line = _result.ObjectList;

            if (_line.Count == 0)
            {
                _result.Success = false;
                _result.ErrMessage = "Order : " + orderId.ToString() + " get getOrderForZoytoPH --No Record found";
                return _result;
            }
          

            #endregion

            #region caculcate bonus and qty

            double _order_Bonus = 0;
            int _quantity = 0;

            foreach (TDM_Order_Detail _item in _line)
            {
                if (_item.IsCommissionable == true)
                {
                    _order_Bonus += _item.ActualPrice;
                }

                _quantity += _item.Pieces_Ordered;
            }

            #endregion
          

            foreach (TDM_Order_Detail _item in _line)
            {
                #region Save Zoyto_PH Order Detail

                _item.Order_Bonus = _order_Bonus;
                _item.Extended_Cost = _item.Unit_Cost * _item.Pieces_Ordered;
                _item.CreatedOn = System.DateTime.Now;
                _item.Owner_Code = Owner.OwnerCode.ToString();

                _item.ReleaseNumber = 1;

                if (_item.B_CountryCode == "UK")
                {
                    _item.B_Country = "United Kingdom";
                }

                if (_item.S_CountryCode == "UK")
                {
                    _item.S_Country = "United Kingdom";
                }

                _item.DataConnectProviders = "ZoytoPH";
                _result = _item.Save();
                if (_result.Success == false)
                {
                    if (_result.Code == 2627)
                    {
                        _result.Success = true;
                    }
                    else
                    {
                        _result.Code = -1;
                        return _result;
                    }
                }

                #endregion
            }

            return _result;
        }

        #endregion

        #region UpdateShipment

        public override ReturnValue UpdateShipment()
        {
            ReturnValue _result = new ReturnValue();

            Common.Connect();

            #region get order list

            TOrder _tOrder = new TOrder();
            _result = _tOrder.getShimentOrderList();
            if (_result.Success == false)
            {
                _result.Success = false;
                _result.ErrMessage = "getShimentOrderList failed. \r\n " + _result.ErrMessage;

                Common.Log("getShimentOrderList---ER \r\n" + _result.ErrMessage);

                return _result;
            }

            EntityList orderList = _result.ObjectList;

            VCBusiness.Model.TProgram_Email _tProgram_Email = Common.CreateObject(this.Owner, "TProgram_Email") as VCBusiness.Model.TProgram_Email;
            _result = _tProgram_Email.getEmailTemplate("SHIP_CONFIRMATION");
            if (_result.Success == false)
            {
                _result.Success = false;
                _result.ErrMessage = "getEmailTemplate failed. \r\n " + _result.ErrMessage;

                Common.Log("getEmailTemplate---ER \r\n" + _result.ErrMessage);

                return _result;
            }

            _tProgram_Email = _result.Object as TProgram_Email;

            

            #endregion

            #region update shipment

            foreach (TOrder order in orderList)
            {
                #region GetOrderShipmentInfo

                EntityList productList = new EntityList();

                if (Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["IsTestMode"].ToString()) == true)
                {
                    #region test


                    TOrder_Line_Item orderline = new TOrder_Line_Item();
                    _result = orderline.getOrderLineItemsByOrderId(order.OrderId);

                    foreach (TOrder_Line_Item item in _result.ObjectList)
                    {
                        item.ShipCarrier = "UPS";
                        item.ShipMethod = "STD";
                        item.TrackingNumber = "123456789";
                        item.ShippedDate = System.DateTime.Now;
                        productList.Add(item);
                    }

                    #endregion
                }
                else
                {
                    #region call Veracore

                    _result = VeraCore.GetOrderShipmentInfo(order.OrderId.ToString());
                    if (_result.Success == false)
                    {
                        if (_result.ErrMessage.IndexOf("Invalid Order ID") > -1)
                        {
                            _result.ErrMessage = "Can't find the order";
                        }
                        else
                        {
                            errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                            failedRecord++;
                        }
                        Common.Log("Order : " + order.OrderId + "  GetOrderShipmentInfo---ER \r\n" + _result.ErrMessage);

                        continue;
                    }

                    productList = _result.ObjectList;

                    if (productList.Count() == 0)
                    {
                        Common.Log("Order : " + order.OrderId + "---Unshipped");

                        continue;
                    }

                    #endregion
                }

                #endregion

                Transaction _tran = new Transaction();

                #region update order line 

              
              

                foreach (TOrder_Line_Item item in productList)
                {
                    order.ShipCarrier = item.ShipCarrier;
                    order.ShipMethod = item.ShipMethod;
                    order.TrackingNumber = item.TrackingNumber;
                    order.ShippedDate = item.ShippedDate.Value;

                    _result = item.updateOrderLineItemShipment(order.OrderId, item.PartNumber, item.ShippedDate.Value, item.TrackingNumber, _tran);
                    if (_result.Success == false)
                    {
                        _tran.RollbackTransaction();
                        errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                        failedRecord++;

                        Common.Log("Order : " + order.OrderId + "  updateOrderLineItemShipment---ER \r\n" + _result.ErrMessage);

                        continue;
                    }

                    if (this.Owner.OwnerInfo["ImportDM"].ToString() == "Y")
                    {
                        TDM_Order_Detail _tDM_Order_Detail = new TDM_Order_Detail();
                        _tDM_Order_Detail.DataConnectProviders = "ZoytoPH";
                        _result = _tDM_Order_Detail.updateDMShipingInfo(order.OrderId, item.PartNumber, order.ShippedDate.Value, order.TrackingNumber);
                        if (_result.Success == false)
                        {
                            _tran.RollbackTransaction();
                            errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                            failedRecord++;

                            Common.Log("Order : " + order.OrderId + "  updateDMShipingInfo---ER \r\n" + _result.ErrMessage);

                            continue;
                        }
                    }



                }

                #endregion

                #region update carton & ASN

                #region get release list

                TOrder_Line_Item _tOrder_Line_Item = new TOrder_Line_Item();
                _result = _tOrder_Line_Item.getOrderReleaseByOrderId(order.OrderId, _tran);
                if (_result.Success == false)
                {
                    _tran.RollbackTransaction();
                    errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                    failedRecord++;

                    Common.Log("Order : " + order.OrderId + "  getOrderReleaseByOrderId---ER \r\n" + _result.ErrMessage);

                    continue;
                }

                EntityList _releaseList = _result.ObjectList;

                #endregion

                #region delete cartion and ASN

                TOrder_Line_Shipment_Carton _tOrder_Line_Shipment_Carton = new TOrder_Line_Shipment_Carton();
                _result = _tOrder_Line_Shipment_Carton.deleteOrderLineShipmentCartonByOrderID(order.OrderId, _tran);
                if (_result.Success == false)
                {
                    _tran.RollbackTransaction();
                    errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                    failedRecord++;

                    Common.Log("Order : " + order.OrderId + "  deleteOrderLineShipmentCartonByOrderID---ER \r\n" + _result.ErrMessage);

                    continue;
                }


                TOrder_Line_Shipment_ASN _tOrder_Line_Shipment_ASN = new TOrder_Line_Shipment_ASN();
                _result = _tOrder_Line_Shipment_ASN.deleteOrderLineShipmentASNByOrderID(order.OrderId, _tran);
                if (_result.Success == false)
                {
                    _tran.RollbackTransaction();
                    errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                    failedRecord++;

                    Common.Log("Order : " + order.OrderId + "  deleteOrderLineShipmentASNByOrderID---ER \r\n" + _result.ErrMessage);

                    continue;
                }

                #endregion

                int releaseID = 1;
                foreach (TOrder_Line_Item item in _releaseList)
                {
                    #region update release number && carton && ASN

                    _result = item.updateOrderReleaseByTracking(order.OrderId,item.TrackingNumber,releaseID, _tran);
                    if (_result.Success == false)
                    {
                        _tran.RollbackTransaction();
                        errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                        failedRecord++;

                        Common.Log("Order : " + order.OrderId + "  updateOrderReleaseByTracking---ER \r\n" + _result.ErrMessage);

                        continue;
                    }

                    _tOrder_Line_Shipment_Carton = new TOrder_Line_Shipment_Carton();
                    _tOrder_Line_Shipment_Carton.ORDER_ID = order.OrderId;
                    _tOrder_Line_Shipment_Carton.RELEASE_NUM = releaseID;
                    _tOrder_Line_Shipment_Carton.CARTON_ID_FROM = releaseID.ToString();
                    _tOrder_Line_Shipment_Carton.CARRIER_ID = order.ShipCarrier;
                    _tOrder_Line_Shipment_Carton.SHIP_METHOD = order.ShipMethod;
                    _tOrder_Line_Shipment_Carton.PACKAGE_TRACE_ID = item.TrackingNumber;
                    _tOrder_Line_Shipment_Carton.Ship_date = item.ShippedDate.Value;

                    _result = _tOrder_Line_Shipment_Carton.Save(_tran);
                    if (_result.Success == false)
                    {
                        _tran.RollbackTransaction();
                        errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                        failedRecord++;

                        Common.Log("Order : " + order.OrderId + "  _tOrder_Line_Shipment_Carton.Save---ER \r\n" + _result.ErrMessage);

                        continue;
                    }


                    _result = item.createASN(order.OrderId,releaseID, _tran);
                    if (_result.Success == false)
                    {
                        _tran.RollbackTransaction();
                        errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                        failedRecord++;

                        Common.Log("Order : " + order.OrderId + " createASN---ER \r\n" + _result.ErrMessage);

                        continue;
                    }

                    #endregion

                    releaseID++;
                }

                #endregion

                #region update phontom item 

                _result = _tOrder.updateOrderPhontomOrderStatus(order.OrderId, order.ShippedDate.Value, order.TrackingNumber, _tran);
                if (_result.Success == false)
                {
                    _tran.RollbackTransaction();
                    errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                    failedRecord++;

                    Common.Log("Order : " + order.OrderId + "  updateOrderPhontomOrderStatus---ER \r\n" + _result.ErrMessage);

                    continue;
                }

                #endregion

                #region customerEventPostShipmentUpdate

                _result = this.customerEventPostShipmentUpdate(order, productList, _tran);
                if (_result.Success == false)
                {
                    _tran.RollbackTransaction();
                    errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                    failedRecord++;

                    Common.Log("Order : " + order.OrderId + "  customerEventPostShipmentUpdate---ER \r\n" + _result.ErrMessage);

                    continue;
                }



                #endregion

                _tran.CommitTransaction();

                #region sent confirm email

                if (Owner.OwnerInfo["ShipConfirmation"].ToString() == "Y")
                {
                    #region check already sent email

                    App_Log_Mail _app_Log_Mail = new App_Log_Mail();
                    _result = _app_Log_Mail.getEmailLog(order.OrderId, releaseID - 1);
                    if (_result.Success == false)
                    {
                        errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                        failedRecord++;

                        Common.Log("Order : " + order.OrderId + "  getEmailLog---ER \r\n" + _result.ErrMessage);

                        continue;
                    }
                    _app_Log_Mail = _result.Object as App_Log_Mail;

                    #endregion

                    if (_app_Log_Mail.ID == 0)
                    {
                        #region sent shipment email

                        #region get email content

                        VCBusiness.EmailFactory EmailFactory = Common.CreateObject(this.Owner, "EmailFactory") as VCBusiness.EmailFactory;
                        _result = EmailFactory.GetMailContent(order.OrderId, releaseID - 1, _tProgram_Email);
                        if (_result.Success == false)
                        {
                            errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                            failedRecord++;

                            Common.Log("Order : " + order.OrderId + "  GetMailContent---ER \r\n" + _result.ErrMessage);

                            continue;
                        }
                        EmailMessage email = _result.ObjectValue as EmailMessage;

                        #endregion

                        #region sent email

                        _result = EmailFactory.SentEmail(order.OrderId, releaseID - 1, email);
                        if (_result.Success == false)
                        {
                            errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                            failedRecord++;

                            Common.Log("Order : " + order.OrderId + "  SentEmail---ER \r\n" + _result.ErrMessage);

                            continue;
                        }
                        #endregion

                        #endregion
                    }
                }

                #endregion

                successfulRecord++;
                Common.Log("Order : " + order.OrderId + "---OK");
            }

            #endregion

            #region update Order Status SH

            _result = _tOrder.updateOrderStatusSH();
            if (_result.Success == false)
            {
                _result.Success = false;
                _result.ErrMessage = "updateOrderStatusSH failed. \r\n " + _result.ErrMessage;

                Common.Log("updateOrderStatusSH---ER \r\n" + _result.ErrMessage);

                return _result;
            }
           

            #endregion

            Common.SentAlterEmail(failedRecord, errorNotes);

            _result.Success = true;


            return _result;
        }


        protected virtual ReturnValue customerEventPostShipmentUpdate(TOrder order, EntityList orderLines,Transaction tran)
        {
            ReturnValue _result = new ReturnValue();


            return _result;
        }

        #endregion
    }
}
