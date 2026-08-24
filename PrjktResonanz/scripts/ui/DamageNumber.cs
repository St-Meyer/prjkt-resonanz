using Godot;

public partial class DamageNumber : Label
{
	public void Setup(int damage, bool crit)
	{
		Text = damage.ToString();
		// Weiße Fontfarbe und kleinere Schrift bei Schaden ohne Crit
		AddThemeColorOverride("font_color", Colors.White);
		AddThemeFontSizeOverride("font_size", 13);
		// Wenn Crit, rote Zahlen und größere Schrift
		if (crit)
		{
			AddThemeColorOverride("font_color", Colors.DarkMagenta);
			AddThemeFontSizeOverride("font_size", 17);
		}

		// Zahlenanimation mit aufsteigender Zahlenposition und 
		// verschwinden der Zahl
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "global_position", GlobalPosition + new Vector2(0, -50), 1);
		tween.TweenProperty(this, "modulate", new Color(1,1,1,0), 1);
		tween.TweenCallback(Callable.From(QueueFree));
	}
}
