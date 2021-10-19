﻿using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Elementos.Relatorios;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RelatorioVM.Relatorios.Construtores
{
    internal class ConstrutorTabelaRelatorio<TConteudo>: ITabelaRelatorioVM<TConteudo>
    {
        private ConfiguracaoRelatorio _configuracaoRelatorio;
        private Tabela<TConteudo> _tabela;        

        public ConstrutorTabelaRelatorio(ConfiguracaoRelatorio configuracaoRelatorio, IEnumerable<TConteudo> conteudo)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _tabela = new Tabela<TConteudo>(conteudo);
            _tabela.Colunas = typeof(TConteudo)
                .ObterPropriedades()
                .Where(x => x.PodeSerColunaTabela())
                .Select(x => x.ObterColunaTabela<TConteudo>())
                .ToDictionary(x => x.Identificador);
        }        

        public TabelaElemento<TConteudo> Construir()
        {
            foreach (var agrupador in _tabela.Agrupadores)
            {
                agrupador.Colunas = agrupador.ObterColunasAgrupamento(_tabela.Colunas);
                agrupador.Colunas.ForEach(x => x.Visivel = false);
                foreach (var total in _tabela.Totais)
                    agrupador.Totais.Add(total.Clonar());
            }
            return new TabelaElemento<TConteudo>(_configuracaoRelatorio, _tabela);
        }

        public ITabelaRelatorioVM<TConteudo> Titulo(string titulo) {
            _tabela.Titulo = titulo;
            return this;
        }

        public ITabelaRelatorioVM<TConteudo> Totalizar(Action<ITabelaTotalRelatorioVM<TConteudo>> opcoes = null)
        {
            var totaisConstrutor = new ConstrutorTabelaTotalRelatorio<TConteudo>(_configuracaoRelatorio);
            opcoes?.Invoke(totaisConstrutor);
            var total = totaisConstrutor.Construir();
            if(total.Totais.Count > 0)
                _tabela.Totais.Add(total);
            return this;
        }

        public ITabelaRelatorioVM<TConteudo> Agrupar(Action<ITabelaAgrupadorRelatorioVM<TConteudo>> opcoes)
        {
            var agrupadorConstrutor = new ConstrutorTabelaAgrupadorRelatorio<TConteudo>(_configuracaoRelatorio);
            opcoes?.Invoke(agrupadorConstrutor);
            var agrupador = agrupadorConstrutor.Construir();
            if(agrupador.Agrupadores.Count > 0)
                _tabela.Agrupadores.Add(agrupador);
            return this;
        }

        public ITabelaRelatorioVM<TConteudo> Ignorar<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao) {
            var propriedade = propriedadeExpressao.ObterPropriedadeBase();
            if (_tabela.Colunas.TryGetValue(propriedade.Name, out var coluna))
                coluna.Visivel = false;

            return this;
        }
    }
}
