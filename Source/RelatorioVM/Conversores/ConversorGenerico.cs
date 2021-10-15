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
        public string Converter(object valor, OpcoesFormatacao opcoes)
        {
            if (valor == null)
                return "";

            if (valor.GetType().IsClass && valor.GetType() != typeof(string))
                return ConverterClasse(valor, opcoes);

            return valor.ToString();
        }

        private string ConverterClasse(object valor, OpcoesFormatacao opcoes) {
            var propriedadeDescricao = valor.GetType()
                .ObterPropriedades()
                .FirstOrDefault(x => x.PropertyType == typeof(string));

            if (propriedadeDescricao != null)
                return Converter(propriedadeDescricao.GetValue(valor), opcoes);            
            else
                return opcoes.ValorNulavel;

        }
    }
}
