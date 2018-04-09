using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
  class BankruptException : Exception
  {
    public BankruptException()
    {
    }

    public BankruptException(string message)
        : base(message)
    {
    }

    public BankruptException(string message, Exception inner)
        : base(message, inner)
    {
    }
  }
}
