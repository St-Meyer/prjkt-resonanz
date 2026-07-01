using Godot;
using System;

[GlobalClass]
public partial class DetectionComponent : Node
{
    
    [Signal] public delegate void TargetDetectedEventHandler(Node2D target);
    [Signal] public delegate void TargetLostEventHandler();
    private Area2D _detectionBox;
    private Node2D _target;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_detectionBox = GetNode<Area2D>("Area2D");
		_detectionBox.BodyEntered += OnDetectionBoxBodyEnterd;
		_detectionBox.BodyExited += OnDetectionBoxBodyExited;
	}

	public void OnDetectionBoxBodyEnterd(Node2D body)
	{
		if (body is ITargetable)
		{
			_target = body;
            EmitSignal(SignalName.TargetDetected, body);
		}
	}

	public void OnDetectionBoxBodyExited(Node2D body)
	{
		if (body is ITargetable)
		{
			_target = null;
			EmitSignal(SignalName.TargetLost);
		}
	}
}
