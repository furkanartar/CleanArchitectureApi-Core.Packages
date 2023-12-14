namespace Core.CrossCuttingConcerns.Logging;

public class LogParameter //loglamak istediğimiz parametreleri tutacağımız sınıf
{
    public string Name { get; set; }
    public object Value { get; set; }
    public string Type { get; set; }

    public LogParameter()
    {
        Name = string.Empty;
        Value = string.Empty;
        Type = string.Empty;
    }

    public LogParameter(string name, object value, string type)
    {
        Name = name;
        Value = value;
        Type = type;
    }
}
