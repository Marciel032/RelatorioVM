using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Relatorios.Estruturas;
using RelatorioVM.Relatorios.Geradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Relatorios.Fabricas
{
    internal class GeradorRelatorioFabrica
    {
        public static IGeradorRelatorioVM Criar(EstruturaRelatorio estrutura, ConfiguracaoRelatorio configuracao) {
            return new GeradorRelatorioBase(estrutura, configuracao);
        }
    }
}
