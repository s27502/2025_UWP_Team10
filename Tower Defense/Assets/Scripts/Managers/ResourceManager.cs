using System;
using UnityEngine;

    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance { get; private set; }

        [SerializeField] private int _money;
        private int _hp;

        public event Action<int> OnMoneyChanged;
        public event Action<int> OnHPChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
                ServiceLocator.Instance.Register(this);
                _money = 100;
                _hp = 100;
            }


        }

        public void AddMoney(int amount)
        {
            if (_money < 0) return;
            _money += amount;
            OnMoneyChanged?.Invoke(_money);
        }

        public bool SpendMoney(int amount)
        {
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
            _hp -= amount;
            if (_hp < 0) _hp = 0;

            OnHPChanged?.Invoke(_hp);

            if (_hp <= 0)
            {
                Debug.Log("Game Over");
                // #TODO:  trigger lose state
            }
        }

        public int GetMoney() => _money;
        public int GetHP() => _hp;
    }
