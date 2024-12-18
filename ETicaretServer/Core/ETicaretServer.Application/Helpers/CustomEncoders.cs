﻿using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.Helpers
{
    public static class CustomEncoders
    {
        public static string UrlEncode(this string value)
        {
            byte[] tokenBytes = Encoding.UTF8.GetBytes(value);
            return WebEncoders.Base64UrlEncode(tokenBytes); //resetToken i url üzerinde tasımak için encode yaptık.

        }
        public static string UrlDecode(this string value)
        {
            byte[] tokenBytes = WebEncoders.Base64UrlDecode(value);
            return Encoding.UTF8.GetString(tokenBytes);
        }
    }
}
