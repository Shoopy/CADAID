using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Describes a regular user.
/// </summary>
public class User
{
    public string UserName { get; set; }
    public string NickName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int Reputation { get; set; }
    public DateTime JoinDate { get; set; }
    public int Status { get; set; }
    public int Experience { get; set; }
    public string Degree { get; set; }
    public int InstituteID { get; set; }

    public User()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public User(string un, string nn, string fn, string ln, string em, int rep, DateTime jd, int stat, 
        int exp, string deg, int instID)
    {
        this.UserName = un;
        this.NickName = nn;
        this.FirstName = fn;
        this.LastName = ln;
        this.Email = em;
        this.Reputation = rep;
        this.JoinDate = jd;
        this.Status = stat;
        this.Experience = exp;
        this.Degree = deg;
        this.InstituteID = instID;
    }
}