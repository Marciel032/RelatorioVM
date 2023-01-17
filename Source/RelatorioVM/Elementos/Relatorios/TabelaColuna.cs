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
        public TipoAlinhamentoHorizontal AlinhamentoHorizontal { get; set; }
        public bool AlinhamentoDefinidoManualmente { get; set; }
        public bool Visivel { get; set; }
        public string Separador { get; set; }
        public FonteEscrita Fonte { get; set; }
        public int MargemBordas { get; set; }

        public bool TemComplemento { get { return PropriedadeComplemento != null;  } }
        public int QuantidadeColunasUtilizadas { get { return TemComplemento ? 2 : 1; } }

        public TabelaColuna()
        {
            Identificador = string.Empty;
            TituloColuna = string.Empty;
            AlinhamentoHorizontal = TipoAlinhamentoHorizontal.Esquerda;
            AlinhamentoDefinidoManualmente = false;
            Visivel = true;
            Separador = "-";
            MargemBordas = 1;
            Fonte = new FonteEscrita();
        }

        public string ObterValorConvertido(T origem, ConfiguracaoFormatacaoRelatorio formatacao) {
            return Propriedade.ObterValorConvertido(origem, formatacao);
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

        public IColunaRelatorioVM<T> Titulo(string titulo)
        {
            TituloColuna = titulo;
            return this;
        }
    }
}
