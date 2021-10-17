using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Elementos.Propriedades;
using RelatorioVM.Infraestruturas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Extensoes
{
    internal static class PropriedadeExtensao
    {
        public static string ObterNome(this PropertyInfo propriedade) {
            var nome = propriedade.GetCustomAttribute<DisplayNameAttribute>();
            if (nome != null && !string.IsNullOrWhiteSpace(nome.DisplayName))
                return nome.DisplayName;

            return propriedade.Name;
        }
    }
}
