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
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
        }
    }
}
