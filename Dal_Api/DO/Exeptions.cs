using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Api.DO
{
    [Serializable]
    public class LnNotExistExeption : Exception
    {
        public LnNotExistExeption()
        {
        }

        public LnNotExistExeption(string message) : base(message)
        {
        }

        public LnNotExistExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LnNotExistExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
