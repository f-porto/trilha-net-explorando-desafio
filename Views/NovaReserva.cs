using Terminal.Gui;

namespace DesafioProjetoHospedagem.Views;

public class NovaReserva: Window, IDBView
{
    private DataBase _db;

    public NovaReserva()
    {
        Title = "Nova Reserva";
    }

    public void SetDataBase(DataBase db)
    {
        _db = db;
    }
}
