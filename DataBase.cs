using System.Text.Json;
using DesafioProjetoHospedagem.Models;

namespace DesafioProjetoHospedagem;

public class DataBase
{
    private readonly Dictionary<string, Suite> _suites = new();

    public DataBase() { }

    public Suite GetSuiteById(string id)
    {
        if (_suites.ContainsKey(id))
        {
            return _suites[id];
        }
        return null;
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

    public void DeleteSuite(string id)
    {
        _suites.Remove(id);
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
        LoadSuites();
    }

    public void Close()
    {
        SaveSuites();
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

    public void SaveSuites()
    {
        using var sw = new StreamWriter("./data/suites.json");
        var suites = _suites.Values.ToList();
        JsonSerializer.Serialize(sw.BaseStream, suites);
    }
}
