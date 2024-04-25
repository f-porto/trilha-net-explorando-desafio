using DesafioProjetoHospedagem.Models;
using Terminal.Gui;

namespace DesafioProjetoHospedagem.Views;

public class NovaSuite : Window, IDBView
{
    const string Identificador = "Identificador";
    const string Tipo = "Tipo";
    const string Capacidade = "Capacidade";
    const string ValorDiaria = "Valor da Diária";

    private DataBase _db;

    public NovaSuite()
    {
        Title = "Nova Suíte";
        var entradas = this.AdicionarEntradas(Identificador, Tipo, Capacidade, ValorDiaria);

        var botaoSalvar = new Button
        {
            Text = "Salvar",
            Y = Pos.Bottom(entradas[ValorDiaria]) + 1,
            X = Pos.Left(entradas[ValorDiaria]),
        };
        botaoSalvar.Clicked += () =>
        {
            var id = entradas[Identificador].Text.ToString().Trim().ToUpper();
            var tipo = entradas[Tipo].Text.ToString().Trim().ToUpper();
            var capacidade = entradas[Capacidade].Text.ToString().Trim();
            var valorDiaria = entradas[ValorDiaria].Text.ToString().Trim();
            if (!int.TryParse(capacidade, out int cap))
            {
                var dialogo = new Dialog("Erro");
                var texto = new Label($"'{capacidade}' não é um número válido para capacidade");
                var botao = new Button("Ok");
                botao.Clicked += () => Remove(dialogo);
                dialogo.AddButton(botao);
                dialogo.Add(texto);
                Add(dialogo);
                return;
            }
            if (!decimal.TryParse(valorDiaria, out decimal vd))
            {
                var dialogo = new Dialog("Erro");
                var texto = new Label($"'{valorDiaria}' não é um número válido para valor da diária");
                var botao = new Button("Ok");
                botao.Clicked += () => Remove(dialogo);
                dialogo.AddButton(botao);
                dialogo.Add(texto);
                Add(dialogo);
                return;
            }
            var suite = new Suite(id, tipo, cap, vd);
            if (!_db.AddSuite(suite))
            {
                var dialogo = new Dialog("Erro");
                var texto = new Label($"Suite com identificador '{id}' já existe");
                var botao = new Button("Ok");
                botao.Clicked += () => Remove(dialogo);
                dialogo.AddButton(botao);
                dialogo.Add(texto);
                Add(dialogo);
            }
            else
            {
                var dialogo = new Dialog("Sucesso");
                var texto = new Label($"Suite '{id}' foi adicionada");
                var botao = new Button("Ok");
                botao.Clicked += () => Remove(dialogo);
                dialogo.AddButton(botao);
                dialogo.Add(texto);
                Add(dialogo);
            }
        };

        Add(botaoSalvar);
    }

    public void SetDataBase(DataBase db)
    {
        _db = db;
    }
}
