using Godot;

public partial class Savepoint : Area2D
{
    [Export] public int SavepointId;
    private SaveManager _saveManager;

    public override void _Ready()
    {
        _saveManager = GetNode<SaveManager>("/root/SaveManager");
        BodyEntered += OnBodyEntered;
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body is ITargetable)
        {   
            _saveManager.Save(SavepointId, 1);
        }
    }
}