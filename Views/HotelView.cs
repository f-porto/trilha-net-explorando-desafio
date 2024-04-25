using Terminal.Gui;

namespace DesafioProjetoHospedagem.Views;

public abstract class HotelView : Window
{
    protected DataBase _db;

    public void SetDatabase(DataBase db) => _db = db;

    public abstract void Init();
}