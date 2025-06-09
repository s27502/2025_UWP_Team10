using System.Collections;
using UnityEngine;

namespace Tutorial
{
    public class TutorialUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject attackMessage;
        [SerializeField] private GameObject welcomeMessage;
        [SerializeField] private GameObject upgradeMessage;
        [SerializeField] private GameObject placingField;

        private int lastHP = -1;
        private bool welcomeMessageClosed = false;
        private bool upgradeMessageClosed = false;
        private bool moneyChangeHandled = false;
        private bool wasAttacked = false;
        private bool isUpgradeCoroutineRunning = false;

        private void Awake()
        {
            welcomeMessage.SetActive(true);
            upgradeMessage.SetActive(false);
        }

        private void OnEnable()
        {
            var resourceManager = ServiceLocator.Instance.GetService<ResourceManager>();
            if (resourceManager != null)
            {
                resourceManager.OnHpChanged += HandleHpChanged;
                resourceManager.OnMoneyChanged += HandleMoneyChanged;
                lastHP = resourceManager.GetHP();
            }
        }

        private void OnDisable()
        {
            var resourceManager = ServiceLocator.Instance.GetService<ResourceManager>();
            if (resourceManager != null)
            {
                resourceManager.OnHpChanged -= HandleHpChanged;
                resourceManager.OnMoneyChanged -= HandleMoneyChanged;
            }
        }

        private void Update()
        {
            if (!welcomeMessageClosed && Input.GetKeyDown(KeyCode.Space))
            {
                welcomeMessage.SetActive(false);
                welcomeMessageClosed = true;
                Debug.Log("Welcome message closed.");
            }

            if (upgradeMessage.activeSelf && Input.GetKeyDown(KeyCode.Mouse0))
            {
                upgradeMessage.SetActive(false);
                upgradeMessageClosed = true;
                Debug.Log("Upgrade message closed by mouse click.");
            }
        }

        private void HandleHpChanged(int newHP)
        {
            if (wasAttacked)
                return;

            if (newHP < lastHP)
            {
                ShowAttackMessage();
            }
            lastHP = newHP;
            wasAttacked = true;
        }

        private void HandleMoneyChanged(int newMoney)
        {
            if (moneyChangeHandled) return;

            HideAttackMessage();
            moneyChangeHandled = true;
        }

        public void ShowAttackMessage()
        {
            attackMessage.SetActive(true);
            placingField.SetActive(true);
            Debug.Log("Attack message shown.");
        }

        public void HideAttackMessage()
        {
            attackMessage.SetActive(false);
            Debug.Log("Attack message hidden.");

            if (!isUpgradeCoroutineRunning)
            {
                Debug.Log("Starting coroutine to show upgrade message...");
                StartCoroutine(OpenUpgradeMessageWithDelay(5f));
            }
        }

        private IEnumerator OpenUpgradeMessageWithDelay(float delay)
        {
            isUpgradeCoroutineRunning = true;
            yield return new WaitForSeconds(delay);

            upgradeMessageClosed = false;
            upgradeMessage.SetActive(true);
            Debug.Log("Upgrade message shown.");

            isUpgradeCoroutineRunning = false;
        }
    }
}
