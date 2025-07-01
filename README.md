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