﻿using RelatorioVM.Elementos.Relatorios;
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

        public static Filtro ObterFiltro<TOrigem>(this PropertyInfo propriedade, TOrigem origem) {
            var filtro = new Filtro();

            filtro.Identificador = propriedade.Name;            
            filtro.Nome = propriedade.ObterNome();
            filtro.Valor = propriedade.GetValue(origem).ToString();

            return filtro;
        }
    }
}
