using UnityEngine;
using Zenject;

public class LevelController : MonoBehaviour
{
    private ISaveService _saveService;

    [Inject]
    public void Construct(ISaveService saveService)
    {
        _saveService = saveService;
    }
    
    public void InitializeLevel()
    {
        var levelSave = _saveService.GetSaveObject<PlayerSave>("level") ?? new PlayerSave();
        LoadLevelDataFromSave(levelSave);
    }

    private void LoadLevelDataFromSave(PlayerSave playerSave)
    {
        if (playerSave == null)
        {
            Debug.Log($"LEVEL SAVE NULL");
            return;
        }
    }
}