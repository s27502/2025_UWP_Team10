using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDModel 
{
    
    public event Action<int> OnMoneyChanged;
    public event Action<int> OnHPChanged;
    public event Action<int> OnWaveChanged;

    private int money;
    private int hp;
    private int wave;

    public void SetMoney(int value)
    {
        money = value;
        OnMoneyChanged?.Invoke(money);
    }

    public void SetHP(int value)
    {
        hp = value;
        OnHPChanged?.Invoke(hp);
    }

    public void SetWave(int value)
    {
        wave = value;
        OnWaveChanged?.Invoke(wave);
    }

    public int GetMoney() => money;
    public int GetHP() => hp;
    public int GetWave() => wave;


}

