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

        [Display(Name = "Book Antiqua")]
        BookAntiqua,

        [Display(Name = "Calibri")]
        Calibri,
        [Display(Name = "Cambria")]
        Cambria,
        [Display(Name = "Century")]
        Century,
        [Display(Name = "Comic Sans MS")]
        ComicSansMS,
        [Display(Name = "Consolas")]
        Consolas,
        [Display(Name = "Courier New")]
        CourierNew,
        [Display(Name = "Cursive")]
        Cursive,

        [Display(Name = "Fangsong")]
        Fangsong,
        [Display(Name = "Fantasy")]
        Fantasy,

        [Display(Name = "Garamond")]
        Garamond,
        [Display(Name = "Georgia")]
        Georgia,

        [Display(Name = "Impact")]
        Impact,

        [Display(Name = "Monospace")]
        Monospace,
        [Display(Name = "Monotype Corsiva")]
        MonotypeCorsiva,
        [Display(Name = "MS Sans Serif")]
        MSSansSerif,

        [Display(Name = "Palatino Linotype")]
        PalatinoLinotype,

        [Display(Name = "Sans-Serif")]
        SansSerif,
        [Display(Name = "Serif")]
        Serif,
        [Display(Name = "System-UI")]
        SystemUI,

        [Display(Name = "Times New Roman")]
        TimesNewRoman,

        [Display(Name = "Tahoma")]
        Tahoma,

        [Display(Name = "Verdana")]
        Verdana,
    }
}
