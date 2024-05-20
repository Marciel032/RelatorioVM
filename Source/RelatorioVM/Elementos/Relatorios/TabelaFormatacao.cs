using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Elementos.Propriedades;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaFormatacao<T>
    {
        public Propriedade<T> CorFundoLinhaConteudoPropriedade { get; set; }

        public string ObterCorFundoLinhaConteudo(T conteudo) { 
            if(CorFundoLinhaConteudoPropriedade == null)
                return string.Empty;

            var valor = CorFundoLinhaConteudoPropriedade.ObterValor(conteudo);

            if (valor == null)
                return string.Empty;

            if (valor.GetType().ObterTipoNaoNullo() == typeof(string))
                return valor as string;

            if (valor.GetType().ObterTipoNaoNullo() == typeof(Color))
                return ((Color)valor).ParaHexadecimalString();

            if (valor.GetType().ObterTipoNaoNullo() == typeof(TipoCor))
                return ((TipoCor)valor).ObterCorHtml();

            return string.Empty;
        }
    }
}
