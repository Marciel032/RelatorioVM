using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RelatorioVM.Dominio.Enumeradores
{
    public enum TipoPosicaoCabecalhoRodape
    {
        [Display(Name = "header-top-left")]
        CabecalhoEsquerdo,
        [Display(Name = "header-top-center")]
        CabecalhoCentro,
        [Display(Name = "header-top-right")]
        CabecalhoDireito,
        [Display(Name = "footer-bottom-left")]
        RodapeEsquerdo,
        [Display(Name = "footer-bottom-center")]
        RodapeCentro,
        [Display(Name = "footer-bottom-right")]
        RodapeDireito
    }
}
