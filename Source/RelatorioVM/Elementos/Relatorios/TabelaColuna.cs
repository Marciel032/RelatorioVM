using HtmlTags;
using RelatorioVM.Dominio.Configuracoes.Formatacoes;
using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Elementos.Propriedades;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaColuna<T>: IColunaRelatorioVM<T>
    {
        private List<TabelaColunaElemento> _elementos;
        public string Identificador { get; set; }
        public string TituloColuna { get; set; }
        public Propriedade<T> Propriedade { get; set; }
        public Propriedade<T> PropriedadeComplemento { get; set; }
        public TipoAlinhamentoHorizontal AlinhamentoHorizontalTitulo { get; set; }
        public TipoAlinhamentoHorizontal AlinhamentoHorizontalColuna { get; set; }
        public bool Visivel { get; set; }
        public string Separador { get; set; }
        public string Prefixo { get; set; }
        public FonteEscrita Fonte { get; set; }
        public int MargemBordas { get; set; }
        public bool Condensado { get; set; }
        public bool PermiteQuebraDeLinha { get; set; }
        public int Posicao { get; set; }

        public bool TemComplemento { get { return PropriedadeComplemento != null;  } }
        public int QuantidadeColunasUtilizadas { get { return TemComplemento && AlinhamentoHorizontalColuna == TipoAlinhamentoHorizontal.Centro ? 3 : 1; } }
        public bool TemPrefixo { get { return !string.IsNullOrEmpty(Prefixo); } }
        public bool TemElementosLinha { get { return _elementos.Any(x => !x.ExibirNaColuna); } }
        public bool TemElementosColuna { get { return _elementos.Any(x => x.ExibirNaColuna); } }

        public Color CorFundoConteudo { get; set; }

        public string Indice
        {
            set
            {
                for (int i = 0; i < _elementos.Count; i++)
                    _elementos[i].Indice = $"{value}-{i}";
            } 
        }

        public TabelaColuna(int posicao)
        {
            _elementos = new List<TabelaColunaElemento>();
            Posicao = posicao;
            Identificador = string.Empty;
            TituloColuna = string.Empty;
            AlinhamentoHorizontalTitulo = TipoAlinhamentoHorizontal.Esquerda;
            AlinhamentoHorizontalColuna = TipoAlinhamentoHorizontal.Esquerda;
            Visivel = true;
            Separador = "-";
            MargemBordas = 1;
            Fonte = new FonteEscrita();
            Condensado = false;
            PermiteQuebraDeLinha = true;
            CorFundoConteudo = Color.Empty;
        }

        public void AdicionarElemento(IElementoRelatorioVM elemento, bool exibirNaColuna = true) {
            var elementoColuna = new TabelaColunaElemento(elemento);
            elementoColuna.ExibirNaColuna = exibirNaColuna;
            _elementos.Add(elementoColuna);

            if (exibirNaColuna)
                Visivel = true;
        }

        public void AdicionarHtmlColuna(HtmlTag parent, T conteudo)
        {
            foreach(var elemento in _elementos.Where(x=> x.ExibirNaColuna))
                parent.AppendHtml(elemento.ObterHtml(Propriedade.ObterValor(conteudo)));
        }

        public void AdicionarHtmlLinha(HtmlTag parent, T conteudo)
        {
            foreach (var elemento in _elementos.Where(x => !x.ExibirNaColuna))
                parent.AppendHtml(elemento.ObterHtml(Propriedade.ObterValor(conteudo)));
        }

        public string ObterEstilo()
        {
            var construtorEstilo = new StringBuilder();

            foreach (var elemento in _elementos)
            {
                var estilo = elemento.ObterEstilo();
                if (string.IsNullOrEmpty(estilo))
                    continue;

                construtorEstilo.AppendLine(estilo);
            }
            return construtorEstilo.ToString();
        }

        public string ObterValorConvertido(T origem, ConfiguracaoFormatacaoRelatorio formatacao) {
            var valor = Propriedade.ObterValorConvertido(origem, formatacao);
            if (TemPrefixo)
                return $"{Prefixo} {valor}";
            else
                return valor;
        }

        public string ObterComplementoConvertido(T origem, ConfiguracaoFormatacaoRelatorio formatacao)
        {
            if (!TemComplemento)
                return formatacao.ValorNulavel;

            return PropriedadeComplemento.ObterValorConvertido(origem, formatacao);
        }

        public string ObterValorConvertidoComComplemento(T origem, ConfiguracaoFormatacaoRelatorio formatacao)
        {
            var valor = ObterValorConvertido(origem, formatacao);
            if (!TemComplemento)
                return valor;

            var valorComplemento = ObterComplementoConvertido(origem, formatacao);
            return $"{valor} {Separador} {valorComplemento}";
        }

        public Color ObterCorFundoConteudo(T origem)
        {
            if (Propriedade.PropriedadeInformacao.PropertyType.EhCor())
                return (Color)Propriedade.ObterValor(origem);

            return CorFundoConteudo;
        }

        public IColunaRelatorioVM<T> DefinirTitulo(string titulo)
        {
            TituloColuna = titulo;
            return this;
        }

        public IColunaRelatorioVM<T> DefinirAlinhamentoHorizontal(TipoAlinhamentoHorizontal alinhamento)
        {
            return DefinirAlinhamentoHorizontalTitulo(alinhamento).DefinirAlinhamentoHorizontalColuna(alinhamento);
        }

        public IColunaRelatorioVM<T> DefinirAlinhamentoHorizontalTitulo(TipoAlinhamentoHorizontal alinhamento)
        {
            AlinhamentoHorizontalTitulo = alinhamento;
            return this;
        }

        public IColunaRelatorioVM<T> DefinirAlinhamentoHorizontalColuna(TipoAlinhamentoHorizontal alinhamento)
        {
            AlinhamentoHorizontalColuna = alinhamento;
            return this;
        }

        public IColunaRelatorioVM<T> DefinirSeparador(string separador)
        {
            Separador = separador;
            return this;
        }

        public IColunaRelatorioVM<T> DefinirPrefixoColuna(string prefixo)
        {
            Prefixo = prefixo;
            return this;
        }

        public IColunaRelatorioVM<T> DefinirCondensado(bool condensado)
        {
            Condensado = condensado;
            return this;
        }

        public IColunaRelatorioVM<T> PermitirQuebraDeLinha(bool quebraDeLinha)
        {
            PermiteQuebraDeLinha = quebraDeLinha;
            return this;
        }

        public IColunaRelatorioVM<T> DefinirCorFundoConteudo(Color cor)
        {
            CorFundoConteudo = cor;
            return this;
        }
    }
}
