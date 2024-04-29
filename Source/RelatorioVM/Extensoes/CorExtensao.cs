using Microsoft.Extensions.DependencyInjection;
using RelatorioVM.Configuradores;
using RelatorioVM.Conversores;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Configuracoes.Interfaces;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Infraestruturas;
using RelatorioVM.Relatorios.Construtores;
using RelatorioVM.Relatorios.Fabricas;
using System;
using System.Drawing;

namespace RelatorioVM.Extensoes
{
    public static class CorExtensao
    {
        public static string ParaHexadecimalString(this Color c) => $"#{c.R:X2}{c.G:X2}{c.B:X2}";
    }
}
