using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
        [Serializable]
        public class Response
        {
            public Signal Signal { get; set; } = Signal.Ok;
            public string Message { get; set; }
            public object Object { get; set; }
        }

        public enum Signal
        {
            Ok,
            Error
        }
   
}
