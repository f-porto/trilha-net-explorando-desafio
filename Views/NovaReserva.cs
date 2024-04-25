using Terminal.Gui;

namespace DesafioProjetoHospedagem.Views;

public class NovaReserva : HotelView
{
    private List<TextField> _entradasNomes = new();
    private List<TextField> _entradasSobrenomes = new();
    private List<Label> _labelNomes = new();

    public override void Init()
    {
        Title = "Nova Reserva";

        var quadroSuite = new FrameView("Suíte")
        {
            Width = Dim.Fill(),
            Height = 2 + 4 * 2,
        };
        var quadroReserva = new FrameView("Reserva")
        {
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            Y = Pos.Bottom(quadroSuite),
        };
        var scrollReserva = new ScrollView()
        {
            Height = Dim.Fill(),
            Width = Dim.Fill(),
            ContentSize = new Size(80, 4),
        };
        var labelDias = new Label("Dias:");
        var entradaDias = new TextField()
        {
            Width = 10,
            X = Pos.Right(labelDias) + 1,
        };
        var botaoReservar = new Button("Reservar")
        {
            Y = 1,
            X = Pos.Right(labelDias) + 1,
        };

        var (abaixo, direita) = quadroSuite.AdicionarLabels("Identificador", "Tipo", "Capacidade", "Valor da Diária");
        var entradaId = new TextField()
        {
            Width = Dim.Fill(),
            X = direita + 1,
        };
        var labelTipo = new Label()
        {
            Y = 2,
            X = direita + 1,
        };
        var labelCapacidade = new Label()
        {
            Y = 4,
            X = direita + 1,
        };
        var labelValorDiaria = new Label()
        {
            Y = 6,
            X = direita + 1,
        };

        var botaoProcurar = new Button("Procurar")
        {
            Y = abaixo,
            X = direita + 1,
        };
        var quadroScrollReserva = new View()
        {
            Height = Dim.Fill(),
            Width = Dim.Fill(),
        };

        botaoProcurar.Clicked += () =>
        {
            var id = entradaId.Text.ToString().Trim().ToUpper();
            var suite = _db.GetSuiteById(id);
            if (suite is null)
            {
                this.CriarDialogo("Erro", $"Suíte '{id}' não existe");
                return;
            }
            labelTipo.Text = suite.TipoSuite.ToString();
            labelCapacidade.Text = suite.Capacidade.ToString();
            labelValorDiaria.Text = suite.ValorDiaria.ToString();
            AdicionarCamposHospedes(quadroScrollReserva, suite.Capacidade);
            botaoReservar.Y = 2 + suite.Capacidade * 2 - 1;
            scrollReserva.ContentSize = new Size(80, 2 + suite.Capacidade * 2);
        };

        quadroSuite.Add(entradaId, labelTipo, labelCapacidade, labelValorDiaria, botaoProcurar);

        quadroScrollReserva.Add(labelDias, entradaDias, botaoReservar);
        scrollReserva.Add(quadroScrollReserva);
        quadroReserva.Add(scrollReserva);

        Add(quadroSuite, quadroReserva);
    }

    private void AdicionarCamposHospedes(View reservaView, int capacidade)
    {
        _labelNomes.ForEach(x => reservaView.Remove(x));
        _entradasNomes.ForEach(x => reservaView.Remove(x));
        _entradasSobrenomes.ForEach(x => reservaView.Remove(x));

        _labelNomes.Clear();
        _entradasNomes.Clear();
        _entradasSobrenomes.Clear();

        reservaView.Height = 2 + 2 + capacidade * 2;
        for (var i = 0; i < capacidade; ++i)
        {
            var labelNome = new Label("Nome:")
            {
                Y = 2 + 2 * i,
            };
            var entradaNome = new TextField()
            {
                Y = 2 + 2 * i,
                X = Pos.Right(labelNome) + 1,
                Width = 20,
            };
            var labelSobrenome = new Label("Sobrebome:")
            {
                Y = 2 + 2 * i,
                X = Pos.Right(entradaNome) + 1,
            };
            var entradaSobrenome = new TextField()
            {
                Y = 2 + 2 * i,
                X = Pos.Right(labelSobrenome) + 1,
                Width = 20,
            };
            reservaView.Add(labelNome, entradaNome, labelSobrenome, entradaSobrenome);
            _labelNomes.Add(labelNome);
            _labelNomes.Add(labelSobrenome);
            _entradasNomes.Add(entradaNome);
            _entradasSobrenomes.Add(entradaNome);
        }
    }
}
