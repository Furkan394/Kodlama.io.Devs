using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Auths.Dtos
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
