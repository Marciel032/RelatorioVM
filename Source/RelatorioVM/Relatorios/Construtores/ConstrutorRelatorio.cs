using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Configuracoes.Interfaces;
using RelatorioVM.Infraestruturas;
using RelatorioVM.Relatorios.Estruturas;
using RelatorioVM.Relatorios.Fabricas;
using RelatorioVM.Relatorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Relatorios.Construtores
{
    internal class ConstrutorRelatorio : IRelatorioVM
    {
        private object _filtros;
        private List<object> _conteudos;
        private ConfiguracaoRelatorio _configuracaoRelatorio;

        public ConstrutorRelatorio()
        {
            _conteudos = new List<object>();
            _configuracaoRelatorio = Configuracao.ConfiguracaoRelatorio.Clone();
        }

        public IRelatorioVM AdicionarConteudo<T>(T conteudo)
        {
            if(conteudo != null)
                _conteudos.Add(conteudo);

            return this;
        }

        public IRelatorioVM AdicionarFiltros<T>(T filtros)
        {
            if (filtros != null)
                _filtros = filtros;

            return this;
        }

        public IRelatorioVM Configurar(Action<IConfiguracaoRelatorio> configuracao) {
            configuracao?.Invoke(_configuracaoRelatorio);
            
            return this;
        }

        public IGeradorRelatorioVM Construir()
        {
            return GeradorRelatorioFabrica.Criar(new EstruturaRelatorio(), _configuracaoRelatorio);
        }
    }
}
