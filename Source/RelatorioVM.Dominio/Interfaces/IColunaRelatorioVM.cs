using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RelatorioVM.Dominio.Interfaces
{
    public interface IColunaRelatorioVM<TConteudo>
    {
        /// <summary>
        /// Define o titulo que sera exibido na coluna.
        /// </summary>
        IColunaRelatorioVM<TConteudo> DefinirTitulo(string titulo);

        /// <summary>
        /// Define o alinhamento horizontal da coluna e do titulo.
        /// </summary>
        IColunaRelatorioVM<TConteudo> DefinirAlinhamentoHorizontal(TipoAlinhamentoHorizontal alinhamento);

        /// <summary>
        /// Define o alinhamento horizontal do titulo. Isso não vai alterar o alinhamento da coluna.
        /// </summary>
        IColunaRelatorioVM<TConteudo> DefinirAlinhamentoHorizontalTitulo(TipoAlinhamentoHorizontal alinhamento);

        /// <summary>
        /// Define o alinhamento horizontal da coluna. Isso não vai alterar o alinhamento do titulo.
        /// </summary>
        IColunaRelatorioVM<TConteudo> DefinirAlinhamentoHorizontalColuna(TipoAlinhamentoHorizontal alinhamento);

        /// <summary>
        /// Define o separador utilizado em colunas com o valor complementado.
        /// </summary>
        IColunaRelatorioVM<TConteudo> DefinirSeparador(string separador);

        /// <summary>
        /// Define o prefixo exibido antes do valor em cada coluna. Ex. R$ 10,00
        /// </summary>
        IColunaRelatorioVM<TConteudo> DefinirPrefixoColuna(string prefixo);

        /// <summary>
        /// Define o condensamento da coluna. Isso remove os espaços em branco na direita e esquerda da coluna.
        /// </summary>
        IColunaRelatorioVM<TConteudo> DefinirCondensado(bool condensado);

        /// <summary>
        /// Quando a quebra de linha da coluna está ativa, o texto pode ser quebrado para diminuir o espaço utilizado pela coluna.
        /// </summary>
        IColunaRelatorioVM<TConteudo> PermitirQuebraDeLinha(bool quebraDeLinha);

    }
}
