using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class LoseWindow : WindowBase
{
    [SerializeField] private Text lostHPTextBox;
    [SerializeField] private Button ContinueForMonneyButton;
    [SerializeField] private Button ContinueForRVButton;
    [SerializeField] private Button FreeContinoeButton;
    private IPersistanseDataService _persistanseDataService;
    private LevelManager _levelManager;

    [Inject]
    private void Construct(IPersistanseDataService persistanseDataService, LevelManager levelManager)
    {
        _levelManager = levelManager;
        _persistanseDataService = persistanseDataService;
    }


    protected override void Initialize()
    {
        var HP = _persistanseDataService.Data.Player.PlayerHP;
        SetButtonVisible(HP  == 0);
        if (HP == 0)
            lostHPTextBox.text = "Машина сломана!";
        else
            lostHPTextBox.text = $"Осталось {HP} жизней";
        
        ContinueForMonneyButton.onClick.AddListener(ContinueForMonney);
        ContinueForRVButton.onClick.AddListener(ContinueForRV);
        FreeContinoeButton.onClick.AddListener(ContinueForFree);
    }

    protected override void SubscribeUpdates()
    {
        
    }

    protected override void Cleanup()
    {
        ContinueForMonneyButton.onClick.RemoveListener(ContinueForMonney);
        ContinueForRVButton.onClick.RemoveListener(ContinueForRV);
        FreeContinoeButton.onClick.RemoveListener(ContinueForFree);
    }

    private void SetButtonVisible(bool isHpZero)
    {
        ContinueForMonneyButton.gameObject.SetActive(isHpZero);
        ContinueForRVButton.gameObject.SetActive(isHpZero);
        FreeContinoeButton.gameObject.SetActive(!isHpZero);
    }
    
    private void ContinueForFree()
    {
        // TODO create SceneLoader
        _levelManager.RestartCurrentLevel();
    }

    private void ContinueForRV()
    {
        _levelManager.RestartCurrentLevel();
    }

    private void ContinueForMonney()
    {
        _persistanseDataService.SpendMoney(30);
        _levelManager.RestartCurrentLevel();
    }
}
