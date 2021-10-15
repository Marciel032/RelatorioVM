using HtmlTags;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Elementos.Interfaces;
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
        public FiltrosElemento Filtro { get; set; }
        public List<IElemento> Tabelas { get; set; }

        public EstruturaRelatorio(ConfiguracaoRelatorio configuracaoRelatorio)
        {
            _configuracaoRelatorio = configuracaoRelatorio;

            Filtro = new FiltrosElemento(configuracaoRelatorio);
            Titulo = new TituloElemento();
            Tabelas = new List<IElemento>();
        }

        public void AdicionarHtml(HtmlTag parent) {
            Titulo.ProcessarHtml(parent);
            Filtro.ProcessarHtml(parent);
            Tabelas.ForEach(x => x.ProcessarHtml(parent));
        }
    }
}
