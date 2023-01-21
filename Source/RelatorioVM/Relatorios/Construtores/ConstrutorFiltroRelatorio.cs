using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Configuracoes.Interfaces;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Elementos.Propriedades;
using RelatorioVM.Elementos.Relatorios;
using RelatorioVM.Extensoes;
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
        private Dictionary<string, Filtro<T>> _filtros;

        public ConstrutorFiltroRelatorio(ConfiguracaoRelatorio configuracaoRelatorio)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _filtros = typeof(T)
                .ObterPropriedades()
                .Where(x => x.PodeSerFiltro(_filtroVM))
                .Select(x => x.ObterFiltro<T>())
                .ToDictionary(x => x.Identificador);
        }        

        public FiltrosElemento<T> Construir() {
            return new FiltrosElemento<T>(_configuracaoRelatorio)
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
                filtro.PropriedadeComplemento = new Propriedade<T>(filtro.Propriedade.PropriedadeInformacao)
                {
                    FuncaoPropriedade = (origem) => valor
                };
            return this;
        }

        public IFiltroRelatorioVM<T> ComplementarValor<TPropriedade, TPropriedadeComplemento>(Expression<Func<T, TPropriedade>> propriedadeExpressao, Expression<Func<T, TPropriedadeComplemento>> propriedadeComplementoExpressao, bool ignorar = true)
        {
            var filtro = Obterfiltro(propriedadeExpressao);
            if (filtro != null)
                filtro.PropriedadeComplemento = new Propriedade<T>(filtro.Propriedade.PropriedadeInformacao) {
                    FuncaoPropriedade = (origem) => propriedadeComplementoExpressao.Compile()(origem)
                };

            if (ignorar)
                Ignorar(propriedadeComplementoExpressao);

            return this;
        }

        public IFiltroRelatorioVM<T> ComplementarValor<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao, Func<TPropriedade, string> funcao) 
        {
            var filtro = Obterfiltro(propriedadeExpressao);
            if (filtro != null)
                filtro.PropriedadeComplemento = new Propriedade<T>(filtro.Propriedade.PropriedadeInformacao)
                {
                    FuncaoPropriedade = (origem) => funcao?.Invoke(propriedadeExpressao.Compile()(origem))
                };
            return this;
        }

        public IFiltroRelatorioVM<T> FaixaDeValor<TPropriedade, TPropriedadeComplemento>(Expression<Func<T, TPropriedade>> propriedadeExpressao, Expression<Func<T, TPropriedadeComplemento>> propriedadeComplementoExpressao, bool ignorar = true)
        {
            ComplementarValor(propriedadeExpressao, propriedadeComplementoExpressao, ignorar);
            Separador(propriedadeExpressao, "até");
            return this;
        }

        public IFiltroRelatorioVM<T> Valor<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao, string valor)
        {
            var filtro = Obterfiltro(propriedadeExpressao);
            if(filtro != null)
                filtro.Propriedade.FuncaoPropriedade = (origem) => valor;
            return this;
        }

        public IFiltroRelatorioVM<T> Valor<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao, Func<TPropriedade, string> funcao) {
            var filtro = Obterfiltro(propriedadeExpressao);
            if (filtro != null)
                filtro.Propriedade.FuncaoPropriedade = (origem) => funcao?.Invoke(propriedadeExpressao.Compile()(origem));
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

        private Filtro<T> Obterfiltro<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao) {
            var propriedade = propriedadeExpressao.ObterPropriedade();
            if (_filtros.ContainsKey(propriedade.Name))
                return _filtros[propriedade.Name];

            return null;
        }        
    }
}
