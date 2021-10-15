using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RelatorioVM.Demo.Modelos
{
    public class ExemploSimplesItemViewModel
    {
        [DisplayName("Filial")]
        public int FilialCodigo { get; set; }

        [DisplayName("Pessoa código")]
        public int PessoaCodigo { get; set; }

        [DisplayName("Nome")]
        public string PessoaNome { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
    }
}
