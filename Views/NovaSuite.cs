using DesafioProjetoHospedagem.Models;
using Terminal.Gui;

namespace DesafioProjetoHospedagem.Views;

public class NovaSuite : HotelView
{
    public override void Init()
    {
        Title = "Nova Suíte";
        var (abaixo, direita) = this.AdicionarLabels("Identificador", "Tipo", "Capacidade", "Valor da Diária");
        var entradaId = new TextField()
        {
            Y = 0,
            X = direita + 1,
            Width = Dim.Fill(),
        };
        var entradaTipo = new TextField()
        {
            Y = 2,
            X = direita + 1,
            Width = Dim.Fill(),
        };
        var entradaCapacidade = new TextField()
        {
            Y = 4,
            X = direita + 1,
            Width = Dim.Fill(),
        };
        var entradaValorDiara = new TextField()
        {
            Y = 6,
            X = direita + 1,
            Width = Dim.Fill(),
        };

        var botaoSalvar = new Button
        {
            Text = "Salvar",
            Y = abaixo + 1,
            X = direita + 1,
        };
        botaoSalvar.Clicked += () =>
        {
            var id = entradaId.Text.ToString().Trim().ToUpper();
            var tipo = entradaTipo.Text.ToString().Trim().ToUpper();
            var capacidade = entradaCapacidade.Text.ToString().Trim();
            var valorDiaria = entradaValorDiara.Text.ToString().Trim();
            if (!int.TryParse(capacidade, out int cap))
            {
                this.CriarDialogo("Erro", $"'{capacidade}' não é um número válido para capacidade");
                return;
            }
            if (!decimal.TryParse(valorDiaria, out decimal vd))
            {
                this.CriarDialogo("Erro", $"'{valorDiaria}' não é um número válido para valor da diária");
                return;
            }
            var suite = new Suite(id, tipo, cap, vd);
            if (!_db.AddSuite(suite))
            {
                this.CriarDialogo("Erro", $"Suite com identificador '{id}' já existe");
            }
            else
            {
                this.CriarDialogo("Sucesso", $"Suite '{id}' foi adicionada");
            }
        };

        Add(botaoSalvar);
    }
}
