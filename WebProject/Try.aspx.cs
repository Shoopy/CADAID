using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Try : System.Web.UI.Page
{
    

    private User currUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["user"] = new User("ShaiL", "bbb", "bbb", "bbb", "bbb", 10, new DateTime(), 1, 0, "sf", 1);
            this.PrintThreads();

        }
        catch (Exception ex)
        {
            this.lblMsg.Text = ex.Message;
        }
        finally { }
    }

    protected void PrintThreads()
    {
        ThreadService ts = new ThreadService();
        try
        {
            List<Question> questionList = ts.GetQuestions();
            foreach (Question question in questionList)
            {
                this.PopulateThread(question);
            }
        }
        catch (Exception ex)
        { throw ex; }
        finally { }
    }

    protected void PopulateThread(Thread question)
    {
        try
        {
            Thread thread = question;
            if (thread == null)
                throw new Exception("Error: QuestionID not found.");

            ThreadControl tControl = (ThreadControl)Page.LoadControl("~/ThreadControl.ascx");
            tControl.ThreadObj = thread;
            tControl.BindThread();

            this.PanelThreads.Controls.Add(tControl);

            (this.PanelThreads.FindControl(tControl.ID) as ThreadControl).voteDownEvent += new EventHandler(Try_voteDownEvent);
            (this.PanelThreads.FindControl(tControl.ID) as ThreadControl).voteUpEvent += new EventHandler(Try_voteUpEvent);

            this.currUser = (User)Session["user"];
            UserService uService = new UserService();
            int rank = uService.GetRank(this.currUser, thread);
            this.SetButtons(tControl, rank);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally { }
    }

    void Try_voteUpEvent(object sender, EventArgs e)
    {
        this.lblMsg.Text = "up! :D";
        ThreadControl tControl = sender as ThreadControl;

        try
        {
            this.VoteQuestion(tControl, 1);
        }
        catch (Exception ex)
        { this.lblMsg.Text = ex.Message; }
    }

    void Try_voteDownEvent(object sender, EventArgs e)
    {
        this.lblMsg.Text = "down! :D";
        ThreadControl tControl = sender as ThreadControl;
        try
        {
            this.VoteQuestion(tControl, -1);
        }
        catch (Exception ex)
        { this.lblMsg.Text = ex.Message; }
    }

    protected void VoteQuestion(ThreadControl tControl, int nRank)
    {
        this.currUser = (User)Session["user"];
        ThreadService tService = new ThreadService();
        UserService uService = new UserService();
        try
        {
            int rank = uService.GetRank(this.currUser, (Question)tControl.ThreadObj);
            if (rank != 0)
                tService.UpdateQuestionRank(this.currUser, (Question)tControl.ThreadObj, nRank);
            else
                tService.InsertQuestionRank(this.currUser, (Question)tControl.ThreadObj, nRank);
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            this.PanelThreads.Controls.Clear();
            this.PrintThreads();
        }
    }

    /// <summary>
    /// Sets true/false values for vote-buttons in ThreadControl.
    /// </summary>
    /// <param name="tControl">The ThreadControl which needs to be set.</param>
    /// <param name="rank">A rank, -1\0\+1, which the current user voted for the question [if he\she voted].</param>
    protected void SetButtons(ThreadControl tControl, int rank)
    {
        if (this.currUser.Status == 0)
        {
            ControlCollection PanelControls = tControl.FindControl("PanelThread").Controls;
            for (int i = 0; i < PanelControls.Count; i++)
            {
                Control curr = PanelControls[i];
                if (curr is ImageButton)
                {
                    ((ImageButton)curr).Enabled = false;
                    ((ImageButton)curr).CssClass = "disabledButton";
                }
            }
            return;
        }
        ImageButton up = (ImageButton)tControl.FindControl("voteUp");
        ImageButton down = (ImageButton)tControl.FindControl("voteDown");
        switch (rank)
        {
            case 1:
                up.Enabled = false;
                up.CssClass = "disabledButton";
                down.Enabled = true;
                down.CssClass = "";
                break;
            case -1:
                up.Enabled = true;
                up.CssClass = "";
                down.Enabled = false;
                down.CssClass = "disabledButton";
                break;
            default:
                up.Enabled = true;
                up.CssClass = "";
                down.Enabled = true;
                down.CssClass = "";
                break;
        }
    }
}