using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RelatorioVM.Dominio.Interfaces
{
    public interface IColunaTotalRelatorioVM<TConteudo>
    {
        /// <summary>
        /// Define o titulo que sera exibido na coluna.
        /// </summary>
        IColunaTotalRelatorioVM<TConteudo> Titulo(string titulo);        
    }
}
