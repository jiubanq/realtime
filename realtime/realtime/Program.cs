using realtime.log;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace realtime
{
    class Program
    {
        static string send_acctoken = string.Empty;
        //查询出来需要操作的数据
        static ConcurrentQueue<model> quereone = new ConcurrentQueue<model>();
        //向已发送表中插入的数据
        static ConcurrentQueue<model> queresuccess = new ConcurrentQueue<model>();
        static DateTime date = DateTime.Now.AddHours(-1);
        static DateTime date1 = DateTime.Now.AddHours(-1);
        static DateTime date2 = DateTime.Now.AddHours(-1);
        static DateTime date3 = DateTime.Now.AddHours(-1);
        #region 错误日志
        static string Ip = Net.Ip;
        static string Host = Net.Host;
        static string Browser = Net.Browser;
        static LogMes logmes = new LogMes();
        static string strMessage = null;
        #endregion
        static void Main(string[] args)
        {
            //查询备份表插入线程
            Thread insertcirlenew = new Thread(new ThreadStart(insertcirle));
            insertcirlenew.Start();
            //从队列中取出数据进行审核
            Thread getsendcirlenew = new Thread(new ThreadStart(getsendcirle));
            getsendcirlenew.Start();
            ////支付宝队列插入
            Thread insertcirlenew_zfb = new Thread(new ThreadStart(insertcirle_zfb));
            insertcirlenew_zfb.Start();
            //支付宝队列取出
            Thread getsendcirlenew_zfb = new Thread(new ThreadStart(getsendcirle_zfb));
            getsendcirlenew_zfb.Start();
        }
        //查询支付宝插入线程
        static void insertcirle_zfb()
        {
            while (true)
            {
                #region 未发送表
                if (true)
                {
                    try
                    {
                        DateTime DQtime = DateTime.Now;
                        if ((int)(DQtime - date).TotalMinutes >= 25)
                        {
                            date = DateTime.Now;//当前时间
                                                //备份表
                            string sqldtBFTables = @"select distinct XSBMB_SFZH as IDcard,'技能证' as identification from JNZBMDY where BS is null";
                            DataTable dtBFTables = DB.fanhui_ds(sqldtBFTables, "sqlcon").Tables[0];//8.33专用

                            string sqldtBFTables_col = @"select shenfenID as IDcard,'新生报到' as identification from xsbd_zfbdyb where  sfcsbj is null union all select distinct XSBMB_SFZH as IDcard,'学历提升' as identification from [hxxy_jxjy_xlts].[dbo].[XLTSDYB] where BS is null ";
                            DataTable dtBFTables_col = DB.fanhui_ds(sqldtBFTables_col, "sqlcon2").Tables[0];//10.7专用
                            if (dtBFTables.Rows.Count > 0)
                            {
                                List<model> content = JsonChangeConvert.ToDataList<model>(dtBFTables);//8.33
                                                                                                      //循环队列放
                                foreach (var item in content)
                                {
                                    queresuccess.Enqueue(item);
                                }
                            }
                            if (dtBFTables_col.Rows.Count > 0)
                            {
                                List<model> content_col = JsonChangeConvert.ToDataList<model>(dtBFTables_col);//10.7
                                                                                                              //循环队列放
                                foreach (var item in content_col)
                                {
                                    queresuccess.Enqueue(item);
                                }
                            }
                        }
                       
                    }
                    catch (Exception er)
                    {
                        Console.WriteLine("备份表支付宝" + DateTime.Now.ToString() + "：" + er.Message);
                        insertLog("备份表异常支付宝：" + er.StackTrace);
                    }
                }
                #endregion
            }
        }
        //从队列中取出支付宝数据进行更新
        static void getsendcirle_zfb()
        {
            while (true)
            {
                if (queresuccess.Count > 0)
                {
                    int m = 0;
                    List<model> listsend = new List<model>();
                    Boolean remain = true;
                    model result = null;
                    while (remain && m < 30)
                    {
                        try
                        {
                            //从接收队列中取一条消息进行应答处理
                            remain = queresuccess.TryDequeue(out result);
                            if (result != null)
                            {
                                listsend.Add(result);
                            }
                            else
                                break;
                        }
                        catch (Exception er)
                        {
                            Console.WriteLine("查询队列进行审核异常支付宝： " + DateTime.Now.ToString() + "：" + er.Message);
                            insertLog("查询队列进行审核异常支付宝：" + er.StackTrace);
                        }
                        m++;
                    }
                    if (listsend.Count > 0)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(insertMes_zfb), listsend);
                    }
                }
                else
                {
                    Thread.Sleep(500);
                }

            }
        }
        //从队列中取出数据进行审核
        private static void insertMes_zfb(object objmessage)
        {
            try
            {
                List<model> result = (List<model>)objmessage;
                foreach (model message in result)
                {
                    string IDcard = message.IDcard;
                    string identification = message.identification;
                    string shenfenID = IDcard;
                    try
                    {
                        if (identification == "技能证")
                        {
                            if (shenfenID.Length == 18)
                            {
                                zhifubao.Getjiaofei(shenfenID, "", "jbxx_jnzzfbmingxi", "sqlcon");
                            }
                            Console.WriteLine("技能证_支付宝:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        if (identification == "学历提升")
                        {
                            if (shenfenID.Length == 18)
                            {
                                zhifubao.Getjiaofei(shenfenID, "", "[hxxy_jxjy_xlts].[dbo].[jbxx_xltszfbmingxi]", "sqlcon2");
                                Console.WriteLine("学历提升_支付宝:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                        }
                        if (identification == "新生报到")
                        {
                            if (shenfenID.Length == 18)
                            {
                                zhifubao.Getjiaofei(shenfenID, "", "zsgl_zhifubaomingxi", "sqlcon2");
                                Console.WriteLine("新生报到_支付宝:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                        }
                    }
                    catch (Exception er)
                    {
                        Console.WriteLine("查询队列进行审核异常支付宝： " + DateTime.Now.ToString() + "：" + er.Message);
                        insertLog("查询队列进行审核异常支付宝：" + er.StackTrace);
                    }
                }
            }
            catch (Exception er)
            {
                Console.WriteLine("查询队列进行审核异常支付宝： " + DateTime.Now.ToString() + "：" + er.Message);
                insertLog("查询队列进行审核异常支付宝：" + er.StackTrace);
            }
        }
        //查询备份表插入线程
        static void insertcirle()
        {
            while (true)
            {
                #region 未发送表
                if (true)
                {
                    try
                    {
                        DateTime DQtime = DateTime.Now;
                        if ((int)(DQtime - date3).TotalMinutes >= 5)
                        {
                            date3 = DateTime.Now;//当前时间
                                                 //备份表
                            string sqldtBFTables = @"select distinct XSBMB_SFZH as IDcard,'技能证' as identification from JNZBMDY where BS is null";
                            DataTable dtBFTables = DB.fanhui_ds(sqldtBFTables, "sqlcon").Tables[0];//8.33专用

                            string sqldtBFTables_col = @" select distinct XSBMB_SFZH as IDcard,'学历提升' as identification from [hxxy_jxjy_xlts].[dbo].[XLTSDYB] where BS is null and XSBMB_KMID!='特殊' union all select XSBMB_SFZH,'学历提升_特殊' from [hxxy_jxjy_xlts].[dbo].[XLTSDYB] where BS is null and XSBMB_KMID='特殊'";
                            DataTable dtBFTables_col = DB.fanhui_ds(sqldtBFTables_col, "sqlcon2").Tables[0];//10.7专用
                            if (dtBFTables.Rows.Count > 0)
                            {
                                List<model> content = JsonChangeConvert.ToDataList<model>(dtBFTables);//8.33
                                                                                                      //循环队列放
                                foreach (var item in content)
                                {
                                    quereone.Enqueue(item);
                                }
                            }
                            if (dtBFTables_col.Rows.Count > 0)
                            {
                                List<model> content_col = JsonChangeConvert.ToDataList<model>(dtBFTables_col);//10.7
                                                                                                              //循环队列放
                                foreach (var item in content_col)
                                {
                                    quereone.Enqueue(item);
                                }
                            }
                        }
                    }
                    catch (Exception er)
                    {
                        Console.WriteLine("备份表" + DateTime.Now.ToString() + "：" + er.Message);
                        insertLog("备份表异常：" + er.StackTrace);
                    }
                }
                #endregion
            }
        }
        //从队列中取出数据进行审核
        static void getsendcirle()
        {
            while (true)
            {

                if (quereone.Count > 0)
                {
                    int m = 0;
                    List<model> listsend = new List<model>();
                    Boolean remain = true;
                    model result = null;
                    while (remain && m < 30)
                    {
                        try
                        {
                            //从接收队列中取一条消息进行应答处理
                            remain = quereone.TryDequeue(out result);
                            if (result != null)
                            {
                                listsend.Add(result);
                            }
                            else
                                break;
                        }
                        catch (Exception er)
                        {
                            Console.WriteLine("查询队列进行审核异常： " + DateTime.Now.ToString() + "：" + er.Message);
                            insertLog("查询队列进行审核异常：" + er.StackTrace);
                        }
                        m++;
                    }
                    if (listsend.Count > 0)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(insertMes), listsend);
                    }
                }
                else
                {
                    Thread.Sleep(500);
                }

            }
        }
        //从队列中取出数据进行审核
        private static void insertMes(object objmessage)
        {
            try
            {
                List<model> result = (List<model>)objmessage;
                foreach (model message in result)
                {
                    string IDcard = message.IDcard;
                    string identification = message.identification;
                    try
                    {
                        if (identification == "技能证")
                        {
                            string shenfenID = IDcard;
                            if (shenfenID.Length == 18)
                            {
                                //zhifubao.Getjiaofei(shenfenID, "", "jbxx_jnzzfbmingxi", "sqlcon");
                                DataTable jfxx = bond_dropdownlist.fanhui_ds(@" select jiaofeikemu,sfjf,out_order_no,jiaofeishijian,shijiao,shenfenID into #temp from [jbxx_jnzzfbmingxi] where shenfenid='" + shenfenID + "' select a.*,b.sfjf,out_order_no,jiaofeishijian,shijiao from [LC0019999].[JNZBMDY] a left join #temp b on a.XSBMB_SFZH=b.shenfenID and a.JFKMMC=b.jiaofeikemu where BS is null and sfjf=1").Tables[0];//查询名称表中未使用的信息
                                if (jfxx.Rows.Count > 0)
                                {
                                    for (int z = 0; z < jfxx.Rows.Count; z++)
                                    {
                                        string update = "update JNZBMDY set BS='1' where ID='" + jfxx.Rows[z]["ID"].ToString() + "'";//更新名称表
                                        string updateBM = "update DO_NJNZXSBMB set XSBMB_DDBH='" + jfxx.Rows[z]["out_order_no"].ToString() + "',XSBMB_ZFSJ='" + jfxx.Rows[z]["jiaofeishijian"].ToString() + "' where XSBMB_JNZBMSZBID='" + jfxx.Rows[z]["XSBMB_JNZBMSZBID"].ToString() + "' and XSBMB_XH='" + jfxx.Rows[z]["XSBMB_XH"].ToString() + "' and XSBMB_JZNKMBID='" + jfxx.Rows[z]["XSBMB_JZNKMBID"].ToString() + "' and XSBMB_DDBH is null";
                                        bond_dropdownlist.execute(update);
                                        bond_dropdownlist.execute(updateBM);
                                        Console.WriteLine(jfxx.Rows[z]["XSBMB_XH"].ToString() + "技能证缴费成功:" + jfxx.Rows[z]["jiaofeishijian"].ToString());
                                    }
                                }
                            }
                            Console.WriteLine("技能证:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                    }
                    catch (Exception er)
                    {
                        Console.WriteLine("查询队列进行审核异常： " + DateTime.Now.ToString() + "：" + er.Message);
                        insertLog("查询队列进行审核异常：" + er.StackTrace);
                    }

                    if (identification == "学历提升")
                    {
                        string shenfenID = IDcard;
                        string gxje = @"select sfjf,XSBMB_XH,JFKMMC,XSBMB_SFZH,shijiao,jiaofeishijian,a.ID,mxid from [hxxy_jxjy_xlts].[dbo].[XLTSDYB] a
        left join [hxxy_jxjy_xlts].[dbo].[jbxx_xltszfbmingxi] b on a.XSBMB_SFZH=b.shenfenID and a.JFKMMC=b.jiaofeikemu where sfjf='1' and XSBMB_SFZH='" + shenfenID + "' and BS IS NULL and XSBMB_KMID!='特殊'";
                        DataTable GXJE_dt = bond_dropdownlist.fanhui_ds(gxje, "sqlcon2").Tables[0];
                        if (GXJE_dt.Rows.Count > 0)
                        {
                            for (int z = 0; z < GXJE_dt.Rows.Count; z++)
                            {
                                string XLTS_XSBMMXBUP = "update [hxxy_jxjy_xlts].[dbo].[XLTS_XSBMMXB] set XLTS_XSBMMXB_JFJE='" + GXJE_dt.Rows[z]["shijiao"].ToString() + "',XLTS_XSBMMXB_SFR='资金中心',XLTS_XSBMMXB_SFSJ='" + GXJE_dt.Rows[z]["jiaofeishijian"].ToString() + "' WHERE ID='" + GXJE_dt.Rows[z]["mxid"].ToString() + "'";
                                string DYBUP = "update [hxxy_jxjy_xlts].[dbo].[XLTSDYB] set BS='1' where ID='" + GXJE_dt.Rows[z]["ID"].ToString() + "'";
                                bond_dropdownlist.execute(XLTS_XSBMMXBUP, "sqlcon2");
                                bond_dropdownlist.execute(DYBUP, "sqlcon2");
                                Console.WriteLine(GXJE_dt.Rows[z]["XSBMB_XH"].ToString() + "学历提升:" + GXJE_dt.Rows[z]["JFKMMC"].ToString() + "_金额：" + GXJE_dt.Rows[z]["shijiao"].ToString());
                            }
                        }
                        Console.WriteLine("学历提升:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    if (identification == "学历提升_特殊")
                    {
                        string shenfenID = IDcard;
                        string gxje = @"select sfjf,XSBMB_XH,out_order_no,JFKMMC,XSBMB_SFZH,shijiao,jiaofeishijian,a.ID,mxid from [hxxy_jxjy_xlts].[dbo].[XLTSDYB] a
        left join [hxxy_jxjy_xlts].[dbo].[jbxx_xltszfbmingxi] b on a.XSBMB_SFZH=b.shenfenID and a.JFKMMC=b.jiaofeikemu where sfjf='1' and XSBMB_SFZH='" + shenfenID + "' and BS IS NULL and XSBMB_KMID='特殊'";
                        DataTable GXJE_dt = bond_dropdownlist.fanhui_ds(gxje, "sqlcon2").Tables[0];
                        if (GXJE_dt.Rows.Count > 0)
                        {
                            for (int z = 0; z < GXJE_dt.Rows.Count; z++)
                            {
                                string XLTS_XSBMMXBUP = "update [hxxy_jxjy_xlts].[dbo].[XLTS_BJFYMDB] set   XLTS_BJFYMDB_JFSJ='" + GXJE_dt.Rows[z]["jiaofeishijian"].ToString() + "',XLTS_BJFYMDB_JFDH='" + GXJE_dt.Rows[z]["out_order_no"].ToString() + "' WHERE ID='" + GXJE_dt.Rows[z]["mxid"].ToString() + "'";
                                string DYBUP = "update [hxxy_jxjy_xlts].[dbo].[XLTSDYB] set BS='1' where ID='" + GXJE_dt.Rows[z]["ID"].ToString() + "'";
                                bond_dropdownlist.execute(XLTS_XSBMMXBUP, "sqlcon2");
                                bond_dropdownlist.execute(DYBUP, "sqlcon2");
                                Console.WriteLine(shenfenID + "特殊学历提升:" + GXJE_dt.Rows[z]["JFKMMC"].ToString() + "_金额：" + GXJE_dt.Rows[z]["shijiao"].ToString());
                            }
                        }
                        Console.WriteLine("特殊学历提升:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
            }
            catch (Exception er)
            {
                Console.WriteLine("查询队列进行审核异常： " + DateTime.Now.ToString() + "：" + er.Message);
                insertLog("查询队列进行审核异常：" + er.StackTrace);
            }
        }
        //插入错误日志
        public static void insertLog(string message)
        {
            logmes = new LogMes();
            logmes.OperationTime = DateTime.Now;
            logmes.Ip = Ip;
            logmes.Host = Host;
            logmes.Browser = Browser;
            logmes.Content = message;
            strMessage = new LogFormat().ExceptionFormat(logmes);
            log.log.Error(strMessage);
        }

    }
}
