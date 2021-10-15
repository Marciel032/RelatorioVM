using RelatorioVM.Dominio.Conversores;
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

        public static string ObterValorConvertido(this PropertyInfo propriedade, object origem, OpcoesFormatacao formato)
        {
            var tipo = propriedade.PropertyType;
            if (tipo.IsGenericType && tipo.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                tipo = Nullable.GetUnderlyingType(tipo);
            }
            var conversor = ConversorValor.ObterConversor(tipo);
            return conversor.Converter(propriedade.GetValue(origem), formato);
        }
    }
}
