﻿using Microsoft.Extensions.DependencyInjection;
using RelatorioVM.Configuradores;
using RelatorioVM.Conversores;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Configuracoes.Interfaces;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Infraestruturas;
using RelatorioVM.Relatorios.Construtores;
using RelatorioVM.Relatorios.Fabricas;
using System;
using System.Drawing;

namespace RelatorioVM.Extensoes
{
    public static class ColecaoDeServicosExtensao
    {
        public static IConfiguradorRelatorio UtilizarRelatorioVM(this IServiceCollection colecaoDeServicos)
        {
            colecaoDeServicos.AddScoped<IRelatorioVM, ConstrutorRelatorio>();

            ConversorValor.DefinirConversor<DateTime>(new ConversorDataHora());
            ConversorValor.DefinirConversor<decimal>(new ConversorDecimal());
            ConversorValor.DefinirConversor<bool>(new ConversorBooleano());
            ConversorValor.DefinirConversor<short>(new ConversorNumerico());
            ConversorValor.DefinirConversor<int>(new ConversorNumerico());
            ConversorValor.DefinirConversor<long>(new ConversorNumerico());
            ConversorValor.DefinirConversor<Color>(new ConversorCor());
            ConversorValor.DefinirConversor<TipoCor>(new ConversorTipoCor());

            return new ConfiguradorRelatorio();
        }
        
        public static IConfiguradorRelatorio UtilizarRelatorioVM(this IServiceCollection colecaoDeServicos, Action<IConfiguracaoRelatorio> opcoes)
        {
            var configurador = colecaoDeServicos.UtilizarRelatorioVM();

            opcoes.Invoke(Configuracao.ConfiguracaoRelatorio);

            return configurador;
        }
    }
}
