using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
    [Serializable]
    [BindingClass("Order")]
    public class TOrderLenovo : TOrder
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


        public TOrderLenovo()
        {

        }

        public override ReturnValue getDownloadOrderList(int programID)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getOrderDownLenovo"));
            ReturnValue _result = this.getEntityList(Usp_SQL);
            return _result;
        }
    
    }
}