using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Extensions
{
    public static class StringExtensions
    {
        #region Json
        public static TKey DeserializeJson<TKey>(this string json) where TKey : new()
        {
            return !String.IsNullOrEmpty(json) ? JsonConvert.DeserializeObject<TKey>(json) : new TKey();
        }

        public static string SerializeJson<TKey>(this TKey value)
        {
            return value != null ? JsonConvert.SerializeObject(value, Formatting.None) : String.Empty;
        } 
        #endregion
    }
}
