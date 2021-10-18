using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Relatorios.Construtores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Relatorios.Fabricas
{
    public class ConstrutorRelatorioFabrica
    {
        public static IRelatorioVM Criar() {
            return new ConstrutorRelatorio();
        }
    }
}
