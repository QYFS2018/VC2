using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
    [Serializable]
    [BindingClass("Order")]
    public class TOrderTF : TOrder
    {


        private int _pWPCustomerId;
        [BindingField("PWPCustomerId", true)]
        public int PWPCustomerId
        {
            set
            {
                _pWPCustomerId = value;
            }
            get
            {
                return _pWPCustomerId;
            }
        }

        private string _pONumber;
        [BindingField("PONumber", true)]
        public string PONumber
        {
            set
            {
                _pONumber = value;
            }
            get
            {
                return _pONumber;
            }
        }



        public TOrderTF()
        {

        }
     
        public override ReturnValue getDownloadOrderList()
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getDownloadOrderListTF"));
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