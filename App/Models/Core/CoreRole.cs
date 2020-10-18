using FitKidCateringApp.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Models.Core
{
    [Table("CoreRoles")]
    public class CoreRole : Entity
    {
        [Required]
        public string RoleName { get; set; }
    }
}
