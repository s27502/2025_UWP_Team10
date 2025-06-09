using System;
using System.Collections;
using Tutorial;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourceManager : SingletonDoNotDestroy<ResourceManager>
{
    [SerializeField] private int _money;
    private int _hp;
    [SerializeField] private GameObject gameOverScreen;
    
    public event Action<int> OnMoneyChanged;
    public event Action<int> OnHpChanged;
    private TutorialUIManager tutorialManager;

    protected override void Awake() {
        base.Awake();
        if (Instance != this) return;

        ServiceLocator.Instance.Register(this);
        tutorialManager = GameObject.FindObjectOfType<TutorialUIManager>();
        _money = 100;
        _hp = 100; 
    }

    public void AddMoney(int amount)
    {
        if (_money < 0) return;
        _money += amount;
        OnMoneyChanged?.Invoke(_money);
    }

    public bool SpendMoney(int amount)
    {
        if (tutorialManager == null)
            tutorialManager = GameObject.FindObjectOfType<TutorialUIManager>();

        if (tutorialManager != null)
            tutorialManager.HideAttackMessage();
        
        if (_money >= amount)
        {
            _money -= amount;
            OnMoneyChanged?.Invoke(_money);
            return true;
        }

        return false;
    }

    public void DealDamage(int amount)
    {
        if (tutorialManager != null) tutorialManager.ShowAttackMessage();
        
        _hp -= amount;
        if (_hp < 0) _hp = 0;
        
        OnHpChanged?.Invoke(_hp);

        if (_hp > 0) return;
        gameOverScreen.gameObject.SetActive(true);
        StartCoroutine(ReturnToMenu());
    }
    
    private IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(5f);
        gameOverScreen.gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
    public int GetMoney() => _money;
    public int GetHP() => _hp;
}