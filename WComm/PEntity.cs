using System;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Text;
using System.Web.Mail;
using System.Runtime.Serialization;
using System.Data.OleDb;
using System.IO;
using System.Xml;
using System.Web.Caching;
using System.Transactions;

namespace WComm
{
    [Serializable]
    public class PEntity : Entity
    {


        public PEntity()
        {

        }

        public virtual PEntity BindDataToNEntity(DataRow dr)
        {
            return new PEntity();
        }

        public override ReturnValue getEntity(string sql)
        {
            PGateway _gateway = new PGateway(this);
            ReturnValue result = _gateway.getEntity(sql);
            //SaveLog(result, _gateway.TableName, "getEntity");

            return result;
        }
        public override ReturnValue getEntity(string sql, Transaction trans)
        {
            PGateway _gateway = new PGateway(this, trans);
            ReturnValue result = _gateway.getEntity(sql);


            return result;
        }
        public override ReturnValue getEntityList(string sql)
        {
            PGateway _gateway = new PGateway(this);
            ReturnValue result = _gateway.getEntityList(sql);

            return result;
        }
        public override ReturnValue getEntityList(string sql, Transaction trans)
        {
            PGateway _gateway = new PGateway(this, trans);
            ReturnValue result = _gateway.getEntityList(sql);

            return result;
        }



        public override ReturnValue ExecSql(string sql)
        {
            ReturnValue _result = new ReturnValue();

            PGateway _gateway = new PGateway(this);

            _result = _gateway.ExecSql(sql);



            return _result;
        }
        public override ReturnValue ExecSql(string sql, Transaction trans)
        {

            ReturnValue _result = new ReturnValue();


            PGateway _gateway = new PGateway(this, trans);
            _result = _gateway.ExecSql(sql);

            if (_result.Success == false)
            {
                if (trans != null)
                {
                    trans.RollbackTransaction();
                }
                return _result;
            }



            return _result;
        }

        public override ReturnValue Save()
        {
            ReturnValue _result = new ReturnValue();

            PGateway _gateway = new PGateway(this);

            _result = _gateway.Save();


            return _result;

        }
        public override ReturnValue Save(Transaction trans)
        {

            ReturnValue _result = new ReturnValue();


            PGateway _gateway = new PGateway(this, trans);
            _result = _gateway.Save();

            if (_result.Success == false)
            {
                if (trans != null)
                {
                    trans.RollbackTransaction();
                }
                return _result;
            }



            return _result;
        }
        public override ReturnValue Update()
        {
            ReturnValue _result = new ReturnValue();

            PGateway _gateway = new PGateway(this);

            _result = _gateway.Update();

            return _result;
        }
        public override ReturnValue Update(Transaction trans)
        {

            ReturnValue _result = new ReturnValue();


            PGateway _gateway = new PGateway(this, trans);
            _result = _gateway.Update();

            if (_result.Success == false)
            {
                if (trans != null)
                {
                    trans.RollbackTransaction();
                }
                return _result;
            }



            return _result;
        }
        public override ReturnValue Delete()
        {


            ReturnValue _result = new ReturnValue();

            PGateway _gateway = new PGateway(this);

            _result = _gateway.Delete();
           

            return _result;
        }
        public override ReturnValue Delete(Transaction trans)
        {

            ReturnValue _result = new ReturnValue();


            PGateway _gateway = new PGateway(this, trans);
            _result = _gateway.Delete();

            if (_result.Success == false)
            {
                if (trans != null)
                {
                    trans.RollbackTransaction();
                }
                return _result;
            }



            return _result;
        }


        public string processMutiLanguageXmlContent(string strValue)
        {
            return "N'" + Process.GenerateContentXML(Utilities.CurrentLangCode, "", strValue == null ? "" : strValue) + "'";
        }

        public string GetContentFromXML(string strXML)
        {
            return Process.GetContentFromXML(Utilities.CurrentLangCode, strXML);
        }

        public void processMutiLanguageXmlContent(string tableName, string condition, ref System.Collections.Hashtable htFields)
        {
            string fieldName = "";
            foreach (string key in htFields.Keys)
            {
                fieldName = fieldName + key + ",";
            }

            fieldName = fieldName.Remove(fieldName.Length - 1);

            string _sql = String.Format("Select Top 1 {0} From [{1}] with (nolock) Where {2}", fieldName, tableName, condition);

            ReturnValue _reVal = this.getDataTable(_sql);

            DataTable dt = _reVal.ObjectValue as DataTable;

            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dr.Table.Columns)
                {
                    string key = dc.ColumnName;
                    if (!htFields.ContainsKey(key))
                    {
                        key = "[" + key + "]";
                    }
                    htFields[key] = Process.GenerateContentXML(Utilities.CurrentLangCode, dr[dc.ColumnName].ToString(), htFields[key] == null ? "" : htFields[key].ToString());
                    htFields[key] = "N'" + htFields[key] + "'";
                }
            }

        }

    }
}
