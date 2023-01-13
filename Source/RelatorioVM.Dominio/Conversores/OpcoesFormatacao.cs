using RelatorioVM.Dominio.Configuracoes.Formatacoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Conversores
{
    public class OpcoesFormatacao
    {
        private Dictionary<string, string> valorNulavelTipo;

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

        /// <summary>
        /// Fonte utilizada no relatório.
        /// </summary>
        public FonteEscrita Fonte { get; set; }

        public OpcoesFormatacao()
        {
            CasasDecimais = 2;
            FormatoData = "dd/MM/yyyy";
            FormatoDataHora = "dd/MM/yyyy HH:mm";
            ValorNulavel = string.Empty;
            valorNulavelTipo = new Dictionary<string, string>();
            Fonte = new FonteEscrita();

            DefinirValorNulavelParaOTipo<DateTime>("__/__/____");
        }

        /// <summary>
        /// Configura um texto padrão para conversões de valores nulos, por tipo.
        /// </summary>
        /// <typeparam name="T">Tipo do valor a ser configurado</typeparam>
        /// <param name="valor">Texto que vai ser exibido sempre que o tipo for null</param>
        public void DefinirValorNulavelParaOTipo<T>(string valor)
        {
            var nomeTipo = typeof(T).FullName;
            if (valorNulavelTipo.ContainsKey(nomeTipo))
                valorNulavelTipo[nomeTipo] = valor;
            else
                valorNulavelTipo.Add(nomeTipo, valor);
        }

        public bool ObterValorNulavelParaOTipo(string tipo, out string valor)
        {
            valor = ValorNulavel;
            if (valorNulavelTipo.ContainsKey(tipo))
            {
                valor = valorNulavelTipo[tipo];
                return true;
            }

            return !string.IsNullOrEmpty(ValorNulavel);
        }
    }
}
