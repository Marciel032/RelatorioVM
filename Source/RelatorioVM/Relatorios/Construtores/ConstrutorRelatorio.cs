using RelatorioVM.Conversores.Interfaces;
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
        private List<object> _conteudos;
        private ConfiguracaoRelatorio _configuracaoRelatorio;
        private IConversor _conversor;

        public ConstrutorRelatorio(IConversor conversor)
        {
            _conversor = conversor;
            _estruturaRelatorio = new EstruturaRelatorio();
            _conteudos = new List<object>();
            _configuracaoRelatorio = Configuracao.ConfiguracaoRelatorio.Clone();            
        }

        public IRelatorioVM AdicionarConteudo<TConteudo>(TConteudo conteudo)
        {
            if(conteudo != null)
                _conteudos.Add(conteudo);

            return this;
        }

        public IRelatorioVM Filtros<TFiltro>(TFiltro filtros, Action<IFiltroRelatorioVM<TFiltro>> opcoes = null)
        {
            if (filtros != null) {
                var construtorFiltro = new ConstrutorFiltroRelatorio<TFiltro>(filtros);

                opcoes?.Invoke(construtorFiltro);
                _estruturaRelatorio.Filtro = construtorFiltro.Construir();
            }            

            return this;
        }

        public IRelatorioVM Configurar(Action<IConfiguracaoRelatorio> configuracao) {
            configuracao?.Invoke(_configuracaoRelatorio);
            
            return this;
        }

        public IGeradorRelatorioVM Construir()
        {
            return GeradorRelatorioFabrica.Criar(new EstruturaRelatorio(), _configuracaoRelatorio, _conversor);
        }
    }
}
