using System;
using Godot;

[GlobalClass]
public partial class AttackComponent : Node, IAttacker
{
	[Signal] public delegate void AttackExecutedEventHandler(int damage, bool crit);
	[Export] public float DamageVariance = 0.2f;
	[Export] public float CritChance = 0.1f;
	[Export] public NodePath PlayerDataPath;


	private Random _rnd = new Random();
	private PlayerData _playerData;
	private EnemyDataComponent _enemyData;
	private int _strength;
	private float _attackTime;
	private float _attackTimer;
	private bool _onAttack;
	private bool _crit;

	public override void _Ready(){
		if (PlayerDataPath == null || PlayerDataPath.IsEmpty)
		{
			CallDeferred(nameof(LoadEnemyData));
		}
		if (PlayerDataPath != null && !PlayerDataPath.IsEmpty)
		{
			_playerData = GetNode<PlayerData>("/root/PlayerData");
			_strength = _playerData.BasicStrength;
			_attackTime = _playerData.AttackTime;
		}
	}

	public override void _PhysicsProcess(double delta){
		if (_onAttack)
		{
			_attackTimer -= (float)delta;
			if (_attackTimer <= 0)
			{
				_onAttack = false;
			}	
		}
	}

	public void LoadEnemyData()
	{
		_enemyData = GetNode<EnemyDataComponent>("../EnemyDataComponent");
		_strength = _enemyData.Strength;
		_attackTime = _enemyData.AttackTime;
	}
	
	public void Attack()
	{
		if (!_onAttack)
		{
			_onAttack = true;
			_attackTimer = _attackTime;
			var result = CalculateDamage();
			if (PlayerDataPath != null && !PlayerDataPath.IsEmpty)
			{
				TimeStop();
			}
			EmitSignal(SignalName.AttackExecuted, result.Item1, result.Item2);
		}
	}

	public (int, bool) CalculateDamage()
	{
		_crit = false;
		double u1 = 1.0 - _rnd.NextDouble();
		double u2 = 1.0 - _rnd.NextDouble();
		// Box-Muller-Transform
		double normalRandom = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
		int finalDamage = Math.Max(1, (int)Math.Round(_strength + normalRandom * _strength * DamageVariance));
		if (_rnd.NextDouble() <= CritChance)
		{
			_crit = true;
			float maxBasicDamage = _strength + _strength * DamageVariance;
			double multiplicator = _rnd.NextDouble() * (2 - 1) + 1;
			finalDamage = (int)Math.Round(maxBasicDamage * multiplicator);
		}

		return (finalDamage, _crit);
	}

	public async void TimeStop()
	{
		GetTree().Paused = true;
		await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
		GetTree().Paused = false;
	}
}
