using RelatorioVM.Dominio.Enumeradores;
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
            coluna.AlinhamentoHorizontal = propriedade.ObterAlinhamentoHorizontal();

            return coluna;
        }

        public static TipoAlinhamentoHorizontal ObterAlinhamentoHorizontal(this PropertyInfo propriedade) {
            var tipo = propriedade.PropertyType.ObterTipoNaoNullo();
            if (tipo == typeof(decimal))
                return TipoAlinhamentoHorizontal.Direita;
            else if (tipo == typeof(short))
                return TipoAlinhamentoHorizontal.Direita;
            else if (tipo == typeof(int))
                return TipoAlinhamentoHorizontal.Direita;
            else if (tipo == typeof(long))
                return TipoAlinhamentoHorizontal.Direita;
            else if (tipo == typeof(ushort))
                return TipoAlinhamentoHorizontal.Direita;
            else if (tipo == typeof(uint))
                return TipoAlinhamentoHorizontal.Direita;
            else if (tipo == typeof(ulong))
                return TipoAlinhamentoHorizontal.Direita;

            return TipoAlinhamentoHorizontal.Esquerda;
        }
    }
}
