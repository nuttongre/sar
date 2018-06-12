﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

public partial class Tools_Backup : System.Web.UI.Page
{
    Connection Conn = new Connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataBind();
        }
    }

    public override void DataBind()
    {
        string[] files;
        string path = ConfigurationManager.AppSettings["BackupData"];
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        files = Directory.GetDirectories(path);

        ICollection ic = default(ICollection);
        ic = (ICollection)files;
        ArrayList alFiles = new ArrayList();
        alFiles.AddRange(files);
        alFiles = SortDesc(alFiles);

        //gridContent.CheckListDataField = "!";
        gridContent.DataSource = alFiles;
        gridContent.DataBind();
    }

    protected string FileName(object file)
    {
        return new DirectoryInfo(file.ToString()).Name;
    }
    protected string FileSize(object file)
    {
        return BTC.GetSize((long)Folder.Size(file.ToString(), false));
        //return FileManager.GetSize((long)Size(file.ToString(), false));
    }
    protected string CreateDate(object file)
    {
        return new DirectoryInfo(file.ToString()).CreationTime.ToString("dd-MMMM-yyyy HH:mm");
    }

    private void BackupData()
    {
        string path = string.Format("{0}{1}\\", ConfigurationManager.AppSettings["BackupData"], DateTime.Now.ToString("yyyy-MM-dd HH-mm-") + CurrentUser.UserName.Trim());
        if (!Directory.Exists(Path.GetDirectoryName(path)))
            Directory.CreateDirectory(Path.GetDirectoryName(path));

        StringBuilder script = new StringBuilder();
        Regex reg = new Regex(@"(Initial Catalog=|Database=){1}([\w-]+)[;]?");
        string dbName = reg.Match(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString).Groups[2].Value;

        script.AppendFormat("INSERT INTO Statistic (UseId,MenuId,Action,URL,CreateDate) values ('{2}','056','4','Backup.aspx',getdate()); ", dbName, path, CurrentUser.ID);
         //script.AppendFormat("INSERT INTO EventLog (UserId, Code, Detail, IPAddress, EventGroup) VALUES ('{0}', {1}, '{2}', '{3}', 2); ", CurrentUser.ID, 5, "", Request.UserHostAddress);
        script.Append("DECLARE @name VARCHAR(50) DECLARE @path VARCHAR(256) DECLARE @fileName VARCHAR(256) DECLARE @fileDate VARCHAR(20) ");
        script.AppendFormat("SET @path = N'{0}'  SELECT @fileDate = CONVERT(VARCHAR(20),GETDATE(),112) DECLARE db_cursor CURSOR FOR  ", path);
        script.AppendFormat("SELECT name FROM master.dbo.sysdatabases WHERE name IN ('{0}')  OPEN db_cursor  FETCH NEXT FROM db_cursor INTO @name ", dbName);
        script.Append("WHILE @@FETCH_STATUS = 0  BEGIN SET @fileName = @path + @name + '_' + @fileDate + '.BAK' BACKUP DATABASE @name TO DISK = @fileName ;");
        script.Append("FETCH NEXT FROM db_cursor INTO @name END CLOSE db_cursor DEALLOCATE db_cursor ;");

        int result = 0;
        result =  Conn.ExecuteNonTransAction(script.ToString());

        if (result > 0) 
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "BackUpComplete", "alert('สำรองข้อมูลเสร็จสมบูรณ์');", true);
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "BackUpNotComplete", "alert('เกิดข้อผิดพลาดในการสำรองข้อมูล');", true);
    }

    private void RestoreData(string path)
    {
        int result = 0;
        StringBuilder script = new StringBuilder();
        string[] files;
        files = Directory.GetFiles(path);
        if (files.Length > 0)
        {
            foreach (string f in files)
            {
                string filename = Path.GetFileName(f);
                string dbname = filename.Substring(0, filename.IndexOf("_"));
                script.AppendFormat("Use Master Alter Database {0} SET SINGLE_USER With ROLLBACK IMMEDIATE RESTORE DATABASE {0} FROM DISK = N'{1}' WITH REPLACE  ALTER DATABASE {0} SET MULTI_USER WITH ROLLBACK IMMEDIATE; ",
                dbname, f);
            }
            Regex reg = new Regex(@"(Initial Catalog=|Database=){1}([\w-]+)[;]?");
            string dbName = reg.Match(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString).Groups[2].Value;

            script.AppendFormat(" INSERT INTO {0}.dbo.Statistic (UseId,MenuId,Action,URL,CreateDate) values ('{1}','056','5','Backup.aspx',getdate()); ", dbName, CurrentUser.ID);

            //script.AppendFormat("INSERT INTO Statistic (UseId,MenuId,Action,URL,CreateDate) values ('{2}','056','4','Backup.aspx',getdate()); ", dbName, path, CurrentUser.ID);
            //script.AppendFormat("INSERT INTO {4}.dbo.EventLog (UserId, Code, Detail, IPAddress, EventGroup) VALUES ('{0}', {1}, '{2}', '{3}', 2); ",
            //    CurrentUser.ID, 4, "", Request.UserHostAddress, dbName);
            result = Conn.ExecuteNonTransAction(script.ToString());
        }

        if (result > 0)
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "RestoreComplete", "alert('ฟื้นฟูข้อมูลเสร็จสมบูรณ์');", true);
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "RestoreNotComplete", "alert('เกิดข้อผิดพลาดในการฟื้นฟูข้อมูล');", true);
    }

    protected void btnBackup_Click(object sender, EventArgs e)
    {
        BackupData();
        DataBind();
    }

    protected void btnRestore_Click(object sender, EventArgs e)
    {
        string rdoItem = Request.Form["rdoItem"];
        if (!string.IsNullOrEmpty(rdoItem))
            RestoreData(rdoItem);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string rdoItem = Request.Form["rdoItem"];
        if (!string.IsNullOrEmpty(rdoItem))
        {
            if (Directory.Exists(Path.GetDirectoryName(rdoItem)))
                Directory.Delete(rdoItem, true);
        }
        DataBind();
    }

    private ArrayList SortAsc(ArrayList itemsArray)
    {
        SortedList sl = new SortedList();
        ArrayList asc = new ArrayList();
        foreach (string s in itemsArray)
        {
            sl.Add(s, "dummy");
        }
        foreach (string s in sl.Keys)
        {
            asc.Add(s);
        }
        return asc;
    }

    private ArrayList SortDesc(ArrayList itemsArray)
    {
        ArrayList asc = SortAsc(itemsArray);
        ArrayList desc = new ArrayList();
        for (int x = asc.Count - 1; x >= 0; x--)
        {
            desc.Add(asc[x]);
        }
        return desc;
    }

    private double sizeInBytes;
    public double Size(string directory, bool deep)
    {
        DirectoryInfo dir = new DirectoryInfo(directory);
        foreach (FileInfo f in dir.GetFiles())
        {
            sizeInBytes += f.Length;
        }
        if (deep)
        {
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                Size(d.FullName, deep);
            }
        }
        double bytes = sizeInBytes;
        sizeInBytes = 0;
        return bytes;
    }
    /*
    private int Execute(string commandText)
    {
        string UserName = ConfigurationManager.AppSettings["DBUserName"];
        string Password = ConfigurationManager.AppSettings["DBPassword"];
        if (Convert.ToBoolean(ConfigurationManager.AppSettings["PasswordEncrypt"]))
        {
            Password = Star.Security.Cryptography.Text.Decrypt(ConfigurationManager.AppSettings["DBPassword"], ConfigurationManager.AppSettings["DBUsername"]);
        }
        string connectionString = String.Format(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString, UserName, Password);

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = commandText;

                conn.Open();
                cmd.CommandTimeout = 5000;
                try
                {
                    int result = cmd.ExecuteNonQuery();
                    return result;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
    */
}
