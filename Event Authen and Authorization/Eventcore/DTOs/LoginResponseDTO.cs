﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventcore.Models
{
    public class LoginResponseDTO
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
