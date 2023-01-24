using HtmlTags;
using RelatorioVM.Dominio.Configuracoes.Formatacoes;
using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Elementos.Propriedades;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaColunaElemento 
    {
        private IElementoRelatorioVM _elemento;

        public bool ExibirNaColuna { get; set; }
        public string Indice { get { return _elemento.Indice; } set { _elemento.Indice = value; } }

        public TabelaColunaElemento(IElementoRelatorioVM elemento)
        {
            _elemento = elemento;  
            ExibirNaColuna = true;
        }

        public string ObterHtml(object conteudo) {
            return _elemento.ObterHtml(conteudo);
        }

        public string ObterEstilo()
        {
            return _elemento.ObterEstilo();
        }
    }
}
