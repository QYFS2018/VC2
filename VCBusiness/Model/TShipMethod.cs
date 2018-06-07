using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
     [Serializable]
     [BindingClass("ShipMethod")]
     public class TShipMethod : Entity 
     {
          public TShipMethod()
          {
              
          }
          
        
          
          #region Basic Property
          private int _shipMethodId;
          private int _shipCarrierId;
          private string _code;
          private string _name;
          private int _contentStatusId;
          private string _trackingURL;
          private bool _delivery_Air;
          private bool _delivery_Saturday;
          private bool _delivery_Overnight;

          [BindingField("ShipMethodId",true)]
          public int ShipMethodId
          {
               set
               {
                    _shipMethodId=value;
               }
               get
               {
                     return _shipMethodId;
               }
           }
          [BindingField("ShipCarrierId",true)]
          public int ShipCarrierId
          {
               set
               {
                    _shipCarrierId=value;
               }
               get
               {
                     return _shipCarrierId;
               }
           }
          [BindingField("Code",true)]
          public string Code
          {
               set
               {
                    _code=value;
               }
               get
               {
                     return _code;
               }
           }
          [BindingField("Name",true)]
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
          [BindingField("TrackingURL",true)]
          public string TrackingURL
          {
               set
               {
                    _trackingURL=value;
               }
               get
               {
                     return _trackingURL;
               }
           }
          [BindingField("Delivery_Air",true)]
          public bool Delivery_Air
          {
               set
               {
                    _delivery_Air=value;
               }
               get
               {
                     return _delivery_Air;
               }
           }
          [BindingField("Delivery_Saturday",true)]
          public bool Delivery_Saturday
          {
               set
               {
                    _delivery_Saturday=value;
               }
               get
               {
                     return _delivery_Saturday;
               }
           }
          [BindingField("Delivery_Overnight",true)]
          public bool Delivery_Overnight
          {
               set
               {
                    _delivery_Overnight=value;
               }
               get
               {
                     return _delivery_Overnight;
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
                 return _description;
             }
         }
          #endregion 
          
          
        public virtual  ReturnValue getShipMethodByCode(string code)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getShipMethodByCode"), code);
            ReturnValue _result = this.getEntity(Usp_SQL);
            return _result;
        }

         public ReturnValue getShipMethodById(int id)
         {
             string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getShipMethodById"), id.ToString());
             ReturnValue _result = this.getEntity(Usp_SQL);
             return _result;
         }
     }
}