using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WComm;
using System.Xml;
using System.Net;
using System.Collections;
using System.Web;


namespace VCBusiness
{
    public class Controler
    {
        public List<Owner> Owners;

        public Controler()
        {
            this.Owners = new List<Owner>();
        }

        public ReturnValue getControler()
        {
            ReturnValue _result = new ReturnValue();

            try
            {
                #region getXML
                string path = "Controler.xml";

                if (HttpContext.Current != null)
                {
                    path = HttpContext.Current.Server.MapPath(path);
                }

                System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("utf-8"));
                sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                string s = "";

                while (sr.Peek() > -1)
                {
                    s += sr.ReadLine();
                }

                sr.Close();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(s);
                #endregion

                #region fillInfo
                XmlNodeList _nl = doc.GetElementsByTagName("Owners").Item(0).SelectNodes("Owner");
                foreach (XmlNode _nd in _nl)
                {
                    Owner _owner = new Owner();
                    _owner.OwnerCode = _nd.Attributes["OwnerCode"].InnerXml;
                    _owner.RegSubKey = _nd.Attributes["RegSubKey"].InnerXml;
                    _owner.Name = _nd.Attributes["Name"].InnerXml;
                    _owner.Enable = Convert.ToBoolean(_nd.Attributes["Enable"].InnerXml);

                    #region ClassType

                    XmlNodeList _nlDictionary = _nd.SelectNodes("ClassType/Class");

                    foreach (XmlNode _ndColumn in _nlDictionary)
                    {
                        ClassType _classType = new ClassType();
                        _classType.ClassCode = _ndColumn.Attributes["ClassCode"].InnerXml;
                        _classType.Type = _ndColumn.Attributes["ClassType"].InnerXml;
                        _classType.AssemblyFile = _ndColumn.Attributes["AssemblyFile"].InnerXml;

                        _owner.ClassType.Add(_classType.ClassCode, _classType);
                    }

                    #endregion

                    #region OwnerInfo
                    _nlDictionary = _nd.SelectNodes("OwnerInfo/Info");

                    foreach (XmlNode _ndColumn in _nlDictionary)
                    {
                        _owner.OwnerInfo.Add(_ndColumn.Attributes["Key"].InnerXml, _ndColumn.InnerXml);
                    }


                    #endregion

                    #region Action
                    _nlDictionary = _nd.SelectNodes("Actions/Action");

                    foreach (XmlNode _ndColumn in _nlDictionary)
                    {
                        _owner.Actions.Add(_ndColumn.Attributes["Code"].InnerXml.ToUpper(), Convert.ToBoolean(_ndColumn.Attributes["Enable"].InnerXml));
                    }


                    #endregion

                    if (_owner.Enable == true)
                    {
                        this.Owners.Add(_owner);
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                _result.Success = false;
                _result.ErrMessage = ex.ToString();
            }

            return _result;
        }

        public int OwnerComparison(Owner a, Owner b)
        {
            return a.OwnerCode.CompareTo(b.OwnerCode);

        }
    }

    public class Owner
    {
        public string OwnerCode;
        public string RegSubKey;
        public string Name;
        public bool Enable;

        public Hashtable OwnerInfo;
        public Hashtable ClassType;
        public Hashtable Actions;

        public Owner()
        {
            this.ClassType = new Hashtable();
            this.OwnerInfo = new Hashtable();
            this.Actions = new Hashtable();
        }
    }

    public class ClassType
    {
        public string ClassCode;
        public string Type;
        public string AssemblyFile;

    }

    public class Action
    {
        public Action() { }

        public string Code;
        public bool Enable;
    }


}
