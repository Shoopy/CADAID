using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

/// <summary>
/// Summary description for Service
/// </summary>
public class Service
{
    protected OleDbConnection myConn;
    protected OleDbCommand cmd;
    public Service()
    {
        this.myConn = new OleDbConnection(Connect.GetconnectionString());
        this.cmd = new OleDbCommand();
        this.cmd.Connection = this.myConn;
        this.cmd.CommandType = CommandType.StoredProcedure;
    }

    public DataSet GetTags()
    {
        this.cmd.CommandText = "spGetTags";
        DataSet ds = new DataSet();
        OleDbDataAdapter adapter = new OleDbDataAdapter(this.cmd);

        try
        {
            adapter.Fill(ds, "Tags");
            ds.Tables["Tags"].PrimaryKey = new DataColumn[] { ds.Tables["Tags"].Columns["TagID"] };
            return ds;
        }
        catch (Exception ex)
        { throw ex; }
    }

    public DataSet GetSubjects()
    {
        this.cmd.CommandText = "spGetSubjects";
        DataSet ds = new DataSet();
        OleDbDataAdapter adapter = new OleDbDataAdapter(this.cmd);

        try
        {
            adapter.Fill(ds, "SubjectsTbl");
            ds.Tables["SubjectsTbl"].PrimaryKey = new DataColumn[] { ds.Tables["SubjectsTbl"].Columns["SubjectID"] };
            return ds;
        }
        catch (Exception ex)
        { throw ex; }
    }
}