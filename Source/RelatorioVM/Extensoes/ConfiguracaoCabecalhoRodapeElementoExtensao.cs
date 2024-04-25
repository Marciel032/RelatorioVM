using RelatorioVM.Dominio.Configuracoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Extensoes
{
    internal static class ConfiguracaoCabecalhoRodapeElementoExtensao
    {
        public static string ObterValor(this ConfiguracaoCabecalhoRodapeElemento elemento) {
            switch (elemento.Tipo)
            {                
                case Dominio.Enumeradores.TipoElementoCabecalhoRodape.TextoFixo:
                    return elemento.Valor;
                case Dominio.Enumeradores.TipoElementoCabecalhoRodape.Data:
                    return DateTime.Now.ToString(elemento.Valor);
                case Dominio.Enumeradores.TipoElementoCabecalhoRodape.NumeroDaPagina:
                    return $"{elemento.Valor} ";
                case Dominio.Enumeradores.TipoElementoCabecalhoRodape.Titulo:
                    return "element(titulo)";
                case Dominio.Enumeradores.TipoElementoCabecalhoRodape.Vazio:                    
                default:
                    return string.Empty;
            }
        }
    }
}
