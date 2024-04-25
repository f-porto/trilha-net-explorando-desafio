using Terminal.Gui;

namespace DesafioProjetoHospedagem.Views;

public class RemoverSuite : Window, IDBView
{
    private DataBase _db;

    public RemoverSuite()
    {
        Text = "Remover Suíte";
    }

    public void SetDataBase(DataBase db)
    {
        _db = db;
    }
}