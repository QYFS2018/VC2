using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
     [Serializable]
     [BindingClass("Order_Line_Shipment_Carton")]
     public class TOrder_Line_Shipment_Carton : Entity 
     {
          public TOrder_Line_Shipment_Carton()
          {
              
          }
          
        
          
          #region Basic Property
          private int _cartonId;
          private int _oRDER_ID;
          private int _rELEASE_NUM;
          private string _cARTON_ID_FROM;
          private double _sTD_ACT_WEIGHT;
          private double _sTD_ACT_CUBE;
          private double _fRT_CHARGE;
          private string _cARRIER_ID;
          private string _sHIP_METHOD;
          private string _cARTON_TYPE;
          private string _cARTON_COND;
          private string _pACKLIST_FLAG;
          private string _pACKAGE_TRACE_ID;
          private string _cod_flag;
          private double _cod_amount;
          private string _sat_delivery_flag;
          private double _declared_value;
          private string _hazardous_flag;
          private string _cert_mail_flag;
          private string _return_receipt_flag;
          private DateTime _ship_date;
          private string _bol_id;
          private string _mbol_id;
          private string _orig_carton_id;
          private string _orig_pallet_id;
          private DateTime _shippingConfirmDate;

          [BindingField("CartonId",true)]
          public int CartonId
          {
               set
               {
                    _cartonId=value;
               }
               get
               {
                     return _cartonId;
               }
           }
          [BindingField("ORDER_ID",true)]
          public int ORDER_ID
          {
               set
               {
                    _oRDER_ID=value;
               }
               get
               {
                     return _oRDER_ID;
               }
           }
          [BindingField("RELEASE_NUM",true)]
          public int RELEASE_NUM
          {
               set
               {
                    _rELEASE_NUM=value;
               }
               get
               {
                     return _rELEASE_NUM;
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
          [BindingField("STD_ACT_WEIGHT",true)]
          public double STD_ACT_WEIGHT
          {
               set
               {
                    _sTD_ACT_WEIGHT=value;
               }
               get
               {
                     return _sTD_ACT_WEIGHT;
               }
           }
          [BindingField("STD_ACT_CUBE",true)]
          public double STD_ACT_CUBE
          {
               set
               {
                    _sTD_ACT_CUBE=value;
               }
               get
               {
                     return _sTD_ACT_CUBE;
               }
           }
          [BindingField("FRT_CHARGE",true)]
          public double FRT_CHARGE
          {
               set
               {
                    _fRT_CHARGE=value;
               }
               get
               {
                     return _fRT_CHARGE;
               }
           }
          [BindingField("CARRIER_ID",true)]
          public string CARRIER_ID
          {
               set
               {
                    _cARRIER_ID=value;
               }
               get
               {
                     return _cARRIER_ID;
               }
           }
          [BindingField("SHIP_METHOD",true)]
          public string SHIP_METHOD
          {
               set
               {
                    _sHIP_METHOD=value;
               }
               get
               {
                     return _sHIP_METHOD;
               }
           }
          [BindingField("CARTON_TYPE",true)]
          public string CARTON_TYPE
          {
               set
               {
                    _cARTON_TYPE=value;
               }
               get
               {
                     return _cARTON_TYPE;
               }
           }
          [BindingField("CARTON_COND",true)]
          public string CARTON_COND
          {
               set
               {
                    _cARTON_COND=value;
               }
               get
               {
                     return _cARTON_COND;
               }
           }
          [BindingField("PACKLIST_FLAG",true)]
          public string PACKLIST_FLAG
          {
               set
               {
                    _pACKLIST_FLAG=value;
               }
               get
               {
                     return _pACKLIST_FLAG;
               }
           }
          [BindingField("PACKAGE_TRACE_ID",true)]
          public string PACKAGE_TRACE_ID
          {
               set
               {
                    _pACKAGE_TRACE_ID=value;
               }
               get
               {
                     return _pACKAGE_TRACE_ID;
               }
           }
          [BindingField("Cod_flag",true)]
          public string Cod_flag
          {
               set
               {
                    _cod_flag=value;
               }
               get
               {
                     return _cod_flag;
               }
           }
          [BindingField("Cod_amount",true)]
          public double Cod_amount
          {
               set
               {
                    _cod_amount=value;
               }
               get
               {
                     return _cod_amount;
               }
           }
          [BindingField("Sat_delivery_flag",true)]
          public string Sat_delivery_flag
          {
               set
               {
                    _sat_delivery_flag=value;
               }
               get
               {
                     return _sat_delivery_flag;
               }
           }
          [BindingField("Declared_value",true)]
          public double Declared_value
          {
               set
               {
                    _declared_value=value;
               }
               get
               {
                     return _declared_value;
               }
           }
          [BindingField("Hazardous_flag",true)]
          public string Hazardous_flag
          {
               set
               {
                    _hazardous_flag=value;
               }
               get
               {
                     return _hazardous_flag;
               }
           }
          [BindingField("Cert_mail_flag",true)]
          public string Cert_mail_flag
          {
               set
               {
                    _cert_mail_flag=value;
               }
               get
               {
                     return _cert_mail_flag;
               }
           }
          [BindingField("Return_receipt_flag",true)]
          public string Return_receipt_flag
          {
               set
               {
                    _return_receipt_flag=value;
               }
               get
               {
                     return _return_receipt_flag;
               }
           }
          [BindingField("Ship_date",true)]
          public DateTime Ship_date
          {
               set
               {
                    _ship_date=value;
               }
               get
               {
                     return _ship_date;
               }
           }
          [BindingField("Bol_id",true)]
          public string Bol_id
          {
               set
               {
                    _bol_id=value;
               }
               get
               {
                     return _bol_id;
               }
           }
          [BindingField("Mbol_id",true)]
          public string Mbol_id
          {
               set
               {
                    _mbol_id=value;
               }
               get
               {
                     return _mbol_id;
               }
           }
          [BindingField("Orig_carton_id",true)]
          public string Orig_carton_id
          {
               set
               {
                    _orig_carton_id=value;
               }
               get
               {
                     return _orig_carton_id;
               }
           }
          [BindingField("Orig_pallet_id",true)]
          public string Orig_pallet_id
          {
               set
               {
                    _orig_pallet_id=value;
               }
               get
               {
                     return _orig_pallet_id;
               }
           }
          [BindingField("ShippingConfirmDate",true)]
          public DateTime ShippingConfirmDate
          {
               set
               {
                    _shippingConfirmDate=value;
               }
               get
               {
                     return _shippingConfirmDate;
               }
           }

          #endregion 

          #region Extend Property

          #endregion 
          
          public ReturnValue deleteOrderLineShipmentCartonByOrderID(int orderId, Transaction tran)
          {
              string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("deleteOrderLineShipmentCartonByOrderID"), orderId);
              ReturnValue _result = this.ExecSql(Usp_SQL, tran);
              return _result;
          }
     }
}