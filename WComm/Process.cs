using System;
using System.Collections;
using System.Xml;
using System.Reflection;
using System.Web;
using System.Text;
using System.Xml.XPath;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WComm
{
  /// <summary>
  ///		Process Utilitlis.
  /// </summary>
  public sealed class Process
  {
    private static XmlHelper _Helper;
    private static XmlHelper Helper
    {
      get
      {
        if (_Helper == null)
        {
          _Helper = XmlHelper.GetInstance();
        }
        return _Helper;
      }
    }
    /// <summary>
    ///		Constructor.
    /// </summary>
    private Process() { }

    public static string Serialize(object obj)
    {
      if (obj == null) return "";

      XmlDocument doc = new XmlDocument();
      doc.LoadXml("<WCommControl.State></WCommControl.State>");

      Type objType = obj.GetType();
      PropertyInfo[] objPropertiesArray =
          objType.GetProperties();

      foreach (PropertyInfo objProperty in objPropertiesArray)
      {
        foreach (BindingFieldAttribute attr in objProperty.GetCustomAttributes(typeof(BindingFieldAttribute), false))
        {
          if (attr.IsSimpleObject == true)
          {
            XmlElement newElem = doc.CreateElement(objProperty.Name);
            newElem.SetAttribute("Type", objProperty.PropertyType.FullName);
            if (objProperty.GetValue(obj, null) != null)
            {
              newElem.InnerText = objProperty.GetValue(obj, null).ToString().Replace("<", "*ILT*").Replace(">", "*IRT*").Replace("\\", "*IFT*").Replace("&", "*AND*"); ;
            }
            else
            {
              newElem.InnerText = "null";
            }
            doc.DocumentElement.AppendChild(newElem);
          }
          else
          {
            object _innerobject = objProperty.GetValue(obj, null);
            if (_innerobject != null)
            {
              XmlElement newElem = doc.CreateElement(objProperty.Name);
              newElem.SetAttribute("Type", objProperty.PropertyType.FullName);

              PropertyInfo[] innerObjPropertiesArray =
                  _innerobject.GetType().GetProperties();
              foreach (PropertyInfo innerObjProperty in innerObjPropertiesArray)
              {
                foreach (BindingFieldAttribute innerAttr in innerObjProperty.GetCustomAttributes(typeof(BindingFieldAttribute), false))
                {
                  if (innerAttr.IsSimpleObject == true)
                  {
                    XmlElement innernewElem = doc.CreateElement(innerObjProperty.Name);
                    innernewElem.SetAttribute("Type", innerObjProperty.PropertyType.FullName);
                    if (innerObjProperty.GetValue(_innerobject, null) != null)
                    {
                      innernewElem.InnerText = innerObjProperty.GetValue(_innerobject, null).ToString().Replace("<", "*ILT*").Replace(">", "*IRT*").Replace("\\", "*IFT*").Replace("&", "*AND*"); ;
                    }
                    else
                    {
                      innernewElem.InnerText = "null";
                    }

                    newElem.AppendChild(innernewElem);
                  }
                }
              }
              doc.DocumentElement.AppendChild(newElem);
            }
          }
        }
      }
      return doc.InnerXml;
    }

    public static string Serialize(ArrayList list)
    {
      if (list == null) return "";

      XmlDocument doc = new XmlDocument();
      doc.LoadXml("<WCommControl.State></WCommControl.State>");

      System.Collections.IEnumerator _elEnumerator = list.GetEnumerator();
      while (_elEnumerator.MoveNext())
      {
        object obj = (object)_elEnumerator.Current;
        XmlElement newElem = doc.CreateElement("ListItem");

        Type objType = obj.GetType();
        PropertyInfo[] objPropertiesArray =
            objType.GetProperties();

        foreach (PropertyInfo objProperty in objPropertiesArray)
        {
          foreach (BindingFieldAttribute attr in objProperty.GetCustomAttributes(typeof(BindingFieldAttribute), false))
          {
            if (attr.IsSimpleObject == true)
            {
              XmlElement itemnewElem = doc.CreateElement(objProperty.Name);
              itemnewElem.SetAttribute("Type", objProperty.PropertyType.FullName);
              if (objProperty.GetValue(obj, null) != null)
              {
                itemnewElem.InnerText = objProperty.GetValue(obj, null).ToString().Replace("<", "*ILT*").Replace(">", "*IRT*").Replace("\\", "*IFT*").Replace("&", "*AND*"); ;
              }
              newElem.AppendChild(itemnewElem);
            }
            else
            {
              object _innerobject = objProperty.GetValue(obj, null);
              if (_innerobject != null)
              {
                XmlElement itemnewElem = doc.CreateElement(objProperty.Name);
                itemnewElem.SetAttribute("Type", objProperty.PropertyType.FullName);

                PropertyInfo[] innerObjPropertiesArray =
                    _innerobject.GetType().GetProperties();
                foreach (PropertyInfo innerObjProperty in innerObjPropertiesArray)
                {
                  foreach (BindingFieldAttribute innerAttr in innerObjProperty.GetCustomAttributes(typeof(BindingFieldAttribute), false))
                  {
                    if (innerAttr.IsSimpleObject == true)
                    {
                      XmlElement inneritemnewElem = doc.CreateElement(innerObjProperty.Name);
                      inneritemnewElem.SetAttribute("Type", innerObjProperty.PropertyType.FullName);
                      if (innerObjProperty.GetValue(_innerobject, null) != null)
                      {
                        inneritemnewElem.InnerText = innerObjProperty.GetValue(_innerobject, null).ToString().Replace("<", "*ILT*").Replace(">", "*IRT*").Replace("\\", "*IFT*").Replace("&", "*AND*"); ;
                      }
                      itemnewElem.AppendChild(inneritemnewElem);
                    }
                  }
                }
                newElem.AppendChild(itemnewElem);
              }
            }
          }
        }
        doc.DocumentElement.AppendChild(newElem);
      }

      return doc.InnerXml;
    }


    public static object Deserialize(Type type, string value)
    {
      PropertyInfo[] objPropertiesArray =
          type.GetProperties();

      object _result = Activator.CreateInstance(type, false);
      XmlDocument doc = new XmlDocument();

      doc.LoadXml(value);

      XmlNodeList _nl;
      _nl = doc.GetElementsByTagName("WCommControl.State").Item(0).ChildNodes;
      foreach (XmlNode _xn in _nl)
      {
        string _fieldName = _xn.Name.ToString();
        string _fieldValue = _xn.InnerXml.ToString().Replace("*ILT*", "<").Replace("*IRT*", ">").Replace("*IFT*", "\\").Replace("*AND*", "&"); ;
        string _fieldType = _xn.Attributes["Type"].InnerXml.ToString();

        foreach (PropertyInfo objProperty in objPropertiesArray)
        {
          if (objProperty.Name == _fieldName)
          {
            foreach (BindingFieldAttribute attr in objProperty.GetCustomAttributes(typeof(BindingFieldAttribute), false))
            {
              if (attr.IsSimpleObject == true)
              {
                SetPropertyValue(_result, objProperty, _fieldValue);
              }
              else
              {
                Type _innerType = objProperty.PropertyType;
                object _innerObject = Activator.CreateInstance(_innerType, false);
                XmlNodeList _innernl;
                _innernl = doc.GetElementsByTagName("WCommControl.State").Item(0).SelectNodes(_fieldName).Item(0).ChildNodes;

                PropertyInfo[] innerobjPropertiesArray = _innerType.GetProperties();

                foreach (XmlNode _innerxn in _innernl)
                {
                  string _innerfieldName = _innerxn.Name.ToString();
                  string _innerfieldValue = _innerxn.InnerXml.ToString().Replace("*ILT*", "<").Replace("*IRT*", ">").Replace("*IFT*", "\\").Replace("*AND*", "&"); ;
                  string _innerfieldType = _innerxn.Attributes["Type"].InnerXml.ToString();

                  foreach (PropertyInfo innerobjProperty in innerobjPropertiesArray)
                  {
                    if (innerobjProperty.Name == _innerfieldName)
                    {
                      foreach (BindingFieldAttribute innerattr in innerobjProperty.GetCustomAttributes(typeof(BindingFieldAttribute), false))
                      {
                        if (innerattr.IsSimpleObject == true)
                        {
                          SetPropertyValue(_innerObject, innerobjProperty, _innerfieldValue);
                        }
                      }
                    }
                  }
                }
                objProperty.SetValue(_result, _innerObject, null);
              }
            }
          }
        }
      }

      return _result;
    }

    public static object Deserialize(Type type, Type ListType, string value)
    {
      ArrayList _datalist = (ArrayList)Activator.CreateInstance(ListType, false);

      XmlDocument doc = new XmlDocument();
      doc.LoadXml(value);

      XmlNodeList _nl;
      _nl = doc.GetElementsByTagName("WCommControl.State").Item(0).ChildNodes;

      foreach (XmlNode _xn in _nl)
      {
        PropertyInfo[] objPropertiesArray =
            type.GetProperties();

        object _result = Activator.CreateInstance(type, false);

        foreach (XmlNode _cxn in _xn.ChildNodes)
        {
          string _fieldName = _cxn.Name.ToString();
          string _fieldValue = _cxn.InnerXml.ToString().Replace("*ILT*", "<").Replace("*IRT*", ">").Replace("*IFT*", "\\").Replace("*AND*", "&"); ;
          string _fieldType = _cxn.Attributes["Type"].InnerXml.ToString();

          foreach (PropertyInfo objProperty in objPropertiesArray)
          {
            if (objProperty.Name == _fieldName)
            {
              foreach (BindingFieldAttribute attr in objProperty.GetCustomAttributes(typeof(BindingFieldAttribute), false))
              {
                if (attr.IsSimpleObject == true)
                {
                  SetPropertyValue(_result, objProperty, _fieldValue);
                }
                else
                {
                  Type _innerType = objProperty.PropertyType;
                  object _innerObject = Activator.CreateInstance(_innerType, false);
                  XmlNodeList _innernl;
                  _innernl = _cxn.ChildNodes;

                  PropertyInfo[] innerobjPropertiesArray = _innerType.GetProperties();

                  foreach (XmlNode _innerxn in _innernl)
                  {
                    string _innerfieldName = _innerxn.Name.ToString();
                    string _innerfieldValue = _innerxn.InnerXml.ToString().Replace("*ILT*", "<").Replace("*IRT*", ">").Replace("*IFT*", "\\").Replace("*AND*", "&"); ;
                    string _innerfieldType = _innerxn.Attributes["Type"].InnerXml.ToString();

                    foreach (PropertyInfo innerobjProperty in innerobjPropertiesArray)
                    {
                      if (innerobjProperty.Name == _innerfieldName)
                      {
                        foreach (BindingFieldAttribute innerattr in innerobjProperty.GetCustomAttributes(typeof(BindingFieldAttribute), false))
                        {
                          if (innerattr.IsSimpleObject == true)
                          {
                            SetPropertyValue(_innerObject, innerobjProperty, _innerfieldValue);
                          }
                        }
                      }
                    }
                  }
                  objProperty.SetValue(_result, _innerObject, null);
                }
              }
            }
          }
        }
        _datalist.Add(_result);
      }
      return _datalist;
    }


    public static void BindObjectToControls(object obj, Object container)
    {
      if (obj == null) return;
      Type objType = obj.GetType();
      PropertyInfo[] objPropertiesArray =
          objType.GetProperties();

      Type containerType = container.GetType();
      PropertyInfo[] containerPropertiesArray =
          containerType.GetProperties();

      foreach (PropertyInfo objProperty in objPropertiesArray)
      {
        foreach (PropertyInfo containerProperty in containerPropertiesArray)
        {
          if (objProperty.Name == containerProperty.Name)
          {
            if (containerProperty.PropertyType == typeof(string))
            {
              if (objProperty.GetValue(obj, null) == null)
              {
                containerProperty.SetValue(container, "", null);
              }
              else
              {
                containerProperty.SetValue(container, HttpContext.Current.Server.HtmlDecode(objProperty.GetValue(obj, null).ToString()), null);
              }
            }
            else
            {
              containerProperty.SetValue(container, objProperty.GetValue(obj, null), null);
            }
          }
        }
      }

    }


    public static void BindControlsToObject(object obj, Object container)
    {
      if (obj == null) return;
      Type objType = obj.GetType();
      PropertyInfo[] objPropertiesArray =
          objType.GetProperties();

      Type containerType = container.GetType();
      PropertyInfo[] containerPropertiesArray =
          containerType.GetProperties();

      foreach (PropertyInfo objProperty in objPropertiesArray)
      {
        foreach (PropertyInfo containerProperty in containerPropertiesArray)
        {
          if (objProperty.Name == containerProperty.Name)
          {
            if (objProperty.PropertyType != typeof(string))
            {
              objProperty.SetValue(obj, containerProperty.GetValue(container, null), null);
            }
            else
            {
              objProperty.SetValue(obj, containerProperty.GetValue(container, null).ToString().Trim(), null);
            }
          }

        }
      }

    }


    public static string GenerateSQLParameters(object obj)
    {
      StringBuilder sb = new StringBuilder();


      PropertyInfo[] objectPropertiesArray =
         obj.GetType().GetProperties();

      foreach (PropertyInfo objProperty in objectPropertiesArray)
      {
        Type _originalType;
        if (objProperty.PropertyType.IsGenericType)
        {
          Type[] _originalTypes = objProperty.PropertyType.GetGenericArguments();
          _originalType = _originalTypes[0];
        }
        else
        {
          _originalType = objProperty.PropertyType;
        }

        foreach (BindingFieldAttribute attr in objProperty.GetCustomAttributes(typeof(BindingFieldAttribute), false))
        {
          if (attr.IsPersistence == true && attr.IsSimpleObject == true)
          {
            if (_originalType == typeof(string))
            {
              if (objProperty.GetValue(obj, null) != null)
              {

                bool _findIsNVarchar = false;
                foreach (NVarcharAttribute attr0 in objProperty.GetCustomAttributes(typeof(NVarcharAttribute), false))
                {
                  if (attr0.IsNVarchar == true)
                  {
                    _findIsNVarchar = true;
                    break;
                  }
                }
                if (_findIsNVarchar == true)
                {
                  sb.Append("N'" + objProperty.GetValue(obj, null).ToString().Replace("'", "''") + "',");
                }
                else
                {
                  sb.Append("'" + objProperty.GetValue(obj, null).ToString().Replace("'", "''") + "',");
                }
              }
              else
                sb.Append("null,");
            }
            else if (_originalType == typeof(Guid))
            {
              if (objProperty.GetValue(obj, null) != null && objProperty.GetValue(obj, null).ToString() != "")
                sb.Append("'" + objProperty.GetValue(obj, null).ToString().Replace("'", "''") + "',");
              else
                sb.Append("null,");

            }
            else if (_originalType == typeof(bool))
            {
              bool? _nullBool = (bool?)objProperty.GetValue(obj, null);
              if (_nullBool == null)
              {
                sb.Append("null,");
              }
              else
              {
                if ((bool)objProperty.GetValue(obj, null) == true)
                  sb.Append("1,");
                else
                  sb.Append("0,");
              }
            }
            else if (_originalType == typeof(DateTime))
            {
              DateTime? _nullDateTime = (DateTime?)objProperty.GetValue(obj, null);
              if (_nullDateTime == null)
              {
                sb.Append("null,");
              }
              else
              {
                DateTime _notnullDateTime = (DateTime)objProperty.GetValue(obj, null);

                if (_notnullDateTime.Year == 1 & _notnullDateTime.Month == 1 & _notnullDateTime.Day == 1)
                {
                  sb.Append("null,");
                }

                else
                {
                  sb.Append("'" + objProperty.GetValue(obj, null).ToString() + "',");
                }
              }
            }
            else if (_originalType == typeof(int))
            {
              if ((int?)objProperty.GetValue(obj, null) != null)
              {
                sb.Append(objProperty.GetValue(obj, null).ToString() + ",");
              }
              else
              {
                sb.Append("null,");
              }
            }
            else
            {
              sb.Append(objProperty.GetValue(obj, null).ToString() + ",");
            }
          }

        }
      }

      string _result = sb.ToString();
      if (_result.IndexOf(",") > 0) _result = _result.Remove(_result.Length - 1, 1);

      return _result;
    }


    public static void SetPropertyValue(object o, PropertyInfo objProperty, string value)
    {
      Type _originalType;
      if (objProperty.PropertyType.IsGenericType)
      {
        Type[] _originalTypes = objProperty.PropertyType.GetGenericArguments();
        _originalType = _originalTypes[0];
      }
      else
      {
        _originalType = objProperty.PropertyType;
      }
      //if (value.Trim().ToUpper() != "NULL")
      if (value!=null && string.Compare(value.Trim(), "NULL", true) != 0)
      {
        if (objProperty.PropertyType == typeof(Guid))
        {
          objProperty.SetValue(o, new Guid(value), null);
        }
        else
        {
          objProperty.SetValue(o, Convert.ChangeType(value.Trim(), _originalType), null);
        }
      }
      else
      {
        objProperty.SetValue(o, null, null);
      }
    }


    //public static void FillXMLField(object obj, string code,string skincode)
    //{
    //    //sArrayList attrList = new ArrayList();


    //    PropertyInfo[] objectPropertiesArray =
    //       obj.GetType().GetProperties();

    //    foreach (PropertyInfo objProperty in objectPropertiesArray)
    //    {
    //        foreach (XMLBindingFieldAttribute attr in objProperty.GetCustomAttributes(typeof(XMLBindingFieldAttribute), false))
    //        {
    //            PropertyInfo sourcePropertyInfo = getPropertyInfoByName(obj, attr.Xmlfield);
    //            string source = "";
    //            if (sourcePropertyInfo.GetValue(obj, null) != null)
    //            {
    //                source = sourcePropertyInfo.GetValue(obj, null).ToString();
    //            }
    //            string xml = getXML(code, skincode, source, attr.SearchPath, attr.SearchOn);
    //            SetPropertyValue(obj, objProperty, xml);
    //        }
    //    }



    //    return;
    //}
    //public static void FillXMLField(ArrayList list, string code,string skincode)
    //{
    //    foreach (object obj in list)
    //    {
    //        FillXMLField(obj, code, skincode);
    //    }

    //    return;
    //}

    public static string GetContentFromXML(string langCode,string xml)
    {
        if (xml == null)
        {
            return null;
        }

        XmlDocument _doc = new XmlDocument();
        try
        {
            _doc.LoadXml(xml);
        }
        catch (System.Xml.XmlException ex)
        {

        }

        XmlNodeList _nodeList = _doc.SelectNodes("Languages/Language");
        XmlNode _node = _doc.SelectSingleNode("//Language[@code='" + langCode + "']");
        if (_node != null)
        {
            return _node.InnerXml;
        }

        string defaultLanguageCode = ConfigurationManager.AppSettings["DefaultLanguageCode"];

        _node = _doc.SelectSingleNode("//Language[@code='" + defaultLanguageCode + "']");
        if (_node != null)
        {
            return _node.InnerXml;
        }

        return "";
    }

    public static string GetContentFromXML(string langCode, string skinCode, string xml)
    {
      //try
      //{
      //    xml = xml.Insert(0, "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
      //    StringReader sr = new StringReader(xml);
      //    XPathDocument _doc = new XPathDocument(sr);
      //    XPathNavigator _navi = _doc.CreateNavigator();
      //    XPathNodeIterator _iter = _navi.Select("//Language[@code='" + langCode + "'][@skincode='" + skinCode + "']");
      //    while (_iter.MoveNext())
      //    {
      //        sr.Close();
      //        return _iter.Current.InnerXml;
      //    }

      //    _iter = _navi.Select("//Language[@code='" + ConfigurationManager.AppSettings["DefaultLanguageCode"] + "'][@skincode='" + ConfigurationManager.AppSettings["DefaultSkinCode"] + "']");
      //    while (_iter.MoveNext())
      //    {
      //        sr.Close();
      //        return _iter.Current.InnerXml;
      //    }

      //    _iter = _navi.Select("//Language[@code='" + langCode + "']");
      //    while (_iter.MoveNext())
      //    {
      //        sr.Close();
      //        return _iter.Current.InnerXml;
      //    }
      //}
      //catch(Exception ex) 
      //{

      //}
      //return string.Empty;

      string value = Helper.GetContent(langCode, skinCode, xml);

      if (HttpContext.Current != null)
      {
          value = HttpContext.Current.Server.HtmlDecode(value);
      }

      value = HttpUtility.HtmlDecode(value);

      return value;
    }

    public static string GenerateContentXML(string langCode, string skinCode, string soucrceXml, string xml)
    {
        return GenerateContentXML(langCode, soucrceXml, xml);
    }

    public static string GenerateContentXML(string langCode, string soucrceXml, string xml)
    {

        string _result = "";

        XmlDocument _doc = new XmlDocument();
        try
        {
            _doc.LoadXml(soucrceXml);
        }
        catch (System.Xml.XmlException ex)
        {

        }

        XmlNodeList _nodeList = _doc.SelectNodes("Languages/Language");
        XmlNode _node = _doc.SelectSingleNode("//Language[@code='" + langCode + "']");
        if (_node != null)
        {
            _node.InnerXml = xml;
            return _doc.InnerXml.ToString().Replace("'", "''");
        }

        if (_nodeList.Count == 0)
        {

            _doc.LoadXml("<Languages></Languages>");
        }

        XmlNode _root = _doc.DocumentElement;

        //languageCodes
        ICollection<string> languageCodes = null;


        string codesExpression = ConfigurationManager.AppSettings["LanguageCodes"];

        string defaultLanguageCode = ConfigurationManager.AppSettings["DefaultLanguageCode"];




        if (string.IsNullOrEmpty(codesExpression))
            languageCodes = new string[] { langCode };
        else
            languageCodes = codesExpression.Split(',') as ICollection<string>;

        if (!languageCodes.Contains(langCode))
            languageCodes.Add(langCode);


        foreach (string languageCode in languageCodes)
        {
            _node = _doc.SelectSingleNode("//Language[@code='" + languageCode + "']");
            if (_node == null)
            {
                XmlElement elem = _doc.CreateElement("Language");
                elem.InnerXml = xml;

                XmlAttribute LangAttr = _doc.CreateAttribute("code");
                LangAttr.Value = languageCode;
                elem.SetAttributeNode(LangAttr);

                _root.AppendChild(elem);
            }
        }

        return _doc.InnerXml.ToString().Replace("'", "''");
    }

    private static string getXML(string code, string skincode, string xml, string searchPath, string searchOn)
    {
      string _result = "";

      string _defaultCode = System.Configuration.ConfigurationManager.AppSettings["DefaultLanguageCode"];

      XmlDocument _doc = new XmlDocument();
      try
      {
        _doc.LoadXml(xml);
      }
      catch { }

      XmlNodeList _nodeList = _doc.SelectNodes(searchPath);

      foreach (XmlNode _item in _nodeList)
      {
        string ll = _item.Attributes[searchOn].InnerXml.ToString().ToUpper();
        if (ll == code.ToUpper())
        {
          _result = _item.InnerXml;
          break;
        }
      }

      if (_result == "")
      {

        foreach (XmlNode _item in _nodeList)
        {
          string ll = _item.Attributes[searchOn].InnerXml.ToString().ToUpper();
          if (ll == _defaultCode.ToUpper())
          {
            _result = _item.InnerXml;
            break;
          }
        }
      }


      return _result;
    }
    private static PropertyInfo getPropertyInfoByName(object obj, string propertyInfoName)
    {
      PropertyInfo _result = null;

      PropertyInfo[] objectPropertiesArray =
      obj.GetType().GetProperties();

      foreach (PropertyInfo objProperty in objectPropertiesArray)
      {
        if (objProperty.Name.ToUpper() == propertyInfoName.ToUpper())
        {
          _result = objProperty;
          break;
        }

      }

      return _result;
    }

  }
}
