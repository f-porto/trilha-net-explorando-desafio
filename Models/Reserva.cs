using DesafioProjetoHospedagem.Exceptions;

namespace DesafioProjetoHospedagem.Models;

public class Reserva
{
    public static int IdCounter { get; set; }

    public int Id { get; set; }
    public List<Pessoa> Hospedes { get; set; }
    public Suite Suite { get; set; }
    public int DiasReservados { get; set; }

    public Reserva() { }

    public Reserva(int diasReservados)
    {
        DiasReservados = diasReservados;
        Id = IdCounter++;
    }

    public void CadastrarHospedes(List<Pessoa> hospedes)
    {
        if (hospedes.Count <= Suite.Capacidade)
        {
            Hospedes = hospedes;
        }
        else
        {
            string msg = $"A quantidade de hóspedes ({hospedes.Count}) supera a capacidade da suíte ({Suite.Capacidade})";
            throw new MuitoHospedesException(msg);
        }
    }

    public void CadastrarSuite(Suite suite)
    {
        Suite = suite;
    }

    public int ObterQuantidadeHospedes()
    {
        return Hospedes.Count;
    }

    public decimal CalcularValorDiaria()
    {
        decimal valor = DiasReservados * Suite.ValorDiaria;
        if (DiasReservados >= 10)
        {
            valor *= 0.9m;
        }
        return valor;
    }
}
