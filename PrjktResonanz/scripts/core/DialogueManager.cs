using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
using FileAccess = Godot.FileAccess;

public partial class DialogueManager : Node
{

	[Signal] public delegate void DialogueStartetEventHandler();
	[Signal] public delegate void DialogueEndedEventHandler();
	private bool _runningDialogue;
	private int _index;
	private List<DialogueLine> _dialogueLine;
	private DialogueBox _dialogueBox;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Always;
	}

	public void StartDialogue(string jsonPath)
	{
		GetTree().Paused = true;
		_runningDialogue = true;
		EmitSignal(SignalName.DialogueStartet);
		_index = 0;
		var json = FileAccess.Open(jsonPath, FileAccess.ModeFlags.Read);
		if (json != null)
		{
			_dialogueLine = JsonSerializer.Deserialize<List<DialogueLine>>(json.GetAsText());
		}
		ShowNextLine();
	}

	public void ShowNextLine()
	{

		if (_dialogueLine.Count <= _index)
		{
			EndDialogue();
		}
		else
		{
			_dialogueBox.ShowDialogue(_dialogueLine[_index].Character, _dialogueLine[_index].Text, GD.Load<Texture2D>("res://PrjktResonanz/assets/portraits/" + _dialogueLine[_index].Portrait + ".png"));
			_index++;
		}

	}

	public void EndDialogue()
	{
		_runningDialogue = false;
		EmitSignal(SignalName.DialogueEnded);
		_dialogueBox.Visible = false;
		GetTree().Paused = false;
	}

	public void RegisterDialogueBox(DialogueBox box)
	{
		_dialogueBox = box;
	}
}
