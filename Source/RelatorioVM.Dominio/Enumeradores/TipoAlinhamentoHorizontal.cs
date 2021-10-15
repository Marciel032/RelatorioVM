using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RelatorioVM.Dominio.Enumeradores
{
    public enum TipoAlinhamentoHorizontal
    {
        [Display(Name = "left")]
        Esquerda,
        [Display(Name = "center")]
        Centro,
        [Display(Name = "right")]
        Direita
    }
}
