using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.Exceptions.AppUser
{
    public class NorFoundUserException : Exception
    {
        public NorFoundUserException() : base("Kullanıcı veya şifre hatalı")
        {
        }

        public NorFoundUserException(string? message) : base(message)
        {
        }

        public NorFoundUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
