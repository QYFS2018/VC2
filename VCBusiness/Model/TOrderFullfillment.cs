using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
    [Serializable]
    [BindingClass("Order")]
    public class TOrderFullfillment : TOrder
    {


   
        public TOrderFullfillment()
        {

        }
     
        public override ReturnValue getDownloadOrderList(int programID)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getDownloadOrderListFullfillment"), programID);
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }

        public override ReturnValue getOrderById(int id)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getOrderTFById"), id);
            ReturnValue _result = this.getEntity(Usp_SQL);
            return _result;
        }
        public  ReturnValue getOrderById(int id,Transaction tran)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getOrderTFById"), id);
            ReturnValue _result = this.getEntity(Usp_SQL, tran);
            return _result;
        }
    }
}