using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        UserService uService = new UserService();
        try
        {
            Session["user"] = uService.LoginUser(this.Login1.UserName, this.Login1.Password);
            if (Session["user"] != null)
                e.Authenticated = true;
        }
        catch (Exception ex)
        {
            e.Authenticated = false;
            this.lblMsg.Text = ex.Message;
        }
    }
}