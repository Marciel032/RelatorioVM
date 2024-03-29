# RelatorioVM

![GitHub code size](https://img.shields.io/github/languages/code-size/marciel032/relatoriovm?style=for-the-badge)
![Nuget version](https://img.shields.io/nuget/v/RelatorioVM?style=for-the-badge)
![GitHub forks](https://img.shields.io/github/forks/Marciel032/RelatorioVM?style=for-the-badge)
![GitHub open pull requests](https://img.shields.io/github/issues-pr/Marciel032/RelatorioVM?style=for-the-badge)


> Permite criar relatórios em html, baseado em view models. Faz a leitura das propriedades dos objetos e monta automaticamente as colunas. 
> Também permite adicionar agrupamentos e totalizadores.

### Ajustes e melhorias

O projeto ainda está em desenvolvimento e as próximas atualizações serão voltadas nas seguintes tarefas:

- [x] Criar totalizadores.
- [x] Criar agrupamentos e seus totalizadores.
- [ ] Permitir customizar as colunas exibidas na tabela.
- [x] Permitir customizar as fontes e estilos dos textos global.
- [ ] Selecionar o alinhamento do campo.
- [ ] Permitir customizar a ordem das colunas.
- [ ] Permitir customizar a fonte dos textos para filtros, tabelas, totais (nome, tamanho, estilo) individualmente.

## 💻 Pré-requisitos

Antes de começar, verifique se você atendeu aos seguintes requisitos:
* Utilize .net core 2.1 ou superior

## 🚀 Instalando RelatorioVM

```
PM> Install-Package RelatorioVM
```

## ☕ Usando RelatorioVM

Para usar RelatorioVM, siga estas etapas:

Inicialize no Startup.cs
```csharp
services.UtilizarRelatorioVM();
```

Receba o construtor do relatório no controlador
```csharp
public class HomeController : Controller
{
    private IRelatorioVM _relatorioVM;
    public HomeController(IRelatorioVM relatorioVM)
    {
        _relatorioVM = relatorioVM;
    }
}
```

Crie uma view model para informar ao construtor do relatório qual a estrutura que você quer imprimir
```csharp
public class ExemploSimplesItemViewModel
{
    [DisplayName("Filial")]
    public int FilialCodigo { get; set; }

    [DisplayName("Pessoa código")]
    public int PessoaCodigo { get; set; }

    public PessoaViewModel Pessoa { get; set; }

    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
}
```

Popule uma lista da sua view model, com os dados que deseja imprimir
```csharp
var itens = new List<ExemploSimplesItemViewModel>(){
    ...
};
```

Utilize o construtor para obter o html, passando a sua lista como conteúdo
```csharp
relatorioConstrutor
    .AdicionarTabela(itens)
    .Titulo("Teste de relatório")
    .Construir()
    .GerarHtml();
```
<img src="Documentacao/Imagens/RelatorioSimples.png" alt="exemplo imagem">


Tambem é possível fazer agrupamento e totalização de valores
```csharp
relatorioConstrutor
    .AdicionarTabela(itens, tabela => {
        tabela
            .Agrupar(agrupar => agrupar.Coluna(x => x.FilialCodigo))
            .Totalizar(totalizar => totalizar.Coluna(x => x.Valor, x => x.Valor));
    })
    .Titulo("Teste de relatório")
    .Construir()
    .GerarHtml();
```
<img src="Documentacao/Imagens/RelatorioAgrupado.png" alt="exemplo imagem">

### 📘 Mais detalhes na [Wiki](https://github.com/Marciel032/RelatorioVM/wiki)




## 🤝 Colaboradores

Agradecemos às seguintes pessoas que contribuíram para este projeto:

<table>
  <tr>
    <td align="center">
      <a href="https://github.com/Marciel032">
        <img src="https://avatars3.githubusercontent.com/Marciel032" width="100px;" alt="Marciel Grützmann"/><br>
        <sub>
          <b>Marciel Grützmann</b>
        </sub>
      </a>
    </td>  
    <td align="center">
      <a href="https://github.com/Marciel032">
        <img src="https://avatars3.githubusercontent.com/UndPat" width="100px;" alt="Marciel Grützmann"/><br>
        <sub>
          <b>Patrick J. M. De Bastiani</b>
        </sub>
      </a>
    </td> 
  </tr>
</table>
