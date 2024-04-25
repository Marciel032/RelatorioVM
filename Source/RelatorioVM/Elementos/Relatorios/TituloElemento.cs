using HtmlTags;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Elementos.Estilos;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TituloElemento: IElementoRelatorioVM
    {
        private readonly ConfiguracaoRelatorio _configuracaoRelatorio;

        public TituloElemento(ConfiguracaoRelatorio configuracaoRelatorio)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
        }

        public string Indice { get; set; }
        public string Texto { get; set; }

        public string ObterHtml(object conteudo) {
            if (string.IsNullOrWhiteSpace(Texto))
                return string.Empty;

            var titulo = new HtmlTag("h3")                
                .AddClass("titulo")
                .Text(Texto);

            return titulo.ToHtmlString();
        }

        public string ObterEstilo()
        {
            var estiloConstrutor = new EstiloConstrutor();

            var estiloTitulo = new EstiloElemento()
                .AdicionarClasse("titulo")
                .DefinirEstiloManual("display: block;")
                .DefinirEstiloManual("margin: 0px;")
                .DefinirAlinhamentoTexto(new EstiloAlinhamentoTexto() { Direcao = TipoAlinhamentoHorizontal.Centro })
                .DefinirPreenchimento(new EstiloElementoPreenchimento()
                {
                    Direcao = TipoPreenchimento.Topo,
                    Tamanho = 20,
                    UnidadeMedida = TipoUnidadeMedida.Pixel
                });
            if (_configuracaoRelatorio.Cabecalho.TemTituloDefinido || _configuracaoRelatorio.Rodape.TemTituloDefinido)
                estiloTitulo.DefinirEstiloManual("position: running(titulo);");

            estiloConstrutor.AdicionarEstilo(estiloTitulo);

            return estiloConstrutor.ToString();
        }
    }
}
