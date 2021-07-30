using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace realtime.log
{
    public class LogMes
    {
        public string Ip { get; set; }
        public string Host { get; set; }
        public string Browser { get; set; }
        public string Content { get; set; }
        public DateTime OperationTime { get; set; }
    }
}
