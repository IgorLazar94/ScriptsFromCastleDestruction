using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWarrior
{
    public int Health { get; set; }
    //public int MaxHealth { get; set; } // Not using now

    public void TakeDamage(int damage);
    public void Die();
    
}
