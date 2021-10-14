using RelatorioVM.Dominio.Configuracoes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Relatorios.Interfaces
{
    public interface IRelatorioVM
    {
        IRelatorioVM AdicionarFiltros<T>(T filtros);
        IRelatorioVM AdicionarConteudo<T>(T conteudo);
        IRelatorioVM Configurar(Action<IConfiguracaoRelatorio> configuracao);
        IGeradorRelatorioVM Construir();
    }
}
