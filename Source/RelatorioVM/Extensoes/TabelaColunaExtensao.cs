﻿using HtmlTags;
using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Elementos.Estilos;
using RelatorioVM.Elementos.Relatorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Extensoes
{
    internal static class TabelaColunaExtensao
    {
        public static List<EstiloElemento> ObterEstilos<T>(this IEnumerable<TabelaColuna<T>> colunas, Tabela<T> tabela, string classeTabela) {
            var indiceColuna = 1;
            var estilos = new List<EstiloElemento>();
            var filtroLinhaConteudo = "tr:not(.tr-t):not(.tr-t-t):not(.tr-g-t) >";

            for (int indiceFracionamento = 0; indiceFracionamento < tabela.QuantidadeFracionamentoDados; indiceFracionamento++)
            {
                foreach (var coluna in colunas)
                {
                    if (!coluna.PrecisaGerarEstilo(tabela, indiceFracionamento))
                    {
                        indiceColuna += coluna.QuantidadeColunasUtilizadas;
                        continue;
                    }

                    var estiloColuna = new EstiloElemento()
                        .AdicionarClasse(classeTabela + " > ")
                        .AdicionarClasseElemento("tbody > ")
                        .AdicionarClasseElemento(filtroLinhaConteudo)
                        .AdicionarClasseElemento($"td:nth-child({indiceColuna})");

                    if (coluna.PrecisaGerarEstiloComplementoColuna())
                    {
                        estiloColuna
                            .DefinirAlinhamentoTexto(new EstiloAlinhamentoTexto() { Direcao = TipoAlinhamentoHorizontal.Direita })
                            .DefinirPreenchimento(new EstiloElementoPreenchimento()
                            {
                                Direcao = TipoPreenchimento.Direita,
                                Tamanho = 1,
                                UnidadeMedida = TipoUnidadeMedida.Pixel
                            });

                        var estiloSeparador = new EstiloElemento()
                            .AdicionarClasse(classeTabela + " > ")
                            .AdicionarClasseElemento("tbody > ")
                            .AdicionarClasseElemento(filtroLinhaConteudo)
                            .AdicionarClasseElemento($"td:nth-child({indiceColuna + 1})")
                            .DefinirMedida(new EstiloElementoMedida()
                            {
                                Direcao = TipoDirecaoMedida.Largura,
                                Tamanho = 1,
                                UnidadeMedida = TipoUnidadeMedida.Pixel
                            })
                            .DefinirPreenchimento(new EstiloElementoPreenchimento()
                            {
                                Direcao = TipoPreenchimento.Direita,
                                Tamanho = 1,
                                UnidadeMedida = TipoUnidadeMedida.Pixel
                            })
                            .DefinirPreenchimento(new EstiloElementoPreenchimento()
                            {
                                Direcao = TipoPreenchimento.Esquerda,
                                Tamanho = 1,
                                UnidadeMedida = TipoUnidadeMedida.Pixel
                            });
                        if (coluna.PrecisaGerarEstiloCondensado())
                            estiloSeparador.DefinirMedida(new EstiloElementoMedida()
                            {
                                Direcao = TipoDirecaoMedida.Largura,
                                Tamanho = 0,
                                UnidadeMedida = TipoUnidadeMedida.Percentual
                            });

                        if (coluna.PrecisaGerarEstiloQuebraDeLinha())
                            estiloSeparador.DefinirEstiloManual("white-space: nowrap;");
                        if (coluna.PrecisaGerarEstiloCorFundoConteudo())
                            estiloSeparador.DefinirCor(new EstiloCor()
                            {
                                Cor = coluna.CorFundoConteudo,
                                Fundo = true
                            });
                        if (coluna.PrecisaGerarEstiloCorConteudo())
                            estiloSeparador.DefinirCor(new EstiloCor()
                            {
                                Cor = coluna.CorConteudo
                            });
                        estilos.Add(estiloSeparador);

                        var estiloComplemento = new EstiloElemento()
                            .AdicionarClasse(classeTabela + " > ")
                            .AdicionarClasseElemento("tbody > ")
                            .AdicionarClasseElemento(filtroLinhaConteudo)
                            .AdicionarClasseElemento($"td:nth-child({indiceColuna + 2})")
                            .DefinirAlinhamentoTexto(new EstiloAlinhamentoTexto() { Direcao = TipoAlinhamentoHorizontal.Esquerda })
                            .DefinirPreenchimento(new EstiloElementoPreenchimento()
                            {
                                Direcao = TipoPreenchimento.Esquerda,
                                Tamanho = 1,
                                UnidadeMedida = TipoUnidadeMedida.Pixel
                            });
                        if (coluna.PrecisaGerarEstiloCondensado())
                            estiloComplemento.DefinirMedida(new EstiloElementoMedida()
                            {
                                Direcao = TipoDirecaoMedida.Largura,
                                Tamanho = 0,
                                UnidadeMedida = TipoUnidadeMedida.Percentual
                            });

                        if (coluna.PrecisaGerarEstiloQuebraDeLinha())
                            estiloComplemento.DefinirEstiloManual("white-space: nowrap;");
                        if (coluna.PrecisaGerarEstiloCorFundoConteudo())
                            estiloComplemento.DefinirCor(new EstiloCor()
                            {
                                Cor = coluna.CorFundoConteudo,
                                Fundo = true
                            });
                        if (coluna.PrecisaGerarEstiloCorConteudo())
                            estiloComplemento.DefinirCor(new EstiloCor()
                            {
                                Cor = coluna.CorConteudo
                            });
                        estilos.Add(estiloComplemento);
                    }
                    else
                    {
                        if (coluna.PrecisaGerarEstiloAlinhamentoHorizontalColuna())
                            estiloColuna.DefinirAlinhamentoTexto(new EstiloAlinhamentoTexto() { Direcao = coluna.AlinhamentoHorizontalColuna });
                    }

                    if (coluna.PrecisaGerarEstiloCondensado())
                        estiloColuna.DefinirMedida(new EstiloElementoMedida()
                        {
                            Direcao = TipoDirecaoMedida.Largura,
                            Tamanho = 0,
                            UnidadeMedida = TipoUnidadeMedida.Percentual
                        });

                    if (coluna.PrecisaGerarEstiloQuebraDeLinha())
                        estiloColuna.DefinirEstiloManual("white-space: nowrap;");

                    if (coluna.PrecisaGerarEstiloFracionamento(tabela, indiceFracionamento))
                        estiloColuna.DefinirBorda(new EstiloElementoBorda()
                        {
                            Direcao = TipoBorda.Esquerda,
                            Tamanho = 1,
                            UnidadeMedida = TipoUnidadeMedida.Pixel,
                            TipoBorda = TipoEstiloBorda.Solida,
                            Cor = "#777"
                        });

                    if (coluna.PrecisaGerarEstiloCorFundoConteudo())
                        estiloColuna.DefinirCor(new EstiloCor()
                        {                            
                            Cor = coluna.CorFundoConteudo,
                            Fundo = true
                        });

                    if (coluna.PrecisaGerarEstiloCorConteudo())
                        estiloColuna.DefinirCor(new EstiloCor()
                        {
                            Cor = coluna.CorConteudo
                        });


                    estilos.Add(estiloColuna);

                    indiceColuna += coluna.QuantidadeColunasUtilizadas;
                }
            }

            return estilos;
        }

        private static bool PrecisaGerarEstilo<T>(this TabelaColuna<T> coluna, Tabela<T> tabela, int indiceFracionamento)
        {
            if (coluna.PrecisaGerarEstiloAlinhamentoHorizontalColuna())
                return true;

            if (coluna.PrecisaGerarEstiloComplementoColuna())
                return true;

            if (coluna.PrecisaGerarEstiloCondensado())
                return true;

            if (coluna.PrecisaGerarEstiloQuebraDeLinha())
                return true;

            if (coluna.PrecisaGerarEstiloFracionamento(tabela, indiceFracionamento))
                return true;

            if (coluna.PrecisaGerarEstiloCorFundoConteudo())
                return true;

            if (coluna.PrecisaGerarEstiloCorConteudo())
                return true;

            return false;
        }

        private static bool PrecisaGerarEstiloAlinhamentoHorizontalColuna<T>(this TabelaColuna<T> coluna)
        {
            return coluna.AlinhamentoHorizontalColuna != Dominio.Enumeradores.TipoAlinhamentoHorizontal.Esquerda;
        }

        private static bool PrecisaGerarEstiloComplementoColuna<T>(this TabelaColuna<T> coluna)
        {
            return coluna.TemComplemento && coluna.AlinhamentoHorizontalColuna == TipoAlinhamentoHorizontal.Centro;
        }

        private static bool PrecisaGerarEstiloCondensado<T>(this TabelaColuna<T> coluna)
        {
            return coluna.Condensado;
        }

        private static bool PrecisaGerarEstiloQuebraDeLinha<T>(this TabelaColuna<T> coluna)
        {
            return !coluna.PermiteQuebraDeLinha;
        }
        private static bool PrecisaGerarEstiloFracionamento<T>(this TabelaColuna<T> coluna, Tabela<T> tabela, int indiceFracionamento)
        {
            return tabela.QuantidadeFracionamentoDados > 1 && coluna.Posicao == 0 && indiceFracionamento > 0;
        }
        private static bool PrecisaGerarEstiloCorFundoConteudo<T>(this TabelaColuna<T> coluna)
        {
            return !string.IsNullOrEmpty(coluna.CorFundoConteudo);
        }

        private static bool PrecisaGerarEstiloCorConteudo<T>(this TabelaColuna<T> coluna)
        {
            return !string.IsNullOrEmpty(coluna.CorConteudo);
        }
    }
}
