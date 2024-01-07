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
        var levelSave = _saveService.GetSaveObject<PlayerSaveData>("level") ?? new PlayerSaveData();
        LoadLevelDataFromSave(levelSave);
    }

    private void LoadLevelDataFromSave(PlayerSaveData playerSaveData)
    {
        if (playerSaveData == null)
        {
            Debug.Log($"LEVEL SAVE NULL");
            return;
        }
    }
}