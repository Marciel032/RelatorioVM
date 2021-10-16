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

        public static bool EhInteiroOuDecimal(this Type tipo)
        {
            return tipo.EhInteiro() || tipo.EhDecimal();
        }

        public static bool EhInteiro(this Type tipo)
        {
            switch (Type.GetTypeCode(tipo.ObterTipoNaoNullo()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return true;
                default:
                    return false;
            }
        }

        public static bool EhDecimal(this Type tipo)
        {
            switch (Type.GetTypeCode(tipo.ObterTipoNaoNullo()))
            {
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}
