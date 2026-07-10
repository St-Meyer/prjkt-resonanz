using Godot;

[GlobalClass]
public partial class HealthComponent : Node, IDamageable
{
	[Signal] public delegate void HealthChangedEventHandler(int newHealth);
	[Signal] public delegate void DiedEventHandler();

	[Export] public NodePath PlayerDataPath;
	
	private PlayerData _playerData;
	private EnemyDataComponent _enemyData;
	private int _maxHealth;
	private int _currentHealth;
	public bool IsDead;


	public void LoadEnemyData()
	{
		_enemyData = GetNode<EnemyDataComponent>("../EnemyDataComponent");
		_maxHealth = _enemyData.MaxHealth;
		_currentHealth = _maxHealth;
		GD.Print(_maxHealth);
	}
	
	public override void _Ready()
	{		
		
		if (PlayerDataPath == null || PlayerDataPath.IsEmpty)
		{
			CallDeferred(nameof(LoadEnemyData));
			GD.Print("Enemy Health in HealthComponent: " + _maxHealth);
		}
		
		if (PlayerDataPath != null && !PlayerDataPath.IsEmpty)
		{
			_playerData = GetNode<PlayerData>("/root/PlayerData");
			_maxHealth = _playerData.MaxHealth;
			_playerData.CurrentHealth = _maxHealth;
			_currentHealth = _maxHealth;
		}

		IsDead = false;
	}

	public void TakeDamage(int damage, bool crit)
	{
		if (!IsDead)
		{
			_currentHealth -= damage;
			EmitSignal(SignalName.HealthChanged, _currentHealth);
			
			if (PlayerDataPath != null && !PlayerDataPath.IsEmpty)
			{
				_playerData.CurrentHealth = _currentHealth;
				_playerData.EmitSignal(PlayerData.SignalName.HealthChanged, _currentHealth);
			}
			
			if (_currentHealth <= 0)
			{
				IsDead = true;
				EmitSignal(SignalName.Died);
			}
		}
	}


}
