using RelatorioVM.Dominio.Configuracoes.Formatacoes;
using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes.Interfaces
{
    public interface IConfiguracaoFormatacaoRelatorio
    {
        /// <summary>
        /// Aplica a fonte informada em todo o relatório.
        /// </summary>
        /// <param name="fonte">Nome da fonte que será utilizada</param>
        IConfiguracaoFormatacaoRelatorio UsarFonte(TipoFonteEscrita fonte);

        /// <summary>
        /// Define as casas decimais ao converter valores. Valor padrão: 2
        /// </summary>
        IConfiguracaoFormatacaoRelatorio DefinirQuantidadeCasasDecimais(int casasDecimais);

        /// <summary>
        /// Define o formato ao converter datas. Valor padrão: dd/MM/yyyy
        /// </summary>
        IConfiguracaoFormatacaoRelatorio DefinirFormatoData(string formato);

        /// <summary>
        /// Define o formato ao converter data e hora. Valor padrão: dd/MM/yyyy HH:mm
        /// </summary>
        IConfiguracaoFormatacaoRelatorio DefinirFormatoDataHora(string formato);

        /// <summary>
        /// Texto exibido quando um valor null é encontrado. Valor padrão é um texto vazio
        /// </summary>
        IConfiguracaoFormatacaoRelatorio DefinirValorNulavel(string valor);

        /// <summary>
        /// Configura um texto padrão para conversões de valores nulos, por tipo.
        /// </summary>
        /// <typeparam name="T">Tipo do valor a ser configurado</typeparam>
        /// <param name="valor">Texto que vai ser exibido sempre que o tipo for null</param>
        IConfiguracaoFormatacaoRelatorio DefinirValorNulavelParaOTipo<T>(string valor);

        /// <summary>
        /// Permite configurar detalhes da fonte utilizada no conteudo do relatorio
        /// </summary>
        IConfiguracaoFormatacaoRelatorio ConfigurarFonteConteudo(Action<FonteEscrita> configurarFonteConteudo);

        /// <summary>
        /// Permite configurar detalhes da fonte utilizada no titulo do relatorio
        /// </summary>
        IConfiguracaoFormatacaoRelatorio ConfigurarFonteTitulo(Action<FonteEscrita> configurarFonteConteudo);
    }
}
