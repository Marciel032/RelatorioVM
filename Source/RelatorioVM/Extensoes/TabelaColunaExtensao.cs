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
        public static List<EstiloElemento> ObterEstilos<T>(this IEnumerable<TabelaColuna<T>> colunas, string classeTabela) {
            var indiceColuna = 0;
            var estilos = new List<EstiloElemento>();

            foreach (var coluna in colunas) {
                if (!coluna.PrecisaGerarEstilo())
                    continue;

                var estiloColuna = new EstiloElemento()
                    .AdicionarClasse(classeTabela);
                if (coluna.PrecisaGerarEstiloComplementoColuna())
                {
                    estiloColuna
                        .AdicionarClasse($"{coluna.Identificador}-valor")
                        .DefinirAlinhamentoTexto(new EstiloAlinhamentoTexto() { Direcao = TipoAlinhamentoHorizontal.Direita })
                        .DefinirPreenchimento(new EstiloElementoPreenchimento() { 
                            Direcao = TipoPreenchimento.Direita,
                            Tamanho = 1,
                            UnidadeMedida = TipoUnidadeMedida.Pixel
                        });

                    var estiloSeparador = new EstiloElemento()
                        .AdicionarClasse(classeTabela)
                        .AdicionarClasse($"{coluna.Identificador}-separador")
                        .DefinirMedida(new EstiloElementoMedida()
                        {
                            Direcao = TipoDirecaoMedida.Largura,
                            Tamanho = 0,
                            UnidadeMedida = TipoUnidadeMedida.Percentual
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
                        .AdicionarClasse(classeTabela)
                        .AdicionarClasse($"{coluna.Identificador}-complemento")
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
                    estiloColuna.AdicionarClasse($"{coluna.Identificador}");
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
