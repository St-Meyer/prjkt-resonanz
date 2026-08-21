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

        // Szene wird beim Start unsichtbar gestellt
        this.Visible = false;

        _btnResume.Pressed += HandleResumePressed;
        _btnLoad.Pressed += HandleLoadPressed;
        _btnTitleMenu.Pressed += HandleTitleMenuPressed;

        // Szene funktionabel, auch wenn Game pausiert ist
        ProcessMode = ProcessModeEnum.Always;
    }

    // Game wird pausiert und Szene wird sichtbar gestellt
    public void ShowPauseMenu()
    {
        GetTree().Paused = true;
        this.Visible = true;
    }    

    // Game wird fortgeführt und Szene wird unsichtbar gestellt
    private void HandleResumePressed()
    {
        GetTree().Paused = false;
        this.Visible = false;
    }

    // Pause wird beendet und wenn Save File besteht, wird Test-Level geladen
    // TODO: Auswahl aus 3 Savefiles
    // TODO: Wechsel auf Szene mit aktuellen Savepoint
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

    // Pause wird beendet und wechsel auf Title Menu
    private void HandleTitleMenuPressed()
    {
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile("res://PrjktResonanz/scenes/ui/start_screen.tscn");
    }
}