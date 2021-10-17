using RelatorioVM.Elementos.Propriedades;
using RelatorioVM.Elementos.Relatorios;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Extensoes
{
    internal static class PropriedadeFiltroExtensao
    {
        public static bool PodeSerFiltro<TOrigem>(this PropertyInfo propriedade, TOrigem origem) {
            if (propriedade.GetValue(origem) == null)
                return false;

            return true;
        }

        public static Filtro<T> ObterFiltro<T>(this PropertyInfo propriedade) {
            var filtro = new Filtro<T>();

            filtro.Identificador = propriedade.Name;            
            filtro.Nome = propriedade.ObterNome();
            filtro.Valor = string.Empty;
            filtro.Propriedade = new Propriedade<T>(propriedade);

            return filtro;
        }


    }
}
