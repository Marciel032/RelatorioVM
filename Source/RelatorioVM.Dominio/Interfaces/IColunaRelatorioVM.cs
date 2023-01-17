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
        IColunaRelatorioVM<TConteudo> Titulo(string titulo);        
    }
}
