using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
    [Serializable]
    [BindingClass("Invoice")]
    public class TInvoice : Entity
    {
        public TInvoice()
        {

        }



        #region Basic Property
        private int _invoiceId;
        private string _qBRef;
        private string _altInvoiceNum;
        private string _pONum;
        private int _salesRep;
        private int _terms;
        private DateTime _invoiceDate;
        private DateTime _dueDate;
        private double _invoiceAmount;
        private double _balanceDue;
        private int _orderId;
        private DateTime _createdOn;
        private string _paymentStatus;
        private double _paiedAmount;
        private DateTime _shippedDate;
        private int _customerId;
        private string _sessionId;
        private double _subtotal;
        private double _shipping;
        private double _tax;
        private DateTime _emailSentOn;

        [BindingField("InvoiceId", true)]
        public int InvoiceId
        {
            set
            {
                _invoiceId = value;
            }
            get
            {
                return _invoiceId;
            }
        }
        [BindingField("QBRef", true)]
        public string QBRef
        {
            set
            {
                _qBRef = value;
            }
            get
            {
                return _qBRef;
            }
        }
        [BindingField("AltInvoiceNum", true)]
        public string AltInvoiceNum
        {
            set
            {
                _altInvoiceNum = value;
            }
            get
            {
                return _altInvoiceNum;
            }
        }
        [BindingField("PONum", true)]
        public string PONum
        {
            set
            {
                _pONum = value;
            }
            get
            {
                return _pONum;
            }
        }
        [BindingField("SalesRep", true)]
        public int SalesRep
        {
            set
            {
                _salesRep = value;
            }
            get
            {
                return _salesRep;
            }
        }
        [BindingField("Terms", true)]
        public int Terms
        {
            set
            {
                _terms = value;
            }
            get
            {
                return _terms;
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
        [BindingField("DueDate", true)]
        public DateTime DueDate
        {
            set
            {
                _dueDate = value;
            }
            get
            {
                return _dueDate;
            }
        }
        [BindingField("InvoiceAmount", true)]
        public double InvoiceAmount
        {
            set
            {
                _invoiceAmount = value;
            }
            get
            {
                return _invoiceAmount;
            }
        }
        [BindingField("BalanceDue", true)]
        public double BalanceDue
        {
            set
            {
                _balanceDue = value;
            }
            get
            {
                return _balanceDue;
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
        [BindingField("PaymentStatus", true)]
        public string PaymentStatus
        {
            set
            {
                _paymentStatus = value;
            }
            get
            {
                return _paymentStatus;
            }
        }
        [BindingField("PaiedAmount", true)]
        public double PaiedAmount
        {
            set
            {
                _paiedAmount = value;
            }
            get
            {
                return _paiedAmount;
            }
        }
        [BindingField("ShippedDate", true)]
        public DateTime ShippedDate
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
        [BindingField("CustomerId", true)]
        public int CustomerId
        {
            set
            {
                _customerId = value;
            }
            get
            {
                return _customerId;
            }
        }
        [BindingField("SessionId", true)]
        public string SessionId
        {
            set
            {
                _sessionId = value;
            }
            get
            {
                return _sessionId;
            }
        }

        [BindingField("Subtotal", true)]
        public double Subtotal
        {
            set
            {
                _subtotal = value;
            }
            get
            {
                return _subtotal;
            }
        }
        [BindingField("Shipping", true)]
        public double Shipping
        {
            set
            {
                _shipping = value;
            }
            get
            {
                return _shipping;
            }
        }
        [BindingField("Tax", true)]
        public double Tax
        {
            set
            {
                _tax = value;
            }
            get
            {
                return _tax;
            }
        }
        [BindingField("EmailSentOn", true)]
        public DateTime EmailSentOn
        {
            set
            {
                _emailSentOn = value;
            }
            get
            {
                return _emailSentOn;
            }
        }


        private string _salesRepName;
        private string _termsName;

        [BindingField("SalesRepName", true)]
        public string SalesRepName
        {
            set
            {
                _salesRepName = value;
            }
            get
            {
                return _salesRepName;
            }
        }
        [BindingField("TermsName", true)]
        public string TermsName
        {
            set
            {
                _termsName = value;
            }
            get
            {
                return _termsName;
            }
        }

        #endregion

        #region Extend Property

        private DateTime _orderDate;
        private string _customerName;
        private string _customerAccount;
        private string _email;
        private string _invoiceEmail;
        private string _salesRepEmail;

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

        [BindingField("CustomerName", true)]
        public string CustomerName
        {
            set
            {
                _customerName = value;
            }
            get
            {
                return _customerName;
            }
        }
        [BindingField("CustomerAccount", true)]
        public string CustomerAccount
        {
            set
            {
                _customerAccount = value;
            }
            get
            {
                return _customerAccount;
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
        [BindingField("InvoiceEmail", true)]
        public string InvoiceEmail
        {
            set
            {
                _invoiceEmail = value;
            }
            get
            {
                return _invoiceEmail;
            }
        }
        [BindingField("SalesRepEmail", true)]
        public string SalesRepEmail
        {
            set
            {
                _salesRepEmail = value;
            }
            get
            {
                return _salesRepEmail;
            }
        }

        private DateTime _dueInvoicesEmailOn;
        private DateTime _pastDueEmailOn;

        [BindingField("DueInvoicesEmailOn", true)]
        public DateTime DueInvoicesEmailOn
        {
            set
            {
                _dueInvoicesEmailOn = value;
            }
            get
            {
                return _dueInvoicesEmailOn;
            }
        }

        [BindingField("PastDueEmailOn", true)]
        public DateTime PastDueEmailOn
        {
            set
            {
                _pastDueEmailOn = value;
            }
            get
            {
                return _pastDueEmailOn;
            }
        }

        #endregion



        public ReturnValue getInvoiceById(int id)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getInvoiceById"), id);
            ReturnValue _result = this.getEntity(Usp_SQL);
            return _result;
        }

        public ReturnValue getInvoice(int orderId, WComm.Transaction _transcation)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getInvoice"), orderId);
            ReturnValue _result = this.getEntity(Usp_SQL, _transcation);
            return _result;
        }

        public ReturnValue getInvoiceByOrderId(int orderId, int releasenumber)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getInvoiceByOrderId"), orderId, releasenumber);
            ReturnValue _result = this.getEntity(Usp_SQL);
            return _result;
        }

        public ReturnValue getInvoiceList()
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getInvoiceList"));
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }

        public ReturnValue getReInvoiceEmailList()
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getReInvoiceEmailList"));
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }

        public ReturnValue getDueInvoiceList()
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getDueInvoiceList"));
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }

        public ReturnValue getPastDueInvoiceList()
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getPastDueInvoiceList"));
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }

        public ReturnValue getHDOrderList()
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getHDOrderList"));
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }
    }
}