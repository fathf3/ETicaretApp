using ETicaretServer.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Application.Features.Commands.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginLoginCommandResponse
    {
        public Token Token { get; set; }
    }
}
