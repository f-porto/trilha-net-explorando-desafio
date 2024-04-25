using System.Data;
using Terminal.Gui;

namespace DesafioProjetoHospedagem.Views;

class ListarSuites : HotelView
{
    public override void Init()
    {
        Title = "Suítes";

        var tabela = new DataTable();

        var colunaId = new DataColumn("Identificador", typeof(string));
        tabela.Columns.Add(colunaId);
        var colunaTipo = new DataColumn("Tipo", typeof(string));
        tabela.Columns.Add(colunaTipo);
        var colunaCapacidade = new DataColumn("Capacidade", typeof(int));
        tabela.Columns.Add(colunaCapacidade);
        var colunaValor = new DataColumn("Valor da diária", typeof(decimal));
        tabela.Columns.Add(colunaValor);

        foreach (var suite in _db.GetSuites())
        {
            var row = tabela.NewRow();
            row["Identificador"] = suite.Id;
            row["Tipo"] = suite.TipoSuite;
            row["Capacidade"] = suite.Capacidade;
            row["Valor da diária"] = suite.ValorDiaria;
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
