using Godot;
using System;

public partial class TestEnemy : CharacterBody2D
{
	[Export]
	public int MaxHealth = 100;
	[Export]
	public float AttackTime = 0.3f;
	[Export] int Strength = 20;
	
	private bool _onAttack = false;
	private float _attackTimer = 0f;
	private int _currentHealth;
	private Area2D _attackHitbox;
	private AnimatedSprite2D _animatedSprite2D;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		GD.Print("READY CALLED");
		_currentHealth = MaxHealth;
		GD.Print("HEALTH GESETZT");
	
		// Animation Logic
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		GD.Print("ANIMATED SPRITE GEFUNDEN");
		_animatedSprite2D.AnimationFinished += OnAnimationFinished;
		_animatedSprite2D.Play("idle");
		GD.Print("ANIMATION GESTARTET");
		
		// Referenz zu AttackHitBox
		_attackHitbox = GetNode<Area2D>("AttackHitbox");
		GD.Print("TestEnemy geladen. HitboxMonitoring: " + _attackHitbox.Monitoring);
		_attackHitbox.Monitoring = true;
		
		// abonieren von AttackHitBox
		_attackHitbox.BodyEntered += OnAttackHitboxBodyEntered;
		GD.Print("ALLES BEREIT");
	}

	public void OnAttackHitboxBodyEntered(Node2D body) {
		GD.Print("Hitbox getroffen von: " + body.Name);
		if (body is Player player) {
			player.GetHit(Strength);
		}
	}
	
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
