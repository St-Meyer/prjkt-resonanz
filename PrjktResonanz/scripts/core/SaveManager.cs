using System.Text.Json;
using Godot;
using FileAccess = Godot.FileAccess;

public partial class SaveManager : Node
{

    public SaveData ActiveSave;

    public void Save(int savepointId, int slotId)
    {
        PlayerData playerData = GetNode<PlayerData>("/root/PlayerData");
        SaveData save = new SaveData()
        {
            SlotID = slotId,
            SavePointID = savepointId,
            CurrentHealth = playerData.CurrentHealth
        };
        FileAccess json = FileAccess.Open("user://save_" + slotId + ".json", FileAccess.ModeFlags.Write);
        string jsonString = JsonSerializer.Serialize(save);
        json.StoreString(jsonString);
        json.Close();
    }

    public SaveData Load(int slotId)
    {
        if (!HasSave(slotId))
        {
            return null;
        }
        SaveData save = new SaveData();
        string jsonPath = "user://save_" + slotId + ".json";
        var json = FileAccess.Open(jsonPath, FileAccess.ModeFlags.Read);
        if (json != null)
        {
            save = JsonSerializer.Deserialize<SaveData>(json.GetAsText());
        }

        return save;
    }

    public bool HasSave(int slotId)
    {
        return FileAccess.FileExists("user://save_" + slotId + ".json");
    }
}