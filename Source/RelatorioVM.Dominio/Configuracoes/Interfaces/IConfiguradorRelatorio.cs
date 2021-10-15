using RelatorioVM.Dominio.Conversores.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes.Interfaces
{
    public interface IConfiguradorRelatorio
    {
        /// <summary>
        /// Permite registrar conversores, para gerar o texto customizado para um tipo especifico
        /// </summary>
        IConfiguradorRelatorio DefinirConversor<TTipo>(IConversorValor conversor);
    }
}
