using Godot;

public partial class Settings : Node
{
    private Button _btnGrafik;
    private Button _btnSound;
    private Button _btnControl;
    private Button _btnZurueck;
    private Button _btnGrafikBack;
    private Button _btnSoundBack;
    private Button _btnControlskBack;
    private Button _btnFullscreen;
    private Button _btnWindowed;
    private Button _btnFrameless;
    private PanelContainer _pcGrafik;
    private PanelContainer _pcSound;
    private PanelContainer _pcControl;
    ConfigFile config = new ConfigFile();

    public override void _Ready()
    {


        _btnGrafik = GetNode<Button>("VBoxContainer/btnGrafik");
        _btnSound = GetNode<Button>("VBoxContainer/btnSound");
        _btnControl = GetNode<Button>("VBoxContainer/btnControl");
        _btnZurueck = GetNode<Button>("VBoxContainer/btnZurueck");
        _btnGrafikBack = GetNode<Button>("GrafikControlPanel/VBoxContainer/btnGrafikBack");
        _btnSoundBack = GetNode<Button>("SoundControlPanel/btnSoundBack");
        _btnControlskBack = GetNode<Button>("ControlsControlPanel/btnControlsBack");
    
        _btnFullscreen = GetNode<CheckButton>("GrafikControlPanel/VBoxContainer/btnFullscreen");
        _btnWindowed = GetNode<CheckButton>("GrafikControlPanel/VBoxContainer/btnWindowed");
        _btnFrameless = GetNode<CheckButton>("GrafikControlPanel/VBoxContainer/btnFrameless");

        _pcGrafik = GetNode<PanelContainer>("GrafikControlPanel");
        _pcSound = GetNode<PanelContainer>("SoundControlPanel");
        _pcControl = GetNode<PanelContainer>("ControlsControlPanel");

        _pcGrafik.Visible = false;
        _pcSound.Visible = false;
        _pcControl.Visible = false;


        var mode = DisplayServer.WindowGetMode();


        Error err = config.Load("user://settings.cfg");

        if (err != Error.Ok)
        {
            return;
        }

        switch (mode)
        {
            case (DisplayServer.WindowMode.Fullscreen):
                _btnFullscreen.ButtonPressed = true;
                break;
            case (DisplayServer.WindowMode.Windowed):
                _btnWindowed.ButtonPressed = true;
                break;
            case(DisplayServer.WindowMode.Maximized):
                _btnFrameless.ButtonPressed = true;
                break;
        }

        _btnGrafik.Pressed += HandleGrafikPressed;
        _btnSound.Pressed += HandleSoundPressed;
        _btnControl.Pressed += HandleControlPressed;
        _btnZurueck.Pressed += HandleZurueckPressed;
        _btnFullscreen.Pressed += HandleFullscreenChecked;
        _btnWindowed.Pressed += HandleWindowedChecked;
        _btnFrameless.Pressed += HandleFramelessChecked;
        _btnGrafikBack.Pressed += HandleBackPressed;
        _btnSoundBack.Pressed += HandleBackPressed;
        _btnControlskBack.Pressed += HandleBackPressed;

    }

    private void HandleGrafikPressed()
    {
        _pcGrafik.Visible = true;
        _pcSound.Visible = false;
        _pcControl.Visible = false;
    }

    private void HandleSoundPressed()
    {
        _pcSound.Visible = true;
        _pcGrafik.Visible = false;
        _pcControl.Visible = false;
    }

    private void HandleControlPressed()
    {
        _pcControl.Visible = true;
        _pcSound.Visible = false;
        _pcGrafik.Visible = false;
    }

    private void HandleBackPressed()
    {
        _pcGrafik.Visible = false;
        _pcSound.Visible = false;
        _pcControl.Visible = false;
    }

    private void HandleFullscreenChecked()
    {
        DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
        DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, false, 0);
        saveFile("Fullscreen");
    }

    private void HandleWindowedChecked()
    {
        var index = DisplayServer.WindowGetCurrentScreen();
        var size = new Vector2I(640, 360);
        DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
        DisplayServer.WindowSetSize(size);
        DisplayServer.WindowSetPosition(DisplayServer.ScreenGetPosition(index) +
                                (DisplayServer.ScreenGetSize(index) -
                                 size) / 2);
        DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, false, 0);
        saveFile("Windowed");

    }

    private void HandleFramelessChecked()
    {
        DisplayServer.WindowSetSize(DisplayServer.ScreenGetSize());
        DisplayServer.WindowSetMode(DisplayServer.WindowMode.Maximized);
        DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, true, 0);
        saveFile("Maximized");

    }

    private void HandleZurueckPressed()
    {
        GetTree().ChangeSceneToFile("res://PrjktResonanz/scenes/ui/start_screen.tscn");
    }

    private void saveFile(string mode)
    {
        config.SetValue("Settings", "window_mode", mode);
    }
}