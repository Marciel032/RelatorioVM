using RelatorioVM.Conversores;
using RelatorioVM.Dominio.Conversores.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Infraestruturas
{
    internal class ConversorValor
    {
        private static Dictionary<Type, IConversorValor> _conversores = new Dictionary<Type, IConversorValor>();

        public static void DefinirConversor<T>(IConversorValor conversor){
            var tipo = typeof(T);
            if (_conversores.ContainsKey(tipo)) {
                _conversores[tipo] = conversor;
                return;
            }

            _conversores.Add(tipo, conversor);
        }

        public static IConversorValor ObterConversor<T>() {
            return ObterConversor(typeof(T));
        }

        public static IConversorValor ObterConversor(Type tipo)
        {
            if (_conversores.ContainsKey(tipo))
                return _conversores[tipo];

            var tipoGenerico = typeof(ConversorGenerico);
            if (!_conversores.ContainsKey(tipoGenerico))
                _conversores.Add(tipoGenerico, new ConversorGenerico());

            return _conversores[tipoGenerico];
        }
    }
}
