using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes.Interfaces
{
    public interface IConfiguracaoRelatorio
    {
        IConfiguracaoRelatorio UsarOrientacao(TipoOrientacao orientacao);
        IConfiguracaoRelatorio ConfigurarCabecalho(Action<IConfiguracaoCabecalhoRelatorio> cabecalho);
        IConfiguracaoRelatorio ConfigurarRodape(Action<IConfiguracaoRodapeRelatorio> rodape);
    }
}
