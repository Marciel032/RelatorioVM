using RelatorioVM.Dominio.Configuracoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Infraestruturas
{
    internal class Configuracao
    {
        public static ConfiguracaoRelatorio ConfiguracaoRelatorio { get; set; } = new ConfiguracaoRelatorio();
    }
}
