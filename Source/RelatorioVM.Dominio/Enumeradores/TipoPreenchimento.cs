using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RelatorioVM.Dominio.Enumeradores
{
    public enum TipoPreenchimento
    {
        [Display(Name = "padding")]
        Tudo,
        [Display(Name = "padding-left")]
        Esquerda,
        [Display(Name = "padding-right")]
        Direita,
        [Display(Name = "padding-top")]
        Topo,
        [Display(Name = "padding-botton")]
        Fundo
    }
}
