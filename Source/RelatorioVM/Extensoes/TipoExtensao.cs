using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Extensoes
{
    internal static  class TipoExtensao
    {
        public static Type ObterTipoNaoNullo(this Type tipo) {
            if (tipo.IsGenericType && tipo.GetGenericTypeDefinition() == typeof(Nullable<>))
                return Nullable.GetUnderlyingType(tipo);

            return tipo;
        }
    }
}
