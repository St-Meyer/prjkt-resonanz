using Godot;
using System;

public partial class Lyra : CharacterBody2D
{	[Export]
	public float Speed = 300.0f;
	[Export]
	public float JumpVelocity = -400.0f;
	[Export]
	public float CoyoteTime = 0.1f;
	[Export]
	public float AttackTime = 0.3f;
	private float _coyoteTimer = 0f;
	private bool _onAttack = false;
	private float _attackTimer = 0f;
	private Area2D _attackHitbox;
	
	public override void _Ready(){
		_attackHitbox = GetNode<Area2D>("AttackHitBox");
		_attackHitbox.Monitoring = false;
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		// Coyote Timer
		if (IsOnFloor()){
			_coyoteTimer = CoyoteTime;
		}
		if (!IsOnFloor())
		{
			if (_coyoteTimer > 0) {
				_coyoteTimer -= (float)delta;
			}
			// Add the gravity.
			velocity += GetGravity() * (float)delta;
		}
		if (Input.IsActionJustPressed("attack") && !_onAttack) {
			_onAttack = true;
			_attackHitbox.Monitoring = true;
			GD.Print("Attack Start");
			_attackTimer = AttackTime;
		}
		if (_attackTimer > 0) {
			_attackTimer -= (float)delta;
			if (_attackTimer <= 0) {
				_onAttack = false;
				_attackHitbox.Monitoring = false;
				GD.Print("Attack End");
			}
		}
		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && (IsOnFloor() || _coyoteTimer > 0))
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		float direction = Input.GetAxis("move_left", "move_right");
		if (direction != 0)
		{
			velocity.X = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		Velocity = velocity;
		MoveAndSlide();
	}
}
