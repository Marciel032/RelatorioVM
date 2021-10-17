using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Infraestruturas;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Extensoes
{
    public static class ObjectExtensao
    {
        public static string ObterValorConvertido(this object valor, OpcoesFormatacao formato) {
            var tipo = valor.GetType().ObterTipoNaoNullo();
            var conversor = ConversorValor.ObterConversor(tipo);
            return conversor.Converter(valor, formato);
        }
    }
}
