using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes.Interfaces
{
    public interface IConfiguracaoConteudoRelatorio
    {
        /// <summary>
        /// Define se as linhas vão ser impressas com cor de fundo intercaladas
        /// </summary>
        bool Zebrado { get; set; }
    }
}
