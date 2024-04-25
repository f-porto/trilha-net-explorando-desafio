using Terminal.Gui;

namespace DesafioProjetoHospedagem;

public static class Extensions
{
    public static (Pos, Pos) AdicionarLabels(this View view, params string[] textos)
    {
        var maiorTamanho = textos.Max(x => x.Length);
        var y = Pos.Top(view);
        Label label = null;
        foreach (var texto in textos.Select(x => (x + ":").PadRight(maiorTamanho)))
        {
            label = new Label(texto)
            {
                Y = y,
            };
            y += 2;
            view.Add(label);
        }
        return (Pos.Bottom(label), Pos.Right(label));
    }

    public static void CriarDialogo(this View janela, string titulo, string mensagem)
    {
        var dialogo = new Dialog(titulo);
        var texto = new Label(mensagem);
        var botao = new Button("Ok");
        botao.Clicked += () => janela.Remove(dialogo);
        dialogo.AddButton(botao);
        dialogo.Add(texto);
        janela.Add(dialogo);
    }
}