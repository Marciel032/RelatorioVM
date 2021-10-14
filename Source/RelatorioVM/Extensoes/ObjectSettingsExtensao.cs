using DinkToPdf;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Extensoes
{
    internal static class ObjectSettingsExtensao
    {
        public static void Configurar(this ObjectSettings documento, ConfiguracaoRelatorio configuracao) {
            documento.PagesCount = true;
            documento.WebSettings.DefaultEncoding = "utf-8";
            documento.HeaderSettings.Configurar(configuracao.Cabecalho);
            documento.FooterSettings.Configurar(configuracao.Rodape);
        }

        private static void Configurar(this HeaderSettings cabecalho, ConfiguracaoCabecalhoRelatorio configuracao) {
            cabecalho.FontName = "Arial";
            cabecalho.FontSize = 9;
            cabecalho.Left = configuracao.ElementoEsquerda.ObterValor();
            cabecalho.Center = configuracao.ElementoCentro.ObterValor();
            cabecalho.Right = configuracao.ElementoDireita.ObterValor();
            cabecalho.Line = true;
        }

        private static void Configurar(this FooterSettings rodape, ConfiguracaoRodapeRelatorio configuracao)
        {
            rodape.FontName = "Arial";
            rodape.FontSize = 9;
            rodape.Left = configuracao.ElementoEsquerda.ObterValor();
            rodape.Center = configuracao.ElementoCentro.ObterValor();
            rodape.Right = configuracao.ElementoDireita.ObterValor();
            rodape.Line = true;
        }
    }
}
