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
