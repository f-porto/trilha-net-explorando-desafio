using DesafioProjetoHospedagem;
using DesafioProjetoHospedagem.Views;
using Terminal.Gui;

var db = new DataBase();
db.Init();

var menu = new MenuBar();
Window janelaAtual = new NovaReserva
{
    Y = Pos.Bottom(menu)
};

menu.Menus = new MenuBarItem[] {
    new("Reserva", new MenuItem[] {
        new("Nova reserva", "", TrocarJanela<NovaReserva>),
        new("Finalizar reserva", "", TrocarJanela<FinalizarReserva>),
        new("Listar reservas", "", TrocarJanela<ListarReservas>),
    }),
    new("Suíte", new MenuItem[] {
        new("Nova suíte", "", TrocarJanela<NovaSuite>),
        new("Remover suíte", "", TrocarJanela<RemoverSuite>),
        new("Listar suítes", "", TrocarJanela<ListarReservas>),
    }),
    new("Fechar", "", () => Application.RequestStop()),
};

Application.Init();
Application.Top.Add(menu, janelaAtual);
Application.Run((Exception e) =>
{
    File.WriteAllText("log.txt", e.ToString());
    return false;
});
Application.Shutdown();

db.SaveSuites();

void TrocarJanela<TWindow>() where TWindow : Window, IDBView, new()
{
    Application.Top.Remove(janelaAtual);
    var janela = new TWindow()
    {
        Y = Pos.Bottom(menu)
    };
    janela.SetDataBase(db);
    janelaAtual = janela;
    Application.Top.Add(janelaAtual);
}
