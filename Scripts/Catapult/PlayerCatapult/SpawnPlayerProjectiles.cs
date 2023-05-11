using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerProjectiles : MonoBehaviour
{
    public static System.Action levelUpProjectile;
    [HideInInspector] public float reloadTime;

    [SerializeField] private BallFactory _ballFactory;
    [SerializeField] private FireBallFactory _fireBallFactory;
    [SerializeField] private SplitShotFactory _splitShotFactory;

    [SerializeField] private int playerProjectileLevel;
    private Timer reloadTimer;

    private void Start()
    {
        reloadTimer = gameObject.GetComponent<Timer>();
        playerProjectileLevel = 1;
        InstantiateNewProjectile();
        reloadTime = GameSettings.Instance.GetPlayerReloadTime();
    }

    private void OnEnable()
    {
        levelUpProjectile += LevelUp;
    }

    private void OnDisable()
    {
        levelUpProjectile -= LevelUp;
    }

    public void StartChangeState()
    {
        StartCoroutine(ChangeState());
    }

    private IEnumerator ChangeState()
    {
        reloadTimer.onStartReloaded.Invoke();
        yield return new WaitForSeconds(reloadTime);
        if (!IdleState.isIdleStateActive)
        {
            ShotState.onProjectileDestroyed.Invoke(); // switch to idle state
        }
        InstantiateNewProjectile();
    }

    private void InstantiateNewProjectile()
    {
        if (playerProjectileLevel == 1)
        {
            var prefabBall = _ballFactory.GetNewInstance();
        }
        else if (playerProjectileLevel == 2)
        {
            var prefabFireBall = _fireBallFactory.GetNewInstance();
        }
        else if (playerProjectileLevel >= 3)
        {
            var prefabSplitShot = _splitShotFactory.GetNewInstance();
        }
    }

    private void LevelUp()
    {
        playerProjectileLevel++;
    }
}
