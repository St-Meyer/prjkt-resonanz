using Godot;

public partial class GameOver : Control
{
	private Button _loadButton;
	private Button _mainMenuButton;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Spielpause wird aufgehoben
		GetTree().Paused = false;

		_loadButton = GetNode<Button>("VBoxContainer/HBoxContainer/Load");
		_mainMenuButton = GetNode<Button>("VBoxContainer/HBoxContainer/MainMenu");
		
		_loadButton.Pressed += HandleLoadButtonPressed;
		_mainMenuButton.Pressed += HandleMainMenuButtonPressed;
	}


	// Wenn aktiver Save besteht wird auf Test Level gewechselt
	// TODO: Auswahl aus mehreren SaveFiles
	public void HandleLoadButtonPressed()
	{
		var saveManager = GetNode<SaveManager>("/root/SaveManager"); 
		saveManager.ActiveSave = saveManager.Load(1);
		if (saveManager.ActiveSave != null )
		{
			GetTree().ChangeSceneToFile("res://PrjktResonanz/scenes/world/test_level.tscn");
		}
	}

	// Wechsel auf Title Menu
	public void HandleMainMenuButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://PrjktResonanz/scenes/ui/start_screen.tscn");
	}
}
