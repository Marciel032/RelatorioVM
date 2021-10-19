using RelatorioVM.Dominio.Atributos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RelatorioVM.Demo.Modelos
{
    public class ExemploSimplesItemViewModel
    {
        [ColunaRelatorio(Nome = "Nome de teste")]
        public DateTime Data { get; set; }

        [DisplayName("Filial")]
        public int FilialCodigo { get; set; }

        [DisplayName("Pessoa código")]
        public int PessoaCodigo { get; set; }

        public PessoaViewModel Pessoa { get; set; }        

        public bool Ativo { get; set; }

        [ColunaRelatorio(Nome = "Situação")]
        public TipoSituacao Situacao { get; set; }

        public decimal Valor { get; set; }        
    }

    public enum TipoSituacao { 
        Digitado,
        [DisplayName("Em processamento")]
        EmProcessamento,
        Cancelado,
        Processado
    }
}
