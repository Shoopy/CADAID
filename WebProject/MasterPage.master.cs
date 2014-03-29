using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected string visitorText = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.visitorText = this.lblUserName.Text;
        }
        if (Session["user"] == null)
        {
            this.lblUserName.Text = this.visitorText;
            this.btnLogout.Visible = false;
        }
        else
        {
            this.lblUserName.Text = ((User)Session["user"]).NickName;
            this.btnLogin.Visible = false;
        }
    }
}
