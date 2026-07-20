using Godot;

public partial class SavePoint : Area2D
{
    [Export] public int SavePointID;
    private SavePoint _savePoint;
    private SaveManager _saveManager;

    public override void _Ready()
    {
        _savePoint = GetNode<SavePoint>("Safepoint");
        //_savePoint += OnBodyEntered;
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body is Player)
        {
            _saveManager = GetNode<SaveManager>("/root/SafeManager");
            _saveManager.Save(SavePointID, 1);
        }
    }
}

