using DinkToPdf;
using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Extensoes
{
    internal static class OrientacaoExtensao
    {
        public static Orientation ParaDinkPDF(this TipoOrientacao orientacao) {
            switch (orientacao)
            {
                case TipoOrientacao.Paisagem:
                    return Orientation.Landscape;
                case TipoOrientacao.Retrato: 
                default:
                    return Orientation.Portrait;
            }
        }
    }
}
