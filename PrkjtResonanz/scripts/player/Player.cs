using Godot;
using System;

public partial class Player : CharacterBody2D, IDamageable{	
	
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
	
	// Signals
	[Signal] public delegate void PlayerDamagedEventHandler(int Damage);

	private float _coyoteTimer;
	private bool _onAttack;
	private float _attackTimer;
	private Area2D _attackHitbox;
	private Sprite2D _sprite;
	private HealthComponent _healthComponent;
	private AttackComponent _attackComponent;
	private IDamageable _currentTarget;
	private Random _rnd = new Random();

	public override void _Ready(){
		
		// Referenz zu AttackHitBox
		_attackHitbox = GetNode<Area2D>("AttackHitBox");
		
		_attackHitbox.Monitoring = true;
		
		// abonieren von AttackHitBox
		_attackHitbox.BodyEntered += OnAttackHitboxBodyEntered;
		
		// Referenz zu Sprite2D
		_sprite = GetNode<Sprite2D>("Sprite2D");

		_healthComponent = GetNode<HealthComponent>("HealthComponent");
		_attackComponent = GetNode<AttackComponent>("AttackComponent");

		_attackComponent.AttackExecuted += OnAttackExecuted;
		
		GetNode<GameManager>("/root/GameManager").ConnectPlayer(this);
	}

	public void TakeDamage(int damage)
	{
		_healthComponent.TakeDamage(damage);
	}

	public void GetHit(int damage){
		EmitSignal(SignalName.PlayerDamaged, damage);
	}

	public void OnAttackExecuted(int damage)
	{
		damage = (int)Math.Ceiling(_rnd.NextDouble() * damage);
		_currentTarget.TakeDamage(damage);
		GD.Print(damage);
	}
	
	public void OnAttackHitboxBodyEntered(Node2D body) {
		if (body is IDamageable currentTarget && body != this)
		{
			_currentTarget = currentTarget;
			_attackComponent.Attack();
		}
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
		
		if (direction != 0)
		{
			if (Input.IsActionPressed("speedup") && (IsOnFloor())) {
				velocity.X = direction * Speed * 1.5f;
			}
			else{
				velocity.X = direction * Speed;
			}
			
			// Looking right
			if (direction < 0) {
				_sprite.FlipH = true;
				_attackHitbox.Position = new Vector2(-Mathf.Abs(
					_attackHitbox.Position.X), _attackHitbox.Position.Y);
			}
			// Looking left
			else {
				_sprite.FlipH = false;
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
