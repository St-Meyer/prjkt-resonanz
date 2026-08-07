using Godot;
using System;

public partial class TestLevel : Node2D
{
	private PauseMenu _pauseMenu;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_pauseMenu = GetNode<PauseMenu>("UI/PauseMenu");
		var saveManager = GetNode<SaveManager>("/root/SaveManager");
		var player = GetNode<Player>("Lyra");
		var playerData = GetNode<PlayerData>("/root/PlayerData");
		var savepoints = GetTree().GetNodesInGroup("savepoints");
		
		if (saveManager.ActiveSave != null)
		{
			foreach (var savepoint in savepoints)
			{
				if (savepoint is Savepoint sp && sp.SavepointId == saveManager.ActiveSave.SavePointID)
				{
					player.GlobalPosition = sp.GlobalPosition;
					playerData.CurrentHealth = saveManager.ActiveSave.CurrentHealth;
					playerData.EmitSignal(PlayerData.SignalName.HealthChanged, playerData.CurrentHealth);
				}
			}
		}

		if (saveManager.ActiveSave == null)
		{
			GetNode<DialogueManager>("/root/DialogueManager")
			.StartDialogue("res://PrjktResonanz/assets/dialogues/intro.json");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pause"))
		{
			_pauseMenu.ShowPauseMenu();
		}
	}
}
