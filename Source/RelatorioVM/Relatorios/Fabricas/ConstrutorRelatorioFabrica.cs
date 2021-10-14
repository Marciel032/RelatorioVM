using RelatorioVM.Conversores;
using RelatorioVM.Conversores.Interfaces;
using RelatorioVM.Relatorios.Construtores;
using RelatorioVM.Relatorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Relatorios.Fabricas
{
    internal class ConstrutorRelatorioFabrica
    {
        public static IRelatorioVM Criar(IConversor conversor) {
            return new ConstrutorRelatorio(conversor);
        }

        public static IRelatorioVM Criar()
        {
            return new ConstrutorRelatorio(new ConversorBasico());
        }
    }
}
