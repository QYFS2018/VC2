using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
     [Serializable]
     [BindingClass("WishList")]
     public class TWishList : Entity 
     {
          public TWishList()
          {
              
          }
          
        
          
          #region Basic Property
          private int _id;
          private int _customerId;
          private DateTime _createdOn;
          private int _createdBy;
          private int _updatedBy;
          private DateTime _updatedOn;

          [BindingField("Id",true)]
          public int Id
          {
               set
               {
                    _id=value;
               }
               get
               {
                     return _id;
               }
           }
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

          #endregion 

          #region Extend Property

         private string _firstName;
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


         private string _lastName;
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

         private string _email;
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

         private string _orderEmail;
         private string _secondaryEmail;
         private string _salesRepEmail;

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


          #endregion 
          
          
        public ReturnValue getWishList()
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getWishList"));
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }

         public ReturnValue updateMailInformedOnByWishListId(int id)
         {
             string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("updateMailInformedOnByWishListId"), id);
             ReturnValue _result = this.ExecSql(Usp_SQL);
             return _result;
         }
     }
}