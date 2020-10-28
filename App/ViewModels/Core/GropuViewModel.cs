using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.ViewModels.Core
{
    public class GroupViewModel
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public List<List<KeyValuePair<string, string>>> Sections { get; set; }
    }
}
