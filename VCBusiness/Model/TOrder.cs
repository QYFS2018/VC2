using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
    [Serializable]
    [BindingClass("Order")]
    public class TOrder : Entity
    {
        public TOrder()
        {

        }

        #region Basic Property

        private int _orderId;
        private string _altOrderNum;
        private DateTime _orderDate;
        private double _totalProductAmount;
        private double _totalTax;
        private double _totalShipping;
        private double _totalOrderAmount;
        private string _statusCode;
        private string _partyCode;

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
        [BindingField("AltOrderNum", true)]
        public string AltOrderNum
        {
            set
            {
                _altOrderNum = value;
            }
            get
            {
                return _altOrderNum;
            }
        }
        [BindingField("OrderDate", true)]
        public DateTime OrderDate
        {
            set
            {
                _orderDate = value;
            }
            get
            {
                return _orderDate;
            }
        }
        [BindingField("TotalProductAmount", true)]
        public double TotalProductAmount
        {
            set
            {
                _totalProductAmount = value;
            }
            get
            {
                return _totalProductAmount;
            }
        }
        [BindingField("TotalTax", true)]
        public double TotalTax
        {
            set
            {
                _totalTax = value;
            }
            get
            {
                return _totalTax;
            }
        }
        [BindingField("TotalShipping", true)]
        public double TotalShipping
        {
            set
            {
                _totalShipping = value;
            }
            get
            {
                return _totalShipping;
            }
        }
        [BindingField("TotalOrderAmount", true)]
        public double TotalOrderAmount
        {
            set
            {
                _totalOrderAmount = value;
            }
            get
            {
                return _totalOrderAmount;
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
        [BindingField("PartyCode", true)]
        public string PartyCode
        {
            set
            {
                _partyCode = value;
            }
            get
            {
                return _partyCode;
            }
        }
    
       
      


        private string _b_company;
        private string _b_firstname;
        private string _b_lastname;
        private string _b_address1;
        private string _b_address2;
        private string _b_address3;
        private string _b_address4;
        private string _b_address5;
        private string _b_city;
        private string _b_state;
        private string _b_zip;
        private string _b_country;
        private string _b_contact;
        private string _b_phone;
        private string _b_fax;
        private string _b_email;
      

        [BindingField("B_COMPANY", true)]
        public string B_COMPANY
        {
            set
            {
                _b_company = value;
            }
            get
            {
                return _b_company;
            }
        }
        [BindingField("B_FIRSTNAME", true)]
        public string B_FIRSTNAME
        {
            set
            {
                _b_firstname = value;
            }
            get
            {
                return _b_firstname;
            }
        }
        [BindingField("B_LASTNAME", true)]
        public string B_LASTNAME
        {
            set
            {
                _b_lastname = value;
            }
            get
            {
                return _b_lastname;
            }
        }
        [BindingField("B_ADDRESS1", true)]
        public string B_ADDRESS1
        {
            set
            {
                _b_address1 = value;
            }
            get
            {
                return _b_address1;
            }
        }
        [BindingField("B_ADDRESS2", true)]
        public string B_ADDRESS2
        {
            set
            {
                _b_address2 = value;
            }
            get
            {
                return _b_address2;
            }
        }
        [BindingField("B_ADDRESS3", true)]
        public string B_ADDRESS3
        {
            set
            {
                _b_address3 = value;
            }
            get
            {
                return _b_address3;
            }
        }
        [BindingField("B_ADDRESS4", true)]
        public string B_ADDRESS4
        {
            set
            {
                _b_address4 = value;
            }
            get
            {
                return _b_address4;
            }
        }
        [BindingField("B_ADDRESS5", true)]
        public string B_ADDRESS5
        {
            set
            {
                _b_address5 = value;
            }
            get
            {
                return _b_address5;
            }
        }
        [BindingField("B_CITY", true)]
        public string B_CITY
        {
            set
            {
                _b_city = value;
            }
            get
            {
                return _b_city;
            }
        }
        [BindingField("B_STATE", true)]
        public string B_STATE
        {
            set
            {
                _b_state = value;
            }
            get
            {
                return _b_state;
            }
        }
        [BindingField("B_ZIP", true)]
        public string B_ZIP
        {
            set
            {
                _b_zip = value;
            }
            get
            {
                return _b_zip;
            }
        }
        [BindingField("B_COUNTRY", true)]
        public string B_COUNTRY
        {
            set
            {
                _b_country = value;
            }
            get
            {
                return _b_country;
            }
        }
        [BindingField("B_CONTACT", true)]
        public string B_CONTACT
        {
            set
            {
                _b_contact = value;
            }
            get
            {
                return _b_contact;
            }
        }
        [BindingField("B_PHONE", true)]
        public string B_PHONE
        {
            set
            {
                _b_phone = value;
            }
            get
            {
                return _b_phone;
            }
        }
        [BindingField("B_FAX", true)]
        public string B_FAX
        {
            set
            {
                _b_fax = value;
            }
            get
            {
                return _b_fax;
            }
        }
        [BindingField("B_EMAIL", true)]
        public string B_EMAIL
        {
            set
            {
                _b_email = value;
            }
            get
            {
                return _b_email;
            }
        }

        private string _d_company;
        private string _d_firstname;
        private string _d_lastname;
        private string _d_address1;
        private string _d_address2;
        private string _d_address3;
        private string _d_address4;
        private string _d_address5;
        private string _d_city;
        private string _d_state;
        private string _d_zip;
        private string _d_country;
        private string _d_contact;
        private string _d_phone;
        private string _d_fax;
        private string _d_email;


        [BindingField("D_COMPANY", true)]
        public string D_COMPANY
        {
            set
            {
                _d_company = value;
            }
            get
            {
                return _d_company;
            }
        }
        [BindingField("D_FIRSTNAME", true)]
        public string D_FIRSTNAME
        {
            set
            {
                _d_firstname = value;
            }
            get
            {
                return _d_firstname;
            }
        }
        [BindingField("D_LASTNAME", true)]
        public string D_LASTNAME
        {
            set
            {
                _d_lastname = value;
            }
            get
            {
                return _d_lastname;
            }
        }
        [BindingField("D_ADDRESS1", true)]
        public string D_ADDRESS1
        {
            set
            {
                _d_address1 = value;
            }
            get
            {
                return _d_address1;
            }
        }
        [BindingField("D_ADDRESS2", true)]
        public string D_ADDRESS2
        {
            set
            {
                _d_address2 = value;
            }
            get
            {
                return _d_address2;
            }
        }
        [BindingField("D_ADDRESS3", true)]
        public string D_ADDRESS3
        {
            set
            {
                _d_address3 = value;
            }
            get
            {
                return _d_address3;
            }
        }
        [BindingField("D_ADDRESS4", true)]
        public string D_ADDRESS4
        {
            set
            {
                _d_address4 = value;
            }
            get
            {
                return _d_address4;
            }
        }
        [BindingField("D_ADDRESS5", true)]
        public string D_ADDRESS5
        {
            set
            {
                _d_address5 = value;
            }
            get
            {
                return _d_address5;
            }
        }
        [BindingField("D_CITY", true)]
        public string D_CITY
        {
            set
            {
                _d_city = value;
            }
            get
            {
                return _d_city;
            }
        }
        [BindingField("D_STATE", true)]
        public string D_STATE
        {
            set
            {
                _d_state = value;
            }
            get
            {
                return _d_state;
            }
        }
        [BindingField("D_ZIP", true)]
        public string D_ZIP
        {
            set
            {
                _d_zip = value;
            }
            get
            {
                return _d_zip;
            }
        }
        [BindingField("D_COUNTRY", true)]
        public string D_COUNTRY
        {
            set
            {
                _d_country = value;
            }
            get
            {
                return _d_country;
            }
        }
        [BindingField("D_CONTACT", true)]
        public string D_CONTACT
        {
            set
            {
                _d_contact = value;
            }
            get
            {
                return _d_contact;
            }
        }
        [BindingField("D_PHONE", true)]
        public string D_PHONE
        {
            set
            {
                _d_phone = value;
            }
            get
            {
                return _d_phone;
            }
        }
        [BindingField("D_FAX", true)]
        public string D_FAX
        {
            set
            {
                _d_fax = value;
            }
            get
            {
                return _d_fax;
            }
        }
        [BindingField("D_EMAIL", true)]
        public string D_EMAIL
        {
            set
            {
                _d_email = value;
            }
            get
            {
                return _d_email;
            }
        }

        #endregion

        #region Extend Property

        private string _shipMethod;
        private string _trackingURL;
        private string _trackingNumber;
        private string _email;
        private string _orderType;
        private int _programID;

        [BindingField("SHName", true)]
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
        [BindingField("TrackingURL", true)]
        public string TrackingURL
        {
            set
            {
                _trackingURL = value;
            }
            get
            {
                return _trackingURL;
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
        [BindingField("Email", true)]
        public string Email
        {
            set
            {
                _email = value;
            }
            get
            {
                return _email;
            }
        }
        [BindingField("OrderType", true)]
        public string OrderType
        {
            set
            {
                _orderType = value;
            }
            get
            {
                return _orderType;
            }
        }
        [BindingField("ProgramID", true)]
        public int ProgramID
        {
            set
            {
                _programID = value;
            }
            get
            {
                return _programID;
            }
        }

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


        private string _shipMethodName;
        [BindingField("ShipMethodName", true, true, true)]
        public string ShipMethodName
        {
            set
            {
                _shipMethodName = value;
            }
            get
            {
                return _shipMethodName;
            }
        }

        private string _firstName;
        private string _lastName;
        private DateTime? _shippedDate;

        [BindingField("FirstName", true)]
        public string FirstName
        {
            set
            {
                _firstName = value;
            }
            get
            {
                return _firstName;
            }
        }
        [BindingField("LastName", true)]
        public string LastName
        {
            set
            {
                _lastName = value;
            }
            get
            {
                return _lastName;
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


        private DateTime? _expectedShipDate;
        [BindingField("ExpectedShipDate", true)]
        public DateTime? ExpectedShipDate
        {
            set
            {
                _expectedShipDate = value;
            }
            get
            {
                return _expectedShipDate;
            }
        }

        private string _shippingAccountNumber;
        [BindingField("ShippingAccountNumber", true)]
        public string ShippingAccountNumber
        {
            set
            {
                _shippingAccountNumber = value;
            }
            get
            {
                return _shippingAccountNumber;
            }
        }

        private bool _usingAccountNumber;
        [BindingField("UsingAccountNumber", true)]
        public bool UsingAccountNumber
        {
            set
            {
                _usingAccountNumber = value;
            }
            get
            {
                return _usingAccountNumber;
            }
        }

        private double _totalWholeSaleAmount;
        [BindingField("TotalWholeSaleAmount", true)]
        public double TotalWholeSaleAmount
        {
            set
            {
                _totalWholeSaleAmount = value;
            }
            get
            {
                return _totalWholeSaleAmount;
            }
        }


        private double _compProductAmount;
        [BindingField("CompProductAmount", true)]
        public double CompProductAmount
        {
            set
            {
                _compProductAmount = value;
            }
            get
            {
                return _compProductAmount;
            }
        }

        private double _compShipingCost;
        [BindingField("CompShipingCost", true)]
        public double CompShipingCost
        {
            set
            {
                _compShipingCost = value;
            }
            get
            {
                return _compShipingCost;
            }
        }

        private double _compTax;
        [BindingField("CompTax", true)]
        public double CompTax
        {
            set
            {
                _compTax = value;
            }
            get
            {
                return _compTax;
            }
        }

        private int _shipToAddressId;
        [BindingField("ShipToAddressId", true)]
        public int ShipToAddressId
        {
            set
            {
                _shipToAddressId = value;
            }
            get
            {
                return _shipToAddressId;
            }
        }

        public int _sourceId;
        [BindingField("SourceId", true)]
        public int SourceId
        {
            set
            {
                _sourceId = value;
            }
            get
            {
                return _sourceId;
            }
        }

        private int _customerAddressId;
        [BindingField("CustomerAddressId", true)]
        public int CustomerAddressId
        {
            set
            {
                _customerAddressId = value;
            }
            get
            {
                return _customerAddressId;
            }
        }


        private double _taxRate;
        [BindingField("TaxRate", true)]
        public double TaxRate
        {
            set
            {
                _taxRate = value;
            }
            get
            {
                return _taxRate;
            }
        }

        public double _totalDiscountAmount;
        [BindingField("TotalDiscountAmount", true)]
        public double TotalDiscountAmount
        {
            set
            {
                _totalDiscountAmount = value;
            }
            get
            {
                return _totalDiscountAmount;
            }
        }

        #endregion 

        public virtual ReturnValue getOrderById(int id)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getOrderById"), id);
            ReturnValue _result = this.getEntity(Usp_SQL);
            return _result;
        }

        public virtual ReturnValue getDownloadOrderList()
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getDownloadOrderList"));
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }

        public virtual ReturnValue getShimentOrderList()
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getShimentOrderList"));
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }



        public virtual ReturnValue updateOrderStatusSH()
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("updateOrderStatusSH"));
            ReturnValue _result = this.ExecSql (Usp_SQL);
            return _result;
        }

        public virtual ReturnValue getOrderExpectedShipDate(int programId, DateTime expectedShipDate, string shipmehtod)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getOrderExpectedShipDate"), programId, expectedShipDate, shipmehtod);
            ReturnValue _result = this.getEntity(Usp_SQL);
            return _result;
        }

        public virtual ReturnValue updateOrderExpectedShipDate(int orderId, DateTime expectedShipDate)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("updateOrderExpectedShipDate"), orderId, expectedShipDate);
            ReturnValue _result = this.ExecSql(Usp_SQL);
            return _result;
        }

        public virtual ReturnValue updateOrderPhontomOrderStatus(int orderId, DateTime shippeddate, string trackingNumber, WComm.Transaction transcation)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("updateOrderPhontomOrderStatus"), orderId, shippeddate, trackingNumber);
            ReturnValue _result = this.ExecSql(Usp_SQL, transcation);
            return _result;
        }
    }
}