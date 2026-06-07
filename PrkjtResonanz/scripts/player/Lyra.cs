using Godot;
using System;

public partial class Lyra : CharacterBody2D
{	[Export]
	public float Speed = 300.0f;
	[Export]
	public float JumpVelocity = -400.0f;
	[Export]
	public float CoyoteTime = 0.1f;
	private float _coyoteTimer = 0f;

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
