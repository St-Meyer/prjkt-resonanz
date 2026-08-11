using Godot;

public partial class Settings : Node
{
    private Button btnGrafik;
    private Button btnSound;
    private Button btnZurueck;

    public override void _Ready()
    {
        btnGrafik = GetNode<Button>("VBoxContainer/btnGrafik");
        btnSound = GetNode<Button>("VBoxContainer/btnSound");
        btnZurueck = GetNode<Button>("VBoxContainer/btnZurueck");

        btnGrafik.Pressed += HandleGrafikPressed;
        btnSound.Pressed += HandleSoundPressed;
        btnZurueck.Pressed += HandleZurueckPressed;
    }

    private void HandleGrafikPressed()
    {
        
    }

    private void HandleSoundPressed()
    {
    }

    private void HandleZurueckPressed()
    {
        GetTree().ChangeSceneToFile("res://PrjktResonanz/scenes/ui/start_screen.tscn");
    }
}