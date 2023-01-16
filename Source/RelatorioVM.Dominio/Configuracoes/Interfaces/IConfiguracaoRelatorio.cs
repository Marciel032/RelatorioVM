using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes.Interfaces
{
    public interface IConfiguracaoRelatorio
    {        
        IConfiguracaoRelatorio ConfigurarConteudo(Action<IConfiguracaoConteudoRelatorio> conteudo);
        IConfiguracaoRelatorio ConfigurarCabecalho(Action<IConfiguracaoCabecalhoRelatorio> cabecalho);
        IConfiguracaoRelatorio ConfigurarRodape(Action<IConfiguracaoRodapeRelatorio> rodape);
        IConfiguracaoRelatorio ConfigurarFormatacao(Action<IConfiguracaoFormatacaoRelatorio> formatacao);
    }
}
