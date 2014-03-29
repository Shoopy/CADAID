using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP;

public partial class ThreadControl : System.Web.UI.UserControl
{
    public event EventHandler voteUpEvent;
    public event EventHandler voteDownEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }


    public ThreadControl()
    {
    }

    public string Title
    {
        get { return this.lblThreadTitle.Text; }
        set { this.lblThreadTitle.Text = value; }
    }
    public string NickName
    {
        get { return this.lblThreadNickName.Text; }
        set { this.lblThreadNickName.Text = value; }
    }
    public string Date
    {
        get { return this.lblThreadDate.Text; }
        set { this.lblThreadDate.Text = value; }
    }
    public string Content
    {
        get { return this.txtContent.Text; }
        set { this.txtContent.Text = value; }
    }
    public int Rank
    {
        get { return Convert.ToInt32(this.lblThreadRank.Text); }
        set { this.lblThreadRank.Text = value.ToString(); }
    }

    public Thread ThreadObj { get; set; }



    protected void voteUp_Click(object sender, ImageClickEventArgs e)
    {
        if (this.voteUpEvent != null)
        {
            //this.Rank++;
            //this.ThreadObj.Rank = this.Rank;
            this.voteUpEvent(this, e);
        }
    }
    protected void voteDown_Click(object sender, ImageClickEventArgs e)
    {
        if (this.voteDownEvent != null)
        {
            //this.Rank--;
            //this.ThreadObj.Rank = this.Rank;
            this.voteDownEvent(this, e);
        }
    }


    public void BindThread()
    {
        this.Title = this.ThreadObj.Title;
        this.NickName = this.ThreadObj.NickName;
        this.Date = this.ThreadObj.ThreadDate.ToShortDateString();
        this.Content = this.ThreadObj.Content;
        this.Rank = this.ThreadObj.Rank;


        this.ID = "ThreadControl" + this.ThreadObj.ThreadID.ToString();
    }

}