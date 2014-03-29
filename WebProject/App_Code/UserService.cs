using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

/// <summary>
/// UserService contains methods which deal with User's actions in the website.
/// </summary>
public class UserService: Service
{
    public UserService()
    {
    }

    public int Register(RegiUser user, int confirm)
    {
        this.cmd.CommandText = "spInsertUser";

        #region parameters
        this.cmd.Parameters.Add("@UserName", OleDbType.BSTR).Value = user.UserName;
        this.cmd.Parameters.Add("@Nickname", OleDbType.BSTR).Value = user.NickName;
        this.cmd.Parameters.Add("@FirstName", OleDbType.BSTR).Value = user.FirstName;
        this.cmd.Parameters.Add("@LastName", OleDbType.BSTR).Value = user.LastName;
        this.cmd.Parameters.Add("@Pass", OleDbType.BSTR).Value = user.GetPass(confirm);
        this.cmd.Parameters.Add("@VerifQuestion", OleDbType.BSTR).Value = user.GetVerifQ(confirm);
        this.cmd.Parameters.Add("@VerifAnswer", OleDbType.BSTR).Value = user.GetVerifA(confirm);
        this.cmd.Parameters.Add("@Email", OleDbType.BSTR).Value = user.Email;
        this.cmd.Parameters.Add("@Date", OleDbType.DBDate).Value = user.JoinDate;
        this.cmd.Parameters.Add("@Degree", OleDbType.BSTR).Value = user.Degree;
        this.cmd.Parameters.Add("@InstituteID", OleDbType.Integer).Value = user.InstituteID;

        #endregion

        try
        {
            this.myConn.Open();
            int check = this.cmd.ExecuteNonQuery();
            if (check == 0)
                throw new Exception("Registration failed");
            return 1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        { 
        }
    }

    public int GetRank(User user, Thread thread)
    {
        try
        {
            if (thread is Question)
                return this.GetQuestionRank(user, (Question)thread);
            return 0;
        }
        catch (Exception ex)
        { throw ex; }
        finally { }
    }

    private int GetQuestionRank(User user, Question q)
    {
        this.cmd.CommandText = "spGetQuestRankByUserAndQuest";
        this.cmd.Parameters.Add("@UserName", OleDbType.BSTR).Value = user.UserName;
        this.cmd.Parameters.Add("@QuestionID", OleDbType.Integer).Value = q.ThreadID;

        OleDbDataAdapter adapter = new OleDbDataAdapter(this.cmd);
        DataSet ds = new DataSet();

        try
        {
            adapter.Fill(ds, "RankTbl");
            if (ds.Tables["RankTbl"].Rows.Count == 0)
                return 0;
            return int.Parse(ds.Tables["RankTbl"].Rows[0]["RankValue"].ToString());
        }
        catch (Exception ex)
        { throw ex; }
        finally { }
    }

    public User LoginUser(string username, string pass)
    {
        this.cmd.CommandText = "spLoginUser";
        this.cmd.Parameters.Add("@UserName", OleDbType.BSTR).Value = username;
        this.cmd.Parameters.Add("@Pass", OleDbType.BSTR).Value = pass;

        User user;

        OleDbDataAdapter adapter = new OleDbDataAdapter(this.cmd);
        DataSet ds = new DataSet();

        try
        {
            adapter.Fill(ds, "UserDetails");
            if (ds.Tables["UserDetails"].Rows.Count == 0)
                throw new Exception("Login error.");
            DataRow row = ds.Tables[0].Rows[0];

            #region buildUser
            user = new User();
            user.UserName = row["UserName"].ToString();
            user.NickName = row["Nickname"].ToString();
            user.FirstName = row["FirstName"].ToString();
            user.LastName = row["LastName"].ToString();
            user.Email = row["Email"].ToString();
            user.Reputation = Convert.ToInt32(row["Reputation"].ToString());
            user.JoinDate = Convert.ToDateTime(row["JoinDate"].ToString());
            user.Status = Convert.ToInt32(row["Status"].ToString());
            user.Experience =  Convert.ToInt32(row["Experience"].ToString());
            user.Degree = row["Degree"].ToString();
            user.InstituteID = Convert.ToInt32(row["InstituteID"].ToString());
            #endregion

            return user;
        }
        catch (Exception ex)
        { throw ex; }
        
    }

}