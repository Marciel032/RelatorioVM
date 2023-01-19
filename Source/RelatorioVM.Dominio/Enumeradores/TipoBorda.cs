using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RelatorioVM.Dominio.Enumeradores
{
    public enum TipoBorda
    {
        [Display(Name = "border")]
        Tudo,
        [Display(Name = "border-left")]
        Esquerda,
        [Display(Name = "border-right")]
        Direita,
        [Display(Name = "border-top")]
        Topo,
        [Display(Name = "border-bottom")]
        Fundo
    }
}
