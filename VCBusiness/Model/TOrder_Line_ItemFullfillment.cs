﻿using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
    [Serializable]
    [BindingClass("Order_Line_Item")]
    public class TOrder_Line_ItemFullfillment : TOrder_Line_Item
    {
        public TOrder_Line_ItemFullfillment()
        {

        }


   

        public  override ReturnValue getOrderLineByOrderId(int id)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getOrderLineDownFullfillment"), id);
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }

        public override ReturnValue getOrdersDetail(int orderId)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getOrdersDetailNU"), orderId);
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }
   
    }
}