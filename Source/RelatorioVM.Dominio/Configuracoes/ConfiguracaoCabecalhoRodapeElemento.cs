using RelatorioVM.Dominio.Configuracoes.Interfaces;
using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes
{
    public class ConfiguracaoCabecalhoRodapeElemento: IConfiguracaoElemento
    {
        public IConfiguracaoCabecalhoRodapeRelatorioBase Parent;

        public TipoElementoCabecalhoRodape Tipo { get; set; }

        public TipoPosicaoCabecalhoRodape Posicao { get; set; }

        /// <summary>
        /// Informe o valor do Texto fixo, quando este tipo for informado.
        /// Tambem é possivel informar o formato da data e hora, quando algum tipo de data for informado.
        /// </summary>
        public string Valor { get; set; }

        public ConfiguracaoCabecalhoRodapeElemento(IConfiguracaoCabecalhoRodapeRelatorioBase parent, TipoPosicaoCabecalhoRodape posicao)
        {
            Parent = parent;
            Posicao = posicao;
            Tipo = TipoElementoCabecalhoRodape.Vazio;
            Valor = string.Empty;
        }

        public IConfiguracaoCabecalhoRodapeRelatorioBase ImprimirTexto(string texto)
        {
            Tipo = TipoElementoCabecalhoRodape.TextoFixo;
            Valor = texto;
            return Parent;
        }

        public IConfiguracaoCabecalhoRodapeRelatorioBase ImprimirData(string formato = "dd/MM/yyyy")
        {
            Tipo = TipoElementoCabecalhoRodape.Data;
            Valor = formato;
            return Parent;
        }

        public IConfiguracaoCabecalhoRodapeRelatorioBase ImprimirDataHora(string formato = "dd/MM/yyyy HH:mm")
        {
            Tipo = TipoElementoCabecalhoRodape.Data;
            Valor = formato;
            return Parent;
        }

        public IConfiguracaoCabecalhoRodapeRelatorioBase ImprimirNumeroDePaginas(string prefixo = "Página ")
        {
            Tipo = TipoElementoCabecalhoRodape.NumeroDaPagina;
            Valor = prefixo;
            return Parent;
        }

        public IConfiguracaoCabecalhoRodapeRelatorioBase ImprimirTitulo()
        {
            Tipo = TipoElementoCabecalhoRodape.Titulo;
            Valor = String.Empty;
            return Parent;
        }

        public ConfiguracaoCabecalhoRodapeElemento Clone(IConfiguracaoCabecalhoRodapeRelatorioBase parent) {
            var clone = MemberwiseClone() as ConfiguracaoCabecalhoRodapeElemento;
            clone.Parent = parent;
            return clone;
        }
    }
}
