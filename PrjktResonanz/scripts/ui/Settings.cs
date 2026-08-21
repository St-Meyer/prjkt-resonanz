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

        // Panel Container werden beim Laden der Szene ausgeblendet
        _pcGrafik.Visible = false;
        _pcSound.Visible = false;
        _pcControl.Visible = false;

        // Daten werden aus cfg-File geladen
        Error err = config.Load("user://settings.cfg");

        // Wenn das File nicht geladen wird, ignorieren.
        if (err != Error.Ok)
        {
            return;
        }

        // Momentaner Anzeigemodus wird in Variable gespeichert
        var mode = DisplayServer.WindowGetMode();

        // Button für jeweilige momentane Anzeigeeinstellung wird beim Start aktiviert 
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

    // Control Panel für Grafikeinstellungen wird angezeigt
    private void HandleGrafikPressed()
    {
        _pcGrafik.Visible = true;
        _pcSound.Visible = false;
        _pcControl.Visible = false;
    }

    // Control Panel für Soundeinstellungen wird angezeigt
    private void HandleSoundPressed()
    {
        _pcSound.Visible = true;
        _pcGrafik.Visible = false;
        _pcControl.Visible = false;
    }

    // Control Panel für Steuerungseinstellungen wird angezeigt
    private void HandleControlPressed()
    {
        _pcControl.Visible = true;
        _pcSound.Visible = false;
        _pcGrafik.Visible = false;
    }

    // Alle Control Panels werden ausgeblendet
    private void HandleBackPressed()
    {
        _pcGrafik.Visible = false;
        _pcSound.Visible = false;
        _pcControl.Visible = false;
    }
    // Fenster wird auf Fullscreen gestellt, Randlos und saveFile Methode wird aufgerufen mit
    // Fullscreen-String Parameter
    private void HandleFullscreenChecked()
    {
        DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
        DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, true, 0);
        saveFile("Fullscreen");
    }

    // Aktueller Monitor und Fenstergröße werden in Variablen gespeichert
    // Anzeigemodus wird auf Fenstermodus gestellt
    // Position des Fensters wird mittig des Monitors platziert
    // Fenster mit Rand
    // Einstellung wird via saveFile-Methode gespeichert
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

    // Fenster wird randlos maximiert.
    private void HandleFramelessChecked()
    {
        DisplayServer.WindowSetSize(DisplayServer.ScreenGetSize());
        DisplayServer.WindowSetMode(DisplayServer.WindowMode.Maximized);
        DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, true, 0);
        saveFile("Maximized");

    }

    // Zurück auf Title Menu
    private void HandleZurueckPressed()
    {
        GetTree().ChangeSceneToFile("res://PrjktResonanz/scenes/ui/start_screen.tscn");
    }

    // Einstellung für Window Mode wird gesetzt und gespeichert.
    private void saveFile(string mode)
    {
        config.SetValue("Settings", "window_mode", mode);
        config.Save("user://settings.cfg");
    }
}