using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace WComm
{
	/// <summary>
	///	the base page class of framework.
	///	you need to inherit this class of all your web page CodeBehind class.
	/// </summary>
    public class BasePage : System.Web.UI.Page
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
        ///	the constructor of this class.
        /// </summary>
        public BasePage()
        {

        }

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

        public void SetViewState(string key, object obj)
        {
            this.ViewState[key] = obj;
        }

        public object GetViewState(string key)
        {
            return this.ViewState[key];
        }

    }
}
