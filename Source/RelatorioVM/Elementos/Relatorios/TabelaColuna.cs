using RelatorioVM.Dominio.Configuracoes.Formatacoes;
using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Elementos.Propriedades;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaColuna<T>
    {
        public string Identificador { get; set; }
        public string Titulo { get; set; }
        public Propriedade<T> Propriedade { get; set; }
        public Propriedade<T> PropriedadeComplemento { get; set; }
        public TipoAlinhamentoHorizontal AlinhamentoHorizontal { get; set; }
        public bool AlinhamentoDefinidoManualmente { get; set; }
        public bool Visivel { get; set; }
        public string Separador { get; set; }
        public FonteEscrita Fonte { get; set; }
        public int MargemBordas { get; set; }

        public TabelaColuna()
        {
            Identificador = string.Empty;
            Titulo = string.Empty;
            AlinhamentoHorizontal = TipoAlinhamentoHorizontal.Esquerda;
            AlinhamentoDefinidoManualmente = false;
            Visivel = true;
            Separador = "-";
            MargemBordas = 1;
            Fonte = new FonteEscrita();
        }

        public string ObterValorConvertido(T origem, ConfiguracaoFormatacaoRelatorio formatacao) {
            var valor = Propriedade.ObterValorConvertido(origem, formatacao);
            if (PropriedadeComplemento == null)
                return valor;

            var valorComplemento = PropriedadeComplemento.ObterValorConvertido(origem, formatacao);
            return $"{valor} {Separador} {valorComplemento}";
        }
    }
}
