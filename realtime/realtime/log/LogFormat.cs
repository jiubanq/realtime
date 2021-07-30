using System.Text;
namespace realtime.log
{
    public class LogFormat
    {
        /// <summary>
        /// 生成错误
        /// </summary>
        /// <param name="logMessage">对象</param>
        /// <returns></returns>
        public string ErrorFormat(LogMes logMessage)
        {
            StringBuilder strInfo = new StringBuilder();
            strInfo.Append("1. 错误: >> 操作时间: " + logMessage.OperationTime + "    \r\n");
            strInfo.Append("4. Ip  : " + logMessage.Ip + "   主机: " + logMessage.Host + "   浏览器: " + logMessage.Browser + "    \r\n");
            strInfo.Append("5. 内容: " + logMessage.Content + "\r\n");
            strInfo.Append("-----------------------------------------------------------------------------------------------------------------------------\r\n");
            return strInfo.ToString();
        }
        /// <summary>
        /// 生成警告
        /// </summary>
        /// <param name="logMessage">对象</param>
        /// <returns></returns>
        public string WarnFormat(LogMes logMessage)
        {
            StringBuilder strInfo = new StringBuilder();
            strInfo.Append("1. 警告: >> 操作时间: " + logMessage.OperationTime + "  \r\n");
            strInfo.Append("4. Ip  : " + logMessage.Ip + "   主机: " + logMessage.Host + "   浏览器: " + logMessage.Browser + "    \r\n");
            strInfo.Append("5. 内容: " + logMessage.Content + "\r\n");
            strInfo.Append("-----------------------------------------------------------------------------------------------------------------------------\r\n");
            return strInfo.ToString();
        }
        /// <summary>
        /// 生成信息
        /// </summary>
        /// <param name="logMessage">对象</param>
        /// <returns></returns>
        public string InfoFormat(LogMes logMessage)
        {
            StringBuilder strInfo = new StringBuilder();
            strInfo.Append("1. 信息: >> 操作时间: " + logMessage.OperationTime + " \r\n");
            strInfo.Append("4. Ip  : " + logMessage.Ip + "   主机: " + logMessage.Host + "   浏览器: " + logMessage.Browser + "    \r\n");
            strInfo.Append("5. 内容: " + logMessage.Content + "\r\n");
            strInfo.Append("-----------------------------------------------------------------------------------------------------------------------------\r\n");
            return strInfo.ToString();
        }
        /// <summary>
        /// 生成调试
        /// </summary>
        /// <param name="logMessage">对象</param>
        /// <returns></returns>
        public string DebugFormat(LogMes logMessage)
        {
            StringBuilder strInfo = new StringBuilder();
            strInfo.Append("1. 调试: >> 操作时间: " + logMessage.OperationTime + " \r\n");
            strInfo.Append("4. Ip  : " + logMessage.Ip + "   主机: " + logMessage.Host + "   浏览器: " + logMessage.Browser + "    \r\n");
            strInfo.Append("5. 内容: " + logMessage.Content + "\r\n");
            strInfo.Append("-----------------------------------------------------------------------------------------------------------------------------\r\n");
            return strInfo.ToString();
        }
        /// <summary>
        /// 生成异常信息
        /// </summary>
        /// <param name="logMessage">对象</param>
        /// <returns></returns>
        public string ExceptionFormat(LogMes logMessage)
        {
            StringBuilder strInfo = new StringBuilder();
            strInfo.Append("1. 异常: >> 操作时间: " + logMessage.OperationTime + "  \r\n");
            strInfo.Append("4. 主机: " + logMessage.Host + "   Ip  : " + logMessage.Ip + "   浏览器: " + logMessage.Browser + "    \r\n");
            strInfo.Append("5. 异常: " + logMessage.Content + "\r\n");
            strInfo.Append("-----------------------------------------------------------------------------------------------------------------------------\r\n");
            return strInfo.ToString();
        }
    }
}
