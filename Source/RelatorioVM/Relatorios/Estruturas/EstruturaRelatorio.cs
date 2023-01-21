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

        private List<IElementoRelatorioVM> _elementos;
        private Dictionary<string, object> _conteudos;
        private IElementoRelatorioVM _filtro;
        public TituloElemento Titulo { get; set; }
        public IElementoRelatorioVM Filtro { get { return _filtro; } }        

        public EstruturaRelatorio(ConfiguracaoRelatorio configuracaoRelatorio)
        {
            _configuracaoRelatorio = configuracaoRelatorio;

            Titulo = new TituloElemento();
            _elementos = new List<IElementoRelatorioVM>();
            _conteudos = new Dictionary<string, object>();
        }

        public void AdicionarHtml(HtmlTag parent) {
            parent.AppendHtml(Titulo.ObterHtml(null));
            if(_conteudos.ContainsKey("filtros"))
                parent.AppendHtml(Filtro?.ObterHtml(_conteudos["filtros"]));            
            _elementos.ForEach(x => parent.AppendHtml(x.ObterHtml(_conteudos[x.Indice])));
        }

        public string ObterEstilo() {
            var construtorEstilo = new StringBuilder();

            construtorEstilo.AppendLine(Titulo.ObterEstilo());

            if(Filtro != null)
                construtorEstilo.AppendLine(Filtro.ObterEstilo());

            foreach (var elemento in _elementos) {
                var estilo = elemento.ObterEstilo();
                if (string.IsNullOrEmpty(estilo))
                    continue;

                construtorEstilo.AppendLine(estilo);
            }
            return construtorEstilo.ToString();
        }

        public void AdicionarElemento(IElementoRelatorioVM elemento, object conteudo) {
            elemento.Indice = _elementos.Count.ToString();
            _elementos.Add(elemento);
            _conteudos.Add(elemento.Indice, conteudo);
        }

        public void AdicionarFiltro(IElementoRelatorioVM filtro, object conteudo)
        {
            _filtro = filtro;
            _conteudos.Add("filtros", conteudo);
        }
    }
}
