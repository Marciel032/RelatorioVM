using RelatorioVM.Conversores.Interfaces;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Relatorios.Estruturas;
using RelatorioVM.Relatorios.Geradores;
using RelatorioVM.Relatorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Relatorios.Fabricas
{
    internal class GeradorRelatorioFabrica
    {
        public static IGeradorRelatorioVM Criar(EstruturaRelatorio estrutura, ConfiguracaoRelatorio configuracao, IConversor conversor) {
            return new GeradorRelatorioBase(estrutura, configuracao, conversor);
        }
    }
}
