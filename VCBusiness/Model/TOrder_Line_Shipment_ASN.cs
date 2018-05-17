using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
     [Serializable]
     [BindingClass("Order_Line_Shipment_ASN")]
     public class TOrder_Line_Shipment_ASN : Entity 
     {
          public TOrder_Line_Shipment_ASN()
          {
              
          }
          
        
          
          #region Basic Property
          private int _aSNId;
          private int _oRDER_ID;
          private int _rELEASE_NUM;
          private int _oRDER_LINE;
          private string _cARTON_ID_FROM;
          private string _pALLET_ID_FROM;
          private string _iTEM_ID;
          private string _iNV_TYPE;
          private string _cFG_CODE;
          private string _lOT_ID;
          private string _sERIAL_NUMBER;
          private string _hOLD_CODE;
          private int _pIECES_TO_MOVE;

          [BindingField("ASNId",true)]
          public int ASNId
          {
               set
               {
                    _aSNId=value;
               }
               get
               {
                     return _aSNId;
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
          [BindingField("ORDER_LINE",true)]
          public int ORDER_LINE
          {
               set
               {
                    _oRDER_LINE=value;
               }
               get
               {
                     return _oRDER_LINE;
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
          [BindingField("PALLET_ID_FROM",true)]
          public string PALLET_ID_FROM
          {
               set
               {
                    _pALLET_ID_FROM=value;
               }
               get
               {
                     return _pALLET_ID_FROM;
               }
           }
          [BindingField("ITEM_ID",true)]
          public string ITEM_ID
          {
               set
               {
                    _iTEM_ID=value;
               }
               get
               {
                     return _iTEM_ID;
               }
           }
          [BindingField("INV_TYPE",true)]
          public string INV_TYPE
          {
               set
               {
                    _iNV_TYPE=value;
               }
               get
               {
                     return _iNV_TYPE;
               }
           }
          [BindingField("CFG_CODE",true)]
          public string CFG_CODE
          {
               set
               {
                    _cFG_CODE=value;
               }
               get
               {
                     return _cFG_CODE;
               }
           }
          [BindingField("LOT_ID",true)]
          public string LOT_ID
          {
               set
               {
                    _lOT_ID=value;
               }
               get
               {
                     return _lOT_ID;
               }
           }
          [BindingField("SERIAL_NUMBER",true)]
          public string SERIAL_NUMBER
          {
               set
               {
                    _sERIAL_NUMBER=value;
               }
               get
               {
                     return _sERIAL_NUMBER;
               }
           }
          [BindingField("HOLD_CODE",true)]
          public string HOLD_CODE
          {
               set
               {
                    _hOLD_CODE=value;
               }
               get
               {
                     return _hOLD_CODE;
               }
           }
          [BindingField("PIECES_TO_MOVE",true)]
          public int PIECES_TO_MOVE
          {
               set
               {
                    _pIECES_TO_MOVE=value;
               }
               get
               {
                     return _pIECES_TO_MOVE;
               }
           }

          #endregion 

          #region Extend Property

          #endregion 
          
          public ReturnValue deleteOrderLineShipmentASNByOrderID(int orderId, Transaction tran)
          {
              string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("deleteOrderLineShipmentASNByOrderID"), orderId);
              ReturnValue _result = this.ExecSql(Usp_SQL, tran);
              return _result;
          }
      
     }
}