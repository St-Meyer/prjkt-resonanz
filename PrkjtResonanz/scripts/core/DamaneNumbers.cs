using Godot;
using System;

public partial class DamageNumbers : Label
{
	private Label testLabel;

	public override void _Ready(){
		testLabel = GetNode<Label>("Label");
		testLabel.Text = "Hello!";
	}

	public override void _Process(double delta){
		testLabel = GetNode<Label>("Label");
		testLabel.Text = "Hello!";
	}
}
