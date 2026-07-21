using Godot;

[GlobalClass]
public partial class EnemyDataComponent : Node
{
    [Export] public string EnemyId;
    public int Strength;
    public int Speed;
    public int MaxHealth;
    public float AttackTime;
    private EnemyDatabase _enemyDatabase;
    

    public override void _Ready()
    {
        _enemyDatabase = GetNode<EnemyDatabase>("/root/EnemyDatabase");
        var data = _enemyDatabase.Get(EnemyId);
        if (data == null)
        {
            GD.Print("Wrong Enemy ID: " + EnemyId);
            return;
        }
        Strength = data.Strength;
        Speed = data.Speed;
        MaxHealth = data.MaxHealth;
        AttackTime = data.Attacktime;
    }
}