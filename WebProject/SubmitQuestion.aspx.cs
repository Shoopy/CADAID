using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SubmitQuestion : System.Web.UI.Page
{
    protected User currUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["user"] == null)
                Response.Redirect(@"~/Login.aspx");
            this.PopulateControls();
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Getting data
        string title = this.txtTitle.Text;
        string subjectName = "";
        int subjectID = -1;
        if (this.ddlSubject.SelectedIndex != -1) //If the user chose from a known subject
        {
            subjectName = this.ddlSubject.SelectedItem.Text;
            subjectID = int.Parse(this.ddlSubject.SelectedItem.Value);
        }
        else //If the user created a new subject
        {
            subjectName = this.txtSubject.Text;
        }
        Dictionary<int, string> tags = new Dictionary<int, string>();
        foreach (ListItem item in this.lstTags.Items)
        {
            if (item.Selected)
            {
                tags.Add(int.Parse(item.Value), item.Text);
            }
        }
        string content = this.txtContent.Text;

        DataRow[] coll = this.GetSelectedTags(tags);

        //Creating a Question instance
        this.currUser = (User)Session["user"];
        Question question = new Question();
        question.UserName = this.currUser.UserName;
        question.NickName = this.currUser.NickName;
        question.Title = title;
        question.ThreadDate = DateTime.Now.Date;
        question.Content = content;
        question.Rank = 0;
        question.SubjectID = subjectID;
        question.IsAnswered = false;

        //Adding to DB
        //ThreadService tService = new ThreadService();
        //try
        //{
        //    tService.InsertQuestionToDB(question);
        //}
        //catch (Exception ex)
        //{
        //    this.lblMsg.Text = ex.Message;
        //}
        //Response.Redirect(@"~/HomePage.aspx");

        ThreadService tService = new ThreadService();
        if (Session["dsTags"] != null)
        {
            try
            {
                tService.UpdateTagsInDB((DataSet)Session["dsTags"]);
            }
            catch (Exception ex)
            { this.lblMsg.Text = ex.Message; }
        }

        
    }


    protected void btnInsertTag_Click(object sender, EventArgs e)
    {
        int itemValue = this.GetMaxValue(this.lstTags.Items) + 1;
        string tagName = this.txtNewTag.Text;
        //this.lstTags.Items.Add(new ListItem(tagName, itemValue.ToString()));
        //this.txtNewTag.Text = "";

        //Add to dataset
        if (Session["dsTags"] != null)
        {
            DataSet dsTags = (DataSet)Session["dsTags"];

            string criteria = "TagName='" + tagName + "'";
            DataRow[] tags = dsTags.Tables[0].Select(criteria);
            ///If tag doesn't exist
            if (tags.Length == 0)
            {
                this.lstTags.Items.Add(new ListItem(tagName, itemValue.ToString()));
                this.txtNewTag.Text = "";

                DataRow nRow = dsTags.Tables[0].NewRow();
                nRow["TagID"] = itemValue;
                nRow["TagName"] = tagName;
                dsTags.Tables[0].Rows.Add(nRow);
            }
            else
            {
                this.lblMsg.Text = "Tag already exists. Consider changing the tag name.";
            }
        }

    }

    /// <summary>
    /// Binds initiate values for the controls in the form.
    /// </summary>
    protected void PopulateControls()
    {
        Service service = new Service();
        DataSet dsTags = service.GetTags();
        Session["dsTags"] = dsTags;
        this.lstTags.DataSource = dsTags;
        this.lstTags.DataTextField = "TagName";
        this.lstTags.DataValueField = "TagID";
        this.lstTags.DataBind();

        DataSet dsSubjects = service.GetSubjects();
        this.ddlSubject.DataSource = dsSubjects;
        this.ddlSubject.DataTextField = "SubjectName";
        this.ddlSubject.DataValueField = "SubjectID";
        this.ddlSubject.DataBind();

    }
    protected int GetMaxValue(ListItemCollection collection)
    {
        int max = 0;
        foreach (ListItem item in collection)
        {
            if (int.Parse(item.Value) > max)
                max = int.Parse(item.Value);
        }
        return max;
    }

    /// <summary>
    /// Gets a DataRow array of the selected tags.
    /// </summary>
    /// <param name="?"></param>
    /// <returns></returns>
    public DataRow[] GetSelectedTags(Dictionary<int, string> tags)
    {
        List<DataRow> selectedTags = new List<DataRow>();
        DataSet ds = null;
        if (Session["dsTags"] != null)
        {
            ds = (DataSet)Session["dsTags"];
        }
        foreach (KeyValuePair<int, string> tag in tags)
        {
            if (ds != null)
            {
                //use Select function to get the DataRow[] and add each item to the list.
                DataRow[] selected = ds.Tables[0].Select("TagName ='" + tag.Value + "'");
                selectedTags.AddRange(selected);
            }
        }

        return selectedTags.ToArray();
    }
}