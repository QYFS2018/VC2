using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
    [Serializable]
    [BindingClass("DM_Order_Detail")]
    public class TDM_Order_DetailNu : TDM_Order_Detail
    {
        public TDM_Order_DetailNu()
        {
            
        }

     


        public override ReturnValue getOrderForZoytoPH(int orderId)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getOrderForZoytoPHNU"), orderId);
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }

    }
}