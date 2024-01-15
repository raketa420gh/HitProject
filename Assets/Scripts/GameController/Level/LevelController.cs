using UnityEngine;
using Zenject;

public class LevelController : MonoBehaviour, ILevelController
{
    private ISaveService _saveService;
    private LevelSave _levelSave;
    private IUIController _uiController;
    private int _lastCompletedLevelNumber;

    [Inject]
    public void Construct(ISaveService saveService, IUIController uiController)
    {
        _saveService = saveService;
        _uiController = uiController;
    }

    public void InitializeLevelSave()
    {
        var levelSlots = _uiController.LevelSelectPanel.LevelSelectSlots;
        
        _levelSave = _saveService.GetSaveObject<LevelSave>("save");

        if (_levelSave == null)
        {
            for (int i = 0; i < levelSlots.Length; i++)
            {
                levelSlots[i].Reset();
            }
        }

        if (_levelSave != null && !_levelSave.LevelSlotSaveDatas.IsNullOrEmpty())
        {
            for (int i = 0; i < _levelSave.LevelSlotSaveDatas.Length; i++)
            {
                levelSlots[i].Load(_levelSave.LevelSlotSaveDatas[i]);
            }
        }

        _levelSave?.LinkLevelSlots(levelSlots);
        
        //Test
        UnlockFirstLevel();
    }

    private void UnlockFirstLevel()
    {
        _uiController.LevelSelectPanel.LevelSelectSlots[0].SetUnlockState(true);
    }
}