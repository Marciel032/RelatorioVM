using RelatorioVM.Dominio.Configuracoes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes
{
    public class ConfiguracaoCabecalhoRelatorio : ConfiguracaoCabecalhoRodapeRelatorioBase, IConfiguracaoCabecalhoRelatorio
    {
        public ConfiguracaoCabecalhoRelatorio()
        {
            ElementoEsquerda = new ConfiguracaoCabecalhoRodapeElemento(this);
            ElementoCentro = new ConfiguracaoCabecalhoRodapeElemento(this);
            ElementoDireita = new ConfiguracaoCabecalhoRodapeElemento(this);
        }

        public ConfiguracaoCabecalhoRelatorio Clone()
        {
            return CloneBase(this) as ConfiguracaoCabecalhoRelatorio;
        }
    }
}
