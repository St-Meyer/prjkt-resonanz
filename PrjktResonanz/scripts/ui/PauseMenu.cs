using Godot;

public partial class PauseMenu : Node
{
    private Button _btnResume;
    private Button _btnLoad;
    private Button _btnTitleMenu;

    public override void _Ready()
    {
        _btnResume = GetNode<Button>("HBoxContainer/btnResume");
        _btnLoad = GetNode<Button>("HBoxContainer/btnLoad");
        _btnTitleMenu = GetNode<Button>("HBoxContainer/btnTitleMenu");
    }

    private void HandleResumePressed()
    {
        
    }

    private void HandleLoadPressed()
    {
        
    }

    private void HandleTitleMenuPressed()
    {
        
    }
}