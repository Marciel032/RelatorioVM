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
    internal class TabelaColuna<T>: IColunaRelatorioVM<T>
    {
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

        public bool TemComplemento { get { return PropriedadeComplemento != null;  } }
        public int QuantidadeColunasUtilizadas { get { return TemComplemento ? 2 : 1; } }
        public bool TemPrefixo { get { return !string.IsNullOrEmpty(Prefixo); } }

        public TabelaColuna()
        {
            Identificador = string.Empty;
            TituloColuna = string.Empty;
            AlinhamentoHorizontalTitulo = TipoAlinhamentoHorizontal.Esquerda;
            AlinhamentoHorizontalColuna = TipoAlinhamentoHorizontal.Esquerda;
            Visivel = true;
            Separador = "-";
            MargemBordas = 1;
            Fonte = new FonteEscrita();
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

        public string ObterSeparadorComComplementoConvertido(T origem, ConfiguracaoFormatacaoRelatorio formatacao)
        {
            if (!TemComplemento)
                return Separador;

            var valorComplemento = ObterComplementoConvertido(origem, formatacao);
            return $"{Separador} {valorComplemento}";
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
    }
}
