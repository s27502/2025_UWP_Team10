using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Enemies
{
    public class FastEnemy : Enemy, IEnemy, IDamageable
    {
        private void Start()
        {
            print("Fast enemy spawned");
        }
    }
}

