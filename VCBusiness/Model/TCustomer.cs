using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
     [Serializable]
     [BindingClass("Customer")]
     public class TCustomer : Entity 
     {
          public TCustomer()
          {
              
          }
          
        
          
          #region Basic Property
          private int _customerId;
          private int _programId;
          private string _company;
          private int _sourceId;
          private int _salesRepId;
          private string _altCustNum;
          private string _firstName;
          private string _lastName;
          private string _middleName;
          private int _contentStatusId;
          private int _customerTypeId;
          private int _createdBy;
          private int _updatedBy;
          private DateTime _createdOn;
          private DateTime _updatedOn;
          private int _billToAddressId;
          private int _shipToAddressId;
          private int _paymentTypeId;
          private string _number;
          private DateTime _expirationDate;
          private string _nameOnCard;
          private int _cCBillingAddressId;
          private int _territoryId;
          private int _accountCodePrefixId;
          private double _sRCommRate;
          private double _creditLimit;
          private bool _taxable;
          private int _paymentTermsId;
          private int _priceLevelId;
          private string _taxID;
          private string _taxDocName;
          private string _taxDocSystemlName;
          private string _applicationDocName;
          private string _applicationDocSystemName;
          private string _dBABusinessName;
          private DateTime _dateBusinessEstablished;
          private int _entityType;
          private string _howLongAtCurrentAddress;
          private int _bankAddressId;
          private int _additionalAddress1Id;
          private int _additionallAddress2Id;
          private string _title;
          private string _preferredShippingCarrier;
          private string _preferredShippingMethod;
          private string _customerAccountNumber;
          private string _email;
          private string _password;

          [BindingField("CustomerId",true)]
          public int CustomerId
          {
               set
               {
                    _customerId=value;
               }
               get
               {
                     return _customerId;
               }
           }
          [BindingField("ProgramId",true)]
          public int ProgramId
          {
               set
               {
                    _programId=value;
               }
               get
               {
                     return _programId;
               }
           }
          [BindingField("Company",true)]
          public string Company
          {
               set
               {
                    _company=value;
               }
               get
               {
                     return _company;
               }
           }
          [BindingField("SourceId",true)]
          public int SourceId
          {
               set
               {
                    _sourceId=value;
               }
               get
               {
                     return _sourceId;
               }
           }
          [BindingField("SalesRepId",true)]
          public int SalesRepId
          {
               set
               {
                    _salesRepId=value;
               }
               get
               {
                     return _salesRepId;
               }
           }
          [BindingField("AltCustNum",true)]
          public string AltCustNum
          {
               set
               {
                    _altCustNum=value;
               }
               get
               {
                     return _altCustNum;
               }
           }
          [BindingField("FirstName",true)]
          public string FirstName
          {
               set
               {
                    _firstName=value;
               }
               get
               {
                     return _firstName;
               }
           }
          [BindingField("LastName",true)]
          public string LastName
          {
               set
               {
                    _lastName=value;
               }
               get
               {
                     return _lastName;
               }
           }
          [BindingField("MiddleName",true)]
          public string MiddleName
          {
               set
               {
                    _middleName=value;
               }
               get
               {
                     return _middleName;
               }
           }
          [BindingField("ContentStatusId",true)]
          public int ContentStatusId
          {
               set
               {
                    _contentStatusId=value;
               }
               get
               {
                     return _contentStatusId;
               }
           }
          [BindingField("CustomerTypeId",true)]
          public int CustomerTypeId
          {
               set
               {
                    _customerTypeId=value;
               }
               get
               {
                     return _customerTypeId;
               }
           }
          [BindingField("CreatedBy",true)]
          public int CreatedBy
          {
               set
               {
                    _createdBy=value;
               }
               get
               {
                     return _createdBy;
               }
           }
          [BindingField("UpdatedBy",true)]
          public int UpdatedBy
          {
               set
               {
                    _updatedBy=value;
               }
               get
               {
                     return _updatedBy;
               }
           }
          [BindingField("CreatedOn",true)]
          public DateTime CreatedOn
          {
               set
               {
                    _createdOn=value;
               }
               get
               {
                     return _createdOn;
               }
           }
          [BindingField("UpdatedOn",true)]
          public DateTime UpdatedOn
          {
               set
               {
                    _updatedOn=value;
               }
               get
               {
                     return _updatedOn;
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
          [BindingField("ShipToAddressId",true)]
          public int ShipToAddressId
          {
               set
               {
                    _shipToAddressId=value;
               }
               get
               {
                     return _shipToAddressId;
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
          [BindingField("CCBillingAddressId",true)]
          public int CCBillingAddressId
          {
               set
               {
                    _cCBillingAddressId=value;
               }
               get
               {
                     return _cCBillingAddressId;
               }
           }
          [BindingField("TerritoryId",true)]
          public int TerritoryId
          {
               set
               {
                    _territoryId=value;
               }
               get
               {
                     return _territoryId;
               }
           }
          [BindingField("AccountCodePrefixId",true)]
          public int AccountCodePrefixId
          {
               set
               {
                    _accountCodePrefixId=value;
               }
               get
               {
                     return _accountCodePrefixId;
               }
           }
          [BindingField("SRCommRate",true)]
          public double SRCommRate
          {
               set
               {
                    _sRCommRate=value;
               }
               get
               {
                     return _sRCommRate;
               }
           }
          [BindingField("CreditLimit",true)]
          public double CreditLimit
          {
               set
               {
                    _creditLimit=value;
               }
               get
               {
                     return _creditLimit;
               }
           }
          [BindingField("Taxable",true)]
          public bool Taxable
          {
               set
               {
                    _taxable=value;
               }
               get
               {
                     return _taxable;
               }
           }
          [BindingField("PaymentTermsId",true)]
          public int PaymentTermsId
          {
               set
               {
                    _paymentTermsId=value;
               }
               get
               {
                     return _paymentTermsId;
               }
           }
          [BindingField("PriceLevelId",true)]
          public int PriceLevelId
          {
               set
               {
                    _priceLevelId=value;
               }
               get
               {
                     return _priceLevelId;
               }
           }
          [BindingField("TaxID",true)]
          public string TaxID
          {
               set
               {
                    _taxID=value;
               }
               get
               {
                     return _taxID;
               }
           }
          [BindingField("TaxDocName",true)]
          public string TaxDocName
          {
               set
               {
                    _taxDocName=value;
               }
               get
               {
                     return _taxDocName;
               }
           }
          [BindingField("TaxDocSystemlName",true)]
          public string TaxDocSystemlName
          {
               set
               {
                    _taxDocSystemlName=value;
               }
               get
               {
                     return _taxDocSystemlName;
               }
           }
          [BindingField("ApplicationDocName",true)]
          public string ApplicationDocName
          {
               set
               {
                    _applicationDocName=value;
               }
               get
               {
                     return _applicationDocName;
               }
           }
          [BindingField("ApplicationDocSystemName",true)]
          public string ApplicationDocSystemName
          {
               set
               {
                    _applicationDocSystemName=value;
               }
               get
               {
                     return _applicationDocSystemName;
               }
           }
          [BindingField("DBABusinessName",true)]
          public string DBABusinessName
          {
               set
               {
                    _dBABusinessName=value;
               }
               get
               {
                     return _dBABusinessName;
               }
           }
          [BindingField("DateBusinessEstablished",true)]
          public DateTime DateBusinessEstablished
          {
               set
               {
                    _dateBusinessEstablished=value;
               }
               get
               {
                     return _dateBusinessEstablished;
               }
           }
          [BindingField("EntityType",true)]
          public int EntityType
          {
               set
               {
                    _entityType=value;
               }
               get
               {
                     return _entityType;
               }
           }
          [BindingField("HowLongAtCurrentAddress",true)]
          public string HowLongAtCurrentAddress
          {
               set
               {
                    _howLongAtCurrentAddress=value;
               }
               get
               {
                     return _howLongAtCurrentAddress;
               }
           }
          [BindingField("BankAddressId",true)]
          public int BankAddressId
          {
               set
               {
                    _bankAddressId=value;
               }
               get
               {
                     return _bankAddressId;
               }
           }
          [BindingField("AdditionalAddress1Id",true)]
          public int AdditionalAddress1Id
          {
               set
               {
                    _additionalAddress1Id=value;
               }
               get
               {
                     return _additionalAddress1Id;
               }
           }
          [BindingField("AdditionallAddress2Id",true)]
          public int AdditionallAddress2Id
          {
               set
               {
                    _additionallAddress2Id=value;
               }
               get
               {
                     return _additionallAddress2Id;
               }
           }
          [BindingField("Title",true)]
          public string Title
          {
               set
               {
                    _title=value;
               }
               get
               {
                     return _title;
               }
           }
          [BindingField("PreferredShippingCarrier",true)]
          public string PreferredShippingCarrier
          {
               set
               {
                    _preferredShippingCarrier=value;
               }
               get
               {
                     return _preferredShippingCarrier;
               }
           }
          [BindingField("PreferredShippingMethod",true)]
          public string PreferredShippingMethod
          {
               set
               {
                    _preferredShippingMethod=value;
               }
               get
               {
                     return _preferredShippingMethod;
               }
           }
          [BindingField("CustomerAccountNumber",true)]
          public string CustomerAccountNumber
          {
               set
               {
                    _customerAccountNumber=value;
               }
               get
               {
                     return _customerAccountNumber;
               }
           }
          [BindingField("Email",true)]
          public string Email
          {
               set
               {
                    _email=value;
               }
               get
               {
                     return _email;
               }
           }
          [BindingField("Password",true)]
          public string Password
          {
               set
               {
                    _password=value;
               }
               get
               {
                     return _password;
               }
           }


         private string _orderEmail;
         private string _invoiceEmail;


           [BindingField("OrderEmail", true)]
         public string OrderEmail
         {
             set
             {
                 _orderEmail = value;
             }
             get
             {
                 return _orderEmail;
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

          #endregion 

          #region Extend Property

         private string _salesRepEmail;
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


       
         private string _secondaryEmail;
         [BindingField("SecondaryEmail", true)]
         public string SecondaryEmail
         {
             set
             {
                 _secondaryEmail = value;
             }
             get
             {
                 return _secondaryEmail;
             }
         }

          #endregion 
          
          
        public ReturnValue getCustomerById(int id)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getCustomerById"), id);
            ReturnValue _result = this.getEntity(Usp_SQL);
            return _result;
        }


         public ReturnValue getCustomerByInvoiceId(int id)
         {
             string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getCustomerByInvoiceId"), id);
             ReturnValue _result = this.getEntity(Usp_SQL);
             return _result;
         }

    
     }
}