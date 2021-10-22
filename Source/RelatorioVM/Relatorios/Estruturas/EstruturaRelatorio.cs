using HtmlTags;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Elementos.Relatorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Relatorios.Estruturas
{
    internal class EstruturaRelatorio
    {
        private readonly ConfiguracaoRelatorio _configuracaoRelatorio;
        public TituloElemento Titulo { get; set; }
        public IElementoRelatorioVM Filtro { get; set; }
        public List<IElementoRelatorioVM> Elementos { get; set; }

        public EstruturaRelatorio(ConfiguracaoRelatorio configuracaoRelatorio)
        {
            _configuracaoRelatorio = configuracaoRelatorio;

            Titulo = new TituloElemento();
            Elementos = new List<IElementoRelatorioVM>();
        }

        public void AdicionarHtml(HtmlTag parent) {
            parent.AppendHtml(Titulo.ObterHtml());
            parent.AppendHtml(Filtro?.ObterHtml());            
            Elementos.ForEach(x => parent.AppendHtml(x.ObterHtml()));
        }
    }
}
