using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Infraestruturas;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Extensoes
{
    public static class ObjectExtensao
    {
        public static string ObterValorConvertido(this object valor, OpcoesFormatacao formato, Type tipo) {
            if (valor == null) {
                if (formato.ObterValorNulavelParaOTipo(tipo.ObterTipoNaoNullo().FullName, out var valorNulo))
                    return valorNulo;
                else
                    valor = tipo.ObterValorPadrao();
            }

            if (valor == null)
                return formato.ValorNulavel;

            tipo = valor.GetType().ObterTipoNaoNullo();
            var conversor = ConversorValor.ObterConversor(tipo);
            return conversor.Converter(valor, formato);
        }

        public static object ObterValorPadrao(this Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type.ObterTipoNaoNullo());

            return null;
        }
    }
}
