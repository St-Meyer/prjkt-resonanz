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
		var saveManager = GetNode<SaveManager>("/root/SaveManager");
		_btnNewGame = GetNode<Button>("HBoxContainer/btnNewGame");
		_btnLoadGame = GetNode<Button>("HBoxContainer/btnLoadGame");
		_btnOptions = GetNode<Button>("HBoxContainer/btnOptions");
		_btnExitGame = GetNode<Button>("HBoxContainer/btnExit");

		_btnNewGame.Pressed += HandleNewGamePressed;
		_btnLoadGame.Pressed += HandleLoadGamePressed;
		_btnOptions.Pressed += HandleOptionsPressed;
		_btnExitGame.Pressed += HandleExitPressed;

		_btnLoadGame.Disabled = !saveManager.HasSave(1);
	}

	private void HandleExitPressed()
	{
		GetTree().Quit();
	}

	private void HandleOptionsPressed()
	{
		// TODO: Options Scene
		GD.Print("Options pressed");
	}

	private void HandleLoadGamePressed()
	{
		var saveManager = GetNode<SaveManager>("/root/SaveManager");
		saveManager.ActiveSave = saveManager.Load(1);
		if (saveManager.ActiveSave != null)
		{
			GetTree().ChangeSceneToFile("res://PrjktResonanz/scenes/world/test_level.tscn");
		}
	}

	private void HandleNewGamePressed()
	{
		GetTree().ChangeSceneToFile("res://PrjktResonanz/scenes/world/test_level.tscn");
	}
}
