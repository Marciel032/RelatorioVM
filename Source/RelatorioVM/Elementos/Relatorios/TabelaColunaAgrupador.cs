using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Elementos.Propriedades;
using RelatorioVM.Extensoes;
using RelatorioVM.Infraestruturas;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaColunaAgrupador: IColunaAgrupadorRelatorioVM
    {
        public string Identificador { get; set; }
        public bool ExibirTitulo { get; set; }
        public bool ExibirTotal { get; set; }

        public TabelaColunaAgrupador(string identificador)
        {
            Identificador = identificador;
            ExibirTitulo = true;
            ExibirTotal = true;
        }

        public IColunaAgrupadorRelatorioVM OcultarNoTitulo()
        {
            ExibirTitulo = false;
            return this;
        }

        public IColunaAgrupadorRelatorioVM OcultarNoTotal()
        {
            ExibirTotal = false;
            return this;
        }

        public IColunaAgrupadorRelatorioVM Ocultar()
        {
            return OcultarNoTitulo().OcultarNoTotal();

        }
    }
}
