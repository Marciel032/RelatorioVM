using RelatorioVM.Dominio.Configuracoes.Interfaces;
using RelatorioVM.Elementos.Relatorios;
using RelatorioVM.Extensoes;
using RelatorioVM.Relatorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RelatorioVM.Relatorios.Construtores
{
    internal class ConstrutorFiltroRelatorio<T>: IFiltroRelatorioVM<T>
    {
        private T _filtroVM;
        private List<Filtro> _filtros;

        public ConstrutorFiltroRelatorio(T filtroVM)
        {
            _filtroVM = filtroVM;
            _filtros = _filtroVM
                .ObterPropriedades()
                .Where(x => x.PodeSerFiltro(_filtroVM))
                .Select(x => x.ObterFiltro(_filtroVM))
                .ToList();
        }

        public FiltrosElemento Construir() {
            return new FiltrosElemento()
            {
                Filtros = _filtros
            };
        }

        public IFiltroRelatorioVM<T> Ignorar<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao)
        {
            var propriedade = propriedadeExpressao.ObterPropriedade(_filtroVM);
            _filtros.RemoveAll(x => x.Identificador == propriedade.Name);
            return this;
        }
    }
}
