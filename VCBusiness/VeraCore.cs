using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Text;
using WComm;
using System.IO;
using System.Collections;
using VCBusiness.VeraCoreOMS;
using VCBusiness.Model;
using System.Net;


namespace VCBusiness
{
    public class VeraCore
    {
        VeraCoreOMS.OMSSoapClient OMSSoapClient;
        VCBusiness.VeraCoreOMS.AuthenticationHeader AuthenticationHeader;
        VCBusiness.VeraCoreOMS.DebugHeader DebugHeader;

        string requestXml;
        string responseXml;

        public VeraCore(string user, string password)
        {
            OMSSoapClient = new VeraCoreOMS.OMSSoapClient();
            AuthenticationHeader = new VeraCoreOMS.AuthenticationHeader();
            DebugHeader = new VeraCoreOMS.DebugHeader();

            AuthenticationHeader.Username = user;
            AuthenticationHeader.Password = password;


            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        }

        public ReturnValue GetInventory(string owner,string sku)
        {
            ReturnValue _result = new ReturnValue();

            try
            {
                requestXml = owner + "--" + sku;

                VCBusiness.VeraCoreOMS.ProductAvailabilities[] prod = OMSSoapClient.GetProductAvailabilities(AuthenticationHeader, ref DebugHeader, sku, owner, "", "", "", "");

                responseXml = WComm.XmlSerializer.Serialize(prod);

                Common.Log(requestXml, responseXml, "GetInventory", sku, true, null);

                _result.ObjectValue = prod[0].Warehouses[0].OnHand;
            }
            catch (Exception ex)
            {
                _result.Success = false;
                _result.ErrMessage = ex.ToString();

                Common.Log(requestXml, responseXml, "GetInventory", sku, false, _result.ErrMessage);

                return _result;
            }


            return _result;
        }

        public ReturnValue PostProduct(string owner, string sku, string title)
        {
            ReturnValue _result = new ReturnValue();

            VCBusiness.VeraCoreOMS.Product product = new VCBusiness.VeraCoreOMS.Product();
            product.Header = new VCBusiness.VeraCoreOMS.ProductHeader();
            product.Header.Owner = new VCBusiness.VeraCoreOMS.Owner();
            product.Header.Owner.ID = owner;
            product.Header.Description = title;
            product.Header.PartNumber = sku;
            product.Header.BuildType = VCBusiness.VeraCoreOMS.BuildType.Product;

            product.Sort = new VCBusiness.VeraCoreOMS.ProductSort();
            product.Sort.ProductType = new VCBusiness.VeraCoreOMS.ProductType();
            product.Sort.ProductType.Description = "Regular";


            VCBusiness.VeraCoreOMS.Offer offer = new VCBusiness.VeraCoreOMS.Offer();
            offer.Header = new VCBusiness.VeraCoreOMS.OfferHeader();
            offer.Header.Description = title;
            offer.Header.ID = sku;
            offer.Components = new VCBusiness.VeraCoreOMS.OfferComponent[1];
            offer.Components[0] = new VCBusiness.VeraCoreOMS.OfferComponent();
            offer.Components[0].Product = new VCBusiness.VeraCoreOMS.ProductID();
            offer.Components[0].Product.Header = new VCBusiness.VeraCoreOMS.ProductIDHeader();
            offer.Components[0].Product.Header.Owner = new VCBusiness.VeraCoreOMS.Owner();
            offer.Components[0].Product.Header.Owner.ID = owner;
            offer.Components[0].Product.Header.PartNumber = sku;
            offer.Components[0].Product.Header.Description = title;
            offer.Components[0].Quantity = 1;


            try
            {
                requestXml = owner + "--" + sku;

                VCBusiness.VeraCoreOMS.GetProductResult getproductResult = OMSSoapClient.GetProduct(AuthenticationHeader, ref DebugHeader, owner, sku);

                responseXml = WComm.XmlSerializer.Serialize(getproductResult);

                Common.Log(requestXml, responseXml, "PostProduct", sku, true, null);

            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("Can NOT find information for Product ID") >= -1)
                {
                    try
                    {
                        requestXml = WComm.XmlSerializer.Serialize(product) + "\r\n" + WComm.XmlSerializer.Serialize(offer);

                        VCBusiness.VeraCoreOMS.AddProductResult productResult = OMSSoapClient.AddProduct(AuthenticationHeader, ref DebugHeader, product, offer);

                        responseXml = WComm.XmlSerializer.Serialize(productResult);

                        Common.Log(requestXml, responseXml, "PostProduct", sku, true, null);

                        return _result;
                    }
                    catch (Exception ex1)
                    {
                        _result.Success = false;
                        _result.ErrMessage = ex1.ToString();

                        Common.Log(requestXml, responseXml, "PostProduct", sku, false, _result.ErrMessage);

                        return _result;
                    }
                }
                else
                {
                    _result.Success = false;
                    _result.ErrMessage = ex.ToString();

                    Common.Log(requestXml, responseXml, "PostProduct", sku, false, _result.ErrMessage);

                    return _result;
                }
            }

