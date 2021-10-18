using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RelatorioVM.Comparadores
{
    internal class DicionarioComparador : IEqualityComparer<IDictionary<string, object>>
    {
        public bool Equals(IDictionary<string, object> atual, IDictionary<string, object> proximo)
        {
            if (atual.Count != proximo.Count)
                return false;
            if (atual.Keys.Except(proximo.Keys).Any())
                return false;
            if (proximo.Keys.Except(atual.Keys).Any())
                return false;
            foreach (var pair in atual)
                if (pair.Value.Equals(proximo[pair.Key]) == false)
                    return false;
            return true;

        }

        public int GetHashCode(IDictionary<string, object> obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
