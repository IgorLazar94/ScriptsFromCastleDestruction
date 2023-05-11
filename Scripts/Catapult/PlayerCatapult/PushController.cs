using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PushController : MonoBehaviour
{
    public Trajectory trajectory;
    public static bool isTrueDirection = true;
    [SerializeField] float pushForce = 4f;
    [HideInInspector] public bool isCurrentProjectile = false;
    [HideInInspector] public bool correctTrajectory;
    private BoxCollider touchInputCollider;
    private Projectile projectile;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 direction;
    private Vector3 force;
    private float distance;
    private float offsetProjSpeed;
    private Sequence seq;

    private bool isActiveTutorial = false;
    private bool isFinishTutorial = false;
    private TutorTextBlock tutorText;
    private float maxDistancePushForce;

    private void Start()
    {
        maxDistancePushForce = GameSettings.Instance.GetMaxDistancePushForce();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            touchInputCollider = GetComponent<BoxCollider>();
            touchInputCollider.enabled = false;
        }

        offsetProjSpeed = GameSettings.Instance.GetPlayerProjectileSpeed();

    }

    private void OnEnable()
    {
        EventBus.onCreateNewProjectile += SetProjectile;

        InputControllerBase.onPlayerTap += OnDragStart;
        InputControllerBase.onPlayerDrag += OnDrag;
        InputControllerBase.onPlayerUp += OnDragEnd;

    }

    private void OnDisable()
    {
        EventBus.onCreateNewProjectile -= SetProjectile;

        InputControllerBase.onPlayerTap -= OnDragStart;
        InputControllerBase.onPlayerDrag -= OnDrag;
        InputControllerBase.onPlayerUp -= OnDragEnd;
    }

    private void SetProjectile(GameObject projectile)
    {
        this.projectile = projectile.GetComponent<Projectile>();
        isCurrentProjectile = true;
    }

    private void OnDragStart()
    {
        if (isCurrentProjectile)
        {
            projectile.DeactivateRb();
            var mousePos = Input.mousePosition;
            //mousePos.z = 25;  // or mousePosition.z find only coordinates of camera
            startPoint = mousePos;
            trajectory.ShowTrajectory();
        }
    }

    private void OnDrag()
    {
        if (isCurrentProjectile)
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 25;
            if (mousePos.x < startPoint.x)
            {
                endPoint = mousePos;
            }
            // limit input on y axis ?

            distance = Vector2.Distance(startPoint, endPoint);
            direction = (startPoint - endPoint).normalized;
            float newForce = distance * (pushForce / 10);
            newForce = Mathf.Min(newForce, maxDistancePushForce);
            force = direction * newForce;
            trajectory.UpdateDots(projectile.projPos, force);
        }
    }

    private void OnDragEnd()
    {
        if (isTrueDirection/* && distance > 5f*/)
        {
            //TutorialPhase();
            if (isActiveTutorial && !isFinishTutorial)
            {
                tutorText.HideTutorText();
                isActiveTutorial = false;
                isFinishTutorial = true;
            }


            PlayerKingController.onPushProjectile.Invoke();
            gameObject.GetComponentInChildren<Animator>().SetTrigger("ReadyToPush");
            PushProjectile();
            trajectory.HideTrajectory();
            projectile.isFlying.Invoke();
            UseSpecificSkillsProjectile();
            MoveProjectileAlongTrajectory(projectile);
            gameObject.GetComponent<SpawnPlayerProjectiles>().StartChangeState();
        }
    }

    private void PushProjectile()
    {
        projectile.isStartFly = true;
        isCurrentProjectile = false;
        projectile.ActivateRb();
        projectile.ActivateCollider();
        projectile.Push(force);
    }

    private void UseSpecificSkillsProjectile()
    {
        if (projectile is SplitShot)
        {
            projectile.GetComponent<SplitShot>().StartDivision(endPoint);
        }
    }

    private void MoveProjectileAlongTrajectory(Projectile projectile)
    {
        StartCoroutine(StartMovingProjectile(projectile));
        projectile.EnableMoveAlongTrajectory();
    }

    private IEnumerator StartMovingProjectile(Projectile _projectile)
    {

        int index = 0;
        bool isMoving = true;
        while (isMoving && _projectile != null)
        {
            yield return new WaitForFixedUpdate();
            if (_projectile != null && _projectile.moveAlongTrajectory)
            {
                Vector3 direction = (trajectory.dotsList[index].position - _projectile.transform.position);
                _projectile.projRB.velocity = direction.normalized * offsetProjSpeed;

                if (Vector3.Distance(_projectile.transform.position, trajectory.dotsList[index].position) < 1.5f)  // ���������� � 1f
                {
                    if (index < trajectory.dotsList.Length - 1)
                    {
                        index++;
                    }
                }
                if (index == (trajectory.dotsList.Length - 1))
                {
                    _projectile.projRB.AddForce(direction, ForceMode.Impulse);
                    _projectile.projRB.useGravity = true;
                    isMoving = false;
                }
            }
        }
    }
    public void ShowPushTutorial()
    {
        touchInputCollider.enabled = true;
        tutorText = GetComponentInChildren<TutorTextBlock>();
        tutorText.ShowTutorText();
        isActiveTutorial = true;
    }
}
