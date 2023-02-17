using HtmlTags;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Elementos.Estilos;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class CabecalhoRodapeTextoElemento: IElementoRelatorioVM
    {
        private readonly ConfiguracaoRelatorio _configuracao;
        private readonly TipoPosicaoCabecalhoRodape _posicao;

        public CabecalhoRodapeTextoElemento(ConfiguracaoRelatorio configuracao, TipoPosicaoCabecalhoRodape posicao)
        {
            _configuracao = configuracao;
            _posicao = posicao;
        }

        public string Indice { get; set; }

        public string ObterHtml(object conteudo) {
            if (!(conteudo is ConfiguracaoCabecalhoRodapeElemento))
                return string.Empty;

            var cabecalhoRodape = conteudo as ConfiguracaoCabecalhoRodapeElemento;
            var valor = cabecalhoRodape.ObterValor();

            if (string.IsNullOrWhiteSpace(valor))
                return string.Empty;

            var textoSpan = new HtmlTag("span")                
                .AddClass(cabecalhoRodape.Posicao.ObterDescricao())
                .AddClass("somente-impressao")
                .Text(valor);

            if (cabecalhoRodape.Tipo == TipoElementoCabecalhoRodape.NumeroDaPagina)
                textoSpan.Add("span").AddClass("paginas").Text("/");

            return textoSpan.ToHtmlString();
        }

        public string ObterEstilo()
        {
            var estiloConstrutor = new EstiloConstrutor();

            estiloConstrutor.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_posicao.ObterDescricao())
                .DefinirFonte(_configuracao.Formatacao.FonteConteudo)
                .DefinirEstiloManual("margin: 0px;")
                .DefinirAlinhamentoTexto(new EstiloAlinhamentoTexto() { Direcao = ObterAlinhamento() })
                .DefinirEstiloManual($"position: running({_posicao.ObterDescricao()});"));

            return estiloConstrutor.ToString();
        }

        private TipoAlinhamentoHorizontal ObterAlinhamento() {
            switch (_posicao) {
                case TipoPosicaoCabecalhoRodape.CabecalhoDireito:
                case TipoPosicaoCabecalhoRodape.RodapeDireito:
                    return TipoAlinhamentoHorizontal.Direita;
                case TipoPosicaoCabecalhoRodape.CabecalhoCentro:
                case TipoPosicaoCabecalhoRodape.RodapeCentro:
                    return TipoAlinhamentoHorizontal.Centro;
                case TipoPosicaoCabecalhoRodape.CabecalhoEsquerdo:
                case TipoPosicaoCabecalhoRodape.RodapeEsquerdo:
                default: 
                    return TipoAlinhamentoHorizontal.Esquerda;
            }
        }
    }
}
