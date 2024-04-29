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
    internal class ImagemElemento: IElementoRelatorioVM
    {
        private readonly ConfiguracaoRelatorio _configuracaoRelatorio;

        public ImagemElemento(ConfiguracaoRelatorio configuracaoRelatorio)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
        }

        public string Indice { get; set; }

        public string ObterHtml(object conteudo) {
            string origem = (string)conteudo;
            if (string.IsNullOrWhiteSpace(origem))
                return string.Empty;

            var imagemHtml = new HtmlTag("img")
                .Attr("src", origem);

            return imagemHtml.ToHtmlString();
        }

        public string ObterEstilo()
        {
            var estiloConstrutor = new EstiloConstrutor();

            return estiloConstrutor.ToString();
        }
    }
}
