using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System;
using System.Web.UI.WebControls;

namespace realtime
{
    public class bond_dropdownlist
    {
        private static int zongjilu = 0;
        public bond_dropdownlist()
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
            DataSet ds = new DataSet();
            sda.Fill(ds);
            con.Close();
            sda.Dispose();
            return ds;
        }
        public static DataSet fanhui_ds(string sql, string config)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[config]);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
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
            cmd.Dispose();
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
            cmd.Dispose();
            con.Close();
            return result;
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
            cmd.Dispose();
            return abc;
        }
        /// <summary>
        /// 执行sql语句，不返回任何值
        /// </summary>
        /// <param name="sql"></param>
        public static string execute(string sql)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["sqlcon"]);
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                int fh = cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return fh.ToString();
            }
            catch
            {
                con.Close();
                cmd.Dispose();
                return "";
            }

        }
        public static string execute(string sql, string config)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[config]);
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                int fh = cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return fh.ToString();
            }
            catch
            {
                con.Close();
                cmd.Dispose();
                return "";
            }

        }
        /// <summary>
        /// 判断字符串是否为数字
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsNumber(string strNumber)
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
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ddl"></param>
        /// <param name="datatext"></param>
        /// <param name="datavalue"></param>
        #region
        #endregion
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="biaoming"></param>
        /// <param name="canshu"></param>
        /// <param name="canshuzhi"></param>
        /// <returns></returns>
        #region
        public static int CunChuGuoCheng(string biaoming, string canshu, string canshuzhi)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["sqlcon2"]);
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
            return Convert.ToInt32(cmd.Parameters["ReturnValue"].Value);
            con.Dispose();
        }
        #endregion
        public static DataSet fanhui_ds(string sql, string sqlcon, params SqlParameter[] values)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings[sqlcon]);
            con.Open();
            SqlCommand sda = new SqlCommand(sql, con);
            sda.Parameters.AddRange(values);
            SqlDataAdapter adapter = new SqlDataAdapter(sda);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();
            sda.Parameters.Clear();//多了这一句，就解决了问题
            return ds;
        }
    }
}
