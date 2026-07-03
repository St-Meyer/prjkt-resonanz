using Godot;
using System;

public partial class GameOver : Control
{
	private Button _loadButton;
	private Button _mainMenuButton;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_loadButton = GetNode<Button>("VBoxContainer/HBoxContainer/Load");
		_mainMenuButton = GetNode<Button>("VBoxContainer/HBoxContainer/MainMenu");
		
		_loadButton.Pressed += HandleLoadButtonPressed;
		_mainMenuButton.Pressed += HandleMainMenuButtonPressed;

	}

	public void HandleLoadButtonPressed()
	{
		GD.Print("Load Pressed");
	}

	public void HandleMainMenuButtonPressed()
	{
		GD.Print("Main Menu Pressed");
	}
}
