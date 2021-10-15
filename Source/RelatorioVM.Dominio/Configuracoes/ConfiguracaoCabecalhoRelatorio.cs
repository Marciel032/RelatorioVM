using RelatorioVM.Dominio.Configuracoes.Interfaces;
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
            ElementoEsquerda = new ConfiguracaoCabecalhoRodapeElemento(this);
            ElementoCentro = new ConfiguracaoCabecalhoRodapeElemento(this);
            ElementoDireita = new ConfiguracaoCabecalhoRodapeElemento(this);
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
