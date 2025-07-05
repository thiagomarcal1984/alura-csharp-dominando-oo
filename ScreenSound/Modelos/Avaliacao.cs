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

    public static Avaliacao Parse(string texto)
    {
        return new Avaliacao(int.Parse(texto));
    }
}
