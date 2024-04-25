using DesafioProjetoHospedagem.Models;
using Terminal.Gui;

namespace DesafioProjetoHospedagem.Views;

public class RemoverSuite : HotelView
{
    public override void Init()
    {
        Title = "Remover Suíte";

        var (abaixo, direita) = this.AdicionarLabels("Id da suíte", "Tipo da suíte", "Capacidade", "Valor da diária");
        var entradaId = new TextField()
        {
            X = direita + 1,
            Y = 1,
            Width = Dim.Fill(),
        };
        var labelTipo = new Label()
        {
            X = direita + 1,
            Y = 3,
        };
        var labelCapacidade = new Label()
        {
            X = direita + 1,
            Y = 5,
        };
        var labelValorDiaria = new Label()
        {
            X = direita + 1,
            Y = 7,
        };

        var botaoProcurar = new Button("Procurar")
        {
            X = direita + 1,
            Y = abaixo,
        };
        var botaoRemover = new Button("Remover")
        {
            X = Pos.Right(botaoProcurar) + 1,
            Y = abaixo,
        };

        Suite suite = null;
        botaoProcurar.Clicked += () =>
        {
            var suiteId = entradaId.Text.ToString().Trim().ToUpper();
            if (suiteId.Length == 0)
            {
                this.CriarDialogo("Erro", "Id da suíte não foi fornecido");
                return;
            }
            suite = _db.GetSuiteById(suiteId);
            if (suite is null)
            {
                this.CriarDialogo("Erro", $"Suíte '{suiteId}' não existe");
                return;
            }
            if (suite.Reservado)
            {
                this.CriarDialogo("Erro", $"Suíte '{suiteId}' foi reservada");
                return;
            }
            labelTipo.Text = suite.TipoSuite;
            labelCapacidade.Text = suite.Capacidade.ToString();
            labelValorDiaria.Text = suite.ValorDiaria.ToString();
        };
        botaoRemover.Clicked += () =>
        {
            if (suite is null)
            {
                this.CriarDialogo("Erro", "Suíte não foi selecionada");
                return;
            }
            if (suite.Reservado)
            {
                this.CriarDialogo("Erro", $"Suíte '{suite.Id}' foi reservada");
                return;
            }
            _db.DeleteSuite(suite.Id);
            this.CriarDialogo("Sucesso", $"Suíte '{suite.Id}' for removida");
        };

        Add(entradaId, labelTipo, labelCapacidade, labelValorDiaria, botaoProcurar, botaoRemover);
    }
}