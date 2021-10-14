using RelatorioVM.Dominio.Configuracoes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RelatorioVM.Relatorios.Interfaces
{
    public interface IFiltroRelatorioVM<T>
    {
        IFiltroRelatorioVM<T> Ignorar<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao);
    }
}
