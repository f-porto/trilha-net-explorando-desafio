using Terminal.Gui;

namespace DesafioProjetoHospedagem.Views;

public class NovaSuite: Window
{
    const string Identificador = "Identificador";
    const string Tipo = "Tipo";
    const string Capacidade = "Capacidade";
    const string ValorDiaria = "Valor da Diária";

    public NovaSuite()
    {
        Title = "Nova Suíte";
        var entradas = this.AdicionarEntradas(Identificador, Tipo, Capacidade, ValorDiaria);

        var butaoSalvar = new Button
        {
            Text = "Salvar",
            Y = Pos.Bottom(entradas[ValorDiaria]) + 1,
            X = Pos.Left(entradas[ValorDiaria]),
        };
        Add(butaoSalvar);
    }
}
