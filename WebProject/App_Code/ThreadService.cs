using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

/// <summary>
/// Summary description for ThreadService
/// </summary>
public class ThreadService : Service
{
    public ThreadService()
    {

    }

    public Thread GetThreadByIDAndKind(int id, string kind)
    {
        if (kind == "question")
            return GetQuestionByID(id);
        return null;

    }

    public Question PopulateQuestionByDataRow(DataRow r)
    {
        //DataRow r = ds.Tables[0].Rows[0];
        Question question = new Question();
        question.ThreadID = Convert.ToInt32(r["QuestID"]);
        question.UserName = r["UserName"].ToString();
        question.NickName = r["NickName"].ToString();
        question.Title = r["Title"].ToString();
        question.ThreadDate = Convert.ToDateTime(r["qDate"]);
        question.Content = r["Content"].ToString();
        object rank = r["TotalRank"];
        if (rank.ToString() == "")
            question.Rank = 0;
        else
            question.Rank = Convert.ToInt32(rank);
        question.SubjectID = Convert.ToInt32(r["SubjectID"].ToString());

        int isAnswered = Convert.ToInt32(r["IsAnswered"]);
        if (Math.Abs(isAnswered) == 1)
            question.IsAnswered = true;
        else
            question.IsAnswered = false;

        return question;

    }

    public Question GetQuestionByID(int id)
    {
        Question q;

        this.cmd.CommandText = "spGetQuestionByID";

        this.cmd.Parameters.Add("@QuestionID", OleDbType.Integer).Value = id;

        DataSet ds = new DataSet();
        OleDbDataAdapter adapter = new OleDbDataAdapter(this.cmd);

        try
        {
            adapter.Fill(ds, "QuestionTbl");
            q = this.PopulateQuestionByDataRow(ds.Tables[0].Rows[0]);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally { }
        return q;
    }

    public List<Question> GetQuestions()
    {
        DataSet ds = new DataSet();
        List<Question> questionList = new List<Question>();
        this.cmd.CommandText = "spGetQuestionsWithRanks2";

        OleDbDataAdapter adapter = new OleDbDataAdapter(this.cmd);

        try
        {
            adapter.Fill(ds, "QuestionsTbl");
            DataTable table = ds.Tables["QuestionsTbl"];
            foreach (DataRow row in table.Rows)
            {
                questionList.Add(this.PopulateQuestionByDataRow(row));
            }
        }
        catch (Exception ex)
        { throw ex; }
        finally { }
        return questionList;
    }

    public void UpdateQuestionRank(User user, Question question, int nRank)
    {
        this.cmd.CommandText = "spUpdateQuestionRank";

        this.cmd.Parameters.Add("@RankValue", OleDbType.Integer).Value = nRank;
        this.cmd.Parameters.Add("@UserName", OleDbType.BSTR).Value = user.UserName;
        this.cmd.Parameters.Add("@QuestionID", OleDbType.Integer).Value = question.ThreadID;

        try
        {
            this.myConn.Open();
            int check = this.cmd.ExecuteNonQuery();
            //if (check == 0)
            //    throw new Exception("Row not found");
        }
        catch (Exception ex)
        { throw ex; }
        finally { this.myConn.Close(); }
    }

    public void InsertQuestionRank(User user, Question question, int nRank)
    {
        this.cmd.CommandText = "spInsertRankToQuestion";

        this.cmd.Parameters.Add("@QuestionID", OleDbType.Integer).Value = question.ThreadID;
        this.cmd.Parameters.Add("@UserName", OleDbType.BSTR).Value = user.UserName;
        this.cmd.Parameters.Add("@RankValue", OleDbType.Integer).Value = nRank;

        try
        {
            this.myConn.Open();
            int check = this.cmd.ExecuteNonQuery();
            if (check == 0)
                throw new Exception("Insert failed.");
        }
        catch (Exception ex)
        { throw ex; }
        finally { this.myConn.Close(); }
    }

    public void InsertQuestionToDB(Question question)
    {
        this.cmd.CommandText = "spInsertQuestion";

        this.cmd.Parameters.Add("@qDate", OleDbType.DBDate).Value = question.ThreadDate;
        this.cmd.Parameters.Add("@UserName", OleDbType.BSTR).Value = question.UserName;
        this.cmd.Parameters.Add("@SubjectID", OleDbType.Integer).Value = question.SubjectID;
        this.cmd.Parameters.Add("@Title", OleDbType.BSTR).Value = question.Title;
        this.cmd.Parameters.Add("@Content", OleDbType.BSTR).Value = question.Content;

        int isAnswered = 0;
        if (question.IsAnswered)
            isAnswered = -1;
        this.cmd.Parameters.Add("@IsAnswered", OleDbType.Integer).Value = isAnswered;

        try
        {
            this.myConn.Open();
            int check = this.cmd.ExecuteNonQuery();
            if (check == 0)
                throw new Exception("Insert failed");
        }
        catch (Exception ex)
        { throw ex; }
        finally { this.myConn.Close(); }
    }

    public void UpdateTagsInDB(DataSet dsTags)
    {
        //this.cmd.CommandText = "spGetTags"; // Doesn't work with stored procedure. needs to be changed.
        //OleDbDataAdapter adapter = new OleDbDataAdapter(this.cmd);
        //adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Tags;", this.myConn);
        OleDbCommandBuilder build = new OleDbCommandBuilder(adapter);
        adapter.InsertCommand = build.GetInsertCommand();      
             
        try
        {
            adapter.Update(dsTags, "Tags");
        }
        catch (Exception ex)
        { throw ex; }
    }

    
}