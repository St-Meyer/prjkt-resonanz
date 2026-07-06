using Godot;
using System;

public partial class DamageNumber : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Setup(int damage, bool crit)
	{
		Text = damage.ToString();
		AddThemeColorOverride("font_color", Colors.White);
		AddThemeFontSizeOverride("font_size", 13);
		if (crit)
		{
			AddThemeColorOverride("font_color", Colors.DarkMagenta);
			AddThemeFontSizeOverride("font_size", 17);
		}

		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "global_position", GlobalPosition + new Vector2(0, -50), 1);
		tween.TweenProperty(this, "modulate", new Color(1,1,1,0), 1);
		tween.TweenCallback(Callable.From(QueueFree));
	}
}
