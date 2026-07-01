using Godot;

public partial class TestEnemy : CharacterBody2D, IDamageable
{

	private Area2D _attackHitbox;
	private Area2D _detectionArea;
	private AnimatedSprite2D _animatedSprite2D;
	private AttackComponent _attackComponent;
	private HealthComponent _healthComponent;
	private DetectionComponent _detectionComponent;
	private Player _player;
	private Node2D _currentTarget;
	private bool _isChasing;
	private bool _isInAttackRange;
	[Export] public float Speed = 150.0f;

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
		_attackHitbox.BodyExited += OnAttackHitboxBodyExited;

		// Referenz zum AttackComponent Node
		_attackComponent = GetNode<AttackComponent>("AttackComponent");

		_attackComponent.AttackExecuted += OnAttackExecuted;
		
		// Referenz zum HealthComponent Node
		_healthComponent = GetNode<HealthComponent>("HealthComponent");
		
		// abonieren der Died Methode des HealthComponents
		_healthComponent.Died += OnDied;

		_detectionComponent = GetNode<DetectionComponent>("DetectionComponent");
		_detectionComponent.TargetDetected += OnDetectionAreaEntered;
		_detectionComponent.TargetLost += OnDetectionAreaExited;
	}

	public void TakeDamage(int damage)
	{
		_healthComponent.TakeDamage(damage);
	}

	public void OnDetectionAreaEntered(Node2D body)
	{
		_currentTarget = body;
		_isChasing = true;
		
	}

	public void OnDetectionAreaExited()
	{
		GD.Print("Player lost");
		_currentTarget = null;
		GD.Print("_currentTarget = null");
		_isChasing = false;
		GD.Print("_isChasing = false");
	}

	public void OnAttackHitboxBodyEntered(Node2D body) {
		if (body is Player player)
		{
			_player = player;
			_isInAttackRange = true;
		}
	}

	public void OnAttackHitboxBodyExited(Node2D body)
	{
		_player = null;
		_isInAttackRange = false;
	}

	public void OnAttackExecuted(int damage)
	{
		if (_player != null)
		{
			_player.TakeDamage(damage);
		}
	}

	public void OnDied()
	{
		_animatedSprite2D.Play("death");
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

		if (_isChasing)
		{
			Vector2 direction = (_currentTarget.GlobalPosition - this.GlobalPosition).Normalized();
			velocity.X = direction.X * Speed;
			
			// Looking right
			if (direction.X < 0)
			{
				_animatedSprite2D.FlipH = false;
				_attackHitbox.Position = new Vector2(-Mathf.Abs(
					_attackHitbox.Position.X), _attackHitbox.Position.Y);
			}
			// Looking left
			else
			{
				_animatedSprite2D.FlipH = true;
				_attackHitbox.Position = new Vector2(Mathf.Abs(
					_attackHitbox.Position.X), _attackHitbox.Position.Y);
			}
		}
		else
		{
			velocity.X = 0;
		}

		if (_isInAttackRange)
		{
			_attackComponent.Attack();
		}
		Velocity = velocity;
		MoveAndSlide();
	}
}
