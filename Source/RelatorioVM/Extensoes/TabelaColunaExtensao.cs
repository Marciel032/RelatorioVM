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
            var estiloConstrutor = new StringBuilder();
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

                    estilos.Add(new EstiloElemento()
                        .AdicionarClasse(classeTabela)
                        .AdicionarClasse($"{coluna.Identificador}-separador")
                        .DefinirMedida(new EstiloElementoMedida() { 
                            Direcao = TipoDirecaoMedida.Largura,
                            Tamanho = 0,
                            UnidadeMedida = TipoUnidadeMedida.Percentual
                        })
                        .DefinirPreenchimento(new EstiloElementoPreenchimento() { 
                            Direcao = TipoPreenchimento.Direita,
                            Tamanho = 1,
                            UnidadeMedida = TipoUnidadeMedida.Pixel
                        })
                        .DefinirPreenchimento(new EstiloElementoPreenchimento()
                        {
                            Direcao = TipoPreenchimento.Esquerda,
                            Tamanho = 1,
                            UnidadeMedida = TipoUnidadeMedida.Pixel
                        })
                    );
                    estilos.Add(new EstiloElemento()
                        .AdicionarClasse(classeTabela)
                        .AdicionarClasse($"{coluna.Identificador}-complemento")
                        .DefinirAlinhamentoTexto(new EstiloAlinhamentoTexto() { Direcao = TipoAlinhamentoHorizontal.Esquerda })
                        .DefinirPreenchimento(new EstiloElementoPreenchimento()
                        {
                            Direcao = TipoPreenchimento.Esquerda,
                            Tamanho = 1,
                            UnidadeMedida = TipoUnidadeMedida.Pixel
                        })
                    );
                }
                else
                {
                    estiloColuna.AdicionarClasse($"{coluna.Identificador}");
                    if (coluna.PrecisaGerarEstiloAlinhamentoHorizontalColuna())
                        estiloColuna.DefinirAlinhamentoTexto(new EstiloAlinhamentoTexto() { Direcao = coluna.AlinhamentoHorizontalColuna });
                }

                estilos.Add(estiloColuna);
            }

            return estilos;
        }

        public static bool PrecisaGerarEstilo<T>(this TabelaColuna<T> coluna)
        {
            if (coluna.PrecisaGerarEstiloAlinhamentoHorizontalColuna())
                return true;

            if (coluna.PrecisaGerarEstiloComplementoColuna())
                return true;


            return false;
        }

        public static bool PrecisaGerarEstiloAlinhamentoHorizontalColuna<T>(this TabelaColuna<T> coluna)
        {
            return coluna.AlinhamentoHorizontalColuna != Dominio.Enumeradores.TipoAlinhamentoHorizontal.Esquerda;
        }

        public static bool PrecisaGerarEstiloComplementoColuna<T>(this TabelaColuna<T> coluna)
        {
            return coluna.TemComplemento;
        }
    }
}
