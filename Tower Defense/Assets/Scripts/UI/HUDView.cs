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

    public void UpdateMoney(int value)
    {
        moneyText.text = $"${value}";
    }

    public void UpdateHP(int value)
    {
        hpSlider.value = value;
    }

    public void UpdateWave(int value)
    {
        waveText.text = $"Wave: {value}";
    }
}
