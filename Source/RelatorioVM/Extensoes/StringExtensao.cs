using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RelatorioVM.Extensoes
{
    internal static class StringExtensao
    {
        public static string SepararCamelCase(this string texto) { 
            return string.Concat(
                         texto.Select((x, i) => i > 0 && char.IsUpper(x) ? " " + x.ToString().ToLower() : x.ToString())
                     );
        }
    }
}
