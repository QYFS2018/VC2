using System;
using System.Collections;
using System.Data.OleDb;
using System.Data;
using System.Web;
using System.Configuration;
using System.IO;

namespace WComm
{
    [Serializable]
    [BindingClass("App_Page_Log")]
    public class App_Page_Log : Entity
    {
        public App_Page_Log()
        {

        }



        #region Basic Property
        private int _iD;
        private DateTime _createdOn;
        private string _sQLText;
        private int _pageId;
        private string _oldlayout;
        private string _newlayout;
        private bool _success;
        private int _programId;
        private string _type;
        private string _tableName;
        private string _sQLType;
        private string _server;
        private int _userId;

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
        [BindingField("PageId", true)]
        public int PageId
        {
            set
            {
                _pageId = value;
            }
            get
            {
                return _pageId;
            }
        }
        [BindingField("OldLayout", true)]
        public string OldLayout
        {
            set
            {
                _oldlayout = value;
            }
            get
            {
                return _oldlayout;
            }
        }
        [BindingField("NewLayout", true)]
        public string NewLayout
        {
            set
            {
                _newlayout = value;
            }
            get
            {
                return _newlayout;
            }
        }
        [BindingField("Success", true)]
        public bool Success
        {
            set
            {
                _success = value;
            }
            get
            {
                return _success;
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
        [BindingField("Type", true)]
        public string Type
        {
            set
            {
                _type = value;
            }
            get
            {
                return _type;
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
        [BindingField("Server", true)]
        public string Server
        {
            set
            {
                _server = value;
            }
            get
            {
                return _server;
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

        #endregion

        #region Extend Property

        #endregion


        
    }
}