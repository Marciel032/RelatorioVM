using RelatorioVM.Dominio.Atributos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace RelatorioVM.Demo.Modelos
{
    public class ExemploSimplesItemViewModel
    {
        [ColunaRelatorio(Titulo = "Campo de data")]
        public DateTime Data { get; set; }

        [DisplayName("Filial")]
        public int FilialCodigo { get; set; }

        [ColunaRelatorio(Titulo = "Pessoa")]
        public int PessoaCodigo { get; set; }

        public PessoaViewModel Pessoa { get; set; }        

        public bool Ativo { get; set; }        
        public decimal Valor { get; set; }

        [ColunaRelatorio(Titulo = "Situação")]
        public TipoSituacao Situacao { get; set; }

        [ColunaRelatorio(Titulo = "Município")]
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string CorFundoLinha { get; set; }
        public Color Cor { get; set; }
        public string Imagem { get; set; }
        public List<ProdutoViewModel> Produtos { get; set; }
    }

    public enum TipoSituacao { 
        Digitado,
        [DisplayName("Em processamento")]
        EmProcessamento,
        Cancelado,
        Processado
    }
}
