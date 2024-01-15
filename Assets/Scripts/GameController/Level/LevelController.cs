using UnityEngine;
using Zenject;

public class LevelController : MonoBehaviour, ILevelController
{
    private ISaveService _saveService;
    private IUIController _uiController;
    private LevelSave _levelSave;
    private int _lastCompletedLevelNumber;

    public int LastCompletedLevelNumber => _lastCompletedLevelNumber;

    [Inject]
    public void Construct(ISaveService saveService, IUIController uiController)
    {
        _saveService = saveService;
        _uiController = uiController;
    }

    public void InitializeLevelSave()
    {
        _levelSave = _saveService.GetSaveObject<LevelSave>("save");
        
        if (_levelSave.IsFirstLoad)
        {
            _levelSave = new LevelSave();
            Debug.Log("New level save created");
        }
        else
        {
            Debug.Log($"Level save is loaded");
        }

        var levelSlots = _uiController.LevelSelectPanel.LevelSelectSlots;
        
        if (_levelSave != null && !_levelSave.LevelSlotSaveDatas.IsNullOrEmpty())
        {
            for (int i = 0; i < _levelSave.LevelSlotSaveDatas.Length; i++)
            {
                for (int j = 0; j < levelSlots.Length; j++)
                {
                    levelSlots[j].Load(_levelSave.LevelSlotSaveDatas[i]);
                    break;
                }
            }
        }

        _levelSave.LinkLevelSlots(levelSlots);
        
        UnlockFirstLevel();
    }

    private void UnlockFirstLevel()
    {
        _uiController.LevelSelectPanel.LevelSelectSlots[0].Unlock();
    }
}