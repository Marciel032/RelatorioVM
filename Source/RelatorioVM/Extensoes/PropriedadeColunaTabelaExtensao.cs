using RelatorioVM.Elementos.Relatorios;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Extensoes
{
    internal static class PropriedadeColunaTabelaExtensao
    {
        public static bool PodeSerColunaTabela(this PropertyInfo propriedade) {
            return true;
        }

        public static TabelaColuna ObterColunaTabela(this PropertyInfo propriedade) {
            var coluna = new TabelaColuna();

            coluna.Identificador = propriedade.Name;            
            coluna.Titulo = propriedade.ObterNome();
            coluna.Propriedade = propriedade;

            return coluna;
        }
    }
}
