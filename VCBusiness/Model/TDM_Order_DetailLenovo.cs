using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
    [Serializable]
    [BindingClass("DM_Order_Detail")]
    public class TDM_Order_DetailLenovo : TDM_Order_Detail
    {
        public TDM_Order_DetailLenovo()
        {
            
        }

     


        public override ReturnValue getOrderForZoytoPH(int orderId)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getOrderForZoytoPHLenovo"), orderId);
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }

    }
}