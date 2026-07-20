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
        GD.Print(EnemyId + " Stärke: " + Strength);

        Speed = data.Speed;
        GD.Print(EnemyId + " Speed: " + Speed);

        MaxHealth = data.MaxHealth;
        GD.Print(EnemyId + " MaxHealth: " + MaxHealth);

        AttackTime = data.Attacktime;
        GD.Print(EnemyId + " AttackTime: " + AttackTime);
    }
}