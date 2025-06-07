using UnityEngine;

namespace Tutorial
{
    public class TutorialUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject attackMessage;
        private bool _isAttackMessageActivated;
    
        private void Start()
        {
            _isAttackMessageActivated = false;
        }

        private void Update()
        {
            
        }

        public void ShowAttackMessage()
        {
            if (_isAttackMessageActivated) return;
            _isAttackMessageActivated = true;
            Time.timeScale = 0.5f;
            attackMessage.gameObject.SetActive(true);
        }

        public void HideAttackMessage()
        {
            Time.timeScale = 1f;
            attackMessage.gameObject.SetActive(false);
        }
    }
}
