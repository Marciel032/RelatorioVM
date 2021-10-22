using RelatorioVM.Dominio.Configuracoes.Interfaces;
using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes
{
    public class ConfiguracaoRelatorio : IConfiguracaoRelatorio
    {
        public TipoOrientacao Orientacao { get; set; }

        public ConfiguracaoCabecalhoRelatorio Cabecalho { get; set; }

        public ConfiguracaoRodapeRelatorio Rodape { get; set; }

        public ConfiguracaoConteudoRelatorio Conteudo { get; set; }

        public OpcoesFormatacao Formatacao { get; set; }

        public ConfiguracaoRelatorio()
        {           
            Orientacao = TipoOrientacao.Retrato;
            Cabecalho = new ConfiguracaoCabecalhoRelatorio();
            Rodape = new ConfiguracaoRodapeRelatorio();
            Conteudo = new ConfiguracaoConteudoRelatorio();
            Formatacao = new OpcoesFormatacao();
        }

        public IConfiguracaoRelatorio UsarOrientacao(TipoOrientacao orientacao)
        {
            Orientacao = orientacao;
            return this;
        }

        public IConfiguracaoRelatorio ConfigurarCabecalho(Action<IConfiguracaoCabecalhoRelatorio> configurarCabecalho)
        {
            configurarCabecalho?.Invoke(Cabecalho);
            return this;
        }

        public IConfiguracaoRelatorio ConfigurarRodape(Action<IConfiguracaoRodapeRelatorio> configurarRodape)
        {
            configurarRodape?.Invoke(Rodape);
            return this;
        }

        public IConfiguracaoRelatorio ConfigurarFormatacao(Action<OpcoesFormatacao> configurarFormatacao)
        {
            configurarFormatacao?.Invoke(Formatacao);
            return this;
        }

        public ConfiguracaoRelatorio Clone()
        {
            ConfiguracaoRelatorio clone = MemberwiseClone() as ConfiguracaoRelatorio;
            clone.Cabecalho = Cabecalho.Clone();
            clone.Rodape = Rodape.Clone();
            clone.Conteudo = Conteudo.Clone();
            return clone;
        }

        public IConfiguracaoRelatorio ConfigurarConteudo(Action<IConfiguracaoConteudoRelatorio> configurarConteudo)
        {
            configurarConteudo?.Invoke(Conteudo);
            return this;
        }
    }
}
