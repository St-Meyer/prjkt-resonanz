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
    private PanelContainer _pcGrafik;
    private PanelContainer _pcSound;
    private PanelContainer _pcControl;

    public override void _Ready()
    {
        _btnGrafik = GetNode<Button>("VBoxContainer/btnGrafik");
        _btnSound = GetNode<Button>("VBoxContainer/btnSound");
        _btnControl = GetNode<Button>("VBoxContainer/btnControl");
        _btnZurueck = GetNode<Button>("VBoxContainer/btnZurueck");
        _btnGrafikBack = GetNode<Button>("GrafikControlPanel/VBoxContainer/btnGrafikBack");
        _btnSoundBack = GetNode<Button>("SoundControlPanel/btnSoundBack");
        _btnControlskBack = GetNode<Button>("ControlsControlPanel/btnControlsBack");

        _pcGrafik = GetNode<PanelContainer>("GrafikControlPanel");
        _pcSound = GetNode<PanelContainer>("SoundControlPanel");
        _pcControl = GetNode<PanelContainer>("ControlsControlPanel");

        _pcGrafik.Visible = false;
        _pcSound.Visible = false;
        _pcControl.Visible = false;

        _btnGrafik.Pressed += HandleGrafikPressed;
        _btnSound.Pressed += HandleSoundPressed;
        _btnControl.Pressed += HandleControlPressed;
        _btnZurueck.Pressed += HandleZurueckPressed;
        _btnGrafikBack.Pressed += HandleBackPressed;
        _btnSoundBack.Pressed += HandleBackPressed;
        _btnControlskBack.Pressed += HandleBackPressed;
    }

    private void HandleGrafikPressed()
    {
        GD.Print("Grafik pressed");
        _pcGrafik.Visible = true;
        _pcSound.Visible = false;
        _pcControl.Visible = false;
    }

    private void HandleSoundPressed()
    {
        GD.Print("Sound pressed");
        _pcSound.Visible = true;
        _pcGrafik.Visible = false;
        _pcControl.Visible = false;
    }

    private void HandleControlPressed()
    {
        GD.Print("Tastenbelegung pressed");
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

    private void HandleZurueckPressed()
    {
        GetTree().ChangeSceneToFile("res://PrjktResonanz/scenes/ui/start_screen.tscn");
    }
}