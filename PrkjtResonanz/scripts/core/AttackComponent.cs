using System;
using Godot;

[GlobalClass]
public partial class AttackComponent : Node, IAttacker
{
	[Signal] public delegate void AttackExecutedEventHandler(int damage);
	[Export] public int Strength = 10;
	[Export] public float AttackTime = 0.3f;

	private float _attackTimer = 0f;
	private bool _onAttack = false;

	public void Attack(){
		if (_attackTimer > 0)
		{
			_onAttack = true;
			EmitSignal(SignalName.AttackExecuted, Strength);
			if (_attackTimer <= 0)
			{
				_onAttack = false;
			}
		}
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
