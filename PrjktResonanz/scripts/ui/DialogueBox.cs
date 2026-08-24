using Godot;
using System;

public partial class DialogueBox : Control
{
	private TextureRect _textBox;
	private TextureRect _portrait;
	private Label _name;
	private Label _dialog;
	private DialogueManager _dm;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_textBox = GetNode<TextureRect>("TextBox");
		_portrait = GetNode<TextureRect>("Portrait");
		_name = GetNode<Label>("Name");
		_dialog = GetNode<Label>("Dialogtext");

		// Textbox wird beim ersten Laden unsichtbar geschaltet
		_textBox.Visible = false;
		_dm = GetNode<DialogueManager>("/root/DialogueManager");
		_dm.RegisterDialogueBox(this);
		ProcessMode = ProcessModeEnum.Always;
	}

	// Dialogbox mit Parametern wird geladen und gezeigt.
	public void ShowDialogue(string charaterName, string text, Texture2D portrait)
	{		
		_textBox.Visible = true;
		_name.Text = charaterName;
		_dialog.Text = text;
		_portrait.Texture = portrait;
	}

	// Dialogbox, Portrait werden unsichtbar geschaltet
	// Dialog und Name werden geleert
	public void HideDialogue()
	{
		_textBox.Visible = false;
		_portrait.Visible = false;
		_name.Text = "";
		_dialog.Text = "";
	}

	public override void _PhysicsProcess(double delta)
	{
		// Nächste Textline wird gezeigt
		if (Input.IsActionJustPressed("ui_accept"))
		{
			_dm.ShowNextLine();
		}
	}
}
