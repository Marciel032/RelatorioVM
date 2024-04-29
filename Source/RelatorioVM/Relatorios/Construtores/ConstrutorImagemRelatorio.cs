using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Elementos.Propriedades;
using RelatorioVM.Elementos.Relatorios;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Relatorios.Construtores
{
    internal class ConstrutorImagemRelatorio: IImagemRelatorioVM
    {
        private ConfiguracaoRelatorio _configuracaoRelatorio;
        private ImagemElemento _imagemElemento;

        public ConstrutorImagemRelatorio(ConfiguracaoRelatorio configuracaoRelatorio)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _imagemElemento = new ImagemElemento(_configuracaoRelatorio);
        }

        public ImagemElemento Construir()
        {
            return _imagemElemento;
        }
    }
}
