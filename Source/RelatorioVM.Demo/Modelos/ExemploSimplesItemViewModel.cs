using RelatorioVM.Dominio.Atributos;
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

        public PessoaViewModel Pessoa { get; set; }

        [ColunaRelatorio(Nome = "Nome de teste")]
        public DateTime Data { get; set; }

        public bool Ativo { get; set; }

        public decimal Valor { get; set; }        
    }
}
