﻿
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Employee : IdentityUser
    {
        [MaxLength(100)]
        public string FullName { get; set; }
        public virtual List<RequestForm> RequestForms { get; set; }
    }
}
