using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Business.Exceptions
{
    public class PasswordChangeFailedException : Exception
    {
        public PasswordChangeFailedException(string message) : base(message) 
        {
        }
    }
}
