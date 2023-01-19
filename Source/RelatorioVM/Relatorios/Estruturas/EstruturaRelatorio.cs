﻿using HtmlTags;
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

        private List<IElementoRelatorioVM> elementos;
        public TituloElemento Titulo { get; set; }
        public IElementoRelatorioVM Filtro { get; set; }        

        public EstruturaRelatorio(ConfiguracaoRelatorio configuracaoRelatorio)
        {
            _configuracaoRelatorio = configuracaoRelatorio;

            Titulo = new TituloElemento();
            elementos = new List<IElementoRelatorioVM>();
        }

        public void AdicionarHtml(HtmlTag parent) {
            parent.AppendHtml(Titulo.ObterHtml());
            parent.AppendHtml(Filtro?.ObterHtml());            
            elementos.ForEach(x => parent.AppendHtml(x.ObterHtml()));
        }

        public string ObterEstilo() {
            var construtorEstilo = new StringBuilder();

            construtorEstilo.AppendLine(Titulo.ObterEstilo());

            if(Filtro != null)
                construtorEstilo.AppendLine(Filtro.ObterEstilo());

            foreach (var elemento in elementos) {
                var estilo = elemento.ObterEstilo();
                if (string.IsNullOrEmpty(estilo))
                    continue;

                construtorEstilo.AppendLine(estilo);
            }
            return construtorEstilo.ToString();
        }

        public void AdicionarElemento(IElementoRelatorioVM elemento) {
            elemento.DefinirIndiceElemento(elementos.Count);
            elementos.Add(elemento);            
        }
    }
}
