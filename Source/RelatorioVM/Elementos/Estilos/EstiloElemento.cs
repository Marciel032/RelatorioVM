using RelatorioVM.Dominio.Configuracoes.Formatacoes;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Estilos
{
    internal class EstiloElemento
    {
        private List<string> _classes;
        private FonteEscrita _fonte;
        private List<EstiloElementoMedida> _medidas;
        private EstiloElementoBorda _borda;
        private EstiloAlinhamentoTexto _alinhamentoTexto;
        private List<EstiloElementoPreenchimento> _preenchimentos;
        private List<string> _estilosManuais;

        public EstiloElemento()
        {
            _classes = new List<string>();
            _medidas = new List<EstiloElementoMedida>();
            _preenchimentos = new List<EstiloElementoPreenchimento>();
            _estilosManuais = new List<string>();
        }

        public EstiloElemento AdicionarClasse(string classe) { 
            _classes.Add($".{classe}");
            return this;
        }

        public EstiloElemento AdicionarClasseElemento(string classe)
        {
            _classes.Add(classe);
            return this;
        }

        public EstiloElemento DefinirFonte(FonteEscrita fonte) { 
            _fonte = fonte;
            return this;
        }

        public EstiloElemento DefinirMedida(EstiloElementoMedida medida)
        {
            _medidas.Add(medida);
            return this;
        }

        public EstiloElemento DefinirBorda(EstiloElementoBorda borda)
        {
            _borda = borda;
            return this;
        }

        public EstiloElemento DefinirAlinhamentoTexto(EstiloAlinhamentoTexto alinhamentoTexto)
        {
            _alinhamentoTexto = alinhamentoTexto;
            return this;
        }
        public EstiloElemento DefinirPreenchimento(EstiloElementoPreenchimento preenchimento)
        {
            _preenchimentos.Add(preenchimento);
            return this;
        }

        public EstiloElemento DefinirEstiloManual(string estilo)
        {
            _estilosManuais.Add(estilo);
            return this;
        }

        public override string ToString()
        {
            var estiloConstrutor = new StringBuilder();
            ConstruirClasses(estiloConstrutor);
            estiloConstrutor.Append("{");
            ConstruirFonte(estiloConstrutor);
            ConstruirMedidas(estiloConstrutor);
            ConstruirBorda(estiloConstrutor);
            ConstruirAlinhamentoTexto(estiloConstrutor);
            ConstruirPreenchimentos(estiloConstrutor);
            ConstruirEstilosManuais(estiloConstrutor);
            estiloConstrutor.Append("}");
            return estiloConstrutor.ToString();
        }

        private void ConstruirClasses(StringBuilder estiloConstrutor) {
            foreach (var classe in _classes)
                estiloConstrutor.Append($"{classe} ");
        }

        private void ConstruirFonte(StringBuilder estiloConstrutor)
        {
            if (_fonte == null)
                return;

            estiloConstrutor
                .Append($"font-family: {_fonte.Nome.ObterDescricao()};");

            if (_fonte.Tamanho > 0)
                estiloConstrutor.Append($"font-size: {_fonte.Tamanho}px;");

            if (_fonte.Italico)
                estiloConstrutor.Append($"font-style: italic;");

            if (_fonte.Negrito)
                estiloConstrutor.Append($"font-weight: bold;");
        }

        private void ConstruirMedidas(StringBuilder estiloConstrutor)
        {
            foreach (var medida in _medidas)
                estiloConstrutor.Append(medida);
        }

        private void ConstruirBorda(StringBuilder estiloConstrutor)
        {
            if (_borda == null)
                return;
            
            estiloConstrutor.Append(_borda);
        }

        private void ConstruirAlinhamentoTexto(StringBuilder estiloConstrutor)
        {
            if (_alinhamentoTexto == null)
                return;

            estiloConstrutor.Append(_alinhamentoTexto);
        }

        private void ConstruirPreenchimentos(StringBuilder estiloConstrutor)
        {
            foreach (var preenchimento in _preenchimentos)
                estiloConstrutor.Append(preenchimento);
        }

        private void ConstruirEstilosManuais(StringBuilder estiloConstrutor)
        {
            foreach (var estilo in _estilosManuais)
                estiloConstrutor.Append(estilo);
        }
    }
}
