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
using System.Reflection ;
using System.Text ;
using System.Web.Mail ;
using System.Runtime.Serialization;
using System.Data.OleDb ;
using System.IO;
using System.Xml;

namespace WComm
{
	/// <summary>
	///	the base user control class of framework.
	///	you need to inherit this class of all your user control CodeBehind class.
	/// </summary>
    public class BaseUserControl : System.Web.UI.UserControl
    {
        /// <summary>
        ///	the state of this page.
        /// </summary>
        protected State State;
        /// <summary>
        ///	the cookie of this page.
        /// </summary>
        protected Cookiee Cookiee;

        /// <summary>
        ///	when the page is on initializtion.
        /// </summary>
        /// <param name="e">EventArgs.</param>
        override protected void OnInit(EventArgs e)
        {
            State = new State(this.Page);
            Cookiee = new Cookiee();
            base.OnInit(e);
        }
        /// <summary>
        ///	a virtual of databind method.
        /// </summary>
        public new virtual void DataBind() { }

        public virtual BasePage CurrentPage
        {
            get
            {
                return this.Page as BasePage;
            }
        }

        protected object this[string key]
        {
            get
            {
                return CurrentPage.GetViewState(key);
            }
            set
            {
                CurrentPage.SetViewState(key, value);
            }
        }
    }
}
