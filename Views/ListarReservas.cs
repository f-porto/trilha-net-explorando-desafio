using Terminal.Gui;

namespace DesafioProjetoHospedagem.Views;

class ListarReservas : Window, IDBView
{
    private DataBase _db;

    public ListarReservas()
    {
        Title = "Reservas";
    }

    public void SetDataBase(DataBase db)
    {
        _db = db;
    }
}
