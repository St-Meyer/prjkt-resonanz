using Godot;
using System;

public partial class StartScreen : Control
{
	private Button _btnNewGame;
	private Button _btnLoadGame;
	private Button _btnOptions;
	private Button _btnExitGame;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_btnNewGame = GetNode<Button>("HBoxContainer/btnNewGame");
		_btnLoadGame = GetNode<Button>("HBoxContainer/btnLoadGame");
		_btnOptions = GetNode<Button>("HBoxContainer/btnOptions");
		_btnExitGame = GetNode<Button>("HBoxContainer/btnExit");

		_btnNewGame.Pressed += HandleNewGamePressed;
		_btnLoadGame.Pressed += HandleLoadGamePressed;
		_btnOptions.Pressed += HandleOptionsPressed;
		_btnExitGame.Pressed += HandleExitPressed;
	}

	private void HandleExitPressed()
	{
		GetTree().Quit();
	}

	private void HandleOptionsPressed()
	{
		GD.Print("Options pressed");
	}

	private void HandleLoadGamePressed()
	{
		GetTree().ChangeSceneToFile("res://PrkjtResonanz/scenes/world/test_level.tscn");
	}

	private void HandleNewGamePressed()
	{
		GD.Print("New Game pressed");
	}
}
