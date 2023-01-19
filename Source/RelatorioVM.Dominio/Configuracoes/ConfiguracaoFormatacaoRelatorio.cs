using RelatorioVM.Dominio.Configuracoes.Formatacoes;
using RelatorioVM.Dominio.Configuracoes.Interfaces;
using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Conversores
{
    public class ConfiguracaoFormatacaoRelatorio: IConfiguracaoFormatacaoRelatorio
    {
        private Dictionary<string, string> valorNulavelTipo;
        
        public int CasasDecimais { get; set; }
        public string FormatoData { get; set; }        
        public string FormatoDataHora { get; set; }        
        public string ValorNulavel { get; set; }

        /// <summary>
        /// Fonte utilizada no relatório.
        /// </summary>
        public FonteEscrita FonteConteudo { get; set; }

        /// <summary>
        /// Fonte utilizada no título do relatório.
        /// </summary>
        public FonteEscrita FonteTitulo { get; set; }

        public ConfiguracaoFormatacaoRelatorio()
        {
            CasasDecimais = 2;
            FormatoData = "dd/MM/yyyy";
            FormatoDataHora = "dd/MM/yyyy HH:mm";
            ValorNulavel = string.Empty;
            valorNulavelTipo = new Dictionary<string, string>();
            FonteConteudo = new FonteEscrita();
            FonteTitulo = new FonteEscrita() { Tamanho = 16 };

            DefinirValorNulavelParaOTipo<DateTime>("--/--/----");
        }

        public IConfiguracaoFormatacaoRelatorio DefinirValorNulavelParaOTipo<T>(string valor)
        {
            var nomeTipo = typeof(T).FullName;
            if (valorNulavelTipo.ContainsKey(nomeTipo))
                valorNulavelTipo[nomeTipo] = valor;
            else
                valorNulavelTipo.Add(nomeTipo, valor);

            return this;
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

        public bool ObterValorNulavelParaOTipo<T>(out string valor)
        {
            return ObterValorNulavelParaOTipo(typeof(T).FullName, out valor);
        }

        public IConfiguracaoFormatacaoRelatorio DefinirQuantidadeCasasDecimais(int casasDecimais)
        {
            CasasDecimais = casasDecimais;
            return this;
        }

        public IConfiguracaoFormatacaoRelatorio DefinirFormatoData(string formato)
        {
            FormatoData = formato;
            return this;
        }

        public IConfiguracaoFormatacaoRelatorio DefinirFormatoDataHora(string formato)
        {
            FormatoDataHora = formato;
            return this;
        }

        public IConfiguracaoFormatacaoRelatorio DefinirValorNulavel(string valor)
        {
            ValorNulavel = valor;
            return this;
        }

        public IConfiguracaoFormatacaoRelatorio UsarFonte(TipoFonteEscrita fonte)
        {
            FonteConteudo.Nome = fonte;
            FonteTitulo.Nome = fonte;
            return this;
        }

        public IConfiguracaoFormatacaoRelatorio ConfigurarFonteConteudo(Action<FonteEscrita> configurarFonteConteudo)
        {
            configurarFonteConteudo?.Invoke(FonteConteudo);
            return this;
        }

        public IConfiguracaoFormatacaoRelatorio ConfigurarFonteTitulo(Action<FonteEscrita> configurarFonteTitulo)
        {
            configurarFonteTitulo?.Invoke(FonteTitulo);
            return this;
        }
    }
}
