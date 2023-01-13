using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RelatorioVM.Dominio.Enumeradores
{
    public enum TipoFonteEscrita
    {
        [Display(Name = "Arial")]
        Arial,
        [Display(Name = "Arial Narrow")]
        ArialNarrow,
        [Display(Name = "Consolas")]
        Consolas,
        [Display(Name = "Courier New")]
        CourierNew,
        [Display(Name = "MS Sans Serif")]
        MSSansSerif,
        [Display(Name = "Times New Roman")]
        TimesNewRoman,
    }
}
