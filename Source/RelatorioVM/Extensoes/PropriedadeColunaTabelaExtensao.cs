using RelatorioVM.Dominio.Atributos;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Elementos.Propriedades;
using RelatorioVM.Elementos.Relatorios;
using System;
using System.Collections.Generic;
using System.Linq;
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
            coluna.TituloColuna = propriedade.ObterNome();
            coluna.Propriedade = new Propriedade<T>(propriedade);
            coluna.AlinhamentoHorizontalTitulo = propriedade.ObterAlinhamentoHorizontal();
            coluna.AlinhamentoHorizontalColuna = coluna.AlinhamentoHorizontalTitulo;
            coluna.Visivel = propriedade.PodeSerVisivel();

            var colunaAtributo = propriedade.GetCustomAttribute<ColunaRelatorioAttribute>();
            if (colunaAtributo != null)
            {
                if (!string.IsNullOrWhiteSpace(colunaAtributo.Titulo))
                    coluna.TituloColuna = colunaAtributo.Titulo;
                if (!colunaAtributo.Visivel)
                    coluna.Visivel = false;
                if (!string.IsNullOrWhiteSpace(colunaAtributo.Prefixo))
                    coluna.Prefixo = colunaAtributo.Prefixo;
            }

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

        public static bool PodeSerVisivel(this PropertyInfo propriedade)
        {
            var tipo = propriedade.PropertyType.ObterTipoNaoNullo();

            return !tipo.EhLista();
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
