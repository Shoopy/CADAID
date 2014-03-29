using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// RegiUser describes a user in Register page. Once the user is registered, 
/// he becomes a regular User so the access to his personal info is canceled.
/// </summary>
public class RegiUser : User
{
    protected string pass;
    protected string verifQ;
    protected string verifA;
    private const int CONFIRM = 42;

    public RegiUser()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public RegiUser(string un, string nn, string fn, string ln, string em, int rep, DateTime jd, int stat,
        int exp, string deg, int instID, string pass, string vq, string va)
        : base(un, nn, fn, ln, em, rep, jd, stat, exp,
            deg, instID)
    {
        this.SetPass(pass);
        this.SetVerifQ(vq);
        this.SetVerifA(va);
    }
    #region GettersAndSetters
    public string GetPass(int confirm)
    {
        if (confirm == CONFIRM)
            return this.pass;
        return "";
    }
    public void SetPass(string value)
    { this.pass = value; }

    public string GetVerifQ(int confirm)
    {
        if (confirm == CONFIRM)
            return this.verifQ;
        return "";
    }
    public void SetVerifQ(string value)
    { this.verifQ = value; }

    public string GetVerifA(int confirm)
    {
        if (confirm == CONFIRM)
            return this.verifA;
        return "";
    }
    public void SetVerifA(string value)
    { this.verifA = value; }

    #endregion

    public User ToUser()
    {
        return (User)this;
    }
}