using Godot;

public partial class PauseMenu : Control
{
    private Button _btnResume;
    private Button _btnLoad;
    private Button _btnTitleMenu;

    public override void _Ready()
    {
        _btnResume = GetNode<Button>("HBoxContainer/btnResume");
        _btnLoad = GetNode<Button>("HBoxContainer/btnLoad");
        _btnTitleMenu = GetNode<Button>("HBoxContainer/btnTitleMenu");

        this.Visible = false;

        _btnResume.Pressed += HandleResumePressed;
        _btnLoad.Pressed += HandleLoadPressed;
        _btnTitleMenu.Pressed += HandleTitleMenuPressed;
    }

    public void ShowPauseMenu()
    {
        GetTree().Paused = true;
        this.Visible = true;
    }    

    private void HandleResumePressed()
    {
        GetTree().Paused = false;
        this.Visible = false;
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