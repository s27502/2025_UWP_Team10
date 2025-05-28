using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDPresenter
{
    private HUDModel model;
    private HUDView view;


    public HUDPresenter(HUDModel model, HUDView view)
    {
        this.model = model;
        this.view = view;

        model.OnMoneyChanged += view.UpdateMoney;
        model.OnHpChanged += view.UpdateHP;
        model.OnWaveChanged += view.UpdateWave;
        model.OnEnemyTypesChanged += view.UpdateEnemyTypes;
        model.OnEnemyInfoChanged += view.UpdateEnemyInfo;

       // view.nextWaveButton.onClick.AddListener(OnNextWaveClicked);
    }

    // #TODO If we decide to have a button to start a wave instead of space
    /*
    private void OnNextWaveClicked()
    {
        
        ServiceLocator.Instance.GetService<GameManager>()?.OnNextWaveButtonClicked(); 
    }
    */
}