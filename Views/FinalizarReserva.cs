using DesafioProjetoHospedagem.Models;
using Terminal.Gui;

namespace DesafioProjetoHospedagem.Views;

public class FinalizarReserva : HotelView
{
    public override void Init()
    {
        Title = "Finalizar Reserva";

        var (abaixo, direita) = this.AdicionarLabels("ID da reserva", "ID da suíte", "Dias Reservados", "Valor da diária", "Valor final");
        var entradaIdReserva = new TextField()
        {
            Y = 1,
            X = direita + 1,
            Width = Dim.Fill(),
        };
        var entradaIdSuite = new TextField()
        {
            X = direita + 1,
            Y = 3,
            Width = Dim.Fill(),
        };
        var labelDiasReservados = new Label()
        {
            X = direita + 1,
            Y = 5,
        };
        var labelValorDiaria = new Label()
        {
            X = direita + 1,
            Y = 7,
        };
        var labelValorFinal = new Label()
        {
            X = direita + 1,
            Y = 9,
        };

        var botaoProcurar = new Button("Procurar")
        {
            X = direita + 1,
            Y = abaixo,
        };
        var botaoFinalizar = new Button("Finalizar")
        {
            X = Pos.Right(botaoProcurar) + 1,
            Y = abaixo,
        };

        Reserva reserva = null;
        botaoProcurar.Clicked += () =>
        {
            var idReservaTexto = entradaIdReserva.Text.ToString().Trim();
            if (idReservaTexto.Length != 0)
            {
                if (!int.TryParse(idReservaTexto, out int idReserva))
                {
                    this.CriarDialogo("Erro", $"'{idReservaTexto}' não é um id válido de reserva");
                    return;
                }
                reserva = _db.GetReservaById(idReserva);
                if (reserva is null)
                {
                    this.CriarDialogo("Erro", $"Reserva com id '{idReservaTexto}' não existe");
                    return;
                }
                entradaIdSuite.Text = reserva.Suite.Id;
                labelDiasReservados.Text = reserva.DiasReservados.ToString();
                labelValorDiaria.Text = reserva.Suite.ValorDiaria.ToString();
                labelValorFinal.Text = CalcularValorFinal(reserva.DiasReservados, reserva.Suite.ValorDiaria).ToString();
                return;
            }
            var idSuite = entradaIdSuite.Text.ToString().Trim().ToUpper();
            var suite = _db.GetSuiteById(idSuite);
            if (suite is null)
            {
                this.CriarDialogo("Erro", $"Não existe suíte com id '{idSuite}'");
                return;
            }
            if (!suite.Reservado)
            {
                this.CriarDialogo("Erro", $"A suíte '{idSuite}' não for reservada");
                return;
            }
            reserva = _db.GetReservaById(suite.IdReserva);
            entradaIdReserva.Text = reserva.Id.ToString();
            labelDiasReservados.Text = reserva.DiasReservados.ToString();
            labelValorDiaria.Text = reserva.Suite.ValorDiaria.ToString();
            labelValorFinal.Text = CalcularValorFinal(reserva.DiasReservados, reserva.Suite.ValorDiaria).ToString();
        };
        botaoFinalizar.Clicked += () =>
        {
            if (reserva is null)
            {
                this.CriarDialogo("Erro", "Reserva não foi selecionada");
                return;
            }
            reserva.Suite.Reservado = false;
            _db.DeleteReserva(reserva.Id);
            _db.UpdateSuite(reserva.Suite);
            this.CriarDialogo("Sucesso", $"Reserva '{reserva.Id}' for finalizada");
        };

        Add(entradaIdReserva, entradaIdSuite, labelDiasReservados, labelValorDiaria, labelValorFinal, botaoProcurar, botaoFinalizar);
    }

    private static decimal CalcularValorFinal(int diasReservados, decimal valorDiaria)
    {
        var valorFinal = diasReservados * valorDiaria;
        if (diasReservados >= 10)
        {
            valorFinal *= 0.9m;
        }
        return valorFinal;
    }
}
