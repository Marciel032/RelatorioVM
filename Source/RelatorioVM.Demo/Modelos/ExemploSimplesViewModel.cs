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
        public string FilialNome { get; set; }
        public int? PessoaCodigo { get; set; }

        public PessoaViewModel Pessoa { get; set; }

        [DisplayName("Data inicial")]
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public int OperacaoCodigo { get; set; }
        public string OperacaoNome { get; set; }
        public string Usuario { get; set; }
        public List<ExemploSimplesItemViewModel> Itens { get; set; }
    }
}
