using System;
using System.Collections;
using System.Data.OleDb;
using System.Data;


namespace WComm
{
    [Serializable]
    [BindingClass("App_Admin_Log")]
    public class App_Admin_Log : Entity
    {
        public App_Admin_Log()    
        {

        } 


        #region Basic Property
        private int _iD;
        private DateTime _createdOn;
        private string _sQLText;
        private int _programId;
        private string _tableName;
        private string _sQLType;
        private string _uRL;
        private string _sessionId;
        private int _userId;
        private string _iPAddress;

        [BindingField("ID", true)]
        public int ID
        {
            set
            {
                _iD = value;
            }
            get
            {
                return _iD;
            }
        }
        [BindingField("CreatedOn", true)]
        public DateTime CreatedOn
        {
            set
            {
                _createdOn = value;
            }
            get
            {
                return _createdOn;
            }
        }
        [BindingField("SQLText", true)]
        public string SQLText
        {
            set
            {
                _sQLText = value;
            }
            get
            {
                return _sQLText;
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
        [BindingField("TableName", true)]
        public string TableName
        {
            set
            {
                _tableName = value;
            }
            get
            {
                return _tableName;
            }
        }
        [BindingField("SQLType", true)]
        public string SQLType
        {
            set
            {
                _sQLType = value;
            }
            get
            {
                return _sQLType;
            }
        }
        [BindingField("URL", true)]
        public string URL
        {
            set
            {
                _uRL = value;
            }
            get
            {
                return _uRL;
            }
        }
        [BindingField("SessionId", true)]
        public string SessionId
        {
            set
            {
                _sessionId = value;
            }
            get
            {
                return _sessionId;
            }
        }
        [BindingField("UserId", true)]
        public int UserId
        {
            set
            {
                _userId = value;
            }
            get
            {
                return _userId;
            }
        }
        [BindingField("IPAddress", true)]
        public string IPAddress
        {
            set
            {
                _iPAddress = value;
            }
            get
            {
                return _iPAddress;
            }
        }
        #endregion

        #region Extend Property

        #endregion
    }
}