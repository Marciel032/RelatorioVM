using Microsoft.Extensions.DependencyInjection;
using RelatorioVM.Configuradores;
using RelatorioVM.Conversores;
using RelatorioVM.Dominio.Atributos;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Configuracoes.Interfaces;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Infraestruturas;
using RelatorioVM.Relatorios.Construtores;
using RelatorioVM.Relatorios.Fabricas;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace RelatorioVM.Extensoes
{
    public static class CorExtensao
    {
        public static string ParaHexadecimalString(this Color cor) {
            if (cor == Color.Empty)
                return string.Empty;

            return $"#{cor.R:X2}{cor.G:X2}{cor.B:X2}"; 
        }

        public static string ObterCorHtml(this TipoCor cor)
        {
            var atributoTipoCor = cor.GetType()
                            .GetMember(cor.ToString())
                            .FirstOrDefault()
                            .GetCustomAttribute<TipoCorAttribute>();

            if (atributoTipoCor == null)
                return string.Empty;

            return atributoTipoCor.CorHtml;
        }

        public static string ObterCorContraste(this TipoCor cor)
        {
            var atributoTipoCor = cor.GetType()
                            .GetMember(cor.ToString())
                            .FirstOrDefault()
                            .GetCustomAttribute<TipoCorAttribute>();

            if (atributoTipoCor == null)
                return string.Empty;

            return atributoTipoCor.CorContraste.ObterCorHtml();
        }

        public static (string, string) ObterCoresHtml(this TipoCor cor)
        {
            var atributoTipoCor = cor.GetType()
                            .GetMember(cor.ToString())
                            .FirstOrDefault()
                            .GetCustomAttribute<TipoCorAttribute>();

            if (atributoTipoCor == null)
                return (string.Empty, string.Empty);

            return (atributoTipoCor.CorHtml, atributoTipoCor.CorContraste.ObterCorHtml());
        }
    }
}
