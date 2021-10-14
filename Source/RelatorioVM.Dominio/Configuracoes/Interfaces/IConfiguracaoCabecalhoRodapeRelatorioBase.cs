using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes.Interfaces
{
    public interface IConfiguracaoCabecalhoRodapeRelatorioBase
    {
        IConfiguracaoElemento Esquerda();
        IConfiguracaoElemento Centro();
        IConfiguracaoElemento Direita();
    }
}
