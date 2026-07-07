using Godot;

public partial class HUD : Node
{
	private ProgressBar _lifeBar; 
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var playerData = GetNode<PlayerData>("/root/PlayerData");
		GD.Print("playerData geladen: " + playerData.Name);
		_lifeBar = GetNode<ProgressBar>("ProgressBar");
		GD.Print("_lifeBar geladen: " + _lifeBar.Name);


		_lifeBar.Value = playerData.CurrentHealth;
		GD.Print("_lifeBar.Value in _Ready = " + _lifeBar.Value);
		playerData.HealthChanged += HandleHealthChange;
		GD.Print("Playerdata.HealthChanged += HandleHealthChange");
	}

	public void HandleHealthChange(int newHealth) {
		_lifeBar.Value = newHealth;
		GD.Print("_lifeBar.Value = " + newHealth);
	}
	
	public override void _ExitTree()
	{
		var playerData = GetNode<PlayerData>("/root/PlayerData");
		playerData.HealthChanged -= HandleHealthChange;
	}
}
