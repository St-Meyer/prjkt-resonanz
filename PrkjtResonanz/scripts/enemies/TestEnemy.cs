using Godot;
using System;

public partial class TestEnemy : CharacterBody2D
{
	[Export]
	public int health = 100;
	private int _damagedHealth;
	private AnimatedSprite2D _animatedSprite2D;
	
	public void OnAnimationFinished(){
		QueueFree();
	}
	
	public void TakeDamage(int damage){
		if (_damagedHealth > 0) {
			_damagedHealth -= damage;
			GD.Print(_damagedHealth);
			if (_damagedHealth <= 0) {
				_animatedSprite2D.Play("death");
			}
		}
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_damagedHealth = health;
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animatedSprite2D.AnimationFinished += OnAnimationFinished;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		if (!IsOnFloor())
		{
			// Add the gravity.
			velocity += GetGravity() * (float)delta;
		}
		Velocity = velocity;
		MoveAndSlide();
	}
}
