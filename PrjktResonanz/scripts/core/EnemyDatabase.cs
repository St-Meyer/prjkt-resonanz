using Godot;
using System.Collections.Generic;
using System.Text.Json;

public partial class EnemyDatabase : Node
{
	Dictionary<string, EnemyData> _enemyDatas;

	public override void _Ready()
	{
		var json = FileAccess.Open("res://PrjktResonanz/assets/data/enemies.json", FileAccess.ModeFlags.Read);
		if (json != null)
		{
			_enemyDatas = JsonSerializer.Deserialize<Dictionary<string, EnemyData>>(json.GetAsText());
		}
	}

	public EnemyData Get(string enemyId)
	{
		if (!_enemyDatas.ContainsKey(enemyId))
		{
			return null;
		}
		return _enemyDatas[enemyId];
	}
}
