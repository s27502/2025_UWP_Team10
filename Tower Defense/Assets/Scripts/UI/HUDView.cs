using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDView : MonoBehaviour
{
    [Header("Text Components")]
    public TMP_Text moneyText;
    [SerializeField] private Slider hpSlider;
    public TMP_Text waveText;
    public Button nextWaveButton;
    [SerializeField] private TMP_Text enemyTypesText;
    [SerializeField] private TMP_Text enemyInfoText;


    public void UpdateEnemyInfo(string info)
    {
        Debug.Log("USTAWIAM enemyInfoText: " + info);
        //enemyInfoText.text = info;
    }


    public void UpdateEnemyTypes(string enemyTypes)
    {
        enemyTypesText.text = enemyTypes;
    }

    public void UpdateMoney(int value)
    {
        moneyText.text = $"${value}";
    }

    public void UpdateHp(int value)
    {
        hpSlider.value = value;
        Debug.Log("USTAWIAM hpSlider: " + hpSlider.value);
    }

    public void UpdateWave(int value)
    {
        waveText.text = $"Wave: {value}";
    }
}
