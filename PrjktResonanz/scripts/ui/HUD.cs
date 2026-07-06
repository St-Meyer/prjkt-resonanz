using Godot;
using System;

public partial class HUD : Node
{
	private ProgressBar _lifeBar; 
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var playerData = GetNode<PlayerData>("/root/PlayerData");
		
		// Referenz zu ProgressBar
		_lifeBar = GetNode<ProgressBar>("ProgressBar");
		
		// ProgressBar Value setzen
		_lifeBar.Value = playerData.CurrentHealth;
	}

	public void HandleHealthChange(int newHealth) {
		_lifeBar.Value = newHealth;
	}
}
