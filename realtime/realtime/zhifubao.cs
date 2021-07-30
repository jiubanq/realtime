using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace realtime
{
    public class zhifubao
    {
        public zhifubao()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public static void GetFunction(string shenfenID)
        {
            //status=0 查询学生未交费信息,去除searchContext参数则为查询全部未交费学生信息
            string serviceAddress = "https://api.mvc.newxiaoyuan.com/api/ThirdRegularPayment/GetPaymentARData?schoolCode=10097&pageIndex=1&pageSize=9999999&sign=mIiazgpL1C1v3VthtUjQiVjKAch7GaiDhmtEyz6ilmq6Y7CGGI0CKH3eR%2FfP770mH%2BrKMNJnnJQPoJ2t%2BQIURuPWvC2DzwYOffAw%2Fn1Xs2QUXHfoQJ08mz0n9TNfXz9B6P4314dNfss%2BjVNSq3Y2uf0Hu4kCR7u10u0wSaMxRjRO%2FQR1G1Ef4s92k4lRA58sjqriwMkqHEG27KmQ1KaxdfV7vB5LTEFSIuQjyqCB0OQMj1q0LcHx7KJNzsO%2B8oXuJmvy%2FwhXD1Y2hybRaZxFQQazEPxNHB%2BTlgSfgGf7pKnRPSX2E%2F8x%2FE6tj4iY8gkyLy6C5w2QRUTnaDziuTPx1g%3D%3D&status=0&interfaceCode=02&searchContext=" + shenfenID + "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.UseDefaultCredentials = true;
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            JObject s = JsonConvert.DeserializeObject<JObject>(retString);
            try
            {


                foreach (var item in s["data"])//遍历所有信息
                {
                    string id = bond_dropdownlist.fanhui_string("select feiyongID from [collegemis2].[dbo].jbxx_sdkzfbmingxi where feiyongID='" + item["id"] + "'", "sqlcon2");
                    if (id == "0")
                    {
                        bond_dropdownlist.execute("insert into [collegemis2].[dbo].jbxx_sdkzfbmingxi(ID,feiyongID, name, shenfenID, yingjiao, shijiao, jiaofeishijian, out_order_no, jiaofeikemu, dianhua, sfjf, pid,addtime) VALUES('" + Guid.NewGuid() + "','" + item["id"] + "','" + item["st_name"] + "','" + item["passport"] + "','" + item["amount"] + "','" + item["fact_amount"] + "','" + item["pay_time"].ToString() + "','" + item["out_order_no"] + "','" + item["name"] + "','" + item["phone"] + "','" + item["status"] + "','" + item["pid"] + "',getdate())", "sqlcon2");
                    }
                    else
                    {
                        bond_dropdownlist.execute("update [collegemis2].[dbo].jbxx_sdkzfbmingxi set name='" + item["st_name"] + "',yingjiao='" + item["amount"] + "',shijiao='" + item["fact_amount"] + "',jiaofeishijian='" + item["pay_time"].ToString() + "',out_order_no='" + item["out_order_no"] + "',jiaofeikemu='" + item["name"] + "',dianhua='" + item["phone"] + "',sfjf='" + item["status"] + "',pid='" + item["pid"] + "',addtime=getdate() where feiyongID='" + item["id"] + "'", "sqlcon2");
                    }
                }
            }
            catch
            {
                myStreamReader.Close();
                myResponseStream.Close();
            }
            myStreamReader.Close();
            myResponseStream.Close();
        }
        public static void GetFunctionjiaofei(string shenfenID)
        {
            //status=1 查询已缴费信息
            string serviceAddress = "https://api.mvc.newxiaoyuan.com/api/ThirdRegularPayment/GetPaymentARData?schoolCode=10097&pageIndex=1&pageSize=9999999&sign=mIiazgpL1C1v3VthtUjQiVjKAch7GaiDhmtEyz6ilmq6Y7CGGI0CKH3eR%2FfP770mH%2BrKMNJnnJQPoJ2t%2BQIURuPWvC2DzwYOffAw%2Fn1Xs2QUXHfoQJ08mz0n9TNfXz9B6P4314dNfss%2BjVNSq3Y2uf0Hu4kCR7u10u0wSaMxRjRO%2FQR1G1Ef4s92k4lRA58sjqriwMkqHEG27KmQ1KaxdfV7vB5LTEFSIuQjyqCB0OQMj1q0LcHx7KJNzsO%2B8oXuJmvy%2FwhXD1Y2hybRaZxFQQazEPxNHB%2BTlgSfgGf7pKnRPSX2E%2F8x%2FE6tj4iY8gkyLy6C5w2QRUTnaDziuTPx1g%3D%3D&status=1&interfaceCode=02&searchContext=" + shenfenID + "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.UseDefaultCredentials = true;
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            JObject s = JsonConvert.DeserializeObject<JObject>(retString);
            try
            {

                foreach (var item in s["data"])
                {
                    string id = bond_dropdownlist.fanhui_string("select feiyongID from [collegemis2].[dbo].jbxx_sdkzfbmingxi where feiyongID='" + item["id"] + "'", "sqlcon2");
                    if (id == "0")
                    {
                        bond_dropdownlist.execute("insert into [collegemis2].[dbo].jbxx_sdkzfbmingxi(ID,feiyongID, name, shenfenID, yingjiao, shijiao, jiaofeishijian, out_order_no, jiaofeikemu, dianhua, sfjf, pid,addtime) VALUES('" + Guid.NewGuid() + "','" + item["id"] + "','" + item["st_name"] + "','" + item["passport"] + "','" + item["amount"] + "','" + item["fact_amount"] + "','" + item["pay_time"].ToString() + "','" + item["out_order_no"] + "','" + item["name"] + "','" + item["phone"] + "','" + item["status"] + "','" + item["pid"] + "',getdate())", "sqlcon2");
                    }
                    else
                    {
                        bond_dropdownlist.execute("update [collegemis2].[dbo].jbxx_sdkzfbmingxi set name='" + item["st_name"] + "',yingjiao='" + item["amount"] + "',shijiao='" + item["fact_amount"] + "',jiaofeishijian='" + item["pay_time"].ToString() + "',out_order_no='" + item["out_order_no"] + "',jiaofeikemu='" + item["name"] + "',dianhua='" + item["phone"] + "',sfjf='" + item["status"] + "',pid='" + item["pid"] + "',addtime=getdate() where feiyongID='" + item["id"] + "'", "sqlcon2");
                    }
                }
            }
            catch
            {
                myStreamReader.Close();
                myResponseStream.Close();
            }
            myStreamReader.Close();
            myResponseStream.Close();
        }
        public static void zfbshanchu(string sfz)//删除支付宝缴费信息,并且自动重新生成支付宝费用
        {
            DataTable dt = bond_dropdownlist.fanhui_ds("select * from [collegemis2].[dbo].jbxx_sdkzfbmingxi where shenfenID='" + sfz + "'").Tables[0];//查询支付宝缴费明细
            string sczt = string.Empty;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string serviceAddress = "https://api.mvc.newxiaoyuan.com/api/ThirdRegularPayment/DeletePaymentARData";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    string strContent = @"{ ""schoolCode"": ""10097"",""interfaceCode"": ""02"",""id"":"; strContent += @"""" + dt.Rows[i]["feiyongID"].ToString() + ""; strContent += @""",""sign"": ""mIiazgpL1C1v3VthtUjQiVjKAch7GaiDhmtEyz6ilmq6Y7CGGI0CKH3eR/fP770mH+rKMNJnnJQPoJ2t+QIURuPWvC2DzwYOffAw/n1Xs2QUXHfoQJ08mz0n9TNfXz9B6P4314dNfss+jVNSq3Y2uf0Hu4kCR7u10u0wSaMxRjRO/QR1G1Ef4s92k4lRA58sjqriwMkqHEG27KmQ1KaxdfV7vB5LTEFSIuQjyqCB0OQMj1q0LcHx7KJNzsO+8oXuJmvy/whXD1Y2hybRaZxFQQazEPxNHB+TlgSfgGf7pKnRPSX2E/8x/E6tj4iY8gkyLy6C5w2QRUTnaDziuTPx1g==""}";//schoolcode:学校固定代码，id:支付宝缴费ID，sign：固定签名
                    using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
                    {
                        dataStream.Write(strContent);
                        dataStream.Close();
                    }
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    string encoding = response.ContentEncoding;
                    if (encoding == null || encoding.Length < 1)
                    {
                        encoding = "UTF-8"; //默认编码  
                    }
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                    string retString = reader.ReadToEnd();
                    //解析josn
                    JObject jo = JObject.Parse(retString);
                    sczt = jo["msg"].ToString();
                }
                if (sczt == "删除成功")//删除成功后新增学生的新缴费信息
                {

                }
            }

        }
        public static void PostFunctionxg(DataTable dt)//新增学生费用
        {
            string jine = string.Empty;
            string pici = string.Empty;
            string jfkm = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jine = dt.Rows[i]["ZYFXKMFY_JE"].ToString();
                pici = dt.Rows[i]["缴费批次"].ToString();
                jfkm = dt.Rows[i]["JFKM_KMMC"].ToString();
                if (dt.Rows[i]["JFKM_KMMC"].ToString() == "学费")//档缴费金额为学费是查看学生之前是否有游学费以及助学贷款
                {
                    string youxuefuwufei = bond_dropdownlist.fanhui_string("select isnull(sum(shoufeijine),0) from [HXZS].[dbo].[a_zsgl_yijianyouxuefuwu] where shenfenID='" + dt.Rows[i]["shenfenID"].ToString() + "'");
                    if (youxuefuwufei != "0")//如果游学费有则总费用减去游学费并且把批次号已经缴费名称改为已减预交状态
                    {
                        jine = (float.Parse(jine) - float.Parse(youxuefuwufei)).ToString();
                        pici = "201907241401";
                        jfkm = "学费（已减预交）";
                    }
                    string zhuxuedaikuan = bond_dropdownlist.fanhui_string("select isnull(sum(daikuanjine), 0) from a_zsgl_bd_zhuxuedaikuanygx where shenfenID = '" + dt.Rows[i]["shenfenID"].ToString() + "'");
                    if (zhuxuedaikuan != "0")//如果助学贷款已审核有则总费用减去助学贷款并且把批次号已经缴费名称改为已减助学贷款状态
                    {
                        jine = (float.Parse(jine) - float.Parse(zhuxuedaikuan)).ToString();
                        pici = "201907251642";
                        jfkm = "学费（已减助学贷款）";
                    }
                }
                string serviceAddress = "https://api.mvc.newxiaoyuan.com/api/ThirdRegularPayment/SavePaymentAR";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
                request.UseDefaultCredentials = true;
                request.Method = "POST";
                request.ContentType = "application/json";
                string strContent = @"{ ""schoolCode"":""10097"",""interfaceCode"": ""02"",""arId"":"; strContent += @"""" + pici + ""; strContent += @""",""name"":"; strContent += @"""" + jfkm + ""; strContent += @""",""price"":"; strContent += @"""" + jine + ""; strContent += @""",""accountPid"": ""2088531732729483"",""studentName"":"; strContent += @"""" + dt.Rows[i]["xingming"].ToString() + ""; strContent += @""",""passport"":"; strContent += @"""" + dt.Rows[i]["shenfenID"].ToString() + ""; strContent += @""",""className"": ""无班级"",""sign"": ""mIiazgpL1C1v3VthtUjQiVjKAch7GaiDhmtEyz6ilmq6Y7CGGI0CKH3eR/fP770mH+rKMNJnnJQPoJ2t+QIURuPWvC2DzwYOffAw/n1Xs2QUXHfoQJ08mz0n9TNfXz9B6P4314dNfss+jVNSq3Y2uf0Hu4kCR7u10u0wSaMxRjRO/QR1G1Ef4s92k4lRA58sjqriwMkqHEG27KmQ1KaxdfV7vB5LTEFSIuQjyqCB0OQMj1q0LcHx7KJNzsO+8oXuJmvy/whXD1Y2hybRaZxFQQazEPxNHB+TlgSfgGf7pKnRPSX2E/8x/E6tj4iY8gkyLy6C5w2QRUTnaDziuTPx1g==""}";
                using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
                {
                    dataStream.Write(strContent);
                    dataStream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码  
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                string retString = reader.ReadToEnd();
                //解析josn
                JObject jo = JObject.Parse(retString);
            }
        }
        public static string PostFunctionsdf(zhifubaojiaofei jiaofei)//新增学生费用
        {
            string serviceAddress = "https://api.mvc.newxiaoyuan.com/api/ThirdRegularPayment/SavePaymentAR";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.UseDefaultCredentials = true;
            request.Method = "POST";
            request.ContentType = "application/json";
            string strContent = @"{ ""schoolCode"":""10097"",""interfaceCode"": ""02"",""arId"":"; strContent += @"""" + jiaofei.piciID + ""; strContent += @""",""name"":"; strContent += @"""" + jiaofei.jiaofeimingcheng + ""; strContent += @""",""price"":"; strContent += @"""" + jiaofei.jine + ""; strContent += @""",""accountPid"": ""2088531732729483"",""studentName"":"; strContent += @"""" + jiaofei.xingming + ""; strContent += @""",""passport"":"; strContent += @"""" + jiaofei.shenfenID + ""; strContent += @""",""className"": ""无班级"",""sign"": ""mIiazgpL1C1v3VthtUjQiVjKAch7GaiDhmtEyz6ilmq6Y7CGGI0CKH3eR/fP770mH+rKMNJnnJQPoJ2t+QIURuPWvC2DzwYOffAw/n1Xs2QUXHfoQJ08mz0n9TNfXz9B6P4314dNfss+jVNSq3Y2uf0Hu4kCR7u10u0wSaMxRjRO/QR1G1Ef4s92k4lRA58sjqriwMkqHEG27KmQ1KaxdfV7vB5LTEFSIuQjyqCB0OQMj1q0LcHx7KJNzsO+8oXuJmvy/whXD1Y2hybRaZxFQQazEPxNHB+TlgSfgGf7pKnRPSX2E/8x/E6tj4iY8gkyLy6C5w2QRUTnaDziuTPx1g==""}";
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(strContent);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            //解析josn
            JObject jo = JObject.Parse(retString);
            if (jo["msg"].ToString() == "添加成功")
            {
                return "成功";
            }
            else
            {
                return "失败";
            }
        }
        public static void PostFunctionsdfry(string xiaohao)//添加人员信息
        {
            string sql = string.Empty;
            string nianji = xiaohao.Substring(0, 2);
            string sqlcon = string.Empty;
            if (int.Parse(nianji) >= 19)
            {
                sqlcon = "sqlcon3";
                sql = @" select * into #temp from DO_HXBscXSXXXZ where XSXX_XH='" + xiaohao + "'"; sql += @"
                        select '学生卡' as '卡类型',XSXX_SFZH as shenfenID,XSXX_XM as xingming,XSXX_SFZH as sfz,'青岛恒星科技学院/'+OrgName+'/无分系/无分班|' as xueyuan,case when zhuanyeleixing='本科' then '2023-07-01 00:00:00.000' else '2024-07-01 00:00:00.000' end as youxiaoqi from #temp a
 left join DO_HXBscXSXXZY b on a.XSXX_XH=b.XSZY_XH
 left join jbxx_zhuanyefangxiang c on b.XSZY_ZYFXBH=c.zhuanyefangxiangbianhao
 left join DO_HXBscXJZRB d on a.XSXX_BJBH=d.ID
 drop table #temp";
            }
            else
            {
                sqlcon = "sqlcon2";
                sql = @"select * into #temp from jbxx_xueshengxinxi where xueshengxiaohao='" + xiaohao + "'"; sql += @"
                         select '学生卡' as '卡类型',shenfenID as shenfenID,xingming,shenfenID as sfz,'青岛恒星科技学院/'+jigoumingcheng+'/无分系/无分班|' as xueyuan,case when zhuanyeleixing='本科' then '2023-07-01 00:00:00.000' else '2024-07-01 00:00:00.000' end as youxiaoqi from #temp a
left join jbxx_zhuanyefangxiang b on a.zhuanyefangxiangbianhao=b.zhuanyefangxiangbianhao
left join jbxx_banjixinxi c on a.banjibianhao=c.banjibianhao
left join jbxx_zhuanyeleixing d on a.zhuanyeleixingbianhao=d.zhuanyeleixingbianhao
left join jbxx_jigou e on a.yuanxibianhao=e.jigoubianhao
drop table #temp";
            }

            DataSet ds = bond_dropdownlist.fanhui_ds(sql, sqlcon);
            string serviceAddress = "http://apitext.newxiaoyuan.com/api/Users/AddUserInfoAndDepartment";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.UseDefaultCredentials = true;
            request.Method = "POST";
            request.ContentType = "application/json";
            string strContent = @"{""schoolcode"":""10097"",""interfaceCode"": ""02"",""sign"":""181ead3d433972a9679c7e34f979a817"",""data"":"; strContent += @"[{""class_id"":""1"",""student_id"":"; strContent += @"""" + ds.Tables[0].Rows[0]["shenfenID"].ToString() + ""; strContent += @""",""validity_time"":"; strContent += @"""" + ds.Tables[0].Rows[0]["youxiaoqi"].ToString() + ""; strContent += @""",""user_name"":"; strContent += @"""" + ds.Tables[0].Rows[0]["xingming"].ToString() + ""; strContent += @""",""pass_port"":"; strContent += @"""" + ds.Tables[0].Rows[0]["shenfenID"].ToString() + ""; strContent += @""",""depart_ment"":"; strContent += @"""" + ds.Tables[0].Rows[0]["xueyuan"].ToString() + ""; strContent += @""",""isMultipleIdentities"":""0"",""card_name"":""学生卡""}]}";
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(strContent);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            //解析josn
            JObject jo = JObject.Parse(retString);
            if (jo["msg"].ToString() == "上传成功")
            {
                if (bond_dropdownlist.fanhui_string("select * from [a_xueshengjiaofeizhifubao$] where 身份证号='" + ds.Tables[0].Rows[0]["shenfenID"].ToString() + "'", "sqlcon2") == "0")
                {
                    bond_dropdownlist.execute("insert into [a_xueshengjiaofeizhifubao$](卡类型,学工号,姓名,身份证号,部门信息,有效期,是否多身份,是否为学生)values('" + ds.Tables[0].Rows[0]["卡类型"].ToString() + "','" + ds.Tables[0].Rows[0]["sfz"].ToString() + "','" + ds.Tables[0].Rows[0]["xingming"].ToString() + "','" + ds.Tables[0].Rows[0]["sfz"].ToString() + "','" + ds.Tables[0].Rows[0]["xueyuan"].ToString() + "','" + ds.Tables[0].Rows[0]["youxiaoqi"].ToString() + "','0','0')", "sqlcon2");
                }
            }
            else
            {

            }
        }
        public static void jsPostFunctionsdfry(string xiaohao)//添加教师信息
        {
            string sql = string.Empty;
            string sqlcon = string.Empty;

            sqlcon = "sqlcon3";
            sql = @" select '教师卡' as '卡类型',HRZGZD_SFZH as shenfenID,LSZGZD_ZGXM as xingming,HRZGZD_SFZH as sfz,'青岛恒星科技学院/无学院/无分系/教职工' as xueyuan,'2029-07-01 00:00:00.000' as youxiaoqi from HRZGZD a
left join HROrgRelation b on a.HRZGZD_HRBMNM=b.ParentNM where HRZGZD_ZGBH='" + xiaohao + "'";
            DataSet ds = bond_dropdownlist.fanhui_ds(sql, sqlcon);
            string serviceAddress = "http://apitext.newxiaoyuan.com/api/Users/AddUserInfoAndDepartment";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "POST";
            request.ContentType = "application/json";
            string strContent = @"{""schoolcode"":""10097"",""interfaceCode"": ""02"",""sign"":""181ead3d433972a9679c7e34f979a817"",""data"":"; strContent += @"[{""class_id"":""2"",""student_id"":"; strContent += @"""" + ds.Tables[0].Rows[0]["shenfenID"].ToString() + ""; strContent += @""",""validity_time"":"; strContent += @"""" + ds.Tables[0].Rows[0]["youxiaoqi"].ToString() + ""; strContent += @""",""user_name"":"; strContent += @"""" + ds.Tables[0].Rows[0]["xingming"].ToString() + ""; strContent += @""",""pass_port"":"; strContent += @"""" + ds.Tables[0].Rows[0]["shenfenID"].ToString() + ""; strContent += @""",""depart_ment"":"; strContent += @"""" + ds.Tables[0].Rows[0]["xueyuan"].ToString() + ""; strContent += @""",""isMultipleIdentities"":""0"",""card_name"":""教师卡""}]}";
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(strContent);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            //解析josn
            JObject jo = JObject.Parse(retString);
            if (jo["msg"].ToString() == "上传成功")
            {
                if (bond_dropdownlist.fanhui_string("select * from [a_xueshengjiaofeizhifubao$] where 身份证号='" + ds.Tables[0].Rows[0]["shenfenID"].ToString() + "'", "sqlcon2") == "0")
                {
                    bond_dropdownlist.execute("insert into [a_xueshengjiaofeizhifubao$](卡类型,学工号,姓名,身份证号,部门信息,有效期,是否多身份,是否为学生)values('" + ds.Tables[0].Rows[0]["卡类型"].ToString() + "','" + ds.Tables[0].Rows[0]["sfz"].ToString() + "','" + ds.Tables[0].Rows[0]["xingming"].ToString() + "','" + ds.Tables[0].Rows[0]["sfz"].ToString() + "','" + ds.Tables[0].Rows[0]["xueyuan"].ToString() + "','" + ds.Tables[0].Rows[0]["youxiaoqi"].ToString() + "','0','0')", "sqlcon2");
                }
            }
            else
            {

            }
        }
        public static void sdfjsPostFunctionsdfry(string xiaohao)//添加教师信息
        {
            string sql = string.Empty;
            string sqlcon = string.Empty;
            if (xiaohao.Contains('j') || xiaohao.Contains('J'))
            {
                sqlcon = "sqlcon3";
                sql = @" select '教师卡' as '卡类型',HRZGZD_SFZH as shenfenID,LSZGZD_ZGXM as xingming,HRZGZD_SFZH as sfz,'青岛恒星科技学院/无学院/无分系/教职工' as xueyuan,'2029-07-01 00:00:00.000' as youxiaoqi from HRZGZD a
left join HROrgRelation b on a.HRZGZD_HRBMNM=b.ParentNM where HRZGZD_ZGBH='" + xiaohao + "'";
            }
            else
            {
                sqlcon = "sqlcon2";
                sql = @"select '教师卡' as '卡类型',shenfenID,Ownername as xingming,shenfenID as sfz,'青岛恒星科技学院/无学院/无分系/教职工' as xueyuan,'2029-07-01 00:00:00.000' as youxiaoqi from jsgy_renyuanxinxi a
 left join Sdn_Ownerinfo b on a.xiaohao=b.ownerBH where CHARINDEX('j',xiaohao)=0 and xiaohao='" + xiaohao + "' ";
            }
            DataSet ds = bond_dropdownlist.fanhui_ds(sql, sqlcon);
            string serviceAddress = "http://apitext.newxiaoyuan.com/api/Users/AddUserInfoAndDepartment";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.UseDefaultCredentials = true;
            request.Method = "POST";
            request.ContentType = "application/json";
            string strContent = @"{""schoolcode"":""10097"",""interfaceCode"": ""02"",""sign"":""181ead3d433972a9679c7e34f979a817"",""data"":"; strContent += @"[{""class_id"":""2"",""student_id"":"; strContent += @"""" + ds.Tables[0].Rows[0]["shenfenID"].ToString() + ""; strContent += @""",""validity_time"":"; strContent += @"""" + ds.Tables[0].Rows[0]["youxiaoqi"].ToString() + ""; strContent += @""",""user_name"":"; strContent += @"""" + ds.Tables[0].Rows[0]["xingming"].ToString() + ""; strContent += @""",""pass_port"":"; strContent += @"""" + ds.Tables[0].Rows[0]["shenfenID"].ToString() + ""; strContent += @""",""depart_ment"":"; strContent += @"""" + ds.Tables[0].Rows[0]["xueyuan"].ToString() + ""; strContent += @""",""isMultipleIdentities"":""0"",""card_name"":""教师卡""}]}";
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(strContent);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            //解析josn
            JObject jo = JObject.Parse(retString);
            if (jo["msg"].ToString() == "上传成功")
            {
                if (bond_dropdownlist.fanhui_string("select * from [a_xueshengjiaofeizhifubao$] where 身份证号='" + ds.Tables[0].Rows[0]["shenfenID"].ToString() + "'", "sqlcon2") == "0")
                {
                    bond_dropdownlist.execute("insert into [a_xueshengjiaofeizhifubao$](卡类型,学工号,姓名,身份证号,部门信息,有效期,是否多身份,是否为学生)values('" + ds.Tables[0].Rows[0]["卡类型"].ToString() + "','" + ds.Tables[0].Rows[0]["sfz"].ToString() + "','" + ds.Tables[0].Rows[0]["xingming"].ToString() + "','" + ds.Tables[0].Rows[0]["sfz"].ToString() + "','" + ds.Tables[0].Rows[0]["xueyuan"].ToString() + "','" + ds.Tables[0].Rows[0]["youxiaoqi"].ToString() + "','0','0')", "sqlcon2");
                }
            }
            else
            {

            }
        }
        public static void cwGetFunction(string shenfenID)
        {
            //status=0 查询学生未交费信息,去除searchContext参数则为查询全部未交费学生信息
            string serviceAddress = "https://api.mvc.newxiaoyuan.com/api/ThirdRegularPayment/GetPaymentARData?schoolCode=10097&pageIndex=1&pageSize=9999999&sign=mIiazgpL1C1v3VthtUjQiVjKAch7GaiDhmtEyz6ilmq6Y7CGGI0CKH3eR%2FfP770mH%2BrKMNJnnJQPoJ2t%2BQIURuPWvC2DzwYOffAw%2Fn1Xs2QUXHfoQJ08mz0n9TNfXz9B6P4314dNfss%2BjVNSq3Y2uf0Hu4kCR7u10u0wSaMxRjRO%2FQR1G1Ef4s92k4lRA58sjqriwMkqHEG27KmQ1KaxdfV7vB5LTEFSIuQjyqCB0OQMj1q0LcHx7KJNzsO%2B8oXuJmvy%2FwhXD1Y2hybRaZxFQQazEPxNHB%2BTlgSfgGf7pKnRPSX2E%2F8x%2FE6tj4iY8gkyLy6C5w2QRUTnaDziuTPx1g%3D%3D&status=0&interfaceCode=02&searchContext=" + shenfenID + "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.UseDefaultCredentials = true;
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            JObject s = JsonConvert.DeserializeObject<JObject>(retString);
            foreach (var item in s["data"])//遍历所有信息
            {
                string id = bond_dropdownlist.fanhui_string("select feiyongID from [collegemis2].[dbo].jbxx_sdfzfbmingxi where feiyongID='" + item["id"] + "'", "sqlcon2");
                if (id == "0")
                {
                    bond_dropdownlist.execute("insert into [collegemis2].[dbo].jbxx_sdfzfbmingxi(ID,feiyongID, name, shenfenID, yingjiao, shijiao, jiaofeishijian, out_order_no, jiaofeikemu, dianhua, sfjf, pid,addtime) VALUES('" + Guid.NewGuid() + "','" + item["id"] + "','" + item["st_name"] + "','" + item["passport"] + "','" + item["amount"] + "','" + item["fact_amount"] + "','" + item["pay_time"].ToString() + "','" + item["out_order_no"] + "','" + item["name"] + "','" + item["phone"] + "','" + item["status"] + "','" + item["pid"] + "',getdate())", "sqlcon2");
                }
                else
                {
                    bond_dropdownlist.execute("update [collegemis2].[dbo].jbxx_sdfzfbmingxi set name='" + item["st_name"] + "',yingjiao='" + item["amount"] + "',shijiao='" + item["fact_amount"] + "',jiaofeishijian='" + item["pay_time"].ToString() + "',out_order_no='" + item["out_order_no"] + "',jiaofeikemu='" + item["name"] + "',dianhua='" + item["phone"] + "',sfjf='" + item["status"] + "',pid='" + item["pid"] + "',addtime=getdate() where feiyongID='" + item["id"] + "'", "sqlcon2");
                }
            }
            myStreamReader.Close();
            myResponseStream.Close();
        }
        public static void cwGetFunctionjiaofei(string shenfenID)
        {
            //status=1 查询已缴费信息
            string serviceAddress = "https://api.mvc.newxiaoyuan.com/api/ThirdRegularPayment/GetPaymentARData?schoolCode=10097&pageIndex=1&pageSize=9999999&sign=mIiazgpL1C1v3VthtUjQiVjKAch7GaiDhmtEyz6ilmq6Y7CGGI0CKH3eR%2FfP770mH%2BrKMNJnnJQPoJ2t%2BQIURuPWvC2DzwYOffAw%2Fn1Xs2QUXHfoQJ08mz0n9TNfXz9B6P4314dNfss%2BjVNSq3Y2uf0Hu4kCR7u10u0wSaMxRjRO%2FQR1G1Ef4s92k4lRA58sjqriwMkqHEG27KmQ1KaxdfV7vB5LTEFSIuQjyqCB0OQMj1q0LcHx7KJNzsO%2B8oXuJmvy%2FwhXD1Y2hybRaZxFQQazEPxNHB%2BTlgSfgGf7pKnRPSX2E%2F8x%2FE6tj4iY8gkyLy6C5w2QRUTnaDziuTPx1g%3D%3D&status=1&interfaceCode=02&searchContext=" + shenfenID + "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.UseDefaultCredentials = true;
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            JObject s = JsonConvert.DeserializeObject<JObject>(retString);
            foreach (var item in s["data"])
            {
                string id = bond_dropdownlist.fanhui_string("select feiyongID from [collegemis2].[dbo].jbxx_sdfzfbmingxi where feiyongID='" + item["id"] + "'", "sqlcon2");
                if (id == "0")
                {
                    bond_dropdownlist.execute("insert into [collegemis2].[dbo].jbxx_sdfzfbmingxi(ID,feiyongID, name, shenfenID, yingjiao, shijiao, jiaofeishijian, out_order_no, jiaofeikemu, dianhua, sfjf, pid,addtime) VALUES('" + Guid.NewGuid() + "','" + item["id"] + "','" + item["st_name"] + "','" + item["passport"] + "','" + item["amount"] + "','" + item["fact_amount"] + "','" + item["pay_time"].ToString() + "','" + item["out_order_no"] + "','" + item["name"] + "','" + item["phone"] + "','" + item["status"] + "','" + item["pid"] + "',getdate())", "sqlcon2");
                }
                else
                {
                    bond_dropdownlist.execute("update [collegemis2].[dbo].jbxx_sdfzfbmingxi set name='" + item["st_name"] + "',yingjiao='" + item["amount"] + "',shijiao='" + item["fact_amount"] + "',jiaofeishijian='" + item["pay_time"].ToString() + "',out_order_no='" + item["out_order_no"] + "',jiaofeikemu='" + item["name"] + "',dianhua='" + item["phone"] + "',sfjf='" + item["status"] + "',pid='" + item["pid"] + "',addtime=getdate() where feiyongID='" + item["id"] + "'", "sqlcon2");
                }
            }
            myStreamReader.Close();
            myResponseStream.Close();
        }
        #region 统一方法
        /// <summary>
        /// 最新统一获取缴费信息
        /// </summary>
        /// <param name=""></param>
        public static void Getjiaofei(string shenfenID, string xiangmu, string biaoming, string sqlcon)
        {
            //status=1 查询已缴费信息
            string serviceAddress = "https://api.mvc.newxiaoyuan.com/api/ThirdRegularPayment/GetPaymentARData?schoolCode=10097&pageIndex=1&pageSize=9999999&sign=mIiazgpL1C1v3VthtUjQiVjKAch7GaiDhmtEyz6ilmq6Y7CGGI0CKH3eR%2FfP770mH%2BrKMNJnnJQPoJ2t%2BQIURuPWvC2DzwYOffAw%2Fn1Xs2QUXHfoQJ08mz0n9TNfXz9B6P4314dNfss%2BjVNSq3Y2uf0Hu4kCR7u10u0wSaMxRjRO%2FQR1G1Ef4s92k4lRA58sjqriwMkqHEG27KmQ1KaxdfV7vB5LTEFSIuQjyqCB0OQMj1q0LcHx7KJNzsO%2B8oXuJmvy%2FwhXD1Y2hybRaZxFQQazEPxNHB%2BTlgSfgGf7pKnRPSX2E%2F8x%2FE6tj4iY8gkyLy6C5w2QRUTnaDziuTPx1g%3D%3D&status=1&interfaceCode=02&searchContext=" + shenfenID + "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.UseDefaultCredentials = true;
            request.Timeout =  1000*1000;
            request.KeepAlive = true;
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            JObject s = JsonConvert.DeserializeObject<JObject>(retString);
            try
            {


                foreach (var item in s["data"])
                {
                    string id = bond_dropdownlist.fanhui_string("select feiyongID from " + biaoming + " where feiyongID='" + item["id"] + "'", sqlcon);

                    if (id == "0")
                    {
                        bond_dropdownlist.execute("insert into " + biaoming + "(ID,feiyongID, name, shenfenID, yingjiao, shijiao, jiaofeishijian, out_order_no, jiaofeikemu, dianhua, sfjf, pid,addtime) VALUES('" + Guid.NewGuid() + "','" + item["id"] + "','" + item["st_name"] + "','" + item["passport"] + "','" + item["amount"] + "','" + item["fact_amount"] + "','" + item["pay_time"].ToString() + "','" + item["out_order_no"] + "','" + item["name"] + "','" + item["phone"] + "','" + item["status"] + "','" + item["pid"] + "',getdate())", sqlcon);
                        Console.WriteLine("支付宝信息更新成功： " + item["passport"] + ":" + DateTime.Now.ToString() );
                    }
                    else
                    {
                        bond_dropdownlist.execute("update " + biaoming + " set name='" + item["st_name"] + "',yingjiao='" + item["amount"] + "',shijiao='" + item["fact_amount"] + "',jiaofeishijian='" + item["pay_time"].ToString() + "',out_order_no='" + item["out_order_no"] + "',jiaofeikemu='" + item["name"] + "',dianhua='" + item["phone"] + "',sfjf='" + item["status"] + "',pid='" + item["pid"] + "',addtime=getdate() where feiyongID='" + item["id"] + "'", sqlcon);
                    }
                }
            }
            catch(Exception ex)
            {
                myStreamReader.Close();
                myResponseStream.Close();
            }
            myStreamReader.Close();
            myResponseStream.Close();
        }
        /// <summary>
        /// 统一获取未交费信息
        /// </summary>
        /// <param name="shenfenID"></param>
        /// <param name="xiangmu"></param>
        /// <param name="biaoming"></param>
        /// <param name="sqlcon"></param>
        public static void Getweijiaofei(string shenfenID, string xiangmu, string biaoming, string sqlcon)
        {
            //status=0 查询学生未交费信息,去除searchContext参数则为查询全部未交费学生信息
            string serviceAddress = "https://api.mvc.newxiaoyuan.com/api/ThirdRegularPayment/GetPaymentARData?schoolCode=10097&pageIndex=1&pageSize=9999999&sign=mIiazgpL1C1v3VthtUjQiVjKAch7GaiDhmtEyz6ilmq6Y7CGGI0CKH3eR%2FfP770mH%2BrKMNJnnJQPoJ2t%2BQIURuPWvC2DzwYOffAw%2Fn1Xs2QUXHfoQJ08mz0n9TNfXz9B6P4314dNfss%2BjVNSq3Y2uf0Hu4kCR7u10u0wSaMxRjRO%2FQR1G1Ef4s92k4lRA58sjqriwMkqHEG27KmQ1KaxdfV7vB5LTEFSIuQjyqCB0OQMj1q0LcHx7KJNzsO%2B8oXuJmvy%2FwhXD1Y2hybRaZxFQQazEPxNHB%2BTlgSfgGf7pKnRPSX2E%2F8x%2FE6tj4iY8gkyLy6C5w2QRUTnaDziuTPx1g%3D%3D&status=0&interfaceCode=02&searchContext=" + shenfenID + "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.UseDefaultCredentials = true;
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            JObject s = JsonConvert.DeserializeObject<JObject>(retString);
            foreach (var item in s["data"])//遍历所有信息
            {

                string id = bond_dropdownlist.fanhui_string("select feiyongID from " + biaoming + " where feiyongID='" + item["id"] + "'", sqlcon);
                if (id == "0")
                {
                    bond_dropdownlist.execute("insert into " + biaoming + "(ID,feiyongID, name, shenfenID, yingjiao, shijiao, jiaofeishijian, out_order_no, jiaofeikemu, dianhua, sfjf, pid,addtime) VALUES('" + Guid.NewGuid() + "','" + item["id"] + "','" + item["st_name"] + "','" + item["passport"] + "','" + item["amount"] + "','" + item["fact_amount"] + "','" + item["pay_time"].ToString() + "','" + item["out_order_no"] + "','" + item["name"] + "','" + item["phone"] + "','" + item["status"] + "','" + item["pid"] + "',getdate())", sqlcon);
                }
                else
                {
                    bond_dropdownlist.execute("update " + biaoming + " set name='" + item["st_name"] + "',yingjiao='" + item["amount"] + "',shijiao='" + item["fact_amount"] + "',jiaofeishijian='" + item["pay_time"].ToString() + "',out_order_no='" + item["out_order_no"] + "',jiaofeikemu='" + item["name"] + "',dianhua='" + item["phone"] + "',sfjf='" + item["status"] + "',pid='" + item["pid"] + "',addtime=getdate() where feiyongID='" + item["id"] + "'", sqlcon);
                }
            }
            myStreamReader.Close();
            myResponseStream.Close();
        }
        #endregion

    }
}
