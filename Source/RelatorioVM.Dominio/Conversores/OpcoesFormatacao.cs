using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Conversores
{
    public class OpcoesFormatacao
    {
        /// <summary>
        /// Define as casas decimais ao converter valores. Valor padrão: 2
        /// </summary>
        public int CasasDecimais { get; set; }

        /// <summary>
        /// Define o formato ao converter datas. Valor padrão: dd/MM/yyyy
        /// </summary>
        public string FormatoData { get; set; }

        /// <summary>
        /// Define o formato ao converter data e hora. Valor padrão: dd/MM/yyyy HH:mm
        /// </summary>
        public string FormatoDataHora { get; set; }

        /// <summary>
        /// Texto exibido quando um valor null é encontrado. Valor padrão é um texto vazio
        /// </summary>
        public string ValorNulavel { get; set; }

        public OpcoesFormatacao()
        {
            CasasDecimais = 2;
            FormatoData = "dd/MM/yyyy";
            FormatoDataHora = "dd/MM/yyyy HH:mm";
            ValorNulavel = string.Empty;
        }        
    }
}
