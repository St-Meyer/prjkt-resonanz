using Godot;
using System;

public partial class TestEnemy : CharacterBody2D
{
	[Export]
	public int health = 100;
	private int _currentHealth;
	private AnimatedSprite2D _animatedSprite2D;
	
	public void OnAnimationFinished(){
		QueueFree();
	}
	
	public void TakeDamage(int damage){
		if (_currentHealth > 0) {
			_currentHealth -= damage;
			GD.Print(_currentHealth);
			if (_currentHealth <= 0) {
				_animatedSprite2D.Play("death");
			}
		}
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_currentHealth = health;
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animatedSprite2D.AnimationFinished += OnAnimationFinished;
		_animatedSprite2D.Play("idle");
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
