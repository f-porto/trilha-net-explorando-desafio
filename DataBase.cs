using System.Text.Json;
using DesafioProjetoHospedagem.Models;

namespace DesafioProjetoHospedagem;

public class DataBase
{
    private readonly Dictionary<string, Suite> _suites = new();
    private readonly Dictionary<int, Reserva> _reservas = new();

    public DataBase() { }

    public Suite GetSuiteById(string id)
    {
        if (_suites.ContainsKey(id))
        {
            return _suites[id];
        }
        return null;
    }

    public List<Suite> GetSuites()
    {
        return _suites.Values.ToList();
    }

    public bool AddSuite(Suite suite)
    {
        if (_suites.ContainsKey(suite.Id))
        {
            return false;
        }
        _suites.Add(suite.Id, suite);
        return true;
    }

    public bool UpdateSuite(Suite suite)
    {
        if (!_suites.ContainsKey(suite.Id))
        {
            return false;
        }
        _suites[suite.Id] = suite;
        return true;
    }

    public void DeleteSuite(string id)
    {
        _suites.Remove(id);
    }

    public Reserva GetReservaById(int id)
    {
        if (_reservas.ContainsKey(id))
        {
            return _reservas[id];
        }
        return null;
    }

    public List<Reserva> GetReservas()
    {
        return _reservas.Values.ToList();
    }

    public bool AddReserva(Reserva reserva)
    {
        if (_reservas.ContainsKey(reserva.Id))
        {
            return false;
        }
        _reservas[reserva.Id] = reserva;
        return true;
    }

    public void DeleteReserva(int id)
    {
        _reservas.Remove(id);
    }

    public void Init()
    {
        if (!Directory.Exists("./data"))
        {
            Directory.CreateDirectory("./data");
        }
        if (!File.Exists("./data/suites.json"))
        {
            File.WriteAllText("./data/suites.json", "[]");
        }
        if (!File.Exists("./data/reservas.json"))
        {
            var data = new ReservaData
            {
                IdCounter = 0,
                Reservas = new(),
            };
            var text = JsonSerializer.Serialize(data);
            File.WriteAllText("./data/reservas.json", text);
        }
        LoadSuites();
        LoadReservas();
    }

    public void Close()
    {
        SaveSuites();
        SaveReservas();
    }

    public void LoadSuites()
    {
        using var sr = new StreamReader("./data/suites.json");
        var suites = JsonSerializer.Deserialize<List<Suite>>(sr.BaseStream);
        foreach (var suite in suites)
        {
            _suites.Add(suite.Id, suite);
        }
    }

    public void LoadReservas()
    {
        using var sr = new StreamReader("./data/reservas.json");
        var data = JsonSerializer.Deserialize<ReservaData>(sr.BaseStream);
        foreach (var reserva in data.Reservas)
        {
            _reservas.Add(reserva.Id, reserva);
        }
        Reserva.IdCounter = data.IdCounter;
    }

    public void SaveSuites()
    {
        using var sw = new StreamWriter("./data/suites.json");
        var suites = _suites.Values.ToList();
        JsonSerializer.Serialize(sw.BaseStream, suites);
    }

    public void SaveReservas()
    {
        using var sw = new StreamWriter("./data/reservas.json");
        var reservas = _reservas.Values.ToList();
        var data = new ReservaData
        {
            IdCounter = Reserva.IdCounter,
            Reservas = reservas,
        };
        JsonSerializer.Serialize(sw.BaseStream, data);
    }
}

struct ReservaData
{
    public int IdCounter { get; set; }
    public List<Reserva> Reservas { get; set; }
}
