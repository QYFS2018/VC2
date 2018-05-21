using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
    [Serializable]
    [BindingClass("Order")]
    public class TOrderTF : TOrder
    {
        public TOrderTF()
        {

        }
     
        public override ReturnValue getDownloadOrderList()
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getDownloadOrderListTF"));
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }
    }
}