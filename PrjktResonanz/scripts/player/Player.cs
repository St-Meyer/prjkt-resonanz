using Godot;
using System;

public partial class Player : CharacterBody2D, IDamageable, ITargetable{	
	
	// Movement Parameter
	[Export]
	public float Speed = 300.0f;
	[Export]
	public float JumpVelocity = -400.0f;
	[Export]
	public float AttackTime = 0.3f;

	// Physics Parameter
	[Export]
	public float CoyoteTime = 0.1f;
	
	private float _coyoteTimer;
	private bool _onAttack;
	private float _attackTimer;
	private bool _isDead;
	private bool _isInCutscene;
	private Area2D _attackHitbox;
	private AnimatedSprite2D _animatedSprite2D;
	private AttackComponent _attackComponent;
	public HealthComponent HealthComponent;
	private IDamageable _currentTarget;

	public override void _Ready()
	{
		_isDead = false;
		// Referenz zu AttackHitBox
		_attackHitbox = GetNode<Area2D>("AttackHitBox");
		
		_attackHitbox.Monitoring = false;
		
		// abonieren von AttackHitBox
		_attackHitbox.BodyEntered += OnAttackHitboxBodyEntered;
		
		// Referenz zu Sprite2D
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animatedSprite2D.AnimationFinished += OnAnimationFinished;


		HealthComponent = GetNode<HealthComponent>("HealthComponent");
		_attackComponent = GetNode<AttackComponent>("AttackComponent");

		_attackComponent.AttackExecuted += OnAttackExecuted;

		HealthComponent.Died += OnDied;
		
		GetNode<GameManager>("/root/GameManager").ConnectPlayer(this);
		GetNode<DialogueManager>("/root/DialogueManager").DialogueStartet += OnDialogueStarted;
		GetNode<DialogueManager>("/root/DialogueManager").DialogueEnded += OnDialogueEnded;
	}

	public void TakeDamage(int damage, bool crit)
	{
		HealthComponent.TakeDamage(damage, crit);
	}

	public void OnAttackExecuted(int damage, bool crit)
	{
		_currentTarget.TakeDamage(damage, crit);
	}
	
	public void OnAttackHitboxBodyEntered(Node2D body) {
		if (body is IDamageable currentTarget && body != this)
		{
			_currentTarget = currentTarget;
			_attackComponent.Attack();
		}
	}

	public void OnDied()
	{
		_animatedSprite2D.Play("death");
		_isDead = true;
	}

	public async void OnAnimationFinished()
	{
		if(_isDead)
		{
			GD.Print("Dead Scene loading...");
			await ToSignal(GetTree().CreateTimer(1.0), "timeout");
			GetTree().ChangeSceneToFile("res://PrjktResonanz/scenes/ui/game_over.tscn");
		}

	}

	public void OnDialogueStarted()
	{
		GetTree().Paused = true;
	}

	public void OnDialogueEnded()
	{
		GetTree().Paused = false;
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
		
		// Attack Logic
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
			_coyoteTimer = 0;
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		float direction = Input.GetAxis("move_left", "move_right");
		
		if (direction != 0 && !_isInCutscene)
		{
			if (Input.IsActionPressed("speedup") && IsOnFloor()) {
				velocity.X = direction * Speed * 1.5f;
			}
			else{
				velocity.X = direction * Speed;
			}
			
			// Looking right
			if (direction < 0) {
				_animatedSprite2D.FlipH = false;
				_attackHitbox.Position = new Vector2(-Mathf.Abs(
					_attackHitbox.Position.X), _attackHitbox.Position.Y);
			}
			// Looking left
			else {
				_animatedSprite2D.FlipH = true;
				_attackHitbox.Position = new Vector2(Mathf.Abs(
					_attackHitbox.Position.X), _attackHitbox.Position.Y);
			} 
			
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		
		Velocity = velocity;
		MoveAndSlide();
	}
}
