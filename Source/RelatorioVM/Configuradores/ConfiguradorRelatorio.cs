using RelatorioVM.Dominio.Configuracoes.Interfaces;
using RelatorioVM.Dominio.Conversores.Interfaces;
using RelatorioVM.Infraestruturas;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Configuradores
{
    internal class ConfiguradorRelatorio : IConfiguradorRelatorio
    {
        public IConfiguradorRelatorio DefinirConversor<TTipo>(IConversorValor conversor)
        {
            ConversorValor.DefinirConversor<TTipo>(conversor);
            return this;
        }
    }
}
