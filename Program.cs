using DesafioProjetoHospedagem;
using DesafioProjetoHospedagem.Views;
using Terminal.Gui;

var db = new DataBase();
db.Init();

Application.Init();

var menu = new MenuBar();
Window janelaAtual = null;

menu.Menus = new MenuBarItem[] {
    new("Reserva", new MenuItem[] {
        new("Nova reserva", "", TrocarJanela<NovaReserva>),
        new("Finalizar reserva", "", TrocarJanela<FinalizarReserva>),
        new("Listar reservas", "", TrocarJanela<ListarReservas>),
    }),
    new("Suíte", new MenuItem[] {
        new("Nova suíte", "", TrocarJanela<NovaSuite>),
        new("Remover suíte", "", TrocarJanela<RemoverSuite>),
        new("Listar suítes", "", TrocarJanela<ListarSuites>),
    }),
    new("Fechar", "", () => Application.RequestStop()),
};

Application.Top.Add(menu);
Application.Run((Exception e) =>
{
    File.WriteAllText("log.txt", e.ToString());
    return false;
});
Application.Shutdown();

db.SaveSuites();

void TrocarJanela<TWindow>() where TWindow : HotelView, new()
{
    Application.Top.Remove(janelaAtual);
    var janela = new TWindow()
    {
        Y = Pos.Bottom(menu)
    };
    janela.SetDatabase(db);
    janela.Init();
    janelaAtual = janela;
    Application.Top.Add(janelaAtual);
}
