using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;


public abstract class PlayerWarriorAbstract : MonoBehaviour, IWarrior
{

    [Header("Warrior Type")]
    //[SerializeField] WarriorType warriorType;
    [SerializeField] public float baseSpeed;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] public float scale;

    
    //[Tooltip("Damage from Enemy")]
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
            if (health <= 0)
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
        animator = GetComponent<Animator>();
        
    }
    public virtual void Die()
    {
        
        //Debug.Log(gameObject.name + " die");
        if (animator != null)
        {
            animator.enabled = false;
        }
        foreach (var value in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            value.isKinematic = true;
        }

       Invoke("FixRagDoll", 0.1f);

        isGetDamage = false;
        isLive = false;
        StartCoroutine(WaitForDie());
        Destroy(gameObject.transform.parent.gameObject, 1);
       
    }

    IEnumerator WaitForDie()
    {
        yield return new WaitForSeconds(.6f);
        particlePuffEffect.SetActive(true);
    }
    private void FixRagDoll()
    {
        foreach (var value in gameObject.GetComponentsInChildren<Rigidbody>())
        {   
             value.isKinematic = false;
        }
    }

    public virtual void TakeDamage(int damageFE)
    {
        if (isGetDamage || Health > 0)
        {

            Health -= damageFE;
            //slider.value -= damageFE;

        }

    }

    public void StartAtackAnim()
    {
        animator.ResetTrigger("StartRun");
        animator.SetTrigger("StartAtack");
    }

    public void StartRunAnim()
    {
        if (animator != null)
        {
            animator.ResetTrigger("StartAtack");
            animator.ResetTrigger("StartShootArrow");
            animator.SetTrigger("StartRun");
        }

    }
    public void StartShootArrowAnim()
    {
        if (animator != null)
        {
            animator.ResetTrigger("StartRun");
            animator.SetTrigger("StartShootArrow");
        }

    }
}
