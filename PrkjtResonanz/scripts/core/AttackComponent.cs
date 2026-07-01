using System;
using Godot;

[GlobalClass]
public partial class AttackComponent : Node, IAttacker
{
	[Signal] public delegate void AttackExecutedEventHandler(int damage);
	[Export] public int Strength = 10;
	[Export] public float AttackTime = 0.3f;
	[Export] public float DamageVariance = 0.2f;
	[Export] public float CritChance = 0.1f;

	private Random _rnd = new Random();
	private float _attackTimer;
	private bool _onAttack;

	public void Attack(){
		if (!_onAttack)
		{
			_onAttack = true;
			_attackTimer = AttackTime;
			EmitSignal(SignalName.AttackExecuted, Strength);
		}
	}

	public int CalculateDamage()
	{
		double u1 = 1.0 - _rnd.NextDouble();
		double u2 = 1.0 - _rnd.NextDouble();
        // Box-Muller-Transform
		double normalRandom = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
		int finalDamage = Math.Max(1, (int)Math.Round(Strength + normalRandom * Strength * DamageVariance));
		if (_rnd.NextDouble() <= CritChance)
		{
			float maxBasicDamage = Strength + Strength * DamageVariance;
            double multiplicator = _rnd.NextDouble() * (2-1) + 1;
			finalDamage = (int)Math.Round(maxBasicDamage * multiplicator);
		}
		return finalDamage;
	}

	public override void _Ready(){
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
}
