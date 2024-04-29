using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RelatorioVM.Dominio.Atributos
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColunaRelatorioAttribute: Attribute
    {
        /// <summary>
        /// Define o titulo exibido na coluna.
        /// </summary>
        public string Titulo { get; set; } = string.Empty;

        /// <summary>
        /// Define se a coluna é exibida no relatório.
        /// </summary>
        public bool Visivel { get; set; } = true;

        /// <summary>
        /// Define um texto para ser exibido antes de cada valor da coluna.
        /// </summary>
        public string Prefixo { get; set; } = string.Empty;
    }
}
