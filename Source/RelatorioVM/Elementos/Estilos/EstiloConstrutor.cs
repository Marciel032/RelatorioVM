using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Estilos
{
    internal class EstiloConstrutor
    {
        private List<EstiloElemento> _elementosEstilo;

        public EstiloConstrutor()
        {
            _elementosEstilo = new List<EstiloElemento>();
        }

        public void AdicionarEstilo(EstiloElemento estilo) { 
            _elementosEstilo.Add(estilo);
        }

        public void AdicionarEstilos(List<EstiloElemento> estilos)
        {
            _elementosEstilo.AddRange(estilos);
        }

        public override string ToString()
        {
            var estiloConstrutor = new StringBuilder();
            foreach (var estilo in _elementosEstilo)
                estiloConstrutor.Append(estilo);
            return estiloConstrutor.ToString();
        }
    }
}
