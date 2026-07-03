using Godot;

[GlobalClass]
public partial class HealthComponent : Node, IDamageable
{
	[Signal] public delegate void HealthChangedEventHandler(int newHealth);
	[Signal] public delegate void DiedEventHandler();
	[Export] public int MaxHealth = 100;
	private int _currentHealth;

	public override void _Ready(){
		_currentHealth = MaxHealth;
	}

	public void TakeDamage(int damage)
	{
		_currentHealth -= damage;
		EmitSignal(SignalName.HealthChanged, _currentHealth);
		if (_currentHealth <= 0){
			EmitSignal(SignalName.Died);
		}
	}
}
