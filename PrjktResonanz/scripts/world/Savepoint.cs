using Godot;

public partial class Savepoint : Area2D
{
    [Export] public int SavepointId;
    private SaveManager _saveManager;
    private Label _infoText;
    private bool _bodyEntered;

    public override void _Ready()
    {
        _saveManager = GetNode<SaveManager>("/root/SaveManager");
        _infoText = GetNode<Label>("Label");
        _infoText.Text = "Enter 'E' to save...";
        _infoText.Visible = false;
        _bodyEntered = false;
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
    }

    public override void _Process(double delta)
    {
        if (_bodyEntered)
        {
            _infoText.Visible = true;
            if (Input.IsActionJustPressed("accept"))
            {
                _saveManager.Save(SavepointId, 1);
                _infoText.Text = "saved...";
            }
        }
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body is ITargetable){
            _bodyEntered = true; 
        }
    }

    public void OnBodyExited(Node2D body)
    {
        if (body is ITargetable)
        {
            _bodyEntered = false;
            _infoText.Text = "Enter 'E' to save...";
            _infoText.Visible = false;
        }
    }
}