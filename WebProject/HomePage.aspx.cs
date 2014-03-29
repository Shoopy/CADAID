using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomePage : System.Web.UI.Page
{
    private User currUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.currUser = (User)Session["user"];
            this.PrintThreads();
        }
        catch (Exception ex)
        {
            this.lblMsg.Text = ex.Message + ", " + ex.Data + ", " + ex.Source;
        }
        finally { }
    }

    /// <summary>
    /// Prints all the questions' threads to the Home Page.
    /// </summary>
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

    /// <summary>
    /// Builds the ThreadControl, with the question Thread object and user details (if logged in).
    /// </summary>
    /// <param name="question">Thread (Question) object.</param>
    protected void PopulateThread(Thread question)
    {
        try
        {
            if (question == null)
                throw new Exception("Error: QuestionID not found.");

            ThreadControl tControl = (ThreadControl)Page.LoadControl("~/ThreadControl.ascx");
            tControl.ThreadObj = question;
            tControl.BindThread();

            this.PanelThreads.Controls.Add(tControl);

            (this.PanelThreads.FindControl(tControl.ID) as ThreadControl).voteDownEvent += new EventHandler(Try_voteDownEvent);
            (this.PanelThreads.FindControl(tControl.ID) as ThreadControl).voteUpEvent += new EventHandler(Try_voteUpEvent);

            if (Session["user"] != null)
            {
                this.currUser = (User)Session["user"];
                UserService uService = new UserService();
                int rank = uService.GetRank(this.currUser, question);
                this.SetButtons(tControl, rank);
            }
            else
            {
                this.DisableButtons(tControl);
            }

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
            this.DisableButtons(tControl);
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

    /// <summary>
    /// Disables all buttons in the ThreadControl.
    /// </summary>
    /// <param name="tControl">Thread Control</param>
    public void DisableButtons(ThreadControl tControl)
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
    }

}