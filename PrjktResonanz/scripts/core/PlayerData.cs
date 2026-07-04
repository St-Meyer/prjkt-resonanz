using Godot;
using System;

public partial class PlayerData : Node
{
	[Export] public int MaxHealth = 100;
	[Export] public int BasicStrength = 20;
	[Signal] public delegate void HealthChangedEventHandler(int newHealth);
	public int CurrentHealth;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		CurrentHealth = MaxHealth;
	}
}
