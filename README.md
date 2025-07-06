# Organizando o projeto
## Preparando o ambiente: projeto do curso

[Link para o projeto inicial](https://github.com/alura-cursos/ScreenSound03/archive/refs/heads/projeto-inicial.zip)


> Apenas para referência, supondo que você queira criar uma solução/projeto do zero: Vá para o diretório raiz e crie a solução com o seguinte comando:
> 
> ```bash
> dotnet new solution -n ScreenSound
> ```
> Vamos criar um novo projeto de console com o nome `ScreenSound` e acrescentá-lo à solução `ScreenSound`:
> 
> ```bash
> dotnet new console -n ScreenSound
> dotnet solution add ScreenSound
> ```
> Para rodar o projeto, execute o seguinte comando na raiz da solução:
> 
> ```bash
> dotnet run --project ScreenSound
> ```

## Abrindo o projeto
As aulas propõem a organização das classes de modelo em um diretório separado. Portanto, vamos criar dentro do diretório do _projeto_ ScreenSound (não confundir com o diretório da _solução_ ScreenSound) chamado `Modelos` e mover as classes de modelo para lá:
```
D:\alura\csharp-dominando-oo>cd ScreenSound

D:\alura\csharp-dominando-oo\ScreenSound>mkdir Modelos

D:\alura\csharp-dominando-oo\ScreenSound>move Album.cs Modelos
        1 arquivo(s) movido(s).

D:\alura\csharp-dominando-oo\ScreenSound>move Banda.cs Modelos
        1 arquivo(s) movido(s).

D:\alura\csharp-dominando-oo\ScreenSound>move Musica.cs Modelos
        1 arquivo(s) movido(s).

D:\alura\csharp-dominando-oo\ScreenSound>
```

## Namespaces
Apesar da mudança da localização dos arquivos de modelo, o código `não` quebrou. Isso acontece porque o `namespace` das classes de modelo permanece o mesmo (o namespace raiz).

> XP na verdade os modelos não eram referenciados no programa principal.

Namespaces ajudam poupando a digitação. Por exemplo: o comando `System.Console.WriteLine` pode ser resumido se invocarmos o namespace `System`:
```CSharp
using System;

Console.WriteLine("Hello, World com namespace!");
```

Sem importar o namespace, o código ficaria mais verboso:
```CSharp
System.Console.WriteLine("Hello, World sem namespace!");
```

Vamos mudar o namespace de cada classe de modelo:
```CSharp
// Modelos/Album.cs
namespace ScreenSound.Modelos;

class Album
{
    // Resto do código
}
```
> Em versões antigas do C#.NET, a sintaxe de namespace era `namespace Nome { ... classes ...}`. Era ruim porque forçava a inserção das classes em um bloco de código era delimitado pelas chaves:
> ```CSharp
> // Jeito antigo de declarar namespaces.
> namespace Modelos 
> {
>     class Album
>     {
>         // Resto do código
>     }
> }
> ```

Agora, vamos testar o uso das classes de modelo após importá-las usando o comando `using ScreendSound.Modelos;`:
```CSharp
// Program.cs
using ScreenSound.Modelos;

Banda ira = new Banda("Ira!"); 
Banda beatles = new Banda("The Beatles");
```
> A palavra `Banda` é um versão resumida de `ScreenSound.Modelos.Banda`: 
>
> ```CSharp
> ScreenSound.Modelos.Banda ira =
>      new ScreenSound.Modelos.Banda("Ira!"); 
> ScreenSound.Modelos.Banda beatles = 
>     new ScreenSound.Modelos.Banda("The Beatles");
> ```
> Note como é mais verboso escrever código sem importar os namespaces com `using`.

## OO no Program.cs
Os objetos do tipo `Banda` e `Album` vão substituir as implementações anteriores do programa principal:

```CSharp
// Program.cs
// Resto do código
Banda ira = new Banda("Ira!");
ira.AdicionarNota(10);
ira.AdicionarNota(8);
ira.AdicionarNota(6);
Banda beatles = new Banda("The Beatles");

Dictionary<string, Banda> bandasRegistradas = new();
bandasRegistradas.Add(ira.Nome, ira);
bandasRegistradas.Add(beatles.Nome, beatles);
// Resto do código
void RegistrarAlbum()
{
    // Resto do código
    string nomeDaBanda = Console.ReadLine()!;
    if (bandasRegistradas.ContainsKey(nomeDaBanda))
    {
        Console.Write("Agora digite o título do álbum: ");
        string tituloAlbum = Console.ReadLine()!;
        bandasRegistradas[nomeDaBanda].AdicionarAlbum(new Album(tituloAlbum));
        Console.WriteLine($"O álbum {tituloAlbum} de {nomeDaBanda} foi registrado com sucesso!");
    }
    else
    {
        Console.WriteLine($"\nA banda {nomeDaBanda} não foi encontrada!");
    }
}
// Resto do código
void RegistrarBanda()
{
    // Resto do código
    string nomeDaBanda = Console.ReadLine()!;
    Banda banda = new Banda(nomeDaBanda);
    bandasRegistradas.Add(nomeDaBanda, banda);
}
// Resto do código
void AvaliarUmaBanda()
{
    // Resto do código
    string nomeDaBanda = Console.ReadLine()!;
    if (bandasRegistradas.ContainsKey(nomeDaBanda))
    {
        Console.Write($"Qual a nota que a banda {nomeDaBanda} merece: ");
        int nota = int.Parse(Console.ReadLine()!);
        Banda banda = bandasRegistradas[nomeDaBanda];
        bandasRegistradas[nomeDaBanda].AdicionarNota(nota);
        // Resto do código
    }
    else
    {
        // Resto do código
    }
}
// Resto do código
void ExibirDetalhes()
{
    // Resto do código
    string nomeDaBanda = Console.ReadLine()!;
    if (bandasRegistradas.ContainsKey(nomeDaBanda))
    {
        Banda banda = bandasRegistradas[nomeDaBanda];
        Console.WriteLine($"\nA média da banda {nomeDaBanda} é {banda.Media}.");
    }
    // Resto do código
}
```
# Nova classe Avaliacao
## Visibilidade internal
A visibilidade `internal` se aplica apenas ao próprio projeto. Por exemplo: a classe `Avaliacao` que vamos criar só fica visível dentro do projeto que a contém, o `ScreenSound`.

No código, passaremos a usar todas as classes de modelo com o modificador de visibilidade `internal`.

Código novo: classe `Avaliacao`:
```CSharp
namespace ScreenSound.Modelos;

internal class Avaliacao
{
    public Avaliacao(int nota)
    {
        Nota = nota;
    }
    public int Nota { get; }
}
```
## Usando o novo tipo
A classe `Banda` vai usar a classe `Avaliacao` desta forma:

```CSharp
namespace ScreenSound.Modelos;
internal class Banda
{
    private List<Album> albuns = new List<Album>();
    private List<Avaliacao> notas = new List<Avaliacao>();

    // Resto do código

    public double Media {
        get {
            if (notas.Count == 0) return 0;
            else return notas.Average(a => a.Nota);
        }
    } 

    // Resto do código
    public void AdicionarNota(Avaliacao nota)
    {
        notas.Add(nota);
    }

    // Resto do código
}
```

E o programa principal vai adaptar os parâmetros do método `AdicionarNota` da classe `Banda` de número inteiro para um objeto da classe `Avaliacao`:
```CSharp
// Program.cs
Banda ira = new Banda("Ira!");
ira.AdicionarNota(new Avaliacao(10));
ira.AdicionarNota(new Avaliacao(8));
ira.AdicionarNota(new Avaliacao(6));
// Resto do código
void AvaliarUmaBanda()
{
    // Resto do código
    if (bandasRegistradas.ContainsKey(nomeDaBanda))
    {
        // Resto do código
        bandasRegistradas[nomeDaBanda].AdicionarNota(new Avaliacao(nota));
        // Resto do código
    }
}
// Resto do código
```
## Melhorando avaliação
Vamos acrescentar um método de conversão de string para `Avaliacao`:
```CSharp
// Avaliacao.cs
namespace ScreenSound.Modelos;

internal class Avaliacao
{
    // Resto do código

    public static Avaliacao Parse(string texto)
    {
        return new Avaliacao(int.Parse(texto));
    }
}
```
> Note que o método `Parse` foi declarado como `static`. Isso permite o uso do método sem instanciar um objeto da classe `Avaliacao`.

Vamos agora mudar o programa principal para usar esse método:
```CSharp
// Program.cs
// Resto do código
void AvaliarUmaBanda()
{
    // Resto do código
    string nomeDaBanda = Console.ReadLine()!;
    if (bandasRegistradas.ContainsKey(nomeDaBanda))
    {
        Console.Write($"Qual a nota que a banda {nomeDaBanda} merece: ");
        Avaliacao nota = Avaliacao.Parse(Console.ReadLine()!);
        Banda banda = bandasRegistradas[nomeDaBanda];
        bandasRegistradas[nomeDaBanda].AdicionarNota(nota);
        Console.WriteLine($"\nA nota {nota.Nota} foi registrada com sucesso para a banda {nomeDaBanda}");
        Thread.Sleep(2000);
        Console.Clear();
        ExibirOpcoesDoMenu();
    }
}
```
> Note que a mensagem de confirmação da inserção da avaliação/nota usa a propriedade `Nota` do objeto `nota` de classe `Avaliacao`. Antes a propriedade `Nota` não era referenciada, e, portanto, ao imprimir essa linha, aparecia o nome da classe ao invés da nota em si.

## Classe Program
Implicitamente, o arquivo `Program.cs` declara uma classe `Program` com o método `Main`, com esta estrutura:
```CSharp
internal class Program
{
    private static void Main(string[] args)
    {
        // Código.
    }
}
```
Em versões anteriores do .NET essa classe `Program` e o método `Main` eram obrigatórios, mas agora não é mais.

## Faça como eu fiz: o poder do encapsulamento
Vamos criar uma regra no construtor que permita apenas valores entre 0 e 10 na classe `Avaliacao`:

```CSharp
namespace ScreenSound.Modelos;

internal class Avaliacao
{
    public Avaliacao(int nota)
    {
        if (nota > 10) nota = 10;
        if (nota < 0) nota = 0;
        Nota = nota;
    }
    public int Nota { get; }
    // Resto do código
}
```
# Comportamentos comuns aos menus
## Isolando cada opção
Vamos enviar cada método de menu para sua respectiva classe no novo namespace `ScreenSound.Menus`:

```CSharp
// Menus\MenuExibirDetalhes.cs
using ScreenSound.Modelos;

namespace ScreenSound.Menus;
internal class MenuExibirDetalhes
{
    void ExibirTituloDaOpcao(string titulo)
    {
        int quantidadeDeLetras = titulo.Length;
        string asteriscos = string.Empty.PadLeft(quantidadeDeLetras, '*');
        Console.WriteLine(asteriscos);
        Console.WriteLine(titulo);
        Console.WriteLine(asteriscos + "\n");
    }

    public void Executar(Dictionary<string, Banda> bandasRegistradas)
    {
        // Resto do código
    }
}
```

Agora, vamos substituir no programa principal o método para chamar o menu de exibição de detalhes:
```CSharp
// Program.cs
using ScreenSound.Menus;

// Resto do código
void ExibirOpcoesDoMenu()
{
    // Resto do código
    switch (opcaoEscolhidaNumerica)
    {
        // Resto do código
        case 5:
            new MenuExibirDetalhes().Executar(bandasRegistradas);
            ExibirOpcoesDoMenu();
            break;
        // Resto do código
    }
    // Resto do código
}
```
Depois faremos essa mesma refatoração com as demais funções presentes no programa principal.

## Identificando semelhanças
Antes de refatorar as funções do programa principal para as classes de menu, precisamos criar uma superclasse da qual os menus herdarão o método `ExibirTituloOpcao`:
```CSharp
// Menus\Menu.cs
namespace ScreenSound.Menus;

internal class Menu
{
    public void ExibirTituloDaOpcao(string titulo)
    {
        int quantidadeDeLetras = titulo.Length;
        string asteriscos = string.Empty.PadLeft(quantidadeDeLetras, '*');
        Console.WriteLine(asteriscos);
        Console.WriteLine(titulo);
        Console.WriteLine(asteriscos + "\n");
    }
}
```
Criação da nova classe:
```CSharp
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuAvaliarBanda : Menu
{
    public void Executar(Dictionary<string, Banda> bandasRegistradas)
    {
        Console.Clear();
        ExibirTituloDaOpcao("Avaliar banda");
        // Resto do código
    }
}
```
> Note que depois da declaração da classe usamos a seguinte notação para aplicar herança: `MenuAvaliarBanda : Menu`. Perceba os dois pontos antes da superclasse.
> 
> Note também que o método `ExibirTituloDaOpcao` não está declarado em `MenuAvaliarBanda`, porque ele já está declarado e implementado na superclasse `Menu`.

Mudança no programa principal:
```CSharp
// Program.cs
// Resto do código
void ExibirOpcoesDoMenu()
{
    // Resto do código
    Console.Write("\nDigite a sua opção: ");
    string opcaoEscolhida = Console.ReadLine()!;
    int opcaoEscolhidaNumerica = int.Parse(opcaoEscolhida);

    switch (opcaoEscolhidaNumerica)
    {
        // Resto do código
        case 4:
            new MenuAvaliarBanda().Executar(bandasRegistradas);
            ExibirOpcoesDoMenu();
            break;
        case 5:
            new MenuExibirDetalhes().Executar(bandasRegistradas);
            ExibirOpcoesDoMenu();
            break;
        // Resto do código
    }
    // Resto do código
}
```
## Reduzindo mais linhas / Removendo o switch

Par reduzir as linhas de código, vamos terminar a migração das funções do programa principal para as seguintes classes:
1. MenuAvaliarBanda;
2. MenuExibirDetalhes;
3. MenuMostrarBanda;
4. MenuRegistrarAlbum;
5. MenuRegistrarBanda;
5. MenuSair.

Todas elas vão herdar da superclasse `Menu`, que vai conter uma implementação comum do método `Executar`:
```CSharp
// Menus\Menu.cs
using ScreenSound.Modelos;

namespace ScreenSound.Menus;
internal class Menu
{
    public void ExibirTituloDaOpcao(string titulo)
    {
        int quantidadeDeLetras = titulo.Length;
        string asteriscos = string.Empty.PadLeft(quantidadeDeLetras, '*');
        Console.WriteLine(asteriscos);
        Console.WriteLine(titulo);
        Console.WriteLine(asteriscos + "\n");
    }

    public virtual void Executar(Dictionary<string, Banda> bandasRegistradas)
    {
        Console.Clear();
    }
}
```
> Note que o método `Executar` está com a palavra reservada `virtual`. Essa palavra reservada permite a sobrescrita do método pelas descendentes desta superclasse. Toda classe filha deve usar a palavra reservada `override` para sobrepor o método da superclasse.
> 
> - Use `virtual` quando você quer fornecer uma implementação padrão, mas permite que classes derivadas a modifiquem.
> 
> - Use `abstract` quando você quer forçar as classes derivadas a implementar o método, pois não faz sentido ter uma implementação padrão.

Implementação de uma das subclasses de `Menu`:
```CSharp
// Menus\MenuMostrarBanda.cs
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuMostrarBanda : Menu
{
    public override void Executar(Dictionary<string, Banda> bandasRegistradas)
    {
        base.Executar(bandasRegistradas);
        ExibirTituloDaOpcao("Exibindo todas as bandas registradas na nossa aplicação");

        foreach (string banda in bandasRegistradas.Keys)
        {
            Console.WriteLine($"Banda: {banda}");
        }

        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
```
> Note na palavra reservada `base`: ela corresponde ao próprio objeto acrescido das implementações realizadas na superclasse (`Menu`, no caso). No exemplo, a sobrecarga do método `Executar` executa o mesmo método da superclasse e personaliza seu comportamento.

Mudanças no programa principal:
```CSharp
// Program.cs
// Resto do código
Dictionary<int, Menu> opcoes = new();
opcoes.Add(1, new MenuRegistrarBanda());
opcoes.Add(2, new MenuRegistrarAlbum());
opcoes.Add(3, new MenuMostrarBanda());
opcoes.Add(4, new MenuAvaliarBanda());
opcoes.Add(5, new MenuExibirDetalhes());
opcoes.Add(-1, new MenuSair());

// Resto do código
void ExibirOpcoesDoMenu()
{
    // Resto do código
    int opcaoEscolhidaNumerica;
    while (true)
    {
        Console.Write("\nDigite a sua opção: ");
        string opcaoEscolhida = Console.ReadLine()!;
        opcaoEscolhidaNumerica = int.Parse(opcaoEscolhida);
        if(!opcoes.ContainsKey(opcaoEscolhidaNumerica))
        {
            Console.WriteLine("Opção inválida");
        }
        else break;
    } 

    opcoes[opcaoEscolhidaNumerica].Executar(bandasRegistradas);

    if (opcaoEscolhidaNumerica != -1) ExibirOpcoesDoMenu();
}

ExibirOpcoesDoMenu();
```
> Veja o quanto o código foi simplificado após o uso do `Dictionary`: ele poupou o uso do `switch/case`.

# Alternativa para anexar semelhanças
## Álbuns e músicas avaliáveis
Vamos aprender a usar interfaces. Por convenção, no C# toda interface é prefixada pela letra `I`. 

Agora vamos criar a interface `IAvaliavel` e implementá-la na classe `Banda`:

```CSharp
// Modelos\IAvaliavel.cs
namespace ScreenSound.Modelos;

internal interface IAvaliavel
{
    void AdicionarNota(Avaliacao nota);
    double Media {get;}
}
```
> Note que `AdicionarNota` é um método, enquanto `Media` é uma propriedade. Esses dois atributos devem ser referenciados em qualquer classe que implemente esta interface.

```CSharp
// Modelos\Banda.cs
namespace ScreenSound.Modelos;
internal class Banda: IAvaliavel
{
    // Resto do código
    public double Media {
        get {
            if (notas.Count == 0) return 0;
            else return notas.Average(a => a.Nota);
        }
    } 
    // Resto do código
    public void AdicionarNota(Avaliacao nota)
    {
        notas.Add(nota);
    }
    // Resto do código
}
```
> Note o conteúdo no fim da declaração da classe: `internal class Banda: IAvaliavel`. Para forçar a implementação da interface, use `: Interface` após a declaração da classe.

## IAvaliavel em álbum e música / Menu para avaliar álbum
Vamos implementar a interface `IAvaliavel` na classe `Album`:
```CSharp
// Modelos\Album.cs
namespace ScreenSound.Modelos;

internal class Album: IAvaliavel
{
    private List<Musica> musicas = new List<Musica>();
    private List<Avaliacao> notas = new();
    // Resto do código
    public double Media {
        get {
            if (notas.Count == 0) return 0;
            else return notas.Average(a => a.Nota);
        }
    }

    public void AdicionarNota(Avaliacao nota)
    {
        notas.Add(nota);
    }
    // Resto do código
}
```

Agora, vamos criar um menu para avaliar álbuns:
```CSharp
// Menus\MenuAvaliarAlbum.cs
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuAvaliarAlbum : Menu
{
    public override void Executar(Dictionary<string, Banda> bandasRegistradas)
    {
        base.Executar(bandasRegistradas);
        ExibirTituloDaOpcao("Avaliar álbum");
        Console.Write("Digite o nome da banda que deseja avaliar: ");
        string nomeDaBanda = Console.ReadLine()!;
        if (bandasRegistradas.ContainsKey(nomeDaBanda))
        {
            Banda banda = bandasRegistradas[nomeDaBanda];
            Console.Write("Agora digite o título do álbum: ");
            string tituloAlbum = Console.ReadLine()!;
            if (banda.Albuns.Any(a => a.Nome == tituloAlbum))
            {
                Album album = banda.Albuns.First(a => a.Nome == tituloAlbum);
                Console.Write($"Qual a nota que o álbum {tituloAlbum} merece: ");
                Avaliacao nota = Avaliacao.Parse(Console.ReadLine()!);
                album.AdicionarNota(nota);
                Console.WriteLine($"\nA nota {nota.Nota} foi registrada com sucesso para o álbum {tituloAlbum}");
            }
            else 
            {
                Console.WriteLine($"\nO álbum {tituloAlbum} não foi encontrado!");
            }
        }
        else
        {
            Console.WriteLine($"\nA banda {nomeDaBanda} não foi encontrada!");
        }
        Console.WriteLine("Digite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
```
Finalmente, vamos incluir esse novo menu no programa principal:
```CSharp
// Program.cs
// Resto do código
Dictionary<int, Menu> opcoes = new();
// Resto do código
opcoes.Add(4, new MenuAvaliarBanda());
opcoes.Add(5, new MenuAvaliarAlbum());
opcoes.Add(6, new MenuExibirDetalhes());
opcoes.Add(-1, new MenuSair());
// Resto do código
void ExibirOpcoesDoMenu()
{
    // Resto do código
    Console.WriteLine("Digite 4 para avaliar uma banda");
    Console.WriteLine("Digite 5 para avaliar um álbum");
    Console.WriteLine("Digite 6 para exibir os detalhes de uma banda");
    // Resto do código
}
ExibirOpcoesDoMenu();
```
## Completando o novo menu
A classe `MenuExibirDetalhes` agora mostrará a média de notas dos álbuns de uma banda, conforme código a seguir:

```CSharp
// Menus\MenuExibirDetalhes.cs
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuExibirDetalhes : Menu
{
    public override void Executar(Dictionary<string, Banda> bandasRegistradas)
    {
        // Resto do código
        if (bandasRegistradas.ContainsKey(nomeDaBanda))
        {
            Banda banda = bandasRegistradas[nomeDaBanda];
            Console.WriteLine($"\nA média da banda {nomeDaBanda} é {banda.Media}.");
            Console.WriteLine($"\nDiscografia:");
            foreach(Album album in banda.Albuns)
            {
                Console.WriteLine($"{album.Nome} -> {album.Media}");
            }
        }
        // Resto do código
    }
}
```
