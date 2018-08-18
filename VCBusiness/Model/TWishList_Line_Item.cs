using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
     [Serializable]
     [BindingClass("WishList_Line_Item")]
     public class TWishList_Line_Item : Entity 
     {
          public TWishList_Line_Item()
          {
              
          }
          
        
          
          #region Basic Property
          private int _id;
          private int _wishListId;
          private int _programProductId;
          private int _quantity;
          private DateTime _createdOn;
          private DateTime _mailInformedOn;

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
          [BindingField("WishListId",true)]
          public int WishListId
          {
               set
               {
                    _wishListId=value;
               }
               get
               {
                     return _wishListId;
               }
           }
          [BindingField("ProgramProductId",true)]
          public int ProgramProductId
          {
               set
               {
                    _programProductId=value;
               }
               get
               {
                     return _programProductId;
               }
           }
          [BindingField("Quantity",true)]
          public int Quantity
          {
               set
               {
                    _quantity=value;
               }
               get
               {
                     return _quantity;
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
          [BindingField("MailInformedOn",true)]
          public DateTime MailInformedOn
          {
               set
               {
                    _mailInformedOn=value;
               }
               get
               {
                     return _mailInformedOn;
               }
           }

          #endregion 

          #region Extend Property

         private string _partNumber;
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


           private string _name;
           [BindingField("Name", true,true ,true)]
         public string Name
           {
               set
               {
                   _name = value;
               }
               get
               {
                   return _name;
               }
           }



          #endregion 
          
          
        public ReturnValue getWishListLineItemByWishListId(int id)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getWishListLineItemByWishListId"), id);
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }

       

     }
}