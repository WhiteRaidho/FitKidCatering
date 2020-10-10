using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.ViewModels.Core
{
    public class TokenViewModel
    {
        public string Token { get; set; }
        public string Refresh { get; set; }
        public DateTime Expires { get; set; }
    }
}
