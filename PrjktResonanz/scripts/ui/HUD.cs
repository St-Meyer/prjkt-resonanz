using Godot;

public partial class HUD : Node
{
	private ProgressBar _lifeBar; 
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var playerData = GetNode<PlayerData>("/root/PlayerData");
		_lifeBar = GetNode<ProgressBar>("ProgressBar");

		// LifeBar Wert wird beim Start auf aktuellen HealthWert aus PlayerData gesetzt
		_lifeBar.Value = playerData.CurrentHealth;
		playerData.HealthChanged += HandleHealthChange;
	}

	// LifeBar Wert wird auf aktuellen Health Wert gesetzt
	public void HandleHealthChange(int newHealth) {
		_lifeBar.Value = newHealth;
	}
	
	// HandleHealthChanged wird beim verlassen der Szene deaboniert
	// Da playerData durch Root auch abseits der Szene eixisitert.
	// Tote Verbinsungen werden bei jeden Reload akkumuliert.
	public override void _ExitTree()
	{
		var playerData = GetNode<PlayerData>("/root/PlayerData");
		playerData.HealthChanged -= HandleHealthChange;
	}
}
