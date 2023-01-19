﻿using RelatorioVM.Dominio.Configuracoes.Formatacoes;
using RelatorioVM.Elementos.Estilos;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Extensoes
{
    internal static class FonteExtensao
    {
        internal static string ObterEstilo(this FonteEscrita fonte, string classe) {
            StringBuilder fonteFormatada = new StringBuilder();
            fonteFormatada.Append($".{classe} ");

            fonteFormatada
                .Append("{")
                .Append($"font-family: {fonte.Nome.ObterDescricao()};");

            if (fonte.Tamanho > 0)
                fonteFormatada.Append($"font-size: {fonte.Tamanho}px;");

            if (fonte.Italico)
                fonteFormatada.Append($"font-style: italic;");

            if (fonte.Negrito)
                fonteFormatada.Append($"font-weight: bold;");

            fonteFormatada.Append("}");

            return fonteFormatada.ToString();
        }

        internal static EstiloElemento ObterEstiloElemento(this FonteEscrita fonte, string classe)
        {
            return new EstiloElemento()
                .AdicionarClasse(classe)
                .DefinirFonte(fonte);
        }
    }
}
