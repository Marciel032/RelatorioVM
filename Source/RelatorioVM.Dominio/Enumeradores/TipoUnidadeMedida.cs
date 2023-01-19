using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RelatorioVM.Dominio.Enumeradores
{
    public enum TipoUnidadeMedida
    {
        [Display(Name = "%")]
        Percentual,
        [Display(Name = "px")]
        Pixel
    }
}
