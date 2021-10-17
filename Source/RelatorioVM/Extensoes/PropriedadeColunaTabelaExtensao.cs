using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Elementos.Propriedades;
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

        public static TabelaColuna<T> ObterColunaTabela<T>(this PropertyInfo propriedade) {
            var coluna = new TabelaColuna<T>();

            coluna.Identificador = propriedade.Name;            
            coluna.Titulo = propriedade.ObterNome();
            coluna.Propriedade = new Propriedade<T>(propriedade);
            coluna.AlinhamentoHorizontal = propriedade.ObterAlinhamentoHorizontal();

            return coluna;
        }

        public static TipoAlinhamentoHorizontal ObterAlinhamentoHorizontal(this PropertyInfo propriedade) {
            var tipo = propriedade.PropertyType;
            if(tipo.EhInteiroOuDecimal())
                return TipoAlinhamentoHorizontal.Direita;

            return TipoAlinhamentoHorizontal.Esquerda;
        }

        public static bool PodeSerTotalTabela(this PropertyInfo propriedade)
        {
            var tipo = propriedade.PropertyType;
            
            return tipo.EhDecimal();
        }

        public static TabelaColunaTotal<T> ObterTotalTabela<T>(this PropertyInfo propriedade)
        {
            var total = new TabelaColunaTotal<T>();

            total.Identificador = propriedade.Name;
            total.AlinhamentoHorizontal = propriedade.ObterAlinhamentoHorizontal();
            total.Propriedade = new Propriedade<T>(propriedade);

            return total;
        }
    }
}
