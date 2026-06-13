using Godot;
using System;

public partial class GameManager : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		
	}
	
	public void ConnectPlayer(Player player) {
		player.HitReceived += OnHitReceived;
	}
	
	public void OnHitReceived(int damage){
		var playerData = GetNode<PlayerData>("/root/PlayerData");
		playerData.CurrentHealth -= damage;
		playerData.EmitSignal(PlayerData.SignalName.HealthChanged, 
										playerData.CurrentHealth);
	}
}
