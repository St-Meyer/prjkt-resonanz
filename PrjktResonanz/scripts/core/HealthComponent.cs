using Godot;

[GlobalClass]
public partial class HealthComponent : Node, IDamageable
{
	[Signal] public delegate void HealthChangedEventHandler(int newHealth);
	[Signal] public delegate void DiedEventHandler();
	[Export] public int MaxHealth = 100;
	private int _currentHealth;
	private bool _isDead;

	public override void _Ready(){
		_currentHealth = MaxHealth;
		_isDead = false;
	}

	public void TakeDamage(int damage)
	{
		if (!_isDead)
		{
			_currentHealth -= damage;
			EmitSignal(SignalName.HealthChanged, _currentHealth);
			if (_currentHealth <= 0)
			{
				_isDead = true;
				EmitSignal(SignalName.Died);
			}
		}
	}
}
