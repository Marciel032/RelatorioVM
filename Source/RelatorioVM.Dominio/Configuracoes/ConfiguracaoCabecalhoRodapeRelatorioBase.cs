using RelatorioVM.Dominio.Configuracoes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes
{
    public class ConfiguracaoCabecalhoRodapeRelatorioBase
    {
        public ConfiguracaoCabecalhoRodapeElemento ElementoEsquerda { get; set; }
        public ConfiguracaoCabecalhoRodapeElemento ElementoCentro { get; set; }
        public ConfiguracaoCabecalhoRodapeElemento ElementoDireita { get; set; }

        public IConfiguracaoElemento Centro()
        {
            return ElementoCentro;
        }

        public IConfiguracaoElemento Direita()
        {
            return ElementoDireita;
        }

        public IConfiguracaoElemento Esquerda()
        {
            return ElementoEsquerda;
        }

        public object CloneBase(IConfiguracaoCabecalhoRodapeRelatorioBase parent)
        {
            ConfiguracaoCabecalhoRodapeRelatorioBase clone = MemberwiseClone() as ConfiguracaoCabecalhoRodapeRelatorioBase;
            clone.ElementoEsquerda = ElementoEsquerda.Clone(parent);
            clone.ElementoCentro = ElementoCentro.Clone(parent);
            clone.ElementoDireita = ElementoDireita.Clone(parent);
            return clone;
        }
    }
}
