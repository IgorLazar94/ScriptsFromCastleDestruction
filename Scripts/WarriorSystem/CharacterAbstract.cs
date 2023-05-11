using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbstract : MonoBehaviour, IWarrior
{
    [SerializeField] private int health;
    [SerializeField] public int damage;
    [SerializeField] Animator animator;
    private PlayerEnergyUI playerEnerjy;
    private bool isGetDamage = false;
    public bool isLive = true;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health == 0)
            {
                Die();
            }
        }
    }

    //public int MaxHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); } // Don`t using now


    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    public virtual void Die()
    {
        //PlayerEnergyUI.SendEnemyKilled(1);
        //animator.SetBool("isTimeToDeath", true);
        if (animator != null)
        {
            animator.enabled = false;
        }
        
        isLive = false;
        isGetDamage = false;
        Destroy(gameObject.transform.parent.gameObject, 5);
    }

    public virtual void TakeDamage(int damageFE)
    {
        if (isLive)
        {
            isGetDamage = true;
            Health -= damageFE;
        }
        
    }

    public void StartAtackAnim()
    {
        if (animator != null)
        {
            animator.SetBool("isAtackTime", true);
            animator.SetBool("isTimeToRun", false);
        }
        
    }

    public void StartRunAnim()
    {
        if (animator != null)
        {
            animator.SetBool("isTimeToRun", true);
            animator.SetBool("isAtackTime", false);
        }
        
    }
}
