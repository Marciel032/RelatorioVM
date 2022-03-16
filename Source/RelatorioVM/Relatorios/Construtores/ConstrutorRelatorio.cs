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
                var construtorTabela = new ConstrutorTabelaHorizontalRelatorio<TConteudo>(_configuracaoRelatorio, conteudo);
                opcoes?.Invoke(construtorTabela);
                _estruturaRelatorio.Elementos.Add(construtorTabela.Construir());
            }

            return this;
        }

        public IRelatorioVM AdicionarTabela<TConteudo>(TConteudo conteudo, Action<ITabelaVerticalRelatorioVM<TConteudo>> opcoes = null)
        {
            if (conteudo != null)
            {
                var construtorTabela = new ConstrutorTabelaVerticalRelatorio<TConteudo>(_configuracaoRelatorio, new List<TConteudo>() { conteudo });
                opcoes?.Invoke(construtorTabela);
                _estruturaRelatorio.Elementos.Add(construtorTabela.Construir());
            }

            return this;
        }

        public IRelatorioVM Filtros<TFiltro>(TFiltro filtros, Action<IFiltroRelatorioVM<TFiltro>> opcoes = null)
        {
            if (filtros != null) {
                var construtorFiltro = new ConstrutorFiltroRelatorio<TFiltro>(_configuracaoRelatorio, filtros);

                opcoes?.Invoke(construtorFiltro);
                _estruturaRelatorio.Filtro = construtorFiltro.Construir();
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
            _estruturaRelatorio.Elementos.Add(elemento);
            return this;
        }

        public IRelatorioVM AdicionarLinhaHorizontal() {
            _estruturaRelatorio.Elementos.Add(new LinhaHorizontalElemento());
            return this;
        }

        public IGeradorRelatorioVM Construir()
        {
            return GeradorRelatorioFabrica.Criar(_estruturaRelatorio, _configuracaoRelatorio);
        }        
    }
}
