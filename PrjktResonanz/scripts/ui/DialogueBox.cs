using Godot;
using System;

public partial class DialogueBox : Control
{
	private TextureRect _textBox;
	private TextureRect _portrait;
	private Label _name;
	private Label _dialog;
	private TextureRect _next;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_textBox = GetNode<TextureRect>("TextBox");
		_portrait = GetNode<TextureRect>("Portrait");
		_name = GetNode<Label>("Name");
		_dialog = GetNode<Label>("Dialogtext");
		_next = GetNode<TextureRect>("Next");

		_textBox.Visible = false;
		//_textBox.Texture = (Texture2D)GD.Load("res://PrkjtResonanz/assets/ui/textbox.png");
		GetNode<DialogueManager>("/root/DialogueManager").RegisterDialogueBox(this);
	}

	public void ShowDialogue(string charaterName, string text, Texture2D portrait)
	{		
		_textBox.Visible = true;
		_name.Text = charaterName;
		_dialog.Text = text;
		_portrait.Texture = portrait;
		GD.Print(portrait);
	}

	public void HideDialogue()
	{
		_textBox.Visible = false;
		_portrait.Visible = false;
		_name.Text = "";
		_dialog.Text = "";
	}
}
