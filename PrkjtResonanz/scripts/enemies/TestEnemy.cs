using Godot;

public partial class TestEnemy : CharacterBody2D, IDamageable
{

	private Area2D _attackHitbox;
	private AnimatedSprite2D _animatedSprite2D;
	private AttackComponent _attackComponent;
	private HealthComponent _healthComponent;
	private Player _player;
	
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

		// Referenz zum AttackComponent Node
		_attackComponent = GetNode<AttackComponent>("AttackComponent");

		_attackComponent.AttackExecuted += OnAttackExecuted;
		
		// Referenz zum HealthComponent Node
		_healthComponent = GetNode<HealthComponent>("HealthComponent");
		
		// abonieren der Died Methode des HealthComponents
		_healthComponent.Died += OnDied;
	}

	public void TakeDamage(int damage)
	{
		_healthComponent.TakeDamage(damage);
	}

	public void OnAttackHitboxBodyEntered(Node2D body) {
		if (body is Player player)
		{
			_player = player;
			_attackComponent.Attack();
		}
	}

	public void OnAttackExecuted(int damage)
	{
		_player.GetHit(damage);
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
		Velocity = velocity;
		MoveAndSlide();
	}
}
