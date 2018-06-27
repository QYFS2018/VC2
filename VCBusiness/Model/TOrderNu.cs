using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
    [Serializable]
    [BindingClass("Order")]
    public class TOrderNu : TOrder
    {

        public TOrderNu()
        {

        }

        public override ReturnValue getDownloadOrderList()
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getOrderDownNU"));
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }

    }
}