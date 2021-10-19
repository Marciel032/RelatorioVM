using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RelatorioVM.Extensoes
{
    internal static class PropriedadeExtensao
    {
        public static string ObterNome(this PropertyInfo propriedade) {            
            var atributoDisplayName = propriedade.GetCustomAttribute<DisplayNameAttribute>();
            if (atributoDisplayName != null && !string.IsNullOrWhiteSpace(atributoDisplayName.DisplayName))
                return atributoDisplayName.DisplayName;

            var atributoDisplay = propriedade.GetCustomAttribute<DisplayAttribute>();
            if (atributoDisplay != null && !string.IsNullOrWhiteSpace(atributoDisplay.Name))
                return atributoDisplay.Name;            

            return propriedade.Name.SepararCamelCase();
        }
    }
}
