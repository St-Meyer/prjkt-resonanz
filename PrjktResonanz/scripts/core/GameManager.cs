using Godot;
using System;

public partial class GameManager : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
	}
	
	public void ConnectPlayer(Player player) {

		// abonieren des HandlePlayerDamage Signals. 
		// Reagiert nur, wenn GetHit ein Signal sendet.
		player.HealthComponent.HealthChanged += HandleHealthChanged;
	}
	
	public void HandleHealthChanged(int newHealth){

		var playerData = GetNode<PlayerData>("/root/PlayerData");
		playerData.CurrentHealth = newHealth;
		playerData.EmitSignal(PlayerData.SignalName.HealthChanged, 
										playerData.CurrentHealth);
	}
}
