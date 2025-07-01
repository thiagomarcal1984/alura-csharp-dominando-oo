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
