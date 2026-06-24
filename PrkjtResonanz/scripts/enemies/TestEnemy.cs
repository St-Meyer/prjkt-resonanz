using Godot;
using System;

public partial class TestEnemy : CharacterBody2D
{

	private bool _onAttack = false;

	private Area2D _attackHitbox;
	private AnimatedSprite2D _animatedSprite2D;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		// Animation Logic
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animatedSprite2D.AnimationFinished += OnAnimationFinished;
		_animatedSprite2D.Play("idle");
		
		// Referenz zu AttackHitBox
		_attackHitbox = GetNode<Area2D>("AttackHitbox");
		_attackHitbox.Monitoring = true;
		
		// abonieren von AttackHitBox
		_attackHitbox.BodyEntered += OnAttackHitboxBodyEntered;
	}

	public void OnAttackHitboxBodyEntered(Node2D body) {
		if (body is Player player) {
			player.GetHit(Strength);
		}
	}
	
	public void OnAnimationFinished(){
		QueueFree();
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
