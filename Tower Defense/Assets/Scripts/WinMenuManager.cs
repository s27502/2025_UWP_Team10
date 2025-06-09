using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuManager : MonoBehaviour
{
    [SerializeField] private float delay = 5f;
    
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        StartCoroutine(ReturnToMenu());
    }
    
    private IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu");
    }
}