            _result.Code = -8;
            _result.Success = false;
            _result.ErrMessage = "The Product already in VeraCore";


            return _result;
        }

        public ReturnValue GetOrderShipmentInfo(string orderid)
        {
            ReturnValue _result = new ReturnValue();


            try
            {
                requestXml = orderid;


                VCBusiness.VeraCoreOMS.OrderInqRecord orderInqRecord = OMSSoapClient.GetOrderInfo(AuthenticationHeader, ref DebugHeader, orderid);

                responseXml = WComm.XmlSerializer.Serialize(orderInqRecord);

                Common.Log(requestXml, responseXml, "GetOrderShipmentInfo", orderid.ToString(), true, null);

                TOrder_Line_Item _tOrder_Line_Item = new TOrder_Line_Item();

                _tOrder_Line_Item.ShipCarrier = orderInqRecord.OrdHead.Carrier;
                _tOrder_Line_Item.ShipMethod = orderInqRecord.OrdHead.Service;
                _tOrder_Line_Item.ShipToAddress = orderInqRecord.ShipToInfo.Address1;
                _tOrder_Line_Item.ShipToCity = orderInqRecord.ShipToInfo.City;
                _tOrder_Line_Item.ShipToState = orderInqRecord.ShipToInfo.State;
                _tOrder_Line_Item.ShipToZip = orderInqRecord.ShipToInfo.PostalCode;

                EntityList lineList = new EntityList();


                foreach (PickPackType itemPackage in orderInqRecord.ShippingOrders)
                {
                    foreach (PackagesType item in itemPackage.Packages)
                    {
                        if (item.DateShipped.Year != 1)
                        {
                            _tOrder_Line_Item.ShippedDate = item.DateShipped;
                            _tOrder_Line_Item.TrackingNumber = item.TrackingId;
                            _tOrder_Line_Item.ShipMethod = item.Service;
                            _tOrder_Line_Item.ShipCarrier = item.Carrier;
                            break;
                        }
                    }

                    if (_tOrder_Line_Item.ShippedDate != null)
                    {
                        foreach (PickPackProductType product in itemPackage.PickPackProducts)
                        {
                            TOrder_Line_Item productItem = _tOrder_Line_Item.Clone() as TOrder_Line_Item;

                            productItem.PartNumber = product.ProductId;
                            productItem.ProductName = product.ProductDesc;
                            productItem.Quantity = product.ToShipQty;
                            lineList.Add(productItem);
                        }
                    }
                }

                _result.ObjectList = lineList;
            }
            catch (Exception ex)
            {
                _result.Success = false;
                _result.ErrMessage = ex.ToString();

                Common.Log(requestXml, responseXml, "GetOrderShipmentInfo", orderid.ToString(), false, _result.ErrMessage);


                return _result;
            }

            return _result;
        }

        public ReturnValue PostOrder(TOrder order, EntityList orderline)
        {
            ReturnValue _result = new ReturnValue();


            VCBusiness.VeraCoreOMS.Order Order = new VeraCoreOMS.Order();


            VCBusiness.VeraCoreOMS.OrderedBy OrderedBy = new VeraCoreOMS.OrderedBy();

            if (string.IsNullOrWhiteSpace(order.B_ADDRESS1) == true && string.IsNullOrWhiteSpace(order.B_STATE) == true)
            {
                OrderedBy.Address1 = order.D_ADDRESS1;
                OrderedBy.Address2 = order.D_ADDRESS2;
                OrderedBy.City = order.D_CITY;
                OrderedBy.CompanyName = order.D_COMPANY;
                OrderedBy.Country = order.D_COUNTRY;
                OrderedBy.Email = order.D_EMAIL;
                OrderedBy.FirstName = order.D_FIRSTNAME;
                OrderedBy.LastName = order.D_LASTNAME;
                OrderedBy.Phone = order.D_PHONE;
                OrderedBy.PostalCode = order.D_ZIP;
                OrderedBy.State = order.D_STATE;
            }
            else
            {
                OrderedBy.Address1 = order.B_ADDRESS1;
                OrderedBy.Address2 = order.B_ADDRESS2;
                OrderedBy.City = order.B_CITY;
                OrderedBy.CompanyName = order.B_COMPANY;
                OrderedBy.Country = order.B_COUNTRY;
                OrderedBy.Email = order.B_EMAIL;
                OrderedBy.FirstName = order.B_FIRSTNAME;
                OrderedBy.LastName = order.B_LASTNAME;
                OrderedBy.Phone = order.B_PHONE;
                OrderedBy.PostalCode = order.B_ZIP;
                OrderedBy.State = order.B_STATE;
            }



            Order.OrderedBy = OrderedBy;


            VCBusiness.VeraCoreOMS.OrderBillTo OrderBillTo = new VeraCoreOMS.OrderBillTo();
            OrderBillTo.Address1 = order.D_ADDRESS1;
            OrderBillTo.Address2 = order.D_ADDRESS2;
            OrderBillTo.City = order.D_CITY;
            OrderBillTo.CompanyName = order.D_COMPANY;
            OrderBillTo.Country = order.D_COUNTRY;
            OrderBillTo.Email = order.D_EMAIL;
            OrderBillTo.FirstName = order.D_FIRSTNAME;
            OrderBillTo.LastName = order.D_LASTNAME;
            OrderBillTo.Phone = order.D_PHONE;
            OrderBillTo.PostalCode = order.D_ZIP;
            OrderBillTo.State = order.D_STATE;

            Order.BillTo = OrderBillTo;


            TOrder_Line_Item orderItem = orderline[0] as TOrder_Line_Item;

            VCBusiness.VeraCoreOMS.OrderShipTo OrderShipTo = new VCBusiness.VeraCoreOMS.OrderShipTo();
            OrderShipTo.Address1 = orderItem.S_ADDRESS1;
            OrderShipTo.Address2 = orderItem.S_ADDRESS2;
            OrderShipTo.City = orderItem.S_CITY;
            OrderShipTo.CompanyName = orderItem.S_COMPANY;
            OrderShipTo.Country = orderItem.S_COUNTRY;
            OrderShipTo.Email = orderItem.S_EMAIL;
            OrderShipTo.FirstName = orderItem.S_FIRSTNAME;
            OrderShipTo.LastName = orderItem.S_LASTNAME;
            OrderShipTo.Phone = orderItem.S_PHONE;
            OrderShipTo.PostalCode = orderItem.S_ZIP;
            OrderShipTo.State = orderItem.S_STATE;
            if (order.ExpectedShipDate != null)
            {
                OrderShipTo.NeededBy = order.ExpectedShipDate.ToString();
            }

            if (string.IsNullOrWhiteSpace(order.ShippingAccountNumber)==false )
            {
                OrderShipTo.ThirdPartyAccountNumber = order.ShippingAccountNumber;
            }

            OrderShipTo.FreightCarrier = new VCBusiness.VeraCoreOMS.FreightCarrier();
            OrderShipTo.FreightCarrier.Name = orderItem.ShipCarrier;

            OrderShipTo.FreightService = new VCBusiness.VeraCoreOMS.FreightService();
            OrderShipTo.FreightService.Description = orderItem.ShipMethod;

            if (Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["IsTestMode"].ToString()) == true)
            {
                OrderShipTo.FreightService.Description = "Standard";
                OrderShipTo.FreightCarrier.Name = "USPS";
            }

