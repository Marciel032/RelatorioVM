using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Extensoes
{
    internal static class ExpressaoExtensao
    {
        public static PropertyInfo ObterPropriedade<TOrigem, TPropriedade>(this Expression<Func<TOrigem, TPropriedade>> propertyLambda, TOrigem origem)
        {
            Type type = typeof(TOrigem);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expressão '{0}' se refere a um método, não a uma propriedade.",
                    propertyLambda.ToString()));

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expressão '{0}' se refere a um campo, não a uma propriedade.",
                    propertyLambda.ToString()));

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expressão '{0}' se refere a uma propriedade que não pertence a {1}.",
                    propertyLambda.ToString(),
                    type));

            return propInfo;
        }
    }
}
