using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
     [Serializable]
     [BindingClass("PaymentArrangement")]
     public class TPaymentArrangement : Entity 
     {
          public TPaymentArrangement()
          {
              
          }
          
        
          
          #region Basic Property
          private int _paymentArrangementId;
          private int _payMethodId;
          private int _paymentTypeId;
          private string _number;
          private string _cCSecurityCode;
          private DateTime _expirationDate;
          private double _amount;
          private string _nameOnCard;
          private int _billToCustomerId;
          private int _billToAddressId;
          private string _transactionType;
          private string _transactionNumber;
          private DateTime _transactionDate;
          private string _responseCode;
          private string _responseReasonCode;
          private string _responseAVSCode;
          private string _responseAuthCode;
          private string _responseAuthComment;
          private int _orderId;
          private int _refundReasonId;
          private string _comment;
          private string _userName;
          private string _bankName;
          private string _routingNumber;
          private string _accountNumber;
          private string _accountType;
          private string _bankPhoneNumber;
          private DateTime _startDate;
          private DateTime _downloadDate;
          private string _partialNumber;
          private bool _cCFee;
          private string _moneyOrderNumber;
          private int _invoiceId;
          private DateTime _qBUpdatedOn;
          private string _editSequence;
          private string _txnID;

          [BindingField("PaymentArrangementId",true)]
          public int PaymentArrangementId
          {
               set
               {
                    _paymentArrangementId=value;
               }
               get
               {
                     return _paymentArrangementId;
               }
           }
          [BindingField("PayMethodId",true)]
          public int PayMethodId
          {
               set
               {
                    _payMethodId=value;
               }
               get
               {
                     return _payMethodId;
               }
           }
          [BindingField("PaymentTypeId",true)]
          public int PaymentTypeId
          {
               set
               {
                    _paymentTypeId=value;
               }
               get
               {
                     return _paymentTypeId;
               }
           }
          [BindingField("Number",true)]
          public string Number
          {
               set
               {
                    _number=value;
               }
               get
               {
                     return _number;
               }
           }
          [BindingField("CCSecurityCode",true)]
          public string CCSecurityCode
          {
               set
               {
                    _cCSecurityCode=value;
               }
               get
               {
                     return _cCSecurityCode;
               }
           }
          [BindingField("ExpirationDate",true)]
          public DateTime ExpirationDate
          {
               set
               {
                    _expirationDate=value;
               }
               get
               {
                     return _expirationDate;
               }
           }
          [BindingField("Amount",true)]
          public double Amount
          {
               set
               {
                    _amount=value;
               }
               get
               {
                     return _amount;
               }
           }
          [BindingField("NameOnCard",true)]
          public string NameOnCard
          {
               set
               {
                    _nameOnCard=value;
               }
               get
               {
                     return _nameOnCard;
               }
           }
          [BindingField("BillToCustomerId",true)]
          public int BillToCustomerId
          {
               set
               {
                    _billToCustomerId=value;
               }
               get
               {
                     return _billToCustomerId;
               }
           }
          [BindingField("BillToAddressId",true)]
          public int BillToAddressId
          {
               set
               {
                    _billToAddressId=value;
               }
               get
               {
                     return _billToAddressId;
               }
           }
          [BindingField("TransactionType",true)]
          public string TransactionType
          {
               set
               {
                    _transactionType=value;
               }
               get
               {
                     return _transactionType;
               }
           }
          [BindingField("TransactionNumber",true)]
          public string TransactionNumber
          {
               set
               {
                    _transactionNumber=value;
               }
               get
               {
                     return _transactionNumber;
               }
           }
          [BindingField("TransactionDate",true)]
          public DateTime TransactionDate
          {
               set
               {
                    _transactionDate=value;
               }
               get
               {
                     return _transactionDate;
               }
           }
          [BindingField("ResponseCode",true)]
          public string ResponseCode
          {
               set
               {
                    _responseCode=value;
               }
               get
               {
                     return _responseCode;
               }
           }
          [BindingField("ResponseReasonCode",true)]
          public string ResponseReasonCode
          {
               set
               {
                    _responseReasonCode=value;
               }
               get
               {
                     return _responseReasonCode;
               }
           }
          [BindingField("ResponseAVSCode",true)]
          public string ResponseAVSCode
          {
               set
               {
                    _responseAVSCode=value;
               }
               get
               {
                     return _responseAVSCode;
               }
           }
          [BindingField("ResponseAuthCode",true)]
          public string ResponseAuthCode
          {
               set
               {
                    _responseAuthCode=value;
               }
               get
               {
                     return _responseAuthCode;
               }
           }
          [BindingField("ResponseAuthComment",true)]
          public string ResponseAuthComment
          {
               set
               {
                    _responseAuthComment=value;
               }
               get
               {
                     return _responseAuthComment;
               }
           }
          [BindingField("OrderId",true)]
          public int OrderId
          {
               set
               {
                    _orderId=value;
               }
               get
               {
                     return _orderId;
               }
           }
          [BindingField("RefundReasonId",true)]
          public int RefundReasonId
          {
               set
               {
                    _refundReasonId=value;
               }
               get
               {
                     return _refundReasonId;
               }
           }
          [BindingField("Comment",true)]
          public string Comment
          {
               set
               {
                    _comment=value;
               }
               get
               {
                     return _comment;
               }
           }
          [BindingField("UserName",true)]
          public string UserName
          {
               set
               {
                    _userName=value;
               }
               get
               {
                     return _userName;
               }
           }
          [BindingField("BankName",true)]
          public string BankName
          {
               set
               {
                    _bankName=value;
               }
               get
               {
                     return _bankName;
               }
           }
          [BindingField("RoutingNumber",true)]
          public string RoutingNumber
          {
               set
               {
                    _routingNumber=value;
               }
               get
               {
                     return _routingNumber;
               }
           }
          [BindingField("AccountNumber",true)]
          public string AccountNumber
          {
               set
               {
                    _accountNumber=value;
               }
               get
               {
                     return _accountNumber;
               }
           }
          [BindingField("AccountType",true)]
          public string AccountType
          {
               set
               {
                    _accountType=value;
               }
               get
               {
                     return _accountType;
               }
           }
          [BindingField("BankPhoneNumber",true)]
          public string BankPhoneNumber
          {
               set
               {
                    _bankPhoneNumber=value;
               }
               get
               {
                     return _bankPhoneNumber;
               }
           }
          [BindingField("StartDate",true)]
          public DateTime StartDate
          {
               set
               {
                    _startDate=value;
               }
               get
               {
                     return _startDate;
               }
           }
          [BindingField("DownloadDate",true)]
          public DateTime DownloadDate
          {
               set
               {
                    _downloadDate=value;
               }
               get
               {
                     return _downloadDate;
               }
           }
          [BindingField("PartialNumber",true)]
          public string PartialNumber
          {
               set
               {
                    _partialNumber=value;
               }
               get
               {
                     return _partialNumber;
               }
           }
          [BindingField("CCFee",true)]
          public bool CCFee
          {
               set
               {
                    _cCFee=value;
               }
               get
               {
                     return _cCFee;
               }
           }
          [BindingField("MoneyOrderNumber",true)]
          public string MoneyOrderNumber
          {
               set
               {
                    _moneyOrderNumber=value;
               }
               get
               {
                     return _moneyOrderNumber;
               }
           }
          [BindingField("InvoiceId",true)]
          public int InvoiceId
          {
               set
               {
                    _invoiceId=value;
               }
               get
               {
                     return _invoiceId;
               }
           }
          [BindingField("QBUpdatedOn",true)]
          public DateTime QBUpdatedOn
          {
               set
               {
                    _qBUpdatedOn=value;
               }
               get
               {
                     return _qBUpdatedOn;
               }
           }
          [BindingField("EditSequence",true)]
          public string EditSequence
          {
               set
               {
                    _editSequence=value;
               }
               get
               {
                     return _editSequence;
               }
           }
          [BindingField("TxnID",true)]
          public string TxnID
          {
               set
               {
                    _txnID=value;
               }
               get
               {
                     return _txnID;
               }
           }

          #endregion 

          #region Extend Property

          #endregion 
          
          
         public ReturnValue getPaymentArrangementList(int orderId)
         {
             string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getPaymentArrangementListByOrderId"), orderId);
             ReturnValue _result = this.getEntityList(Usp_SQL);
             return _result;
         }

         public ReturnValue getTFOrderPaymentArrangementList(int orderId)
         {
             string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getTFOrderPaymentArrangementList"), orderId);
             ReturnValue _result = this.getEntityList(Usp_SQL);
             return _result;
         }

     }
}