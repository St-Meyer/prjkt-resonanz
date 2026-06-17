using Godot;
using System;

public partial class GameManager : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		
	}
	
	public void ConnectPlayer(Player player) {
		
		// abonieten des OnHitReceived Signals. 
		// Reagiert nur, wenn OnHitReceived ein Signal sendet.
		player.HitReceived += OnHitReceived;
	}
	
	public void OnHitReceived(int Damage){
		var playerData = GetNode<PlayerData>("/root/PlayerData");
		playerData.CurrentHealth -= Damage;
		playerData.EmitSignal(PlayerData.SignalName.HealthChanged, 
										playerData.CurrentHealth);
	}
}
