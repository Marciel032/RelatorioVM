using RelatorioVM.Dominio.Configuracoes.Interfaces;
using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes
{
    public class ConfiguracaoRodapeRelatorio : ConfiguracaoCabecalhoRodapeRelatorioBase, IConfiguracaoRodapeRelatorio
    {
        public ConfiguracaoRodapeRelatorio()
        {
            ElementoEsquerda = new ConfiguracaoCabecalhoRodapeElemento(this, TipoPosicaoCabecalhoRodape.RodapeEsquerdo);
            ElementoCentro = new ConfiguracaoCabecalhoRodapeElemento(this, TipoPosicaoCabecalhoRodape.RodapeCentro);
            ElementoDireita = new ConfiguracaoCabecalhoRodapeElemento(this, TipoPosicaoCabecalhoRodape.RodapeDireito);
        }

        public ConfiguracaoRodapeRelatorio Clone()
        {
            return CloneBase(this) as ConfiguracaoRodapeRelatorio;
        }
    }
}
