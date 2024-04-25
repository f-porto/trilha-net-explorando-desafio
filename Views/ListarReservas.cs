using System.Data;
using Terminal.Gui;

namespace DesafioProjetoHospedagem.Views;

class ListarReservas : HotelView
{
    public override void Init()
    {
        Title = "Reservas";

        var tabela = new DataTable();

        var colunaId = new DataColumn("Identificador", typeof(string));
        tabela.Columns.Add(colunaId);
        var colunaIdSuite = new DataColumn("Id da Suíte", typeof(string));
        tabela.Columns.Add(colunaIdSuite);
        var colunaDias = new DataColumn("Dias Reservados", typeof(int));
        tabela.Columns.Add(colunaDias);

        foreach (var reserva in _db.GetReservas())
        {
            var row = tabela.NewRow();
            row["Identificador"] = reserva.Id;
            row["Id da Suíte"] = reserva.Suite.Id;
            row["Dias Reservados"] = reserva.DiasReservados;
            tabela.Rows.Add(row);
        }

        var tabelaView = new TableView(tabela)
        {
            Height = Dim.Fill(),
            Width = Dim.Fill(),
        };
        Add(tabelaView);
    }
}
