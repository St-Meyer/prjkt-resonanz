using Godot;

public partial class PlayerData : Node
{
	[Export] public int MaxHealth = 100;
	[Export] public int BasicStrength = 10;
	[Export] public float AttackTime = 0.3f;
	[Export] public int Speed = 200;
	[Signal] public delegate void HealthChangedEventHandler(int newHealth);
	public int CurrentHealth;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		CurrentHealth = MaxHealth;
	}
}
