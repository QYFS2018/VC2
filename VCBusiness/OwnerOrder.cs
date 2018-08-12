using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCBusiness.Model;
using WComm;

namespace VCBusiness
{
    public class TecnifibreOrder : Order
    {
        protected override ReturnValue customerEventPreOrderDownload(TOrder order, EntityList orderline)
        {
            ReturnValue _result = new ReturnValue();

            TOrderTF _tOrder = new TOrderTF();
            _result = _tOrder.getOrderById(order.OrderId);
            if (_result.Success == false)
            {
                return _result;
            }
            _tOrder = _result.Object as TOrderTF;


            if (_tOrder.UsingAccountNumber == false)
            {
                order.ShippingAccountNumber = "";
            }
            else
            {
                order.ShippingAccountNumber = _tOrder.ShippingAccountNumber;
            }



            return _result;
        }

        protected override ReturnValue customerEventPostShipmentUpdate(TOrder order, EntityList orderline, Transaction tran)
        {
            ReturnValue _result = new ReturnValue();

            #region Invoice

            #region get order

            TOrderTF _tOrder = new TOrderTF();
            _result = _tOrder.getOrderById(order.OrderId, tran);
            if (_result.Success == false)
            {
                return _result;
            }
            _tOrder = _result.Object as TOrderTF;

            #endregion

            #region search invoice

            TInvoice _tInvoice = new TInvoice();
            _result = _tInvoice.getInvoice(order.OrderId, tran);
            if (_result.Success == false)
            {
                return _result;
            }
            _tInvoice = _result.Object as TInvoice;

            #endregion

            if (_tInvoice.InvoiceId == 0)
            {
                #region Create new invoice

                TCustomer _tCustomer = new TCustomer();
                _result = _tCustomer.getCustomerById(_tOrder.PWPCustomerId);
                if (_result.Success == false)
                {
                    return _result;
                }
                _tCustomer = _result.Object as TCustomer;

                TPaymentTerms _tPaymentTerms = new TPaymentTerms();
                _result = _tPaymentTerms.getPaymentTermsById(_tCustomer.PaymentTermsId);
                if (_result.Success == false)
                {
                    return _result;
                }
                _tPaymentTerms = _result.Object as TPaymentTerms;

                TUser _tUser = new TUser();
                _result = _tUser.getUserById(_tCustomer.SalesRepId);
                if (_result.Success == false)
                {
                    return _result;
                }
                _tUser = _result.Object as TUser;

                _tInvoice.InvoiceDate = System.DateTime.Now;
                _tInvoice.ShippedDate = order.ShippedDate.Value;
                _tInvoice.CreatedOn = System.DateTime.Now;
                _tInvoice.OrderId = order.OrderId;
                //_tInvoice.SessionId = _sessionId;
                _tInvoice.PONum = _tOrder.PONumber;
                _tInvoice.CustomerId = _tOrder.PWPCustomerId;
                _tInvoice.AltInvoiceNum = order.OrderId.ToString();

                _tInvoice.SalesRep = _tCustomer.SalesRepId;
                _tInvoice.Terms = _tCustomer.PaymentTermsId;

                _tInvoice.TermsName = _tPaymentTerms.Description;
                _tInvoice.SalesRepName = _tUser.SaleRepInitials;

                _tInvoice.DueDate = System.DateTime.Now.AddDays(_tPaymentTerms.NetDueInDays);
                _tInvoice.PaymentStatus = "PEND";

                _result = _tInvoice.Save(tran);
                if (_result.Success == false)
                {
                    return _result;
                }
                //_tInvoice.AltInvoiceNum = _orderId.ToString();
                _tInvoice.InvoiceId = _result.IdentityId;
                _tInvoice.QBRef = _tInvoice.InvoiceId.ToString();
                _result = _tInvoice.Update(tran);
                if (_result.Success == false)
                {
                    return _result;
                }


                //_invoiceList.Add(_tInvoice);

                #endregion
            }

            #region invoice line

            foreach (TOrder_Line_Item item in orderline)
            {
                _result = item.getOrderLineByOrderPartNumber(order.OrderId, item.PartNumber, item.Quantity,tran);
                if (_result.Success == false)
                {
                    return _result;
                }
                TOrder_Line_Item _tOrder_Line_Item = _result.Object as TOrder_Line_Item;

                if (_tOrder_Line_Item.ShippedDate == null || _tOrder_Line_Item.Quantity == 0 || item.Quantity==0)
                {
                    continue;
                }

                TInvoice_Line_Item _tInvoice_Line_Item = new TInvoice_Line_Item();
                _tInvoice_Line_Item.InvoiceId = _tInvoice.InvoiceId;
                _tInvoice_Line_Item.LineNum = _tOrder_Line_Item.LineNum;
                _tInvoice_Line_Item.ProgramProductId = _tOrder_Line_Item.ProgramProductId;
                _tInvoice_Line_Item.ProductName = _tOrder_Line_Item.ProductName;
                _tInvoice_Line_Item.PartNumber = _tOrder_Line_Item.PartNumber;
                _tInvoice_Line_Item.Quantity = item.Quantity;
                _tInvoice_Line_Item.ShippedDate = _tOrder_Line_Item.ShippedDate.Value;
                _tInvoice_Line_Item.TrackingNumber = _tOrder_Line_Item.TrackingNumber;
                _tInvoice_Line_Item.ReleaseNumber = _tOrder_Line_Item.ReleaseNumber.Value;

                _tInvoice_Line_Item.OrderLineItemId = _tOrder_Line_Item.OrderLineItemId;
                _tInvoice_Line_Item.Amount = (_tOrder_Line_Item.ActualPrice - _tOrder_Line_Item.ComAmount) * (item.Quantity / _tOrder_Line_Item.Quantity);
                _tInvoice_Line_Item.Price = _tInvoice_Line_Item.Amount / item.Quantity;
                _result = _tInvoice_Line_Item.Save(tran);
                if (_result.Success == false)
                {
                    return _result;
                }

            }

            #endregion

            #region update paid amount & invocie status

            TPaymentArrangement _tPaymentArrangement = new TPaymentArrangement();
            _result = _tPaymentArrangement.getTFOrderPaymentArrangementList(order.OrderId);
            if (_result.Success == false)
            {
                return _result;
            }
            EntityList _payList = _result.ObjectList;


            double _paiedAmount = 0.00;

            bool _noPT = true;

            foreach (TPaymentArrangement _pItem in _payList)
            {
                if (_pItem.PayMethodId != 4)
                {
                    _paiedAmount += _pItem.Amount;
                }
                else
                {
                    _noPT = false;
                }
            }


            TInvoice_Line_Item _Invoice_Line_Item = new TInvoice_Line_Item();
            _result = _Invoice_Line_Item.getTotalInvoiceLineItemByInvoiceId(_tInvoice.InvoiceId, tran);
            if (_result.Success == false)
            {
                return _result;
            }
            _Invoice_Line_Item = _result.Object as TInvoice_Line_Item;

            if ((_tOrder.TotalWholeSaleAmount - _tOrder.CompProductAmount) != 0)
            {
                Double _productAmountRate = _Invoice_Line_Item.Amount / (_tOrder.TotalWholeSaleAmount - _tOrder.CompProductAmount);

                _tInvoice.Subtotal = WComm.Utilities.Round(_Invoice_Line_Item.Amount, 2);
                _tInvoice.Shipping = WComm.Utilities.Round(_productAmountRate * (_tOrder.TotalShipping - _tOrder.CompShipingCost), 2);
                _tInvoice.Tax = WComm.Utilities.Round(_productAmountRate * (_tOrder.TotalTax - _tOrder.CompTax), 2);
                _tInvoice.InvoiceAmount = WComm.Utilities.Round(_tInvoice.Subtotal + _tInvoice.Shipping + _tInvoice.Tax, 2);
                _tInvoice.PaiedAmount = WComm.Utilities.Round(_paiedAmount * _productAmountRate, 2);
                _tInvoice.BalanceDue = WComm.Utilities.Round(_tInvoice.InvoiceAmount - _tInvoice.PaiedAmount, 2);
                if (_noPT == true)
                {
                    _tInvoice.PaymentStatus = "PAID";
                    _tInvoice.BalanceDue = 0;
                    _tInvoice.PaiedAmount = _tInvoice.InvoiceAmount;

                }
                _result = _tInvoice.Update(tran);
                if (_result.Success == false)
                {
                    return _result;
                }
            }

            #endregion

            #region sent invoice email

            VCBusiness.TecnifibreEmailFactory EmailFactory = new TecnifibreEmailFactory();

            _result = EmailFactory.SentInvoiceEmail(_tInvoice.InvoiceId);
            if (_result.Success == false)
            {
                return _result;
            }


            _tInvoice.EmailSentOn = System.DateTime.Now;
            _result = _tInvoice.Update(tran);
            if (_result.Success == false)
            {
                return _result;
            }

            #endregion


            #endregion

            return _result;
        }
    }

