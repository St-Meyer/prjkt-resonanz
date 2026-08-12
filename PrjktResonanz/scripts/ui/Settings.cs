using Godot;

public partial class Settings : Node
{
    private Button btnGrafik;
    private Button btnSound;
    private Button btnControl;
    private Button btnZurueck;
    private Button btnGrafikBack;
    private Button btnSoundBack;
    private Button btnControlskBack;
    private PanelContainer pcGrafik;
    private PanelContainer pcSound;
    private PanelContainer pcControl;

    public override void _Ready()
    {
        btnGrafik = GetNode<Button>("VBoxContainer/btnGrafik");
        btnSound = GetNode<Button>("VBoxContainer/btnSound");
        btnControl = GetNode<Button>("VBoxContainer/btnControl");
        btnZurueck = GetNode<Button>("VBoxContainer/btnZurueck");
        btnGrafikBack = GetNode<Button>("GrafikControlPanel/btnGrafikBack");
        btnSoundBack = GetNode<Button>("SoundControlPanel/btnSoundBack");
        btnControlskBack = GetNode<Button>("ControlsControlPanel/btnControlsBack");

        pcGrafik = GetNode<PanelContainer>("GrafikControlPanel");
        pcSound = GetNode<PanelContainer>("SoundControlPanel");
        pcControl = GetNode<PanelContainer>("ControlsControlPanel");

        pcGrafik.Visible = false;
        pcSound.Visible = false;
        pcControl.Visible = false;

        btnGrafik.Pressed += HandleGrafikPressed;
        btnSound.Pressed += HandleSoundPressed;
        btnControl.Pressed += HandleControlPressed;
        btnZurueck.Pressed += HandleZurueckPressed;
        btnGrafikBack.Pressed += HandleBackPressed;
        btnSoundBack.Pressed += HandleBackPressed;
        btnControlskBack.Pressed += HandleBackPressed;
    }

    private void HandleGrafikPressed()
    {
        GD.Print("Grafik pressed");
        pcGrafik.Visible = true;
        pcSound.Visible = false;
        pcControl.Visible = false;
    }

    private void HandleSoundPressed()
    {
        GD.Print("Sound pressed");
        pcSound.Visible = true;
        pcGrafik.Visible = false;
        pcControl.Visible = false;
    }

    private void HandleControlPressed()
    {
        GD.Print("Tastenbelegung pressed");
        pcControl.Visible = true;
        pcSound.Visible = false;
        pcGrafik.Visible = false;
    }

    private void HandleBackPressed()
    {
        pcGrafik.Visible = false;
        pcSound.Visible = false;
        pcControl.Visible = false;
    }

    private void HandleZurueckPressed()
    {
        GetTree().ChangeSceneToFile("res://PrjktResonanz/scenes/ui/start_screen.tscn");
    }
}