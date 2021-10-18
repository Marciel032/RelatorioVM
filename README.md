# RelatorioVM

![GitHub repo size](https://img.shields.io/github/repo-size/Marciel032/RelatorioVM?style=for-the-badge)
![GitHub language count](https://img.shields.io/github/languages/count/Marciel032/RelatorioVM?style=for-the-badge)
![GitHub forks](https://img.shields.io/github/forks/Marciel032/RelatorioVM?style=for-the-badge)
![Bitbucket open issues](https://img.shields.io/bitbucket/issues/Marciel032/RelatorioVM?style=for-the-badge)
![Bitbucket open pull requests](https://img.shields.io/bitbucket/pr-raw/Marciel032/RelatorioVM?style=for-the-badge)


> Permite criar relatórios em html, baseado em view models. Faz a leitura das propriedades dos objetos e monta automaticamente as colunas. 
> Tambem permite adicionar agrupamentos e totalizadores.

### Ajustes e melhorias

O projeto ainda está em desenvolvimento e as próximas atualizações serão voltadas nas seguintes tarefas:

- [x] Criar totalizadores
- [x] Criar agrupamentos e seus totalizadores
- [ ] Permitir customizar as fontes e estilos dos textos.

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

Receba o construtor do relatorio no controlador
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

Utilize o construtor para obter o html
```csharp

```

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
  </tr>
</table>
