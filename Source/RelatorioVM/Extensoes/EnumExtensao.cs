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
            var atributoDisplay = enumerador.GetType()
                            .GetMember(enumerador.ToString())
                            .FirstOrDefault()
                            .GetCustomAttribute<DisplayAttribute>();

            if (atributoDisplay != null)
                return atributoDisplay.Name;

            var atributoNome = enumerador.GetType()
                            .GetMember(enumerador.ToString())
                            .FirstOrDefault()
                            .GetCustomAttribute<DisplayNameAttribute>();

            if (atributoNome != null)
                return atributoNome.DisplayName;

            return enumerador.ToString();
        }
    }
}
