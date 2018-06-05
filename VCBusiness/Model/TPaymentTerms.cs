using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
     [Serializable]
     [BindingClass("PaymentTerms")]
     public class TPaymentTerms : Entity 
     {
          public TPaymentTerms()
          {
              
          }
          
        
          
          #region Basic Property
          private int _id;
          private string _description;
          private int _netDueInDays;
          private double _discount;
          private double _discountIfPaidInDays;
          private DateTime _updatedOn;
          private int _updatedBy;
          private DateTime _createdOn;
          private int _createdBY;

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
          [BindingField("Description",true)]
          public string Description
          {
               set
               {
                    _description=value;
               }
               get
               {
                     return _description;
               }
           }
          [BindingField("NetDueInDays",true)]
          public int NetDueInDays
          {
               set
               {
                    _netDueInDays=value;
               }
               get
               {
                     return _netDueInDays;
               }
           }
          [BindingField("Discount",true)]
          public double Discount
          {
               set
               {
                    _discount=value;
               }
               get
               {
                     return _discount;
               }
           }
          [BindingField("DiscountIfPaidInDays",true)]
          public double DiscountIfPaidInDays
          {
               set
               {
                    _discountIfPaidInDays=value;
               }
               get
               {
                     return _discountIfPaidInDays;
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
          [BindingField("CreatedBY",true)]
          public int CreatedBY
          {
               set
               {
                    _createdBY=value;
               }
               get
               {
                     return _createdBY;
               }
           }

          #endregion 

          #region Extend Property

          #endregion 
          
          
        public ReturnValue getPaymentTermsById(int id)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getPaymentTermsById"), id);
            ReturnValue _result = this.getEntity(Usp_SQL);
            return _result;
        }

      
     }
}