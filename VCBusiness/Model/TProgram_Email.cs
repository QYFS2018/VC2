using System;
using System.Data;
using WComm;


namespace VCBusiness.Model
{
    [Serializable]
    [BindingClass("Program_Email")]
    public class TProgram_Email : Entity
    {
        public TProgram_Email()
        {

        }



        #region Basic Property
        private int _programEmailId;
        private int _programId;
        private int _emailTypeId;
        protected string _fullText;
        private int _contentStatusId;
        private DateTime _updatedOn;
        protected string _subject;
        private string _respondTo;
        private string _bccAddress;
        protected string _fullHtml;
        private string _updatedBy;
        private string _ccAddress;
        private string _replyTo;
        private string _replyToLabel;
        private string _respondLabel;
        private int _orderTypeId;
        private string _toAddress;
        private string _respondToName;

        [BindingField("ProgramEmailId", true)]
        public int ProgramEmailId
        {
            set
            {
                _programEmailId = value;
            }
            get
            {
                return _programEmailId;
            }
        }
        [BindingField("ProgramId", true)]
        public int ProgramId
        {
            set
            {
                _programId = value;
            }
            get
            {
                return _programId;
            }
        }
        [BindingField("EmailTypeId", true)]
        public int EmailTypeId
        {
            set
            {
                _emailTypeId = value;
            }
            get
            {
                return _emailTypeId;
            }
        }
        [BindingField("FullText", true, true, true)]
        public virtual string FullText
        {
            set
            {
                _fullText = value;
            }
            get
            {
                return _fullText;
            }
        }
        [BindingField("ContentStatusId", true)]
        public int ContentStatusId
        {
            set
            {
                _contentStatusId = value;
            }
            get
            {
                return _contentStatusId;
            }
        }
        [BindingField("UpdatedOn", true)]
        public DateTime UpdatedOn
        {
            set
            {
                _updatedOn = value;
            }
            get
            {
                return _updatedOn;
            }
        }
        [BindingField("Subject", true, true, true)]
        public virtual string Subject
        {
            set
            {
                _subject = value;
            }
            get
            {
                return _subject;
            }
        }
        [BindingField("RespondTo", true)]
        public string RespondTo
        {
            set
            {
                _respondTo = value;
            }
            get
            {
                return _respondTo;
            }
        }
        [BindingField("BccAddress", true)]
        public string BccAddress
        {
            set
            {
                _bccAddress = value;
            }
            get
            {
                return _bccAddress;
            }
        }
        [BindingField("FullHtml", true, true, true)]
        public virtual string FullHtml
        {
            set
            {
                _fullHtml = value;
            }
            get
            {
                return _fullHtml;
            }
        }
        [BindingField("UpdatedBy", true)]
        public string UpdatedBy
        {
            set
            {
                _updatedBy = value;
            }
            get
            {
                return _updatedBy;
            }
        }
        [BindingField("CCAddress", true)]
        public string CCAddress
        {
            set
            {
                _ccAddress = value;
            }
            get
            {
                return _ccAddress;
            }
        }
        [BindingField("ReplyTo", true)]
        public string ReplyTo
        {
            set
            {
                _replyTo = value;
            }
            get
            {
                return _replyTo;
            }
        }
        [BindingField("RespondLabel", true)]
        public string RespondLabel
        {
            set
            {
                _respondLabel = value;
            }
            get
            {
                return _respondLabel;
            }
        }
        [BindingField("ReplyToLabel", true)]
        public string ReplyToLabel
        {
            set
            {
                _replyToLabel = value;
            }
            get
            {
                return _replyToLabel;
            }
        }
        [BindingField("OrderTypeId", true)]
        public int OrderTypeId
        {
            set
            {
                _orderTypeId = value;
            }
            get
            {
                return _orderTypeId;
            }
        }
        [BindingField("ToAddress", true)]
        public string ToAddress
        {
            set
            {
                _toAddress = value;
            }
            get
            {
                return _toAddress;
            }
        }
        [BindingField("RespondToName", true)]
        public string RespondToName
        {
            set
            {
                _respondToName = value;
            }
            get
            {
                return _respondToName;
            }
        }


        #endregion

        #region Extend Property


        #endregion

        public virtual ReturnValue getEmailTemplate(string code)
        {
            string Usp_SQL = String.Format(WComm.SqlDefine.getSQL("getEmailTemplate"), code);
            ReturnValue _result = this.getEntity(Usp_SQL);
            return _result;
        }


    }
}