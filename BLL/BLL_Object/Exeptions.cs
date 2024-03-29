﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BLL_Object
{
    [Serializable]
    public class TooLongNumExeption : Exception
    {
        public TooLongNumExeption()
        {
        }

        public TooLongNumExeption(string message) : base(message)
        {
        }

        public TooLongNumExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TooLongNumExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class IncorrectSomethingExeption : Exception
    {
        public IncorrectSomethingExeption()
        {
        }

        public IncorrectSomethingExeption(string message) : base(message)
        {
        }

        public IncorrectSomethingExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IncorrectSomethingExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class NotReadyException : Exception
    {
        public NotReadyException() { }
        public NotReadyException(string message) : base(message) { }
        public NotReadyException(string message, Exception inner) : base(message, inner) { }
        protected NotReadyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

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
    public class SimulationExeption : Exception
    {
        public SimulationExeption()
        {
        }

        public SimulationExeption(string message) : base(message)
        {
        }

        public SimulationExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SimulationExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class AlreadyExistExeption : Exception
    {
        public AlreadyExistExeption()
        {
        }

        public AlreadyExistExeption(string message) : base(message)
        {
        }

        public AlreadyExistExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyExistExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
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
}
