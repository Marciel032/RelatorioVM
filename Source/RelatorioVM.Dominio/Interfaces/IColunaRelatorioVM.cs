using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        /// <summary>
        /// Define a cor da coluna onde o conteudo é exibido. Não altera a cor do titulo nem dos totais.
        /// </summary>
        IColunaRelatorioVM<TConteudo> DefinirCorConteudo(TipoCor corFundo, TipoCor corConteudo = TipoCor.Indefinido);

        /// <summary>
        /// Define a cor da coluna onde o conteudo é exibido. Não altera a cor do titulo nem dos totais.
        /// </summary>
        IColunaRelatorioVM<TConteudo> DefinirCorConteudo(Color corFundo, Color corConteudo);
                
    }
}
