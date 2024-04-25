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
    internal class ConstrutorFormatacaoTabela<TConteudo>: ITabelaFormatacaoRelatorioVM<TConteudo>
    {
        protected ConfiguracaoRelatorio _configuracaoRelatorio;
        protected TabelaFormatacao<TConteudo> _tabelaFormatacao;        

        public ConstrutorFormatacaoTabela(ConfiguracaoRelatorio configuracaoRelatorio)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _tabelaFormatacao = new TabelaFormatacao<TConteudo>();
        }

        public TabelaFormatacao<TConteudo> Construir() {
            return _tabelaFormatacao;
        }

        public void DefinirCorFundoLinhaConteudo(Expression<Func<TConteudo, string>> propriedadeExpressao) { 
            _tabelaFormatacao.CorFundoLinhaConteudoPropriedade = new Propriedade<TConteudo>(propriedadeExpressao.ObterPropriedadeBase())
            {                
                FuncaoPropriedade = (origem) => propriedadeExpressao.Compile()(origem)
            };
        }
    }
}
