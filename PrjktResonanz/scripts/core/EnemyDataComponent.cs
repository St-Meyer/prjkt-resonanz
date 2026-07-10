using Godot;
using System.Collections.Generic;
using System.Text.Json;

[GlobalClass]
public partial class EnemyDataComponent : Node
{
    public int Strength;
    public int Speed;
    public int MaxHealth;
    public float AttackTime;
    private string _name;
    private Dictionary<string, JsonElement> _enemyDatas;


    public override void _Ready()
    {
        _name = GetParent().Name;
        Strength = GetValue<int>("Strength", _name);
        Speed = GetValue<int>("Speed", _name);
        MaxHealth = GetValue<int>("MaxHealth", _name);
        AttackTime = GetValue<float>("AttackTime", _name);
    }

    public T GetValue<T>(string key, string name)
    {
        var json = FileAccess.Open("res://PrjktResonanz/assets/data/enemies.json", FileAccess.ModeFlags.Read);
        T output = default(T);

        if (json != null)
        {
            _enemyDatas = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json.GetAsText());
            if (_enemyDatas.ContainsKey(name))
            {
                output = _enemyDatas[name].GetProperty(key).Deserialize<T>();
            }
        }
        return output;
    }
}