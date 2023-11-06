using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viking.Models.JWTModels
{
    public class RefreshToken
    {
        public string? Token { get; set; }

        public DateTime ExpiryTime { get; set; }

        public DateTime CreatedTime { get; set; }
    }

}
