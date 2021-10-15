using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Extensoes
{
    public static class EnumExtensao
    {
        public static string ObterDescricao(this Enum enumerador) {
            var atributoDescricao = enumerador.GetType()
                            .GetMember(enumerador.ToString())
                            .FirstOrDefault()
                            .GetCustomAttribute<DisplayAttribute>();

            if (atributoDescricao != null)
                return atributoDescricao.Name;

            return enumerador.ToString();
        }
    }
}
