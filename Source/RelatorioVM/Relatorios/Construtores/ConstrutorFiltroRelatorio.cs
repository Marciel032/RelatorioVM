using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Configuracoes.Interfaces;
using RelatorioVM.Elementos.Propriedades;
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
        private ConfiguracaoRelatorio _configuracaoRelatorio;
        private T _filtroVM;
        private Dictionary<string, Filtro> _filtros;

        public ConstrutorFiltroRelatorio(ConfiguracaoRelatorio configuracaoRelatorio, T filtroVM)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _filtroVM = filtroVM;
            _filtros = _filtroVM
                .ObterPropriedades()
                .Where(x => x.PodeSerFiltro(_filtroVM))
                .Select(x => x.ObterFiltro())
                .ToDictionary(x => x.Identificador);
        }        

        public FiltrosElemento Construir() {
            foreach (var filtro in _filtros.Values)
            {
                if (!string.IsNullOrWhiteSpace(filtro.Valor))
                    continue;

                filtro.Valor = filtro.Propriedade.ObterValorConvertido(_filtroVM, _configuracaoRelatorio.Formatacao);
                if (filtro.PropriedadeComplemento != null)
                    filtro.ValorComplemento = filtro.PropriedadeComplemento.ObterValorConvertido(_filtroVM, _configuracaoRelatorio.Formatacao);
            }

            return new FiltrosElemento(_configuracaoRelatorio)
            {
                Filtros = _filtros.Values.ToList()
            };
        }

        public IFiltroRelatorioVM<T> Ignorar<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao)
        {            
            var propriedade = propriedadeExpressao.ObterPropriedadeBase();
            _filtros.Remove(propriedade.Name);
            return this;
        }

        public IFiltroRelatorioVM<T> ComplementarValor<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao, string valor)
        {
            var filtro = Obterfiltro(propriedadeExpressao);
            if (filtro != null)
                filtro.ValorComplemento = valor;
            return this;
        }

        public IFiltroRelatorioVM<T> ComplementarValor<TPropriedade, TPropriedadeComplemento>(Expression<Func<T, TPropriedade>> propriedadeExpressao, Expression<Func<T, TPropriedadeComplemento>> propriedadeComplementoExpressao, bool ignorar = true)
        {
            var filtro = Obterfiltro(propriedadeExpressao);
            if (filtro != null)
                filtro.PropriedadeComplemento = new Propriedade() { 
                    PropriedadeInformacao = propriedadeComplementoExpressao.ObterPropriedade(),
                    FuncaoPropriedade = () => propriedadeComplementoExpressao.Compile()(_filtroVM)
                };

            if (ignorar)
                Ignorar(propriedadeComplementoExpressao);

            return this;
        }

        public IFiltroRelatorioVM<T> ComplementarValor<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao, Func<TPropriedade, string> funcao) 
        {
            var filtro = Obterfiltro(propriedadeExpressao);
            if (filtro != null)
                filtro.ValorComplemento = funcao?.Invoke(propriedadeExpressao.Compile()(_filtroVM));
            return this;
        }

        public IFiltroRelatorioVM<T> Valor<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao, string valor)
        {
            var filtro = Obterfiltro(propriedadeExpressao);
            if(filtro != null)
                filtro.Valor = valor;
            return this;
        }

        public IFiltroRelatorioVM<T> Valor<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao, Func<TPropriedade, string> funcao) {
            var filtro = Obterfiltro(propriedadeExpressao);
            if (filtro != null)
                filtro.Valor = funcao?.Invoke(propriedadeExpressao.Compile()(_filtroVM));
            return this;
        }

        public IFiltroRelatorioVM<T> Nome<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao, string nome)
        {
            var filtro = Obterfiltro(propriedadeExpressao);
            if (filtro != null)
                filtro.Nome = nome;
            return this;
        }

        public IFiltroRelatorioVM<T> Separador<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao, string separador)
        {
            var filtro = Obterfiltro(propriedadeExpressao);
            if (filtro != null)
                filtro.Separador = separador;
            return this;
        }

        private Filtro Obterfiltro<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao) {
            var propriedade = propriedadeExpressao.ObterPropriedade();
            if (_filtros.ContainsKey(propriedade.Name))
                return _filtros[propriedade.Name];

            return null;
        }        
    }
}
