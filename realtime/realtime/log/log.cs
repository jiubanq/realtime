using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace realtime.log
{
    public class log
    {
        private static log4net.ILog logs = log4net.LogManager.GetLogger("log");
        public static void Error(object message)
        {
            logs.Error(message);
        }
        public static void Debug(object message)
        {
            logs.Debug(message);
        }
    }
}
