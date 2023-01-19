using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RelatorioVM.Dominio.Enumeradores
{
    public enum TipoDirecaoMedida
    {
        [Display(Name = "height")]
        Altura,
        [Display(Name = "width")]
        Largura
    }
}
