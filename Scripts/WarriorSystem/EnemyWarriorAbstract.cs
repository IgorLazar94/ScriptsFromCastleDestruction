using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarriorAbstract : MonoBehaviour, IWarrior
{
    [Header ("Warrior Type")]
    [SerializeField] WarriorType warriorType;

    [SerializeField] public float baseSpeed;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] public float scale;

    //[Tooltip("Damage from Player")]
    [SerializeField] public int damage;

    [SerializeField] public GameObject particlePuffEffect;

    [Space(20)]
    [SerializeField] Animator animator;
    private bool isGetDamage = true;
    public bool isLive = true;
    public int Health
    {
        get { return health; }
        set 
        { 
            health = value; 
            if(health <= 0)
            {
                Die();
            }
        }
    }
    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value;
        }
    }



    public void Start()
    {
       // Debug.Log("Dam = " + damage);
       particlePuffEffect.SetActive(false);
        animator = GetComponent<Animator>();
    }

   
    public virtual void Die()
    {
        particlePuffEffect.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer(LayerList.Dead);
        AnimatorOff();
        //PlayerEnergyUI.OnAddEnergy.Invoke(1);
        EnergyManager.OnAddEnergy.Invoke(warriorType);
        CoinControll.instance.OnPlayerGetCoins(200);

        isGetDamage = false;
        isLive = false;
        gameObject.layer = 14;
        StartCoroutine(WaitForDie());
        Destroy(gameObject.transform.parent.gameObject, 1);
        
        
    }

    IEnumerator WaitForDie()
    {
        yield return new WaitForSeconds(.6f);
        particlePuffEffect.SetActive(true);
    }
    public void AnimatorOff()
    {
        if (animator != null)
        {
            animator.enabled = false;
        }
        foreach (var value in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            value.isKinematic = true;
        }

        Invoke("FixRagDoll", 0.1f);
    }

    private void FixRagDoll()
    {
        foreach (var value in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            value.isKinematic = false;
        }
    }
    public virtual void TakeDamage(int damageFP)
    {
        if (isGetDamage || Health > 0)
        {
            Health -= damageFP;
        }
        
    }

    public void StartAtackAnim()
    {
        animator.ResetTrigger("StartRun") ;
        animator.SetTrigger("StartAtack");
    }

    public void StartRunAnim()
    {
        if (animator != null)
        {
            animator.ResetTrigger("StartAtack");
            animator.SetTrigger("StartRun");
        }

    }
}
