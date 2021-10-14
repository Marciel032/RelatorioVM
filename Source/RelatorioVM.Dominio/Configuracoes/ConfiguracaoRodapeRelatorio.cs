using RelatorioVM.Dominio.Configuracoes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes
{
    public class ConfiguracaoRodapeRelatorio : ConfiguracaoCabecalhoRodapeRelatorioBase, IConfiguracaoRodapeRelatorio
    {
        public ConfiguracaoRodapeRelatorio()
        {
            ElementoEsquerda = new ConfiguracaoCabecalhoRodapeElemento(this);
            ElementoCentro = new ConfiguracaoCabecalhoRodapeElemento(this);
            ElementoDireita = new ConfiguracaoCabecalhoRodapeElemento(this);
        }

        public ConfiguracaoRodapeRelatorio Clone()
        {
            return CloneBase(this) as ConfiguracaoRodapeRelatorio;
        }
    }
}
