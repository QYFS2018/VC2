using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
     [Serializable]
     [BindingClass("User")]
     public class TUser : Entity 
     {
          public TUser()
          {
              
          }
          
        
          
          #region Basic Property
          private int _userId;
          private string _email;
          private string _firstName;
          private string _midName;
          private string _lastName;
          private string _password;
          private int _createdBy;
          private DateTime _createdOn;
          private int _updatedBy;
          private DateTime _updatedOn;
          private int _type;
          private bool _status;
          private string _saleRepInitials;
          private int _addressId;
          private DateTime _firstLogin;

          [BindingField("UserId",true)]
          public int UserId
          {
               set
               {
                    _userId=value;
               }
               get
               {
                     return _userId;
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
          [BindingField("MidName",true)]
          public string MidName
          {
               set
               {
                    _midName=value;
               }
               get
               {
                     return _midName;
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
          [BindingField("Type",true)]
          public int Type
          {
               set
               {
                    _type=value;
               }
               get
               {
                     return _type;
               }
           }
          [BindingField("Status",true)]
          public bool Status
          {
               set
               {
                    _status=value;
               }
               get
               {
                     return _status;
               }
           }
          [BindingField("SaleRepInitials",true)]
          public string SaleRepInitials
          {
               set
               {
                    _saleRepInitials=value;
               }
               get
               {
                     return _saleRepInitials;
               }
           }
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
          [BindingField("FirstLogin",true)]
          public DateTime FirstLogin
          {
               set
               {
                    _firstLogin=value;
               }
               get
               {
                     return _firstLogin;
               }
           }

          #endregion 

           #region Extend Property

         private string _description;
           [BindingField("Description", true)]
         public string Description
         {
             set
             {
                 _description = value;
             }
             get
             {
                 return this._description;
             }
         }

           public string SalesRepName
           {
               get
               {
                   return this.SaleRepInitials + " - " + this.FirstName + " " + this.LastName;
               }
           }

           public string SalesRepName2
           {
               get
               {
                   return this.SaleRepInitials + " - " + this.LastName + " " + this.FirstName;
               }
           }
           #endregion



         public ReturnValue getUserById(int id)
         {
             string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getUserById"), id);
             ReturnValue _result = this.getEntity(Usp_SQL);
             return _result;
         }
     }
}