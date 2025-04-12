using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Enemies
{
    public class SlowEnemy : Enemy, IEnemy, IDamageable
    {
        private void Start()
        {
            print("Slow enemy spawned");
        }
    }
}

