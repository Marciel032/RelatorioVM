using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes.Interfaces
{
    public interface IConfiguracaoElemento
    {
        IConfiguracaoCabecalhoRodapeRelatorioBase ImprimirTexto(string texto);
        IConfiguracaoCabecalhoRodapeRelatorioBase ImprimirData(string formato = "dd/MM/yyyy");
        IConfiguracaoCabecalhoRodapeRelatorioBase ImprimirDataHora(string formato = "dd/MM/yyyy HH:mm");
        IConfiguracaoCabecalhoRodapeRelatorioBase ImprimirNumeroDePaginas(string prefixo = "Página ");
        IConfiguracaoCabecalhoRodapeRelatorioBase ImprimirTitulo();
    }
}
