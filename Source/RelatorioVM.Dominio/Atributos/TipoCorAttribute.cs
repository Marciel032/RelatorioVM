using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RelatorioVM.Dominio.Atributos
{
    [AttributeUsage(AttributeTargets.Field)]
    public class TipoCorAttribute: Attribute
    {
        /// <summary>
        /// Define a cor a ser usada ao gerar o html.
        /// </summary>
        public string CorHtml { get; set; } = string.Empty;

        /// <summary>
        /// Define a cor a ser utilizada no texto, para ficar visivel quando o fundo é muito escuro.
        /// </summary>
        public TipoCor CorContraste { get; set; }
    }
}
