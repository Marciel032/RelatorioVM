﻿using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Elementos.Relatorios;
using RelatorioVM.Extensoes;
using RelatorioVM.Relatorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Relatorios.Construtores
{
    internal class ConstrutorTabelaAgrupadorRelatorio<TConteudo>: ITabelaAgrupadorRelatorioVM<TConteudo>
    {
        private readonly ConfiguracaoRelatorio _configuracaoRelatorio;
        private TabelaAgrupador _agrupador;

        public ConstrutorTabelaAgrupadorRelatorio(ConfiguracaoRelatorio configuracaoRelatorio)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _agrupador = new TabelaAgrupador();
        }

        public ITabelaAgrupadorRelatorioVM<TConteudo> Coluna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> expressaoPropriedade)
        {
            var propriedade = expressaoPropriedade.ObterPropriedadeBase();
            var colunaAgrupador = new TabelaColunaAgrupador(propriedade.Name);

            if (_agrupador.Agrupadores.ContainsKey(colunaAgrupador.Identificador))
                _agrupador.Agrupadores[colunaAgrupador.Identificador] = colunaAgrupador;
            else
                _agrupador.Agrupadores.Add(colunaAgrupador.Identificador, colunaAgrupador);
            return this;
        }

        public TabelaAgrupador Construir() {
            return _agrupador;
        }
    }
}