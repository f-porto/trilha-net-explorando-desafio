using DesafioProjetoHospedagem.Views;
using Terminal.Gui;

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
Application.Run();
Application.Shutdown();

void TrocarJanela<TWindow>() where TWindow : Window, new()
{
    Application.Top.Remove(janelaAtual);
    janelaAtual = new TWindow
    {
        Y = Pos.Bottom(menu)
    };
    Application.Top.Add(janelaAtual);
}