            OrderShipTo.Key = "0";
            OrderShipTo.Flag = VCBusiness.VeraCoreOMS.ShipToFlag.Other;

            Order.ShipTo = new VCBusiness.VeraCoreOMS.OrderShipTo[1];
            Order.ShipTo[0] = OrderShipTo;

            Order.Header = new VCBusiness.VeraCoreOMS.OrderHeader();
            Order.Header.ID = order.OrderId.ToString ();
            Order.Header.EntryDate = System.DateTime.Now;
            Order.Header.InsertDate = System.DateTime.Now;
            Order.Header.ReferenceNumber = order.AltOrderNum;

            Order.Classification = new OrderClassification();
            Order.Classification.CampaignID = order.PartyCode;

            Order.Offers = new VCBusiness.VeraCoreOMS.OfferOrdered[orderline.Count];

            int i = 0;
            foreach (TOrder_Line_Item _line in orderline)
            {

                VCBusiness.VeraCoreOMS.OfferOrdered OfferOrdered = new VCBusiness.VeraCoreOMS.OfferOrdered();
                OfferOrdered.LineNumber = i;
                OfferOrdered.LineTaxAmount = 0;
                OfferOrdered.Quantity = _line.Quantity;
                OfferOrdered.OrderShipTo = new VCBusiness.VeraCoreOMS.OrderShipToKey();
                OfferOrdered.OrderShipTo.Key = "0";
                OfferOrdered.Offer = new VCBusiness.VeraCoreOMS.OfferID();
                OfferOrdered.Offer.Header = new VCBusiness.VeraCoreOMS.OfferIDHeader();
                OfferOrdered.Offer.Header.ID = _line.PartNumber;
                OfferOrdered.UnitPrice = Convert.ToDecimal(_line.Price);


                OfferOrdered.ProductDetails = new VCBusiness.VeraCoreOMS.OrderProductDetail[1];
                OfferOrdered.ProductDetails[0] = new VCBusiness.VeraCoreOMS.OrderProductDetail();
                OfferOrdered.ProductDetails[0].PartNumber = _line.PartNumber;
                Order.Offers[i] = OfferOrdered;

                i++;
            }

            try
            {
                requestXml = WComm.XmlSerializer.Serialize(Order);

                VCBusiness.VeraCoreOMS.AddOrderResult orderResult = OMSSoapClient.AddOrder(AuthenticationHeader, ref DebugHeader, Order);

                responseXml = WComm.XmlSerializer.Serialize(orderResult);

                Common.Log(requestXml, responseXml, "PostOrder", order.OrderId.ToString (), true, null);
            }
            catch (Exception ex)
            {
                _result.ErrMessage = ex.ToString();
                _result.Success = false;

                Common.Log(requestXml, responseXml, "PostOrder", order.OrderId.ToString(), false, _result.ErrMessage);
            }


            return _result;

        }
    }
}