    public class DermisVitamins : Order
    {
        protected override ReturnValue customerEventPreOrderDownload(TOrder order, EntityList orderline)
        {
            ReturnValue _result = new ReturnValue();


            string shipMethod = ((TOrder_Line_Item)orderline[0]).ShipMethod;

            if (shipMethod == "FEH" || shipMethod == "FE2" || shipMethod == "FEP" || shipMethod == "FE3" 
                || shipMethod == "FES" || shipMethod == "FEG" || shipMethod == "FSP")
            {
                order.ShippingAccountNumber = this.Owner.OwnerInfo["ShippingAccountNumber"].ToString();
            }
            else
            {
                order.ShippingAccountNumber = "";
            }



            return _result;
        }

    }

    public class Snoopguard : Order
    {
        protected override ReturnValue customerEventPreOrderDownload(TOrder order, EntityList orderline)
        {
            ReturnValue _result = new ReturnValue();


            string shipMethod = ((TOrder_Line_Item)orderline[0]).ShipMethod;



            if (shipMethod == "FE2" || shipMethod == "FEP" || shipMethod == "FE3" || shipMethod == "FES" || shipMethod == "FEG" || shipMethod == "FSP")
            {
                order.ShippingAccountNumber = this.Owner.OwnerInfo["ShippingAccountNumber"].ToString();
                
            }
            else
            {
                order.ShippingAccountNumber = "";
            }

            if (order.OrderType != "MAIL-00462" && order.OrderType != "IPC-00462")
            {
                order.OrderType = "REG";
            }

            return _result;
        }

    }
}
