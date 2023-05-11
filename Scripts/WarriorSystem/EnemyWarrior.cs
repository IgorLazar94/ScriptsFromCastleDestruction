using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWarrior : EnemyWarriorAbstract
{


    [SerializeField] MovingController movingController;
    [SerializeField] Vector3 baseDirection;

    private PlayerWarrior playerWarrior;
    private bool isHitTarget = false;
    private bool isHitTower = false;
    private bool isFighting = false;
    private bool isOnTrigger = false;
    private PlayerTower playerTower;

    [SerializeField] Slider slider;
    //[SerializeField] Animator animator;

    [Header("Weapon")]
    [SerializeField] Transform spownPoint;
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] Transform weaponParent;

    public void Awake()
    {
        tag = TagList.EnemyWarrior;
    }
    private void Start()
    {
        Health = MaxHealth;
        slider.maxValue = MaxHealth;
        slider.value = MaxHealth;

        transform.localScale *= scale; //Міняємо скейл для бійця
        GameObject enemyWeapon = Instantiate(weaponPrefab, spownPoint.position, Quaternion.Euler(0, 90, 0), weaponParent);
        movingController.SetDirection(baseDirection);
        movingController.SetSpeed(baseSpeed);
    }

        

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagList.PlayerWarrior) && !isFighting)
        {
           
            isFighting = true;
            playerWarrior = other.GetComponent<PlayerWarrior>();

            
            StartCoroutine(StartFight());

            movingController.SetDirection(other.transform.position - transform.position);
        }
        else if (other.CompareTag(TagList.TowerGate) && !isFighting) //Коллайдер ворот
        {
            isFighting = true;
            playerTower = other.gameObject.transform.parent.GetComponent<PlayerTower>();
            //isHitTower = true;

            StartCoroutine(StartFightTower());
        }
        else if (other.CompareTag(TagList.CastleGate) && !isOnTrigger) //Коллайдер замку
        {
            isOnTrigger = true;
            //Debug.Log(" Enemy Attack Player Castle ");
            other.GetComponent<PlayerGate>().DamagePlayerCastle(damage);

            StartCoroutine(StartFightCastle());
        }

        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerWarrior>(out PlayerWarrior player))
        {
            if (player == playerWarrior) { }
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<PlayerWarrior>(out PlayerWarrior player) || other.collider.CompareTag(TagList.CastleGate) )
        {
            gameObject.layer = LayerMask.NameToLayer(LayerList.IsFighting);
            isHitTarget = true;
            movingController.SetSpeed(0);
        }
        if (other.collider.CompareTag(TagList.TowerGate))
        {
            playerTower = other.gameObject.transform.parent.GetComponent<PlayerTower>();
            isHitTower = true;
        }
        else if (other.collider.CompareTag(TagList.Projectile)) //Коллайдер ядер-снярядів
        {
            slider.value = 0;
            Die();
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerWarrior>(out PlayerWarrior player))
        {
            isHitTarget = false;
            isFighting = false;
           /* movingController.SetDirection(baseDirection);
            movingController.SetSpeed(baseSpeed);*/
        }
        gameObject.layer = LayerMask.NameToLayer(LayerList.EnemyWarrior);
    }

    public override void TakeDamage(int damageFE)
    {
        base.TakeDamage(damageFE);
        slider.value -= damageFE;
    }

    IEnumerator StartFight()
    {
        yield return new WaitUntil(()=>isHitTarget);
        
        while (isHitTarget && playerWarrior.isLive && isFighting)
        {
            StartAtackAnim();
            
            yield return new WaitForSeconds(2);
            if (isLive)
            {
                playerWarrior.TakeDamage(damage);
                SFXSourcesController.instance.SoundPlay(SoundType.UnitDamage);
            }
            
        }
        gameObject.layer = LayerMask.NameToLayer(LayerList.EnemyWarrior);
        isFighting = false;
        isHitTarget = false;
        StartRun();
    }

    IEnumerator StartFightCastle()
    {
        StartAtackAnim();
        yield return new WaitForSeconds(3);
        slider.value = 0;
        Die();
    }

    IEnumerator StartFightTower()
    {
        
        yield return new WaitUntil(() => isHitTower);
        while (playerTower != null && !playerTower.isTowerDestroy && isHitTower)
        {
            StartAtackAnim();

            yield return new WaitForSeconds(2);
            if (isLive)
            {
                playerTower.DestroyTowerFromUnit(damage);
            }

        }
        isFighting = false;
        isHitTower = false;
        StartRun();

    }
    public void StartRun()
    {
        StartRunAnim();
        movingController.SetSpeed(baseSpeed);
        movingController.SetDirection(baseDirection);
    }
}
