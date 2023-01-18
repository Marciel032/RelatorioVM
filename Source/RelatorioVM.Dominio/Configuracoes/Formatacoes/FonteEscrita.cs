using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes.Formatacoes
{
    public class FonteEscrita
    {
        /// <summary>
        /// Nome da fonte pré configurada.
        /// </summary>
        public TipoFonteEscrita Nome { get; set; } = TipoFonteEscrita.Arial;

        public bool Italico { get; set; } = false;

        public bool Negrito { get; set; } = false;

        /// <summary>
        /// Tamanho da fonte.
        /// </summary>
        public int Tamanho { get; set; } = 12;

        public bool Sublinhado { get; set; } = false;

        /// <summary>
        /// Nome da fonte personalizada.
        /// </summary>
        public string TipoPersonalizado { get; set; } = string.Empty;
    }
}
