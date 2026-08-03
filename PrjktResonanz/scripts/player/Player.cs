using Godot;

public partial class Player : CharacterBody2D, IDamageable, ITargetable{	
	
	// Movement Parameter
	[Export]
	public float JumpVelocity = -400.0f;

	// Physics Parameter
	[Export]
	public float CoyoteTime = 0.1f;

	private int _speed;
	private float _attackTime;
	private float _coyoteTimer;
	private bool _onAttack;
	private float _attackTimer;
	private bool _isDead;
	private bool _isInCutscene;
	private Area2D _attackHitbox;
	private AnimatedSprite2D _animatedSprite2D;
	private AttackComponent _attackComponent;
	private PlayerData _playerData;
	public HealthComponent HealthComponent;
	private IDamageable _currentTarget;

	public override void _Ready()
	{
		_attackHitbox = GetNode<Area2D>("AttackHitBox");
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		HealthComponent = GetNode<HealthComponent>("HealthComponent");
		_attackComponent = GetNode<AttackComponent>("AttackComponent");
		_playerData = GetNode<PlayerData>("/root/PlayerData");

		_speed = _playerData.Speed;
		_attackTime = _playerData.AttackTime;
		
		_isDead = false;
		_attackHitbox.Monitoring = false;

		_attackHitbox.BodyEntered += OnAttackHitboxBodyEntered;
		_animatedSprite2D.AnimationFinished += OnAnimationFinished;
		_attackComponent.AttackExecuted += OnAttackExecuted;
		HealthComponent.Died += OnDied;
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
			await ToSignal(GetTree().CreateTimer(1.0), "timeout");
			GetTree().ChangeSceneToFile("res://PrjktResonanz/scenes/ui/game_over.tscn");
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
		if (Input.IsActionJustPressed("attack") && !_onAttack && !_isDead) {
			_onAttack = true;
			_attackHitbox.Monitoring = true;
			_attackTimer = _attackTime;
		}
		if (_attackTimer > 0) {
			_attackTimer -= (float)delta;
			if (_attackTimer <= 0) {
				_onAttack = false;
				_attackHitbox.Monitoring = false;
			}
		}
		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && (IsOnFloor() || _coyoteTimer > 0) && !_isDead)
		{
			_coyoteTimer = 0;
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		float direction = Input.GetAxis("move_left", "move_right");
		
		if (direction != 0 && !_isInCutscene && !_isDead)
		{
			if (Input.IsActionPressed("speedup") && IsOnFloor() && !_isDead) {
				velocity.X = direction * _speed * 1.5f;
			}
			else{
				velocity.X = direction * _speed;
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
			velocity.X = Mathf.MoveToward(Velocity.X, 0, _speed);
		}
		
		Velocity = velocity;
		MoveAndSlide();
	}
}
