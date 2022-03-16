using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Elementos.Propriedades;
using RelatorioVM.Elementos.Relatorios;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Relatorios.Construtores
{
    internal class ConstrutorTabelaVerticalRelatorio<TConteudo>: ConstrutorTabelaRelatorio<TConteudo>, ITabelaVerticalRelatorioVM<TConteudo>
    {   
        public ConstrutorTabelaVerticalRelatorio(ConfiguracaoRelatorio configuracaoRelatorio, IEnumerable<TConteudo> conteudo): base(configuracaoRelatorio, conteudo)
        {

        }        

        public TabelaVerticalElemento<TConteudo> Construir()
        {
            return new TabelaVerticalElemento<TConteudo>(_configuracaoRelatorio, _tabela);
        }

        public ITabelaVerticalRelatorioVM<TConteudo> Titulo(string titulo) {
            DefinirTitulo(titulo);
            return this;
        }

        public ITabelaVerticalRelatorioVM<TConteudo> Ignorar<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao) {
            IgnorarColuna(propriedadeExpressao);
            return this;
        }

        public ITabelaVerticalRelatorioVM<TConteudo> ComplementarValor<TPropriedade, TPropriedadeComplemento>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Expression<Func<TConteudo, TPropriedadeComplemento>> propriedadeComplementoExpressao, bool ignorar = true)
        {
            ComplementarValorPropriedade(propriedadeExpressao, propriedadeComplementoExpressao, ignorar);
            return this;
        }

        public ITabelaVerticalRelatorioVM<TConteudo> ComplementarValor<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Func<TPropriedade, string> funcao)
        {
            ComplementarValorPropriedade(propriedadeExpressao, funcao);
            return this;
        }

        public ITabelaVerticalRelatorioVM<TConteudo> ComplementarValor<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Func<TConteudo, TPropriedade, string> funcao)
        {
            ComplementarValorPropriedade(propriedadeExpressao, funcao);
            return this;
        }
    }
}
