using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Extensoes
{
    public static class IEnumerableExtensao
    {
        public static IEnumerable<IEnumerable<T>> CriarGruposDe<T>(this IEnumerable<T> lista, int quantidade)
        {
            var grupo = new List<T>();
            foreach (var item in lista)
            {
                grupo.Add(item);
                if (grupo.Count == quantidade)
                {
                    yield return grupo;
                    grupo = new List<T>();
                }
            }

            if (grupo.Count != 0)
            {
                yield return grupo;
            }
        }
    }
}
