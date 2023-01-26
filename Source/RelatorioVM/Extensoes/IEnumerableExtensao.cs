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
                quantidadeItensFracionamento = (int)Math.Floor((decimal)listaItens.Count / (decimal)quantidade);
            var itensSobrando = listaItens.Count > quantidade ? (listaItens.Count % quantidade) : 0;
            var grupos = new List<List<T>>();


            var itensAdicionais = 0;
            for (int j = 0; j < quantidade; j++)
            {
                var quantidadeItens = quantidadeItensFracionamento;
                var somarItemAdicional = false;
                if (itensSobrando > 0)
                {
                    somarItemAdicional = true;
                    quantidadeItens++;
                    itensSobrando--;
                }
                for (int i = 0; i < quantidadeItens; i++)
                {
                    if (grupos.Count == i)
                        grupos.Add(new List<T>());

                    var indice = (j * quantidadeItensFracionamento) + i + itensAdicionais;
                    if (indice < listaItens.Count)
                        grupos[i].Add(listaItens[indice]);
                }
                if (somarItemAdicional)
                    itensAdicionais++;
            }
            

            return grupos;
        }
    }
}
