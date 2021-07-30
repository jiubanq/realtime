using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

/// <summary>
/// bond_dropdownlist 的摘要说明
/// </summary>
public class DB
{
    private static int zongjilu = 0;
    public DB()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 返回DataSet
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static DataSet fanhui_ds(string sql)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["sqlcon"]);
        con.Open();
        SqlDataAdapter sda = new SqlDataAdapter(sql, con);
        sda.SelectCommand.CommandTimeout = 50000;
        DataSet ds = new DataSet();
        sda.Fill(ds);
        con.Close();
        return ds;
    }
    public static DataSet fanhui_ds(string sql, string config)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[config]);
        con.Open();
        SqlDataAdapter sda = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        sda.Fill(ds);
        con.Close();
        return ds;
    }
    /// <summary>
    /// 返回String,如果没有值则返回0
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static string fanhui_string(string sql)
    {
        string abc = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["sqlcon"]);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        object result = cmd.ExecuteScalar();
        if (result == null)
            abc = "0";
        else
            abc = result.ToString();
        con.Close();
        return abc;
    }
    public static string fanhui_string(string sql, string config)
    {
        string abc = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[config]);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        object result = cmd.ExecuteScalar();
        if (result == null)
            abc = "0";
        else
            abc = result.ToString();
        con.Close();
        return abc;
    }
    /// <summary>
    /// 返回String,如果没有值则返回NULL
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static object fanhui_string_emptyisnull(string sql)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["sqlcon"]);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        object result = cmd.ExecuteScalar();

        con.Close();
        return result;
    }
    public static object fanhui_string_emptyisnull(string sql, string config)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[config]);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        object result = cmd.ExecuteScalar();

        con.Close();
        return result;
    }
    /// <summary>
    /// 执行sql语句，不返回任何值
    /// </summary>
    /// <param name="sql"></param>
    public static int execute(string sql)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["sqlcon"]);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        int fh = cmd.ExecuteNonQuery();
        con.Close();
        return fh;
    }
    public static int execute(string sql, string config)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[config]);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        int fh = cmd.ExecuteNonQuery();
        con.Close();
        return fh;
    }
    /// <summary>
    /// 执行分页存储过程，返回DataSet
    /// </summary>
    /// <param name="tblNmae"></param>
    /// <param name="fldName"></param>
    /// <param name="PageSize"></param>
    /// <param name="PageIndex"></param>
    /// <param name="OrderType"></param>
    /// <param name="IsCount"></param>
    /// <param name="strWhere"></param>
    /// <param name="xianshi"></param>
    /// <param name="cout"></param>
    /// <returns></returns>
    public static DataSet sp_ds(string tblNmae, string fldName, int PageSize, int PageIndex, bool OrderType, int IsCount, string strWhere, string xianshi, string cout)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["sqlcon"]);
        SqlDataAdapter sda = new SqlDataAdapter();
        sda.SelectCommand = new SqlCommand("fenye_p", con);
        sda.SelectCommand.CommandType = CommandType.StoredProcedure;
        SqlParameter par = new SqlParameter("@tblName", SqlDbType.VarChar);
        par.Value = tblNmae;
        sda.SelectCommand.Parameters.Add(par);
        SqlParameter par1 = new SqlParameter("@fldName", SqlDbType.VarChar);
        par1.Value = fldName;
        sda.SelectCommand.Parameters.Add(par1);
        SqlParameter par2 = new SqlParameter("@PageSize", SqlDbType.Int);
        par2.Value = PageSize;
        sda.SelectCommand.Parameters.Add(par2);
        SqlParameter par3 = new SqlParameter("@PageIndex", SqlDbType.Int);
        par3.Value = PageIndex;
        sda.SelectCommand.Parameters.Add(par3);
        SqlParameter par4 = new SqlParameter("@OrderType", SqlDbType.Bit);
        par4.Value = OrderType;
        sda.SelectCommand.Parameters.Add(par4);
        SqlParameter par5 = new SqlParameter("@IsCount", SqlDbType.Int);
        par5.Value = IsCount;
        sda.SelectCommand.Parameters.Add(par5);
        SqlParameter par6 = new SqlParameter("@strWhere", SqlDbType.VarChar);
        par6.Value = strWhere;
        sda.SelectCommand.Parameters.Add(par6);
        SqlParameter par7 = new SqlParameter("@xianshi", SqlDbType.VarChar);
        par7.Value = xianshi;
        sda.SelectCommand.Parameters.Add(par7);

        SqlParameter par8 = new SqlParameter("@cout", SqlDbType.NVarChar, 10);
        par8.Direction = ParameterDirection.InputOutput;
        par8.Value = cout;
        sda.SelectCommand.Parameters.Add(par8);
        DataSet ds = new DataSet();
        con.Open();
        sda.Fill(ds);
        con.Close();
        if (IsCount == 0)
        {
            zongjilu = Convert.ToInt32(sda.SelectCommand.Parameters["@cout"].Value);
        }
        sda.Dispose();
        return ds;
    }
    /// <summary>
    /// 返回分页存储过程的总记录数
    /// </summary>
    /// <returns></returns>
    public static int fanhui_jilushu()
    {
        return zongjilu;
    }
    /// <summary>
    /// 判断字符串是否为数字
    /// </summary>
    /// <param name="strNumber"></param>
    /// <returns></returns>
    public static bool IsNumber(String strNumber)
    {
        Regex objNotNumberPattern = new Regex("[^0-9.-]");
        Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
        Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
        String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
        String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
        Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
        return !objNotNumberPattern.IsMatch(strNumber) && !objTwoDotPattern.IsMatch(strNumber) && !objTwoMinusPattern.IsMatch(strNumber) && objNumberPattern.IsMatch(strNumber);
    }
    /// <summary>
    /// 合并Table里面的某一列为string
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="ColumnsName"></param>
    /// <returns></returns>
    public static string TableColumnsToString(DataTable dt, string ColumnsName)
    {

        string str = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str += "'" + dt.Rows[i][ColumnsName].ToString() + "',";
        }
        if (str != "")
            str = str.Substring(0, str.Length - 1);
        return str;
    }
    //存储过程
    public static int CunChuGuoCheng(string biaoming, string canshu, string canshuzhi)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["sqlcon"]);
        con.Open();
        SqlCommand cmd = new SqlCommand(biaoming, con);
        cmd.CommandType = CommandType.StoredProcedure;
        string[] canshuji = canshu.Split(new char[] { '*' });
        string[] canshuzhiji = canshuzhi.Split(new char[] { '*' });
        SqlParameter[] par = new SqlParameter[canshuji.Length];
        for (int i = 0; i < canshuji.Length; i++)
        {
            par[i] = new SqlParameter();
            par[i].ParameterName = canshuji[i];
            par[i].Value = canshuzhiji[i];
            cmd.Parameters.Add(par[i]);
        }
        SqlParameter parr = new SqlParameter("ReturnValue", SqlDbType.Int);
        parr.Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(parr);
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
        return Convert.ToInt32(cmd.Parameters["ReturnValue"].Value);
    }
}
