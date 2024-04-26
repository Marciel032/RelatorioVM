using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Configuracoes.Interfaces;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Elementos.Complementares;
using RelatorioVM.Infraestruturas;
using RelatorioVM.Relatorios.Estruturas;
using RelatorioVM.Relatorios.Fabricas;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Relatorios.Construtores
{
    internal class ConstrutorRelatorio : IRelatorioVM
    {
        private EstruturaRelatorio _estruturaRelatorio;
        private ConfiguracaoRelatorio _configuracaoRelatorio;

        public ConstrutorRelatorio()
        {
            _configuracaoRelatorio = Configuracao.ConfiguracaoRelatorio.Clone();
            _estruturaRelatorio = new EstruturaRelatorio(_configuracaoRelatorio);
                        
        }

        public IRelatorioVM AdicionarTabela<TConteudo>(IEnumerable<TConteudo> conteudo, Action<ITabelaHorizontalRelatorioVM<TConteudo>> opcoes = null)
        {
            if (conteudo != null)
            {
                var construtorTabela = new ConstrutorTabelaHorizontalRelatorio<TConteudo>(_configuracaoRelatorio);
                opcoes?.Invoke(construtorTabela);
                _estruturaRelatorio.AdicionarElemento(construtorTabela.Construir(), conteudo);
            }

            return this;
        }

        public IRelatorioVM AdicionarTabelaVertical<TConteudo>(TConteudo conteudo, Action<ITabelaVerticalRelatorioVM<TConteudo>> opcoes = null)
        {
            if (conteudo != null)
            {
                var construtorTabela = new ConstrutorTabelaVerticalRelatorio<TConteudo>(_configuracaoRelatorio);
                opcoes?.Invoke(construtorTabela);
                _estruturaRelatorio.AdicionarElemento(construtorTabela.Construir(), conteudo);
            }

            return this;
        }

        public IRelatorioVM AdicionarTabelaVertical<TConteudo>(IEnumerable<TConteudo> conteudo, Action<ITabelaVerticalRelatorioVM<TConteudo>> opcoes = null)
        {
            if (conteudo != null)
            {
                var construtorTabela = new ConstrutorTabelaVerticalRelatorio<TConteudo>(_configuracaoRelatorio);
                opcoes?.Invoke(construtorTabela);
                _estruturaRelatorio.AdicionarElemento(construtorTabela.Construir(), conteudo);
            }

            return this;
        }

        public IRelatorioVM AdicionarTabelaVertical<TConteudo>(List<TConteudo> conteudo, Action<ITabelaVerticalRelatorioVM<TConteudo>> opcoes = null)
        {
            if (conteudo != null)
                AdicionarTabelaVertical(conteudo as IEnumerable<TConteudo>, opcoes);

            return this;
        }

        public IRelatorioVM Filtros<TFiltro>(TFiltro filtros, Action<IFiltroRelatorioVM<TFiltro>> opcoes = null)
        {
            if (filtros != null) {
                var construtorFiltro = new ConstrutorFiltroRelatorio<TFiltro>(_configuracaoRelatorio);

                opcoes?.Invoke(construtorFiltro);
                _estruturaRelatorio.AdicionarFiltro(construtorFiltro.Construir(), filtros);
            }            

            return this;
        }

        public IRelatorioVM Configurar(Action<IConfiguracaoRelatorio> configuracao) {
            configuracao?.Invoke(_configuracaoRelatorio);
            
            return this;
        }

        public IRelatorioVM Titulo(string titulo)
        {
            _estruturaRelatorio.Titulo.Texto = titulo;
            return this;
        }

        public IRelatorioVM AdicionarComponenteCustomizado(IElementoRelatorioVM elemento)
        {
            _estruturaRelatorio.AdicionarElemento(elemento, null);
            return this;
        }

        public IRelatorioVM AdicionarLinhaHorizontal() {
            _estruturaRelatorio.AdicionarElemento(new LinhaHorizontalElemento(), null);
            return this;
        }

        public IGeradorRelatorioVM Construir(bool resetarConstrutor = true)
        {
            var gerador = GeradorRelatorioFabrica.Criar(_estruturaRelatorio, _configuracaoRelatorio);
            if(resetarConstrutor)
                _estruturaRelatorio = new EstruturaRelatorio(_configuracaoRelatorio);
            return gerador;
        }        
    }
}
