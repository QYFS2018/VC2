using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
     [Serializable]
     [BindingClass("Invoice_Line_Item")]
     public class TInvoice_Line_Item : Entity 
     {
          public TInvoice_Line_Item()
          {
              
          }
          
        
          
          #region Basic Property
          private int _id;
          private int _invoiceId;
          private int _lineNum;
          private int _programProductId;
          private string _partNumber;
          private string _productName;
          private int _quantity;
          private DateTime _shippedDate;
          private string _trackingNumber;
          private int _releaseNumber;
          private string _cARTON_ID_FROM;
         private double _price;
         private double _amount;

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
          [BindingField("LineNum",true)]
          public int LineNum
          {
               set
               {
                    _lineNum=value;
               }
               get
               {
                     return _lineNum;
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
          [BindingField("PartNumber",true)]
          public string PartNumber
          {
               set
               {
                    _partNumber=value;
               }
               get
               {
                     return _partNumber;
               }
           }
          [BindingField("ProductName",true)]
          public string ProductName
          {
               set
               {
                    _productName=value;
               }
               get
               {
                     return _productName;
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
          [BindingField("ShippedDate",true)]
          public DateTime ShippedDate
          {
               set
               {
                    _shippedDate=value;
               }
               get
               {
                     return _shippedDate;
               }
           }
          [BindingField("TrackingNumber",true)]
          public string TrackingNumber
          {
               set
               {
                    _trackingNumber=value;
               }
               get
               {
                     return _trackingNumber;
               }
           }
          [BindingField("ReleaseNumber",true)]
          public int ReleaseNumber
          {
               set
               {
                    _releaseNumber=value;
               }
               get
               {
                     return _releaseNumber;
               }
           }
          [BindingField("CARTON_ID_FROM",true)]
          public string CARTON_ID_FROM
          {
               set
               {
                    _cARTON_ID_FROM=value;
               }
               get
               {
                     return _cARTON_ID_FROM;
               }
           }

           private int _orderLineItemId;
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

         [BindingField("Price", true)]
         public double Price
         {
             set
             {
                 _price = value;
             }
             get
             {
                 return _price;
             }
         }
         [BindingField("Amount", true)]
         public double  Amount
         {
             set
             {
                 _amount = value;
             }
             get
             {
                 return _amount;
             }
         }

          #endregion 

          #region Extend Property

         private decimal _standardPrice;
         private decimal _discountAmount;
         private decimal _wholeSalePrice;
         private decimal _totalCost;


         [BindingField("StandardPrice", true)]
         public decimal StandardPrice
         {
             set
             {
                 _standardPrice = value;
             }
             get
             {
                 return _standardPrice;
             }
         }
         [BindingField("DiscountAmount", true)]
         public decimal DiscountAmount
         {
             set
             {
                 _discountAmount = value;
             }
             get
             {
                 return _discountAmount;
             }
         }
         [BindingField("WholeSalePrice", true)]
         public decimal WholeSalePrice
         {
             set
             {
                 _wholeSalePrice = value;
             }
             get
             {
                 return _wholeSalePrice;
             }
         }
         [BindingField("TotalCost", true)]
         public decimal TotalCost
         {
             set
             {
                 _totalCost = value;
             }
             get
             {
                 return _totalCost;
             }
         }


          #endregion 

         public ReturnValue getTotalInvoiceLineItemByInvoiceId(int invoiceId,Transaction tran)
         {
             string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getTotalInvoiceLineItemByInvoiceId"), invoiceId);
             ReturnValue _result = this.getEntity(Usp_SQL, tran);
             return _result;
         }


         public ReturnValue getInvoice_Line_ItemListByInvoiceId(int invoiceId)
         {
             string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getInvoice_Line_ItemListByInvoiceId", "CreateInvoicePDF"), invoiceId);
             ReturnValue _result = this.getEntityList(Usp_SQL);
             return _result;
         }

         public ReturnValue getShipDiscount(int invoiceId)
         {
             string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getShipDiscount", "CreateInvoicePDF"), invoiceId);
             ReturnValue _result = this.getEntity(Usp_SQL);
             return _result;
         }
   
     }
}