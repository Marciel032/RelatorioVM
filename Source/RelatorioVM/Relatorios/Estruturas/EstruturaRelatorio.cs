using HtmlTags;
using RelatorioVM.Elementos.Interfaces;
using RelatorioVM.Elementos.Relatorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Relatorios.Estruturas
{
    internal class EstruturaRelatorio
    {
        public TituloElemento Titulo { get; set; }
        public FiltrosElemento Filtro { get; set; }
        public List<IElemento> Tabelas { get; set; }

        public EstruturaRelatorio()
        {
            Filtro = new FiltrosElemento();
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
