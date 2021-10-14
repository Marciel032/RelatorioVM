using RelatorioVM.Relatorios.Construtores;
using RelatorioVM.Relatorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Relatorios.Fabricas
{
    internal class ConstrutorRelatorioFabrica
    {
        public static IRelatorioVM Criar() {
            return new ConstrutorRelatorio();
        }
    }
}
