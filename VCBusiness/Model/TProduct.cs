using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
     [Serializable]
     [BindingClass("Product")]
     public class TProduct : Entity 
     {
          public TProduct()
          {
              
          }
          
        
          
          #region Basic Property
          private int _productId;
          private string _partNumber;
          private string _name;
          private string _shortDesc;
          private string _longDesc;
          private string _keyword;
          private DateTime _createdOn;
          private int _createdBy;
          private DateTime _updatedOn;
          private int _updatedBy;
          private double _weight;
          private int _estUnits;
          private int _estCommitted;
          private int _estLow;
          private int _maxQtySell;
          private int _maxBackOrder;
          private int _maxPreOrder;
          private int _safeStockLevel;
          private bool _phantom;
          private string _simpleNameSndxStr;
          private int _isActive;
          private int _pHOnHand;
          private int _pHCommitted;
          private int _pHBlocked;
          private int _pHOnhold;
          private int _pHHard;
          private string _productClass;

          [BindingField("ProductId",true)]
          public int ProductId
          {
               set
               {
                    _productId=value;
               }
               get
               {
                     return _productId;
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
          [BindingField("Name", true, true, true)]
          public string Name
          {
               set
               {
                    _name=value;
               }
               get
               {
                     return _name;
               }
           }
          [BindingField("Name", true, true, true)]
          public string ShortDesc
          {
               set
               {
                    _shortDesc=value;
               }
               get
               {
                     return _shortDesc;
               }
           }
          [BindingField("Name", true, true, true)]
          public string LongDesc
          {
               set
               {
                    _longDesc=value;
               }
               get
               {
                     return _longDesc;
               }
           }
          [BindingField("Keyword",true)]
          public string Keyword
          {
               set
               {
                    _keyword=value;
               }
               get
               {
                     return _keyword;
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
          [BindingField("Weight",true)]
          public double Weight
          {
               set
               {
                    _weight=value;
               }
               get
               {
                     return _weight;
               }
           }
          [BindingField("EstUnits",true)]
          public int EstUnits
          {
               set
               {
                    _estUnits=value;
               }
               get
               {
                     return _estUnits;
               }
           }
          [BindingField("EstCommitted",true)]
          public int EstCommitted
          {
               set
               {
                    _estCommitted=value;
               }
               get
               {
                     return _estCommitted;
               }
           }
          [BindingField("EstLow",true)]
          public int EstLow
          {
               set
               {
                    _estLow=value;
               }
               get
               {
                     return _estLow;
               }
           }
          [BindingField("MaxQtySell",true)]
          public int MaxQtySell
          {
               set
               {
                    _maxQtySell=value;
               }
               get
               {
                     return _maxQtySell;
               }
           }
          [BindingField("MaxBackOrder",true)]
          public int MaxBackOrder
          {
               set
               {
                    _maxBackOrder=value;
               }
               get
               {
                     return _maxBackOrder;
               }
           }
          [BindingField("MaxPreOrder",true)]
          public int MaxPreOrder
          {
               set
               {
                    _maxPreOrder=value;
               }
               get
               {
                     return _maxPreOrder;
               }
           }
          [BindingField("SafeStockLevel",true)]
          public int SafeStockLevel
          {
               set
               {
                    _safeStockLevel=value;
               }
               get
               {
                     return _safeStockLevel;
               }
           }
          [BindingField("Phantom",true)]
          public bool Phantom
          {
               set
               {
                    _phantom=value;
               }
               get
               {
                     return _phantom;
               }
           }
          [BindingField("SimpleNameSndxStr",true)]
          public string SimpleNameSndxStr
          {
               set
               {
                    _simpleNameSndxStr=value;
               }
               get
               {
                     return _simpleNameSndxStr;
               }
           }
          [BindingField("IsActive",true)]
          public int IsActive
          {
               set
               {
                    _isActive=value;
               }
               get
               {
                     return _isActive;
               }
           }
          [BindingField("PHOnHand",true)]
          public int PHOnHand
          {
               set
               {
                    _pHOnHand=value;
               }
               get
               {
                     return _pHOnHand;
               }
           }
          [BindingField("PHCommitted",true)]
          public int PHCommitted
          {
               set
               {
                    _pHCommitted=value;
               }
               get
               {
                     return _pHCommitted;
               }
           }
          [BindingField("PHBlocked",true)]
          public int PHBlocked
          {
               set
               {
                    _pHBlocked=value;
               }
               get
               {
                     return _pHBlocked;
               }
           }
          [BindingField("PHOnhold",true)]
          public int PHOnhold
          {
               set
               {
                    _pHOnhold=value;
               }
               get
               {
                     return _pHOnhold;
               }
           }
          [BindingField("PHHard",true)]
          public int PHHard
          {
               set
               {
                    _pHHard=value;
               }
               get
               {
                     return _pHHard;
               }
           }
          [BindingField("ProductClass",true)]
          public string ProductClass
          {
               set
               {
                    _productClass=value;
               }
               get
               {
                     return _productClass;
               }
           }

          #endregion 

          #region Extend Property

          #endregion 
    

        public ReturnValue getProductList()
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getProductList"));
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }

        public ReturnValue updateProductEstUnit(int productID, int estUnit, int onHand)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("updateProductEstUnit"), productID, estUnit, onHand);
            ReturnValue _result = this.ExecSql(Usp_SQL);
            return _result;
        }

        public ReturnValue UpdateBOOrder()
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("UpdateBOOrder"));
            ReturnValue _result = this.ExecSql(Usp_SQL);
            return _result;
        }



        public virtual ReturnValue resetProductEstcommitted()
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("resetProductEstcommitted"));
            ReturnValue _result = this.ExecSql(Usp_SQL);
            return _result;
        }
     }
}