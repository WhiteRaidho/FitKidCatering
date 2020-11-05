using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FitKidCateringApp.Helpers
{
    //HACK Tutaj dodajemy nowe uprawnienia, może pojawić się konieczność podziału na uprawnienia w zależności od modułu
    [GlobalPermissions("Uprawnienia ogólne")]
    public enum StandardPermissions
    {
        [Description("Dostęp administratora")]
        AdminAccess,

        [Description("Pracownik kateringu")]
        CateringEmployee
    }

    public enum PermissionState
    {
        Allow,
        Deny
    }

    public static class Permissions
    {
        #region GetGlobalPermissionsTypes()
        public static List<Type> GetGlobalPermissionsTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(p => p.GetName().Name.StartsWith("FitKidCateringApp"))
                .SelectMany(p => p.GetTypes())
                .Where(p =>
                    p.IsEnum &&
                    p.GetCustomAttribute<GlobalPermissionsAttribute>() != null
                )
                .ToList();
        }
        #endregion

        #region GetGlobalPermissionsGroups()
        public static Dictionary<Type, List<string>> GetGlobalPermissionsGroups()
        {
            return GetGlobalPermissionsTypes()
                .ToDictionary(
                    p => p,
                    p => p.GetCustomAttribute<GlobalPermissionsAttribute>().Name
                );
        }
        #endregion

        #region FromString()
        public static object FromString(string permission)
        {
            if (!permission.Contains("@"))
                throw new Exception($"Nieprawidłowy format nazwy uprawnienia ({permission}). Poprawny format składa się z pełnej nazwy obiektu uprawnień, znaku @ oraz nazwy uprawnienia.");

            var parts = permission.Split('@').ToList();
            var type = GetGlobalPermissionsTypes().Where(p => p.FullName == parts.First()).FirstOrDefault();

            if (type == null)
                throw new Exception($"Podano nieprawidłową nazwę typu uprawnienia ({permission}). Podany typ nie istnieje.");

            return Enum.Parse(type, parts.Last(), true);
        }
        #endregion
    }

    #region GlobalPermissionsAttribute
    public class GlobalPermissionsAttribute : Attribute
    {
        public List<string> Name { get; set; }

        #region GlobalPermissionsAttribute()
        public GlobalPermissionsAttribute(params string[] name)
        {
            Name = name.ToList();
        }
        #endregion
    }
    #endregion

    #region ResourcePermissionsAttribute
    public class ResourcePermissionsAttribute : Attribute
    {
        public List<string> Name { get; set; }

        #region ResourcePermissionsAttribute()
        public ResourcePermissionsAttribute(params string[] name)
        {
            Name = name.ToList();
        }
        #endregion
    }
    #endregion

    #region SectionAttribute
    public class SectionAttribute : Attribute
    {
        public int Number { get; set; }

        #region SectionAttribute()
        public SectionAttribute(int number)
        {
            Number = number;
        }
        #endregion
    }
    #endregion
}
