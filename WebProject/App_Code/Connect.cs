﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Connect
/// </summary>
public class Connect
{
    const string FILENAME = "ProjectDB_Final.mdb";
	public Connect()
	{
		//
		// TODO: Add constructor logic here
		//

	}

    public static string GetconnectionString()
    {
        string location = HttpContext.Current.Server.MapPath("~/App_Data/" + FILENAME);
        string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; data source=" + location;
        return ConnectionString;
    }
}