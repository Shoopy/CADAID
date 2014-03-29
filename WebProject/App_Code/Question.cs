using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Question
/// </summary>
public class Question : Thread
{
    public bool IsAnswered { get; set; }
    public int SubjectID { get; set; }
    public Question()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">QuestionID</param>
    /// <param name="nn">NickName</param>
    /// <param name="t">Title</param>
    /// <param name="d">Date</param>
    /// <param name="c">Content</param>
    /// <param name="r">Rank</param>
    /// <param name="ans">SubjectID</param>
    /// <param name="ans">IsAsnwered</param>
    public Question(int id, string username, string nickname, string t, DateTime d, string c, int r, int subID, bool ans)
        : base(id, username, nickname, t, d, c, r)
    {
        this.IsAnswered = ans;
    }
}