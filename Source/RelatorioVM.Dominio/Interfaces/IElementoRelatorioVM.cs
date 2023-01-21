using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Interfaces
{
    public interface IElementoRelatorioVM
    {
        /// <summary>
        /// Indice unico dentro da estrutura do relatorio. Pode ser utilizado para aplicar um estilo especifico para este elemento.
        /// </summary>
        string Indice { get; set; }
        string ObterHtml(object conteudo);
        string ObterEstilo();
    }
}
