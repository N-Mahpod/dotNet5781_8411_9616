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

    [Serializable]
    public class BadBusLicenseNumException : Exception
    {
        public BadBusLicenseNumException()
        {
        }

        public BadBusLicenseNumException(string message) : base(message)
        {
        }

        public BadBusLicenseNumException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadBusLicenseNumException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }

    [Serializable]
    public class KeyNotExistExeption : Exception
    {
        public KeyNotExistExeption()
        {
        }

        public KeyNotExistExeption(string message) : base(message)
        {
        }

        public KeyNotExistExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected KeyNotExistExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class BadUserIdException : Exception
    {
        public int ID;
        public BadUserIdException(int id) : base() => ID = id;
        public BadUserIdException(int id, string message) :
            base(message) => ID = id;
        public BadUserIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad user id: {ID}";
    }
}
