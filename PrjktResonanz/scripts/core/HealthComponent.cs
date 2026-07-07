using Godot;
using System.Collections.Generic;
using System.Text.Json;

[GlobalClass]
public partial class HealthComponent : Node, IDamageable
{
	[Signal] public delegate void HealthChangedEventHandler(int newHealth);
	[Signal] public delegate void DiedEventHandler();

	[Export] public NodePath PlayerDataPath;

	private int _maxHealth;
	private PlayerData _playerData;
	private int _currentHealth;
	private bool _isDead;
	private Dictionary<string, EnemyData> _enemyDatas;

	public override void _Ready()
	{		
		_playerData = GetNode<PlayerData>("/root/PlayerData");
		string name = GetParent().Name;
		
		if (PlayerDataPath == null || PlayerDataPath.IsEmpty)
		{
			var json = FileAccess.Open("res://PrjktResonanz/assets/data/enemies.json", FileAccess.ModeFlags.Read);
			if (json != null)
			{
				_enemyDatas = JsonSerializer.Deserialize<Dictionary<string, EnemyData>>(json.GetAsText());
				if (_enemyDatas.ContainsKey(name))
				{
					_maxHealth = _enemyDatas[name].MaxHealth;
				}
			}
		}
		
		if (PlayerDataPath != null && !PlayerDataPath.IsEmpty)
		{
			_maxHealth = _playerData.MaxHealth;
			_playerData.CurrentHealth = _maxHealth;
		}
		_currentHealth = _maxHealth;
		_isDead = false;
	}

	public void TakeDamage(int damage, bool crit)
	{
		if (!_isDead)
		{
			_currentHealth -= damage;
			EmitSignal(SignalName.HealthChanged, _currentHealth);
			
			if (PlayerDataPath != null && !PlayerDataPath.IsEmpty)
			{
				_playerData.CurrentHealth = _currentHealth;
				GD.Print("_playerData Emit Signal: " + _currentHealth);
				_playerData.EmitSignal(PlayerData.SignalName.HealthChanged, _currentHealth);
				GD.Print("Signal versendet.");
			}
			
			if (_currentHealth <= 0)
			{
				_isDead = true;
				EmitSignal(SignalName.Died);
			}
		}
	}
}
