using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Conversores.Interfaces;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RelatorioVM.Conversores
{
    internal class ConversorGenerico : IConversorValor
    {
        public string Converter(object valor, ConfiguracaoFormatacaoRelatorio opcoes)
        {
            if (valor == null)
                return opcoes.ValorNulavel;

            var tipo = valor.GetType().ObterTipoNaoNullo();

            if (tipo.BaseType == typeof(Enum))
                return ConverterEnumerador(valor, opcoes, tipo);

            if (tipo.IsClass && tipo != typeof(string))
                return ConverterClasse(valor, opcoes, tipo);

            return valor.ToString();
        }

        private string ConverterClasse(object valor, ConfiguracaoFormatacaoRelatorio opcoes, Type tipo) {
            var propriedadeDescricao = valor.GetType()
                .ObterPropriedades()
                .FirstOrDefault(x => x.PropertyType == typeof(string));

            if (propriedadeDescricao != null)
                return Converter(propriedadeDescricao.GetValue(valor), opcoes);            
            else
                return opcoes.ValorNulavel;

        }

        private string ConverterEnumerador(object valor, ConfiguracaoFormatacaoRelatorio opcoes, Type tipo)
        {
            return ((Enum)valor).ObterDescricao();
        }
    }
}
