using System.Text.Json;
using Godot;
using FileAccess = Godot.FileAccess;

public partial class SaveManager : Node
{
    public void Save(int savepointID, int slotID)
    {
        PlayerData playerData = GetNode<PlayerData>("/root/PlayerData");
        SaveData _save = new SaveData()
        {
            SlotID = slotID,
            SavePointID = savepointID,
            CurrentHealth = playerData.CurrentHealth
        };
        FileAccess json = FileAccess.Open("user://save_" + slotID + ".json", FileAccess.ModeFlags.Write);
        string jsonString = JsonSerializer.Serialize(_save);
        json.StoreString(jsonString);

    }

    public SaveData Load()
    {
        return null;
    }

    public bool HasSave(int slotID)
    {
        return false;
    }
}