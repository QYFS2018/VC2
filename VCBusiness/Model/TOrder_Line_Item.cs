using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
    [Serializable]
    [BindingClass("Order_Line_Item")]
    public class TOrder_Line_Item : Entity
    {
        public TOrder_Line_Item()
        {

        }

        #region Basic Property
        private int _orderLineItemId;
        private int _orderId;
        protected string _partNumber;
        private string _productName;
        protected int _lineNum;
        protected int _quantity;
        private double _price;
        private double _actualPrice;
        private string _statusCode;
        private DateTime? _shippedDate;
        private string _trackingNumber;
        private int? _releaseNumber;

        [BindingField("OrderLineItemId", true)]
        public int OrderLineItemId
        {
            set
            {
                _orderLineItemId = value;
            }
            get
            {
                return _orderLineItemId;
            }
        }
        [BindingField("OrderId", true)]
        public int OrderId
        {
            set
            {
                _orderId = value;
            }
            get
            {
                return _orderId;
            }
        }
        [BindingField("PartNumber", true)]
        public string PartNumber
        {
            set
            {
                _partNumber = value;
            }
            get
            {
                return _partNumber;
            }
        }
        [BindingField("ProductName", true)]
        public string ProductName
        {
            set
            {
                _productName = value;
            }
            get
            {
                return _productName;
            }
        }
        [BindingField("LineNum", true)]
        public int LineNum
        {
            set
            {
                _lineNum = value;
            }
            get
            {
                return _lineNum;
            }
        }
        [BindingField("Quantity", true)]
        public int Quantity
        {
            set
            {
                _quantity = value;
            }
            get
            {
                return _quantity;
            }
        }
        [BindingField("Price", true)]
        public double Price
        {
            set
            {
                _price = value;
            }
            get
            {
                return _price;
            }
        }
        [BindingField("ActualPrice", true)]
        public double ActualPrice
        {
            set
            {
                _actualPrice = value;
            }
            get
            {
                return _actualPrice;
            }
        }
        [BindingField("StatusCode", true)]
        public string StatusCode
        {
            set
            {
                _statusCode = value;
            }
            get
            {
                return _statusCode;
            }
        }
        [BindingField("ShippedDate", true)]
        public DateTime? ShippedDate
        {
            set
            {
                _shippedDate = value;
            }
            get
            {
                return _shippedDate;
            }
        }
        [BindingField("TrackingNumber", true)]
        public string TrackingNumber
        {
            set
            {
                _trackingNumber = value;
            }
            get
            {
                return _trackingNumber;
            }
        }
        [BindingField("ReleaseNumber", true)]
        public int? ReleaseNumber
        {
            set
            {
                _releaseNumber = value;
            }
            get
            {
                return _releaseNumber;
            }
        }

        private string _s_company;
        private string _s_firstname;
        private string _s_lastname;
        private string _s_address1;
        private string _s_address2;
        private string _s_address3;
        private string _s_address4;
        private string _s_address5;
        private string _s_city;
        private string _s_state;
        private string _s_zip;
        private string _s_country;
        private string _s_contact;
        private string _s_phone;
        private string _s_fax;
        private string _s_email;

        [BindingField("S_COMPANY", true)]
        public string S_COMPANY
        {
            set
            {
                _s_company = value;
            }
            get
            {
                return _s_company;
            }
        }
        [BindingField("S_FIRSTNAME", true)]
        public string S_FIRSTNAME
        {
            set
            {
                _s_firstname = value;
            }
            get
            {
                return _s_firstname;
            }
        }
        [BindingField("S_LASTNAME", true)]
        public string S_LASTNAME
        {
            set
            {
                _s_lastname = value;
            }
            get
            {
                return _s_lastname;
            }
        }
        [BindingField("S_ADDRESS1", true)]
        public string S_ADDRESS1
        {
            set
            {
                _s_address1 = value;
            }
            get
            {
                return _s_address1;
            }
        }
        [BindingField("S_ADDRESS2", true)]
        public string S_ADDRESS2
        {
            set
            {
                _s_address2 = value;
            }
            get
            {
                return _s_address2;
            }
        }
        [BindingField("S_ADDRESS3", true)]
        public string S_ADDRESS3
        {
            set
            {
                _s_address3 = value;
            }
            get
            {
                return _s_address3;
            }
        }
        [BindingField("S_ADDRESS4", true)]
        public string S_ADDRESS4
        {
            set
            {
                _s_address4 = value;
            }
            get
            {
                return _s_address4;
            }
        }
        [BindingField("S_ADDRESS5", true)]
        public string S_ADDRESS5
        {
            set
            {
                _s_address5 = value;
            }
            get
            {
                return _s_address5;
            }
        }
        [BindingField("S_CITY", true)]
        public string S_CITY
        {
            set
            {
                _s_city = value;
            }
            get
            {
                return _s_city;
            }
        }
        [BindingField("S_STATE", true)]
        public string S_STATE
        {
            set
            {
                _s_state = value;
            }
            get
            {
                return _s_state;
            }
        }
        [BindingField("S_ZIP", true)]
        public string S_ZIP
        {
            set
            {
                _s_zip = value;
            }
            get
            {
                return _s_zip;
            }
        }
        [BindingField("S_COUNTRY", true)]
        public string S_COUNTRY
        {
            set
            {
                _s_country = value;
            }
            get
            {
                return _s_country;
            }
        }
        [BindingField("S_CONTACT", true)]
        public string S_CONTACT
        {
            set
            {
                _s_contact = value;
            }
            get
            {
                return _s_contact;
            }
        }
        [BindingField("S_PHONE", true)]
        public string S_PHONE
        {
            set
            {
                _s_phone = value;
            }
            get
            {
                return _s_phone;
            }
        }
        [BindingField("S_FAX", true)]
        public string S_FAX
        {
            set
            {
                _s_fax = value;
            }
            get
            {
                return _s_fax;
            }
        }
        [BindingField("S_EMAIL", true)]
        public string S_EMAIL
        {
            set
            {
                _s_email = value;
            }
            get
            {
                return _s_email;
            }
        }


        private string _shipMethod;
        [BindingField("ShipMethod", true)]
        public string ShipMethod
        {
            set
            {
                _shipMethod = value;
            }
            get
            {
                return _shipMethod;
            }
        }

        private string _shipCarrier;
        [BindingField("ShipCarrier", true)]
        public string ShipCarrier
        {
            set
            {
                _shipCarrier = value;
            }
            get
            {
                return _shipCarrier;
            }
        }

        #endregion

        #region Extend Property


        private string _shipToCountry;
        [BindingField("ShipToCountry", true)]
        public string ShipToCountry
        {
            set
            {
                _shipToCountry = value;
            }
            get
            {
                return _shipToCountry;
            }
        }



        private string _shipToState;
        [BindingField("ShipToState", true)]
        public string ShipToState
        {
            set
            {
                _shipToState = value;
            }
            get
            {
                return _shipToState;
            }
        }

        private string _shipToCity;
        [BindingField("ShipToCity", true)]
        public string ShipToCity
        {
            set
            {
                _shipToCity = value;
            }
            get
            {
                return _shipToCity;
            }
        }

        private string _shipToZip;
        [BindingField("ShipToZip", true)]
        public string ShipToZip
        {
            set
            {
                _shipToZip = value;
            }
            get
            {
                return _shipToZip;
            }
        }

        private string _shipToAddress;
        [BindingField("ShipToAddress", true)]
        public string ShipToAddress
        {
            set
            {
                _shipToAddress = value;
            }
            get
            {
                return _shipToAddress;
            }
        }

    

        #endregion 

        public  ReturnValue getOrderLineByOrderId(int id)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getOrderLineByOrderId"), id);
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }

        public  ReturnValue ReleaseOrderLineByOrderId(int id)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("ReleaseOrderLineByOrderId"), id);
            ReturnValue _result = this.ExecSql (Usp_SQL);
            return _result;
        }


        public ReturnValue updateOrderLineItemShipment(int id,DateTime shipDate,string tracking,Transaction tran)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("updateOrderLineItemShipment"), id, shipDate,tracking);
            ReturnValue _result = this.ExecSql(Usp_SQL, tran);
            return _result;
        }

        public ReturnValue createASN(int id, Transaction tran)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("createASN"), id);
            ReturnValue _result = this.ExecSql(Usp_SQL, tran);
            return _result;
        }


        public ReturnValue getOrderDetailsByOrderId(int id)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getOrderDetailsByOrderId"), id);
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }
    }
}