using System;
using System.Collections.Generic;
using System.Linq;
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

        public static IEnumerable<IEnumerable<T>> CriarGruposDeFracionamento<T>(this IEnumerable<T> lista, int quantidade)
        {
            var listaItens = lista.ToList();
            var quantidadeItensFracionamento = 1;
            if(listaItens.Count > quantidade)
                quantidadeItensFracionamento = (int)Math.Ceiling((decimal)listaItens.Count / (decimal)quantidade);
            var grupos = new List<List<T>>(quantidade);

            for (int i = 0; i < quantidadeItensFracionamento; i++)
            {
                var grupo = new List<T>();
                for (int j = 0; j < quantidade; j++)
                {
                    var indice = (j * quantidadeItensFracionamento) + i;
                    if (indice < listaItens.Count)
                        grupo.Add(listaItens[indice]);
                    else
                        grupo.Add(default(T));
                }
                grupos.Add(grupo);
            }

            return grupos;
        }
    }
}
