using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Reflection;
using System.Web;

namespace WComm
{
    /// <summary>
    ///	Easily to use Cookiee to keep state for base page or base user control.
    ///	The difference of cookiee is this will save less things.
    ///	it can only save string object.
    /// </summary>
    public class State
    {
        private System.Web.UI.Page _page;
        /// <summary>
        ///	The page for this state.
        /// </summary>
        public System.Web.UI.Page Page
        {
            get
            {
                return _page;
            }
            set
            {
                _page = value;
            }
        }



        /// <summary>
        ///	Constructor method.
        /// </summary>
        public State()
        {

        }
        /// <summary>
        ///	Constructor method.
        /// </summary>
        /// <param name="page">The page for this state.</param>
        public State(System.Web.UI.Page page)
        {
            this.Page = page;
        }


        /// <summary>
        ///	the method to save state.
        ///	<code>
        ///	State.saveState("ProductId","21");
        ///	</code>
        /// </summary>
        /// <param name="key">the key string for save.</param>
        /// <param name="value">the value to save.</param>
        public static void saveState(string key, string value)
        {
            string _value = Utilities.FransferChar(value);

            HttpCookie MyCookie;
            if (HttpContext.Current.Request.Cookies["WComm.Core.State"] != null)
            {
                MyCookie = HttpContext.Current.Request.Cookies["WComm.Core.State"];

                MyCookie[key] = HttpContext.Current.Server .HtmlEncode (_value);
                HttpContext.Current.Response.Cookies.Add  (MyCookie);

            }
            else
            {
                MyCookie = new HttpCookie("WComm.Core.State");

                MyCookie[key] = HttpContext.Current.Server.HtmlEncode(_value);
                HttpContext.Current.Response.Cookies.Add(MyCookie);
            }
            HttpContext.Current.Items[key] = _value;
        }

        /// <summary>
        ///	the method to get state.
        ///	<code>
        ///	string ProductId = State.getState("ProductId");
        ///	</code>
        /// </summary>
        /// <param name="key">the key string to get.</param>
        /// <returns>the value to get.</returns>
        public static string getState(string key)
        {
            if (HttpContext.Current.Items[key] != null)
            {
                return Utilities.UnFransferChar(HttpContext.Current.Items[key].ToString());

            }
            HttpCookie MyCookie;

            if (HttpContext.Current.Request.Cookies["WComm.Core.State"] != null)
            {
                MyCookie = HttpContext.Current.Request.Cookies["WComm.Core.State"];
                if (MyCookie[key] != null)
                {
                    string _value = HttpContext.Current.Server.HtmlDecode ( MyCookie[key].ToString());

                    return Utilities.UnFransferChar(_value);
                }
                else
                    return "";
            }
            else
                return "";
        }


        /// <summary>
        ///	To get the value.
        /// </summary>
        public string this[string key]
        {
            get
            {
                return State.getState(key);
            }
            set
            {
                State.saveState(key, value);
            }
        }


        /// <summary>
        ///	Find control by control id from a control container. 
        ///	<code>
        ///	TextBox txt_name = (TextBox)State.FindControl("txt_name",this);
        ///	</code>
        /// </summary>
        /// <param name="controlId">control id to find.</param>
        /// <param name="control">control container.</param>
        /// <returns>The result control.</returns>
        public System.Web.UI.Control FindControl(string controlId, System.Web.UI.Control control)
        {
            System.Web.UI.Control _result = null;
            foreach (System.Web.UI.Control _control in control.Controls)
            {
                if (_control.ID == controlId)
                {
                    _result = _control;
                    break;
                }
                else
                {
                    if (_control.HasControls())
                    {
                        _result = FindControl(controlId, _control);
                        if (_result != null)
                            break;
                    }
                }
            }
            return _result;
        }
        /// <summary>
        ///	Set the current view UserControl of current page.
        ///	<code>
        ///	this.State.CurrentView = "Edit";
        ///	</code>
        /// </summary>
        public string CurrentView
        {
            set
            {

                string _pageName = WComm.Utilities.getCurPage;
                List<View> _viewList = View.getViewList(_pageName);
                View _view = View.getViewByName(value, _viewList);

                if (_view == null)
                {
                    throw new Exception("Can't find Usercontrol " + value + " in View.config");
                }



                System.Web.UI.Control _control = null;

                List<View> _parentViewList;
                if (_view.ParentView == null)
                {
                    _parentViewList = _viewList;
                }
                else
                {
                    _parentViewList = _view.ParentView.ChildrenViews;
                }

                foreach (View _cview in _parentViewList)
                {
                    if (_view.Group == _cview.Group)
                    {
                        _control = FindControl(_cview.Type, this.Page);
                        if (_control == null)
                        {
                            throw new Exception("Can't find Usercontrol " + _cview.Name + " in Page");
                        }
                        _control.Visible = false;
                    }
                }

                _control = FindControl(_view.Type, this.Page);
                if (_control == null)
                {
                    throw new Exception("Can't find Usercontrol " + _view.Name + " in Page");
                }
                _control.Visible = true;
                MethodInfo mi = _control.GetType().GetMethod("DataBind");
                if (mi != null)
                    mi.Invoke(_control, null);
            }

        }

        public void DataBind(string viewName)
        {
            string _pageName = WComm.Utilities.getCurPage;
            List<View> _viewList = View.getViewList(_pageName);
            View _view = View.getViewByName(viewName, _viewList);

            if (_view == null)
            {
                throw new Exception("Can't find Usercontrol " + viewName + " in View.config");
            }

            System.Web.UI.Control _control = null;
            _control = FindControl(_view.Type, this.Page);
            if (_control == null)
            {
                throw new Exception("Can't find Usercontrol " + _view.Name + " in Page");
            }

            MethodInfo mi = _control.GetType().GetMethod("DataBind");
            if (mi != null)
                mi.Invoke(_control, null);

        }
    }
}
