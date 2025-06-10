using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WavePreviewManager : MonoBehaviour
{
    [SerializeField] private Image slowEnemyImg, fastEnemyImg;
    [SerializeField] private TMP_Text slowEnemyCountText, fastEnemyCountText;

    public void RefreshWavePreview(int fastEnemyCount, int slowEnemyCount)
    {
        slowEnemyCountText.text = slowEnemyCount.ToString();
        fastEnemyCountText.text = fastEnemyCount.ToString();
    }
}
