using Terminal.Gui;

namespace DesafioProjetoHospedagem;

public static class Extensions
{
    public static Dictionary<string, TextField> AdicionarEntradas(this Window janela, params string[] textos)
    {
        var indiceMaior = 0;
        var maior = 0;
        var labels = new List<Label>();
        for (var i = 0; i < textos.Length; ++i)
        {
            if (textos[i].Length > maior)
            {
                maior = textos[i].Length;
                indiceMaior = i;
            }
            labels.Add(new Label(textos[i] + ":"));
        }

        janela.Add(labels[0]);
        for (var i = 1; i < labels.Count; ++i)
        {
            labels[i].Y = Pos.Bottom(labels[i - 1]) + 1;
            janela.Add(labels[i]);
        }

        var itens = new Dictionary<string, TextField>();
        for (var i = 0; i < labels.Count; ++i)
        {
            var entrada = new TextField("")
            {
                Y = Pos.Top(labels[i]),
                X = Pos.Right(labels[indiceMaior]) + 1,
                Width = Dim.Fill(5),
            };
            janela.Add(entrada);
            itens.Add(textos[i], entrada);
        }
        return itens;
    }
}