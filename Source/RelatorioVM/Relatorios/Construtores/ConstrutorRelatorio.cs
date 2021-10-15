using RelatorioVM.ConversoresPdf.Interfaces;
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
        private EstruturaRelatorio _estruturaRelatorio;
        private ConfiguracaoRelatorio _configuracaoRelatorio;
        private IConversorPdf _conversor;

        public ConstrutorRelatorio(IConversorPdf conversor)
        {
            _configuracaoRelatorio = Configuracao.ConfiguracaoRelatorio.Clone();
            _conversor = conversor;
            _estruturaRelatorio = new EstruturaRelatorio(_configuracaoRelatorio);
                        
        }

        public IRelatorioVM AdicionarTabela<TConteudo>(IEnumerable<TConteudo> conteudo, Action<ITabelaRelatorioVM<TConteudo>> opcoes = null)
        {
            if (conteudo != null)
            {
                var construtorTabela = new ConstrutorTabelaRelatorio<TConteudo>(_configuracaoRelatorio, conteudo);
                opcoes?.Invoke(construtorTabela);
                _estruturaRelatorio.Tabelas.Add(construtorTabela.Construir());
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

        public IGeradorRelatorioVM Construir()
        {
            return GeradorRelatorioFabrica.Criar(_estruturaRelatorio, _configuracaoRelatorio, _conversor);
        }        
    }
}
