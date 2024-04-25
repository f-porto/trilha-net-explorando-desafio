using Terminal.Gui;

namespace DesafioProjetoHospedagem.Views;

class ListarSuites : Window, IDBView
{
    private DataBase _db;

    public ListarSuites()
    {
        Title = "Suítes";
    }

    public void SetDataBase(DataBase db)
    {
        _db = db;
    }
}
