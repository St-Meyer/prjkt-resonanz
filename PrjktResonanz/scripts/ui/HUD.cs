using Godot;

public partial class HUD : Node
{
	private ProgressBar _lifeBar; 
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var playerData = GetNode<PlayerData>("/root/PlayerData");
		_lifeBar = GetNode<ProgressBar>("ProgressBar");

		_lifeBar.Value = playerData.CurrentHealth;
		playerData.HealthChanged += HandleHealthChange;
	}

	public void HandleHealthChange(int newHealth) {
		_lifeBar.Value = newHealth;
	}
	
	public override void _ExitTree()
	{
		var playerData = GetNode<PlayerData>("/root/PlayerData");
		playerData.HealthChanged -= HandleHealthChange;
	}
}
