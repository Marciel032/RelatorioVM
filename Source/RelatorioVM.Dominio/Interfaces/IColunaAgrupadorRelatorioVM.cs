using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RelatorioVM.Dominio.Interfaces
{
    public interface IColunaAgrupadorRelatorioVM
    {
        /// <summary>
        /// Oculta a coluna na descrição do titulo do agrupador.
        /// </summary>
        IColunaAgrupadorRelatorioVM OcultarNoTitulo();

        /// <summary>
        /// Oculta a coluna na descrição do total do agrupador.
        /// </summary>
        IColunaAgrupadorRelatorioVM OcultarNoTotal();

        /// <summary>
        /// Oculta a coluna na descrição do título e no total do agrupador.
        /// </summary>
        IColunaAgrupadorRelatorioVM Ocultar();
    }
}
