﻿using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        public static bool EhLista(this Type tipo)
        {
            var tipoNaoNullo = tipo.ObterTipoNaoNullo();
            if (tipoNaoNullo == typeof(string))
                return false;

            return tipoNaoNullo.GetInterfaces().Any(
                i => i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }

        public static bool EhInteiroOuDecimal(this Type tipo)
        {
            return tipo.EhInteiro() || tipo.EhDecimal();
        }

        public static bool EhInteiro(this Type tipo)
        {
            if (tipo.EhEnumerador())
                return false;

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

        public static bool EhEnumerador(this Type tipo) {
            return tipo.IsEnum;
        }

        public static bool EhCor(this Type tipo)
        {
            var tipoNaoNullo = tipo.ObterTipoNaoNullo();
            if (tipoNaoNullo == typeof(Color))
                return true;

            if (tipoNaoNullo == typeof(TipoCor))
                return true;

            return false;
        }
    }
}
