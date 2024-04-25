using Terminal.Gui;

namespace DesafioProjetoHospedagem.Views;

public class FinalizarReserva: Window, IDBView
{
    private DataBase _db;

    public FinalizarReserva()
    {
        Title = "Finalizar Reserva";
    }

    public void SetDataBase(DataBase db)
    {
        _db = db;
    }
}
