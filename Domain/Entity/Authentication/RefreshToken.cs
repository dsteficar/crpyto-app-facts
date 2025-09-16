﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Authentication
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string? Token { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
