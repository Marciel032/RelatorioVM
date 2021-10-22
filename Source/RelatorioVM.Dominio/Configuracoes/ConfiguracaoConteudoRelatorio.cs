using RelatorioVM.Dominio.Configuracoes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes
{
    public class ConfiguracaoConteudoRelatorio : IConfiguracaoConteudoRelatorio
    {      
        public bool Zebrado { get; set; }

        public ConfiguracaoConteudoRelatorio()
        {
            Zebrado = true;
        }

        public ConfiguracaoConteudoRelatorio Clone() {
            return MemberwiseClone() as ConfiguracaoConteudoRelatorio;
        }
    }
}
