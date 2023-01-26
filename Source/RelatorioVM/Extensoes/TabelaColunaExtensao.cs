using HtmlTags;
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

            for (int i = 0; i < tabela.QuantidadeFracionamentoDados; i++)
            {
                foreach (var coluna in colunas)
                {
                    if (!coluna.PrecisaGerarEstilo())
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

                    estilos.Add(estiloColuna);

                    indiceColuna += coluna.QuantidadeColunasUtilizadas;
                }
            }

            return estilos;
        }

        private static bool PrecisaGerarEstilo<T>(this TabelaColuna<T> coluna)
        {
            if (coluna.PrecisaGerarEstiloAlinhamentoHorizontalColuna())
                return true;

            if (coluna.PrecisaGerarEstiloComplementoColuna())
                return true;

            if (coluna.PrecisaGerarEstiloCondensado())
                return true;

            if (coluna.PrecisaGerarEstiloQuebraDeLinha())
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
    }
}
