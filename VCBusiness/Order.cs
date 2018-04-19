using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCBusiness.Model;
using WComm;
using DotNetOpenMail;



namespace VCBusiness
{
    public class Order
    {
        int successfulRecord = 0;
        int failedRecord = 0;
        string errorNotes = "";

        public ReturnValue OrderDownload()
        {
            ReturnValue _result = new ReturnValue();

            Common.Connect();

            VCBusiness.VeraCore VeraCore = new VeraCore();

            #region get order list

            TOrder _tOrder = new TOrder();
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
                #region post order to VeraCore

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


                _result = _tOrder_Line_Item.ReleaseOrderLineByOrderId(order.OrderId);
                if (_result.Success == false)
                {
                    errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                    failedRecord++;

                    Common.Log("Order : " + order.OrderId + "  ReleaseOrderLineByOrderId---ER \r\n" + _result.ErrMessage);

                    continue;
                }

                #endregion


                successfulRecord++;
                Common.Log("Order : " + order.OrderId + "---OK");
            }

            #endregion

            Common.SentAlterEmail(failedRecord, errorNotes);

            _result.Success = true;

            return _result;
        }

        public ReturnValue ShipmentUpdate()
        {
            ReturnValue _result = new ReturnValue();

            Common.Connect();

            VCBusiness.VeraCore VeraCore = new VeraCore();

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

            TProgram_Email _tProgram_Email = new TProgram_Email();
            _result = _tProgram_Email.getEmailTemplate("SHIP_CONFIRMATION");
            if (_result.Success == false)
            {
                _result.Success = false;
                _result.ErrMessage = "getEmailTemplate failed. \r\n " + _result.ErrMessage;

                Common.Log("getEmailTemplate---ER \r\n" + _result.ErrMessage);

                return _result;
            }

            _tProgram_Email = _result.Object as TProgram_Email;

            VCBusiness.EmailFactory EmailFactory = new VCBusiness.EmailFactory();

            #endregion

            #region post order to VerCore

            foreach (TOrder order in orderList)
            {
                #region GetOrderShipmentInfo

                _result = VeraCore.GetOrderShipmentInfo(order.OrderId.ToString ());
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
                TOrder_Line_Item shippingInfo = _result.Object as TOrder_Line_Item;

                #endregion

                if (shippingInfo.ShippedDate != null)
                {
                    #region update back to ZStore

                    Transaction _tran = new Transaction();

                    TOrder_Line_Shipment_Carton _tOrder_Line_Shipment_Carton = new TOrder_Line_Shipment_Carton();
                    _tOrder_Line_Shipment_Carton.ORDER_ID = order.OrderId;
                    _tOrder_Line_Shipment_Carton.RELEASE_NUM = 1;
                    _tOrder_Line_Shipment_Carton.CARTON_ID_FROM = "1";
                    _tOrder_Line_Shipment_Carton.CARRIER_ID = shippingInfo.ShipCarrier;
                    _tOrder_Line_Shipment_Carton.SHIP_METHOD = shippingInfo.ShipMethod;
                    _tOrder_Line_Shipment_Carton.PACKAGE_TRACE_ID = shippingInfo.TrackingNumber;
                    _tOrder_Line_Shipment_Carton.Ship_date = shippingInfo.ShippedDate.Value;

                    _result = _tOrder_Line_Shipment_Carton.Save(_tran);
                    if (_result.Success == false)
                    {
                        _tran.RollbackTransaction();
                        errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                        failedRecord++;

                        Common.Log("Order : " + order.OrderId + "  _tOrder_Line_Shipment_Carton---ER \r\n" + _result.ErrMessage);

                        continue;
                    }
                    _result = shippingInfo.createASN(order.OrderId, _tran);
                    if (_result.Success == false)
                    {
                        _tran.RollbackTransaction();
                        errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                        failedRecord++;

                        Common.Log("Order : " + order.OrderId + "  createASN---ER \r\n" + _result.ErrMessage);

                        continue;
                    }

                    _result = shippingInfo.updateOrderLineItemShipment(order.OrderId, shippingInfo.ShippedDate.Value, shippingInfo.TrackingNumber, _tran);
                    if (_result.Success == false)
                    {
                        _tran.RollbackTransaction();
                        errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                        failedRecord++;

                        Common.Log("Order : " + order.OrderId + "  updateOrderLineItemShipment---ER \r\n" + _result.ErrMessage);

                        continue;
                    }

                    _tran.CommitTransaction();

                    #endregion

                    #region sent shipment email

                    #region get email content

                    _result = EmailFactory.GetMailContent(order.OrderId, _tProgram_Email);
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

                    _result = EmailFactory.SentEmail(order.OrderId, email);
                    if (_result.Success == false)
                    {
                        errorNotes = errorNotes + order.OrderId.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                        failedRecord++;

                        Common.Log("Order : " + order.OrderId + "  SentEmail---ER \r\n" + _result.ErrMessage);

                        continue;
                    }
                    #endregion

                    #endregion

                    successfulRecord++;
                    Common.Log("Order : " + order.OrderId + "---OK");
                }
                else
                {
                    Common.Log("Order : " + order.OrderId + "---Unshipped");
                }

               
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


    }
}
