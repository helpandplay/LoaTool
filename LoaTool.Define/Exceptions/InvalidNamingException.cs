using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoaTool.Define.Exceptions;
public class InvalidNamingException : Exception
{
    public InvalidNamingException()
    {
    }

    public InvalidNamingException(string message)
        : base(message)
    {
    }

    public InvalidNamingException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
