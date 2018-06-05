using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
    [Serializable]
    [BindingClass("DM_Order_Detail")]
    public class TDM_Order_Detail : Entity
    {
        public TDM_Order_Detail()
        {

        }

        #region Basic Property
        private int _dM_Order_Detail_ID;
        private int _programID;
        private string _program_Name;
        private string _warehouse_Code;
        private string _owner_Code;
        private string _orderID;
        private string _b_FirstName;
        private string _b_LastName;
        private string _s_FirstName;
        private string _s_LastName;
        private double _order_Shipping;
        private double _order_Handling;
        private double _order_Product_Total;
        private double _order_Discount;
        private double _order_Tax;
        private double _order_Total;
        private double _order_Bonus;
        private double _order_GST;
        private double _order_PST;
        private double _order_HST;
        private string _party_ID;
        private string _order_Alt_Customer;
        private string _shippng_Method_Code;
        private string _shipper;
        private string _shiping_Method;
        private int _line_Number;
        private string _description;
        private int _pieces_Ordered;
        private double _unit_Cost;
        private double _extended_Cost;
        private string _order_Source;
        private DateTime _createdOn;
        private string _updatedBy;
        private DateTime _updatedOn;
        private string _country_Of_Origin;
        private string _hTC;
        private string _eCCN;
        private double _declared_Value;
        private double _return_Value;
        private bool _order_ITN_Required;
        private string _order_ITN;
        private bool _customs_Invoice;
        private double _gross_Weight;
        private string _return_Carrier;
        private DateTime _invoiceDate;
        private string _s_Address1;
        private string _s_Address2;
        private string _s_City;
        private string _s_State;
        private string _s_Zip;
        private string _s_Country;
        private string _s_Phone;
        private string _b_Address1;
        private string _b_Address2;
        private string _b_City;
        private string _b_State;
        private string _b_Zip;
        private string _b_Country;
        private string _b_Phone;
        private string _s_Company;
        private string _b_Company;
        private string _partNumber;
        private bool _return_Expected;
        private int _releaseNumber;
        private string _deliver_Term;
        private string _assembleInstruction;
        private int? _parent_Line_Number;
        private DateTime _orderDate;
        private DateTime? _order_PICC;
        private bool _order_PICC_Required;
        private DateTime? _shipDate;
        private string _shipTracking;
        private string _orderType;

        [BindingField("DM_Order_Detail_ID", true)]
        public int DM_Order_Detail_ID
        {
            set
            {
                _dM_Order_Detail_ID = value;
            }
            get
            {
                return _dM_Order_Detail_ID;
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
        [BindingField("Program_Name", true)]
        public string Program_Name
        {
            set
            {
                _program_Name = value;
            }
            get
            {
                return _program_Name;
            }
        }
        [BindingField("Warehouse_Code", true)]
        public string Warehouse_Code
        {
            set
            {
                _warehouse_Code = value;
            }
            get
            {
                return _warehouse_Code;
            }
        }
        [BindingField("Owner_Code", true)]
        public string Owner_Code
        {
            set
            {
                _owner_Code = value;
            }
            get
            {
                return _owner_Code;
            }
        }
        [BindingField("OrderID", true)]
        public string OrderID
        {
            set
            {
                _orderID = value;
            }
            get
            {
                return _orderID;
            }
        }
        [BindingField("B_FirstName", true)]
        public string B_FirstName
        {
            set
            {
                _b_FirstName = value;
            }
            get
            {
                return _b_FirstName;
            }
        }
        [BindingField("B_LastName", true)]
        public string B_LastName
        {
            set
            {
                _b_LastName = value;
            }
            get
            {
                return _b_LastName;
            }
        }
        [BindingField("S_FirstName", true)]
        public string S_FirstName
        {
            set
            {
                _s_FirstName = value;
            }
            get
            {
                return _s_FirstName;
            }
        }
        [BindingField("S_LastName", true)]
        public string S_LastName
        {
            set
            {
                _s_LastName = value;
            }
            get
            {
                return _s_LastName;
            }
        }
        [BindingField("Order_Shipping", true)]
        public double Order_Shipping
        {
            set
            {
                _order_Shipping = value;
            }
            get
            {
                return _order_Shipping;
            }
        }
        [BindingField("Order_Handling", true)]
        public double Order_Handling
        {
            set
            {
                _order_Handling = value;
            }
            get
            {
                return _order_Handling;
            }
        }
        [BindingField("Order_Product_Total", true)]
        public double Order_Product_Total
        {
            set
            {
                _order_Product_Total = value;
            }
            get
            {
                return _order_Product_Total;
            }
        }
        [BindingField("Order_Discount", true)]
        public double Order_Discount
        {
            set
            {
                _order_Discount = value;
            }
            get
            {
                return _order_Discount;
            }
        }
        [BindingField("Order_Tax", true)]
        public double Order_Tax
        {
            set
            {
                _order_Tax = value;
            }
            get
            {
                return _order_Tax;
            }
        }
        [BindingField("Order_Total", true)]
        public double Order_Total
        {
            set
            {
                _order_Total = value;
            }
            get
            {
                return _order_Total;
            }
        }
        [BindingField("Order_Bonus", true)]
        public double Order_Bonus
        {
            set
            {
                _order_Bonus = value;
            }
            get
            {
                return _order_Bonus;
            }
        }
        [BindingField("Order_GST", true)]
        public double Order_GST
        {
            set
            {
                _order_GST = value;
            }
            get
            {
                return _order_GST;
            }
        }
        [BindingField("Order_PST", true)]
        public double Order_PST
        {
            set
            {
                _order_PST = value;
            }
            get
            {
                return _order_PST;
            }
        }
        [BindingField("Order_HST", true)]
        public double Order_HST
        {
            set
            {
                _order_HST = value;
            }
            get
            {
                return _order_HST;
            }
        }
        [BindingField("Party_ID", true)]
        public string Party_ID
        {
            set
            {
                _party_ID = value;
            }
            get
            {
                return _party_ID;
            }
        }
        [BindingField("Order_Alt_Customer", true)]
        public string Order_Alt_Customer
        {
            set
            {
                _order_Alt_Customer = value;
            }
            get
            {
                return _order_Alt_Customer;
            }
        }
        [BindingField("Shippng_Method_Code", true)]
        public string Shippng_Method_Code
        {
            set
            {
                _shippng_Method_Code = value;
            }
            get
            {
                return _shippng_Method_Code;
            }
        }
        [BindingField("Shipper", true)]
        public string Shipper
        {
            set
            {
                _shipper = value;
            }
            get
            {
                return _shipper;
            }
        }
        [BindingField("Shiping_Method", true)]
        public string Shiping_Method
        {
            set
            {
                _shiping_Method = value;
            }
            get
            {
                return _shiping_Method;
            }
        }
        [BindingField("Line_Number", true)]
        public int Line_Number
        {
            set
            {
                _line_Number = value;
            }
            get
            {
                return _line_Number;
            }
        }
        [BindingField("Description", true)]
        public string Description
        {
            set
            {
                _description = value;
            }
            get
            {
                return _description;
            }
        }
        [BindingField("Pieces_Ordered", true)]
        public int Pieces_Ordered
        {
            set
            {
                _pieces_Ordered = value;
            }
            get
            {
                return _pieces_Ordered;
            }
        }
        [BindingField("Unit_Cost", true)]
        public double Unit_Cost
        {
            set
            {
                _unit_Cost = value;
            }
            get
            {
                return _unit_Cost;
            }
        }
        [BindingField("Extended_Cost", true)]
        public double Extended_Cost
        {
            set
            {
                _extended_Cost = value;
            }
            get
            {
                return _extended_Cost;
            }
        }
        [BindingField("Order_Source", true)]
        public string Order_Source
        {
            set
            {
                _order_Source = value;
            }
            get
            {
                return _order_Source;
            }
        }
        [BindingField("CreatedOn", true)]
        public DateTime CreatedOn
        {
            set
            {
                _createdOn = value;
            }
            get
            {
                return _createdOn;
            }
        }
        [BindingField("UpdatedBy", true)]
        public string UpdatedBy
        {
            set
            {
                _updatedBy = value;
            }
            get
            {
                return _updatedBy;
            }
        }
        [BindingField("UpdatedOn", true)]
        public DateTime UpdatedOn
        {
            set
            {
                _updatedOn = value;
            }
            get
            {
                return _updatedOn;
            }
        }
        [BindingField("Country_Of_Origin", true)]
        public string Country_Of_Origin
        {
            set
            {
                _country_Of_Origin = value;
            }
            get
            {
                return _country_Of_Origin;
            }
        }
        [BindingField("HTC", true)]
        public string HTC
        {
            set
            {
                _hTC = value;
            }
            get
            {
                return _hTC;
            }
        }
        [BindingField("ECCN", true)]
        public string ECCN
        {
            set
            {
                _eCCN = value;
            }
            get
            {
                return _eCCN;
            }
        }
        [BindingField("Declared_Value", true)]
        public double Declared_Value
        {
            set
            {
                _declared_Value = value;
            }
            get
            {
                return _declared_Value;
            }
        }
        [BindingField("Return_Value", true)]
        public double Return_Value
        {
            set
            {
                _return_Value = value;
            }
            get
            {
                return _return_Value;
            }
        }
        [BindingField("Order_ITN_Required", true)]
        public bool Order_ITN_Required
        {
            set
            {
                _order_ITN_Required = value;
            }
            get
            {
                return _order_ITN_Required;
            }
        }
        [BindingField("Order_ITN", true)]
        public string Order_ITN
        {
            set
            {
                _order_ITN = value;
            }
            get
            {
                return _order_ITN;
            }
        }
        [BindingField("Customs_Invoice", true)]
        public bool Customs_Invoice
        {
            set
            {
                _customs_Invoice = value;
            }
            get
            {
                return _customs_Invoice;
            }
        }
        [BindingField("GrossWeight", true)]
        public double Gross_Weight
        {
            set
            {
                _gross_Weight = value;
            }
            get
            {
                return _gross_Weight;
            }
        }
        [BindingField("ReturnCarrier", true)]
        public string Return_Carrier
        {
            set
            {
                _return_Carrier = value;
            }
            get
            {
                return _return_Carrier;
            }
        }
        [BindingField("InvoiceDate", true)]
        public DateTime InvoiceDate
        {
            set
            {
                _invoiceDate = value;
            }
            get
            {
                return _invoiceDate;
            }
        }
        [BindingField("S_Address1", true)]
        public string S_Address1
        {
            set
            {
                _s_Address1 = value;
            }
            get
            {
                return _s_Address1;
            }
        }
        [BindingField("S_Address2", true)]
        public string S_Address2
        {
            set
            {
                _s_Address2 = value;
            }
            get
            {
                return _s_Address2;
            }
        }
        [BindingField("S_City", true)]
        public string S_City
        {
            set
            {
                _s_City = value;
            }
            get
            {
                return _s_City;
            }
        }
        [BindingField("S_State", true)]
        public string S_State
        {
            set
            {
                _s_State = value;
            }
            get
            {
                return _s_State;
            }
        }
        [BindingField("S_Zip", true)]
        public string S_Zip
        {
            set
            {
                _s_Zip = value;
            }
            get
            {
                return _s_Zip;
            }
        }
        [BindingField("S_Country", true)]
        public string S_Country
        {
            set
            {
                _s_Country = value;
            }
            get
            {
                return _s_Country;
            }
        }
        [BindingField("S_Phone", true)]
        public string S_Phone
        {
            set
            {
                _s_Phone = value;
            }
            get
            {
                return _s_Phone;
            }
        }
        [BindingField("B_Address1", true)]
        public string B_Address1
        {
            set
            {
                _b_Address1 = value;
            }
            get
            {
                return _b_Address1;
            }
        }
        [BindingField("B_Address2", true)]
        public string B_Address2
        {
            set
            {
                _b_Address2 = value;
            }
            get
            {
                return _b_Address2;
            }
        }
        [BindingField("B_City", true)]
        public string B_City
        {
            set
            {
                _b_City = value;
            }
            get
            {
                return _b_City;
            }
        }
        [BindingField("B_State", true)]
        public string B_State
        {
            set
            {
                _b_State = value;
            }
            get
            {
                return _b_State;
            }
        }
        [BindingField("B_Zip", true)]
        public string B_Zip
        {
            set
            {
                _b_Zip = value;
            }
            get
            {
                return _b_Zip;
            }
        }
        [BindingField("B_Country", true)]
        public string B_Country
        {
            set
            {
                _b_Country = value;
            }
            get
            {
                return _b_Country;
            }
        }
        [BindingField("B_Phone", true)]
        public string B_Phone
        {
            set
            {
                _b_Phone = value;
            }
            get
            {
                return _b_Phone;
            }
        }
        [BindingField("S_Company", true)]
        public string S_Company
        {
            set
            {
                _s_Company = value;
            }
            get
            {
                return _s_Company;
            }
        }
        [BindingField("B_Company", true)]
        public string B_Company
        {
            set
            {
                _b_Company = value;
            }
            get
            {
                return _b_Company;
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
        [BindingField("Return_Expected", true)]
        public bool Return_Expected
        {
            set
            {
                _return_Expected = value;
            }
            get
            {
                return _return_Expected;
            }
        }
        [BindingField("ReleaseNumber", true)]
        public int ReleaseNumber
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
        [BindingField("Deliver_Term", true)]
        public string Deliver_Term
        {
            set
            {
                _deliver_Term = value;
            }
            get
            {
                return _deliver_Term;
            }
        }
        [BindingField("AssembleInstruction", true)]
        public string AssembleInstruction
        {
            set
            {
                _assembleInstruction = value;
            }
            get
            {
                return _assembleInstruction;
            }
        }
        [BindingField("Parent_Line_Number", true)]
        public int? Parent_Line_Number
        {
            set
            {
                _parent_Line_Number = value;
            }
            get
            {
                return _parent_Line_Number;
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
        [BindingField("Order_PICC", true)]
        public DateTime? Order_PICC
        {
            set
            {
                _order_PICC = value;
            }
            get
            {
                return _order_PICC;
            }
        }
        [BindingField("Order_PICC_Required", true)]
        public bool Order_PICC_Required
        {
            set
            {
                _order_PICC_Required = value;
            }
            get
            {
                return _order_PICC_Required;
            }
        }

        [BindingField("ShipDate", true)]
        public DateTime? ShipDate
        {
            set
            {
                _shipDate = value;
            }
            get
            {
                return _shipDate;
            }
        }
        [BindingField("ShipTracking", true)]
        public string ShipTracking
        {
            set
            {
                _shipTracking = value;
            }
            get
            {
                return _shipTracking;
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
        #endregion

        #region Extend Property

        private bool _isCommissionable;
        private double _actualPrice;
        private int _orderLineItemId;

        [BindingField("IsCommissionable", true)]
        public bool IsCommissionable
        {
            set
            {
                _isCommissionable = value;
            }
            get
            {
                return _isCommissionable;
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


        private string _s_CountryCode;
        private string _b_CountryCode;

        [BindingField("S_CountryCode", true)]
        public string S_CountryCode
        {
            set
            {
                _s_CountryCode = value;
            }
            get
            {
                return _s_CountryCode;
            }
        }
        [BindingField("B_CountryCode", true)]
        public string B_CountryCode
        {
            set
            {
                _b_CountryCode = value;
            }
            get
            {
                return _b_CountryCode;
            }
        }


        private int _orderTypeId;
        [BindingField("OrderTypeId", true)]
        public int OrderTypeId
        {
            set
            {
                _orderTypeId = value;
            }
            get
            {
                return _orderTypeId;
            }
        }


        #endregion



        public virtual ReturnValue getOrderForZoytoPH(int orderId)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getOrderForZoytoPH"), orderId);
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }

    }
}