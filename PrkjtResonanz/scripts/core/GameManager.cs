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
		PlayerData.Instance.CurrentHealth -= damage;
		PlayerData.Instance.EmitSignal(PlayerData.SignalName.HealthChanged, 
										PlayerData.Instance.CurrentHealth);
	}
}
