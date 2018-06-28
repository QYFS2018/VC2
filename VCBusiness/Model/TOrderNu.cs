using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
    [Serializable]
    [BindingClass("Order")]
    public class TOrderNu : TOrder
    {


        private string _mailTo;
        [BindingField("MailTo", true)]
        public string MailTo
        {
            set
            {
                _mailTo = value;
            }
            get
            {
                return _mailTo;
            }
        }

        private string _mailCC;
        [BindingField("MailCC", true)]
        public string MailCC
        {
            set
            {
                _mailCC = value;
            }
            get
            {
                return _mailCC;
            }
        }

        private string _mailBCC;
        [BindingField("MailBCC", true)]
        public string MailBCC
        {
            set
            {
                _mailBCC = value;
            }
            get
            {
                return _mailBCC;
            }
        }


        public TOrderNu()
        {

        }

        public override ReturnValue getDownloadOrderList()
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getOrderDownNU"));
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }

        public override ReturnValue getOrderById(int id)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getOrderByIdNU"), id);
            ReturnValue _result = this.getEntity(Usp_SQL);
            return _result;
        }

        public ReturnValue getMailAddress(int customerId)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getMailAddress"), customerId);
            ReturnValue _result = this.getEntity(Usp_SQL);
            return _result;
        }
    }
}