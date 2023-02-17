using RelatorioVM.Dominio.Configuracoes.Interfaces;
using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes
{
    public class ConfiguracaoCabecalhoRelatorio : ConfiguracaoCabecalhoRodapeRelatorioBase, IConfiguracaoCabecalhoRelatorio
    {
        public int QuantidadeDeFiltrosPorLinha { get; set; }
        public ConfiguracaoCabecalhoRelatorio()
        {
            ElementoEsquerda = new ConfiguracaoCabecalhoRodapeElemento(this, TipoPosicaoCabecalhoRodape.CabecalhoEsquerdo);
            ElementoCentro = new ConfiguracaoCabecalhoRodapeElemento(this, TipoPosicaoCabecalhoRodape.CabecalhoCentro);
            ElementoDireita = new ConfiguracaoCabecalhoRodapeElemento(this, TipoPosicaoCabecalhoRodape.CabecalhoDireito);
            QuantidadeDeFiltrosPorLinha = 4;
        }

        public ConfiguracaoCabecalhoRelatorio Clone()
        {
            return CloneBase(this) as ConfiguracaoCabecalhoRelatorio;
        }

        public IConfiguracaoCabecalhoRelatorio DefinirQuantidadeDeFiltrosPorLinha(short quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentOutOfRangeException();

            QuantidadeDeFiltrosPorLinha = quantidade;
            return this;
        }
    }
}
