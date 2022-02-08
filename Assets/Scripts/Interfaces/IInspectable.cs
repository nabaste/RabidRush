using System.Collections.Generic;

public interface IInspectable
{
    public string GetName();
    public float GetLife();
    public Dictionary<string, float> GetStats();
}