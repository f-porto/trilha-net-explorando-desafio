namespace DesafioProjetoHospedagem.Models;

public class Suite
{
    public Suite() { }

    public Suite(string id, string tipoSuite, int capacidade, decimal valorDiaria)
    {
        Id = id;
        TipoSuite = tipoSuite;
        Capacidade = capacidade;
        ValorDiaria = valorDiaria;
        Reservado = false;
    }

    public string Id { get; init; }
    public string TipoSuite { get; set; }
    public int Capacidade { get; set; }
    public decimal ValorDiaria { get; set; }
    public bool Reservado { get; set; }
    public int IdReserva { get; set; }
}
