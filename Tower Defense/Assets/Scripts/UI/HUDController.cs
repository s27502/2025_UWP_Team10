using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HUDController : MonoBehaviour
{
    [SerializeField] private HUDView view;  
    private HUDModel model;  
    private HUDPresenter presenter;  
    
    private void Awake()
    {
        
        ServiceLocator.Instance.Register(this);
        
        
        model = new HUDModel();
        presenter = new HUDPresenter(model, view);
        
        
        var resourceManager = ServiceLocator.Instance.GetService<ResourceManager>();
        model.SetMoney(resourceManager.GetMoney());
        model.SetHP(resourceManager.GetHP());

        
        resourceManager.OnMoneyChanged += model.SetMoney;
        resourceManager.OnHpChanged += model.SetHP;
        
       
        var gameManager = ServiceLocator.Instance.GetService<GameManager>();
        model.SetWave(gameManager?.GetCurrentWave() ?? 1);
        
        
        var waveInfo = gameManager?.GetEnemyTypesAndCountForWave() ?? "Brak danych";
        model.SetEnemyTypes(waveInfo);  

        string enemyInfo = gameManager?.GetEnemyTypesAndCountForWave() ?? "Brak danych";
        model.SetEnemyInfo(enemyInfo);  

        Debug.Log("POBRANA INFORMACJA: " + enemyInfo);

        var waveManager = ServiceLocator.Instance.GetService<WaveManager>();
        waveManager.OnWaveStart.AddListener(UpdateEnemyInfoUI);
    }

    
    public void UpdateEnemyInfoUI()
    {
        var gameManager = ServiceLocator.Instance.GetService<GameManager>();
        string enemyInfo = gameManager?.GetEnemyTypesAndCountForWave() ?? "Brak danych";
        model.SetEnemyInfo(enemyInfo);
        Debug.Log("HUD UPDATE enemyInfo: " + enemyInfo);
    }

    
    public void SetMoney(int value) => model.SetMoney(value);
    public void SetHP(int value) => model.SetHP(value);
    public void SetWave(int value) => model.SetWave(value);

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<HUDController>();
    }
    
    public HUDView GetHUDView() => view;
}
