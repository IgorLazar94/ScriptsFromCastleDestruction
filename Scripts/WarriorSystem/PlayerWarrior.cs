using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerWarrior : PlayerWarriorAbstract
{
    [SerializeField] public WarriorType warriorType;
    [SerializeField] protected MovingController movingController;
    [SerializeField] Vector3 baseDirection;
    //private WarriorType warrior;

    

    [SerializeField] Slider slider;
    [SerializeField] GameObject unitSprite;

    protected EnemyWarrior enemyWarrior;
    protected EnemyTower enemyTower;
    protected bool isHitTarget = false;
    protected bool isFighting = false;
    private bool isFBowman = true;
    protected bool isHitTower = false;
    private bool isOnTrigger = false;
    private EnemyTower targetTower;
    private EnemyCastle targetCastle;
    //private WarriorType warrior;
    //[SerializeField] Animator animator;

    [Header("Weapon")]
    [SerializeField] Transform spownPoint;
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] Transform weaponParent;


    private void Awake()
    {
        particlePuffEffect.SetActive(false);
        particlePuffEffect.SetActive(true);
        unitSprite.SetActive(false);
        
    }

    private void Start()
    {

        StartCoroutine(SpownCoolDown());

    }

    IEnumerator SpownCoolDown()
    {
        
        yield return new WaitForSeconds(.5f);
        unitSprite.SetActive(true);
        targetTower = FindObjectOfType<EnemyTower>();
        targetCastle = FindObjectOfType<EnemyCastle>();
        base.Start();
        tag = TagList.PlayerWarrior;
        Health = MaxHealth;
        slider.maxValue = MaxHealth;
        slider.onValueChanged.AddListener(delegate { OnHealthSliderChanged(); });
        slider.value = MaxHealth;
        transform.localScale *= scale; //Для великого юніта
        GameObject playerWeapon = Instantiate(weaponPrefab, spownPoint.position, Quaternion.Euler(90, 90, 0), weaponParent);
        movingController.SetDirection(baseDirection);
        /*if (targetTower != null && !targetTower.isTowerDestroy)
        {
            movingController.SetDirection(targetTower.transform.position - transform.position);
        }
        else
        {
            movingController.SetDirection(targetCastle.transform.position - transform.position);
        }*/

        movingController.SetSpeed(baseSpeed);
        particlePuffEffect.SetActive(false);
    }

    void OnHealthSliderChanged()
    {
        Health = MaxHealth * (int)slider.value;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.TryGetComponent<EnemyWarrior>(out EnemyWarrior _enemyWarrior) && !isFighting && other.isTrigger)
        {
            isFighting = true;
            enemyWarrior = _enemyWarrior;
            
            if (warriorType == WarriorType.Bowman && !isHitTarget) //Лучник
            {
                isHitTarget = true;
                
                AttackEnemy(other.gameObject);
                //Debug.Log("Call AttackEnemy ");

            }
            else
            {
                AttackEnemy(other.gameObject);
                movingController.SetDirection(other.transform.position - transform.position);
            }
        }
        else if (other.CompareTag(TagList.TowerGate)) //Коллайдер башні
        {
            enemyTower = other.gameObject.transform.parent.GetComponent<EnemyTower>();
            if (warriorType == WarriorType.Bowman && !isHitTarget) //Лучник
            {
                isHitTower = true;
                AttackTowerByBowman(other.gameObject, enemyTower);  
            }
            else
            {
                StartCoroutine(StartFightTower());
            }
        }
        else if (other.CompareTag(TagList.CastleGate) && !isOnTrigger) //Коллайдер замку
        {
            isOnTrigger = true;
            other.GetComponent<EnemyGate>().DamageEnemyCastle(damage);
            //Debug.Log(" Player Attack Enemy Castle --- " + "Damage = " + damage);
            StartAtackAnim();
            StartCoroutine(StartFightCastle());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<EnemyWarrior>(out EnemyWarrior enemy))
        {
            if (enemy == enemyWarrior)
            {
                gameObject.layer = LayerMask.NameToLayer(LayerList.PlayerWarrior);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.TryGetComponent<EnemyWarrior>(out EnemyWarrior enemy) || other.collider.CompareTag(TagList.CastleGate))
        {
            gameObject.layer = LayerMask.NameToLayer(LayerList.IsFighting);
            isHitTarget = true;
        }
        else if(other.collider.CompareTag(TagList.TowerGate))
        {
            enemyTower = other.gameObject.transform.parent.GetComponent<EnemyTower>();
            isHitTower = true;
        }
        else if (other.collider.CompareTag(TagList.EnemyProjectile)) //Коллайдер ядер-снярядів
        {
            slider.value = 0;
            Die();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyWarrior>(out EnemyWarrior enemy))
        {
            if (enemy == enemyWarrior){ }
        }
        gameObject.layer = LayerMask.NameToLayer(LayerList.PlayerWarrior);
    }
    public virtual void AttackTowerByBowman(GameObject target, EnemyTower enemyTower)
    {
        //enemyTower.DestroyTowerFromUnit(damage);
    }

    public override void TakeDamage(int damageFE)
    {
        base.TakeDamage(damageFE);
        slider.value -= damageFE;
    }
    public virtual void AttackEnemy(GameObject target)
    {
        StartCoroutine(StartFight());
    }
    IEnumerator StartFight()
    {
        yield return new WaitUntil(() => isHitTarget);

        
        while (isHitTarget && enemyWarrior.isLive)
        {
            StartAtackAnim();
            movingController.SetSpeed(0);
            yield return new WaitForSeconds(2);
            if (isLive)
            {
                enemyWarrior.TakeDamage(damage);
                SFXSourcesController.instance.SoundPlay(SoundType.UnitDamage);
                //Debug.Log("call SOUNDS + " + SoundType.UnitDamage);
            }
        }
        gameObject.layer  = LayerMask.NameToLayer(LayerList.PlayerWarrior);
        isHitTarget = false;
        isFighting = false;
        StartRun();
    }

    public void StartRun()
    {
        
        StartRunAnim();
        movingController.SetSpeed(baseSpeed);
        movingController.SetDirection(baseDirection);
        //Debug.Log(" START RUN  + " + baseSpeed + "  " + baseDirection);
    }
    IEnumerator StartFightCastle()
    {
        yield return new WaitForSeconds(3);
        slider.value = 0;
        Die();
        
    }
    IEnumerator StartFightTower()
    {
        //Debug.Log("I`m in StartFightTower + " + isHitTower);
        yield return new WaitUntil(() => isHitTower);
        while (isHitTower && !enemyTower.isTowerDestroy)
        {
            StartAtackAnim();

            yield return new WaitForSeconds(2);
            if (isLive)
            {
                enemyTower.DestroyTowerFromUnit(damage);
                //Debug.Log("Give Damage to Enemy Gate");
            }

        }
        isFighting = false;
        StartRun();
        isHitTower = false;

    }
}
