using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Extensoes
{
    internal static class ReflectionExtensao
    {
        public static List<PropertyInfo> ObterPropriedades<T>(this T origem) {
            return typeof(T).ObterPropriedades();
        }

        public static List<PropertyInfo> ObterPropriedades(this Type tipo)
        {
            return tipo.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
        }
    }
}
