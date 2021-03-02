using System;
using System.Collections.Generic;

namespace PosApiJwt.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime TS { get; set; }
        public bool Active { get; set; }
        public bool Blocked { get; set; }

    }

}
