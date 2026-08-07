using Godot;

public partial class PauseMenu : Node
{
    private Button _btnResume;
    private Button _btnLoad;
    private Button _btnTitleMenu;
    private Control _pauseMenu;
    //private ColorRect _background;
    //private ColorRect _menubox;
    //private HBoxContainer _container;

    public override void _Ready()
    {
        //_background = GetNode<ColorRect>("ColorRect");
        //_menubox = GetNode<ColorRect>("ColorRect2");
        //_container = GetNode<HBoxContainer>("HBoxContainer");
        _pauseMenu = GetNode<Control>("PauseMenu");
        _btnResume = GetNode<Button>("HBoxContainer/btnResume");
        _btnLoad = GetNode<Button>("HBoxContainer/btnLoad");
        _btnTitleMenu = GetNode<Button>("HBoxContainer/btnTitleMenu");

        _pauseMenu.Visible = false;

        _btnResume.Pressed += HandleResumePressed;
        _btnLoad.Pressed += HandleLoadPressed;
        _btnTitleMenu.Pressed += HandleTitleMenuPressed;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("pause"))
        {
            ShowPauseMenu();
        }
    }

    private void ShowPauseMenu()
    {
        GetTree().Paused = true;
        _pauseMenu.Visible = true;
    }    

    private void HandleResumePressed()
    {
        GetTree().Paused = false;
        _pauseMenu.Visible = false;
    }

    private void HandleLoadPressed()
    {
        GetTree().Paused = false;
        var saveManager = GetNode<SaveManager>("/root/SaveManager");
        saveManager.ActiveSave = saveManager.Load(1);
        if (saveManager.ActiveSave != null)
        {
            GetTree().ChangeSceneToFile("res://PrjktResonanz/scenes/world/test_level.tscn");
        }
    }

    private void HandleTitleMenuPressed()
    {
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile("res://PrjktResonanz/scenes/ui/start_screen.tscn");
    }
}