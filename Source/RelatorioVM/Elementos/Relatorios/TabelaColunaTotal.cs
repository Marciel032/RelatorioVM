using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Elementos.Propriedades;
using RelatorioVM.Extensoes;
using RelatorioVM.Infraestruturas;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaColunaTotal<T>
    {
        public string Identificador { get; set; }
        public Propriedade<T> Propriedade { get; set; }
        public TipoAlinhamentoHorizontal AlinhamentoHorizontal { get; set; }
        public object Valor { get; set; }

        public TabelaColunaTotal()
        {
            Identificador = string.Empty;
            AlinhamentoHorizontal = TipoAlinhamentoHorizontal.Direita;
            Zerar();
        }

        public void Calcular(T origem) {
            dynamic valorTotal = Valor;
            dynamic valorOrigem = Propriedade.ObterValor(origem);
            
            if (valorOrigem == null)
                return;

            if (valorTotal == null)
                valorTotal = valorOrigem;
            else
                valorTotal += valorOrigem;

            Valor = valorTotal;
        }

        public string ObterValorConvertido(OpcoesFormatacao formato) {
            return Valor.ObterValorConvertido(formato);
           
        }

        public void Zerar() {
            Valor = null;
        }

        public TabelaColunaTotal<T> Clonar() {
            return MemberwiseClone() as TabelaColunaTotal<T>;
        }
    }
}
