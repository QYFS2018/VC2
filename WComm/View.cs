using System;
using System.Collections.Generic;
using System.Xml;
using System.Web;

namespace WComm
{
	/// <summary>
	/// Summary description for View.
	/// </summary>
	internal class View 
	{
		private string _name;
		private string _type;
		private string _group;

		private View _parentView;
        private List<View> _childrenViews;

		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name=value;
			}
		}
		public string Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type=value;
			}
		}
		public string Group
		{
			get
			{
				return _group;
			}
			set
			{
				_group=value;
			}
		}


		public View ParentView
		{
			get
			{
				return _parentView;
			}
			set
			{
				_parentView=value;
			}
		}
        public List<View> ChildrenViews
		{
			get
			{
				return _childrenViews;
			}
			set
			{
				_childrenViews=value;
			}
		}


        public static List<View> getViewList(string pageName)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load (HttpContext.Current.Server.MapPath("View.Config"));

            List<View> _arraylist = new List<View>();

			//get current page
			XmlNodeList _pagenl=doc.GetElementsByTagName ("WComm.Core").Item(0).SelectNodes("State").Item(0).ChildNodes ;
			XmlNode _pagend=null;
			foreach (XmlNode _xn in _pagenl)
			{
				if (_xn.Attributes ["name"].InnerXml.ToUpper() ==pageName.ToUpper())
				{
					_pagend=_xn;
					break;
				}
			}

			//search view in this page
			if(_pagend!=null)
			{
				XmlNodeList _nl=_pagend.SelectNodes ("Views").Item(0).ChildNodes ;

				foreach (XmlNode _xn in _nl)
				{
					View _view= new View ();
					_view.Name =_xn.Attributes ["name"].InnerXml.ToString ();
					_view.Type =_xn.Attributes ["type"].InnerXml.ToString ();
                    try
                    {
                        _view.Group = _xn.Attributes["group"].InnerXml.ToString();
                    }
                    catch
                    {
                        _view.Group = "1";
                    }
					_view.ParentView =null;

					if (_xn.HasChildNodes )
					{
                        List<View> _carraylist = new List<View>();
						foreach (XmlNode _cxn in _xn.ChildNodes)
						{
							View _cview= new View ();
							_cview.Name =_cxn.Attributes ["name"].InnerXml.ToString ();
							_cview.Type =_cxn.Attributes ["type"].InnerXml.ToString ();
                            try
                            {
                                _cview.Group = _cxn.Attributes["group"].InnerXml.ToString();
                            }
                            catch
                            {
                                _cview.Group = "1";
                            }
							_cview.ParentView =_view;
							_cview.ChildrenViews =null;
							_carraylist.Add (_cview);
						}
						_view.ChildrenViews =_carraylist;
					}
					_arraylist.Add (_view);
				}
			}
			return _arraylist;
		}

        public static View getViewByName(string name, List<View> viewlist)
		{
			View _result=null;
			foreach (View _view in viewlist)
			{
                if (_view.Name.Trim().ToUpper() == name.Trim().ToUpper())
				{
					_result=_view;
					break;
				}
				else
				{
					if (_view.ChildrenViews !=null)
					{
						_result=getViewByName(name,_view.ChildrenViews );
						if (_result!=null)
							break;
					}
				}
			}
			return _result;
		}
	}
}
