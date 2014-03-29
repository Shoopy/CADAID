using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// A class that represents a question or an answer.
/// </summary>
public abstract class Thread
{
    public string NickName { get; set; }
    public string UserName { get; set; }
    public string Title { get; set; }
    public DateTime ThreadDate { get; set; }
    public string Content { get; set; }
    public int Rank { get; set; }
    public int ThreadID { get; set; }

	public Thread()
	{
        this.ThreadDate = new DateTime();
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nn">NickName</param>
    /// <param name="t">Title</param>
    /// <param name="d">Date</param>
    /// <param name="c">Content</param>
    /// <param name="r">Rank</param>
    public Thread(int id, string username, string nickname, string t, DateTime d, string c, int r)
    {
        this.ThreadID = id;
        this.UserName = username;
        this.NickName = nickname;
        this.Title = t;
        this.ThreadDate = d;
        this.Content = c;
        this.Rank = r;
    }
}