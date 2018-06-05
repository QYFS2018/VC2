using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
     [Serializable]
     [BindingClass("Address")]
     public class TAddress : Entity 
     {
          public TAddress()
          {
              
          }
          
        
          
          #region Basic Property
          private int _addressId;
          private string _addressName;
          private string _firstName;
          private string _lastName;
          private string _address1;
          private string _address2;
          private string _address3;
          private string _city;
          private int _stateId;
          private string _postalCode;
          private string _blockCode;
          private string _county;
          private int _countryId;
          private string _phoneNumber1;
          private string _phoneNumber2;
          private string _residenceType;
          private string _matchKey;
          private string _email;
          private string _countryCode;
          private string _stateCode;
          private string _company;
          private bool _uSPSVerfied;
          private int _updatedBy;
          private DateTime _updatedOn;
          private int _createdBy;
          private DateTime _createdOn;
          private string _gEOCODE;
          private bool _override;
          private bool _outsideCityLimit;
          private string _taxCity;
          private double _taxRateCity;
          private double _taxRateCounty;
          private double _taxRateState;
          private bool _outsideLocalLimit;
          private int _taxLimitType;
          private string _mobilePhone;
          private bool _isSend;
          private DateTime _birthDate;
          private double _uTaxRateState;
          private double _uTaxRateCounty;
          private double _uTaxRateCity;
          private double _taxRateCountyLocal;
          private double _taxRateCityLocal;
          private double _uTaxRateCountyLocal;
          private double _uTaxRateCityLocal;
          private string _phoneNumber;
          private string _fax;
          private string _accountNumber;

          [BindingField("AddressId",true)]
          public int AddressId
          {
               set
               {
                    _addressId=value;
               }
               get
               {
                     return _addressId;
               }
           }
          [BindingField("AddressName",true)]
          public string AddressName
          {
               set
               {
                    _addressName=value;
               }
               get
               {
                     return _addressName;
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
          [BindingField("Address1",true)]
          public string Address1
          {
               set
               {
                    _address1=value;
               }
               get
               {
                     return _address1;
               }
           }
          [BindingField("Address2",true)]
          public string Address2
          {
               set
               {
                    _address2=value;
               }
               get
               {
                     return _address2;
               }
           }
          [BindingField("Address3",true)]
          public string Address3
          {
               set
               {
                    _address3=value;
               }
               get
               {
                     return _address3;
               }
           }
          [BindingField("City",true)]
          public string City
          {
               set
               {
                    _city=value;
               }
               get
               {
                     return _city;
               }
           }
          [BindingField("StateId",true)]
          public int StateId
          {
               set
               {
                    _stateId=value;
               }
               get
               {
                     return _stateId;
               }
           }
          [BindingField("PostalCode",true)]
          public string PostalCode
          {
               set
               {
                    _postalCode=value;
               }
               get
               {
                     return _postalCode;
               }
           }
          [BindingField("BlockCode",true)]
          public string BlockCode
          {
               set
               {
                    _blockCode=value;
               }
               get
               {
                     return _blockCode;
               }
           }
          [BindingField("County",true)]
          public string County
          {
               set
               {
                    _county=value;
               }
               get
               {
                     return _county;
               }
           }
          [BindingField("CountryId",true)]
          public int CountryId
          {
               set
               {
                    _countryId=value;
               }
               get
               {
                     return _countryId;
               }
           }
          [BindingField("PhoneNumber1",true)]
          public string PhoneNumber1
          {
               set
               {
                    _phoneNumber1=value;
               }
               get
               {
                     return _phoneNumber1;
               }
           }
          [BindingField("PhoneNumber2",true)]
          public string PhoneNumber2
          {
               set
               {
                    _phoneNumber2=value;
               }
               get
               {
                     return _phoneNumber2;
               }
           }
          [BindingField("ResidenceType",true)]
          public string ResidenceType
          {
               set
               {
                    _residenceType=value;
               }
               get
               {
                     return _residenceType;
               }
           }
          [BindingField("MatchKey",true)]
          public string MatchKey
          {
               set
               {
                    _matchKey=value;
               }
               get
               {
                     return _matchKey;
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
          [BindingField("CountryCode",true)]
          public string CountryCode
          {
               set
               {
                    _countryCode=value;
               }
               get
               {
                     return _countryCode;
               }
           }
          [BindingField("StateCode",true)]
          public string StateCode
          {
               set
               {
                    _stateCode=value;
               }
               get
               {
                     return _stateCode;
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
          [BindingField("USPSVerfied",true)]
          public bool USPSVerfied
          {
               set
               {
                    _uSPSVerfied=value;
               }
               get
               {
                     return _uSPSVerfied;
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
          [BindingField("GEOCODE",true)]
          public string GEOCODE
          {
               set
               {
                    _gEOCODE=value;
               }
               get
               {
                     return _gEOCODE;
               }
           }
          [BindingField("Override",true)]
          public bool Override
          {
               set
               {
                    _override=value;
               }
               get
               {
                     return _override;
               }
           }
          [BindingField("OutsideCityLimit",true)]
          public bool OutsideCityLimit
          {
               set
               {
                    _outsideCityLimit=value;
               }
               get
               {
                     return _outsideCityLimit;
               }
           }
          [BindingField("TaxCity",true)]
          public string TaxCity
          {
               set
               {
                    _taxCity=value;
               }
               get
               {
                     return _taxCity;
               }
           }
          [BindingField("TaxRateCity",true)]
          public double TaxRateCity
          {
               set
               {
                    _taxRateCity=value;
               }
               get
               {
                     return _taxRateCity;
               }
           }
          [BindingField("TaxRateCounty",true)]
          public double TaxRateCounty
          {
               set
               {
                    _taxRateCounty=value;
               }
               get
               {
                     return _taxRateCounty;
               }
           }
          [BindingField("TaxRateState",true)]
          public double TaxRateState
          {
               set
               {
                    _taxRateState=value;
               }
               get
               {
                     return _taxRateState;
               }
           }
          [BindingField("OutsideLocalLimit",true)]
          public bool OutsideLocalLimit
          {
               set
               {
                    _outsideLocalLimit=value;
               }
               get
               {
                     return _outsideLocalLimit;
               }
           }
          [BindingField("TaxLimitType",true)]
          public int TaxLimitType
          {
               set
               {
                    _taxLimitType=value;
               }
               get
               {
                     return _taxLimitType;
               }
           }
          [BindingField("MobilePhone",true)]
          public string MobilePhone
          {
               set
               {
                    _mobilePhone=value;
               }
               get
               {
                     return _mobilePhone;
               }
           }
          [BindingField("IsSend",true)]
          public bool IsSend
          {
               set
               {
                    _isSend=value;
               }
               get
               {
                     return _isSend;
               }
           }
          [BindingField("BirthDate",true)]
          public DateTime BirthDate
          {
               set
               {
                    _birthDate=value;
               }
               get
               {
                     return _birthDate;
               }
           }
          [BindingField("UTaxRateState",true)]
          public double UTaxRateState
          {
               set
               {
                    _uTaxRateState=value;
               }
               get
               {
                     return _uTaxRateState;
               }
           }
          [BindingField("UTaxRateCounty",true)]
          public double UTaxRateCounty
          {
               set
               {
                    _uTaxRateCounty=value;
               }
               get
               {
                     return _uTaxRateCounty;
               }
           }
          [BindingField("UTaxRateCity",true)]
          public double UTaxRateCity
          {
               set
               {
                    _uTaxRateCity=value;
               }
               get
               {
                     return _uTaxRateCity;
               }
           }
          [BindingField("TaxRateCountyLocal",true)]
          public double TaxRateCountyLocal
          {
               set
               {
                    _taxRateCountyLocal=value;
               }
               get
               {
                     return _taxRateCountyLocal;
               }
           }
          [BindingField("TaxRateCityLocal",true)]
          public double TaxRateCityLocal
          {
               set
               {
                    _taxRateCityLocal=value;
               }
               get
               {
                     return _taxRateCityLocal;
               }
           }
          [BindingField("UTaxRateCountyLocal",true)]
          public double UTaxRateCountyLocal
          {
               set
               {
                    _uTaxRateCountyLocal=value;
               }
               get
               {
                     return _uTaxRateCountyLocal;
               }
           }
          [BindingField("UTaxRateCityLocal",true)]
          public double UTaxRateCityLocal
          {
               set
               {
                    _uTaxRateCityLocal=value;
               }
               get
               {
                     return _uTaxRateCityLocal;
               }
           }
          [BindingField("PhoneNumber",true)]
          public string PhoneNumber
          {
               set
               {
                    _phoneNumber=value;
               }
               get
               {
                     return _phoneNumber;
               }
           }
          [BindingField("Fax",true)]
          public string Fax
          {
               set
               {
                    _fax=value;
               }
               get
               {
                     return _fax;
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

          #endregion 

          #region Extend Property

          #endregion 
          
          
         public ReturnValue getAddressById(int id)
         {
             string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getAddressById"), id);
             ReturnValue _result = this.getEntity(Usp_SQL);
             return _result;
         }

     }
}