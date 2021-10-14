using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RelatorioVM.Demo.Modelos
{
    public class ExemploSimplesViewModel
    {
        [DisplayName("Filial")]
        public int? FilialCodigo { get; set; }
        public int? PessoaCodigo { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }

        public List<ExemploSimplesItemViewModel> Itens { get; set; }
    }
}
