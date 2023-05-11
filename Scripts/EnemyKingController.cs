using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKingController : MonoBehaviour
{
    [SerializeField] private ParticleSystem magicDefense;
    [SerializeField] private ParticleSystem breakMagicDefense;
    [SerializeField] private TutorTextBlock tutorTextBlock;
    [SerializeField] private GameObject winPanel;
    [HideInInspector] public bool isEnablePlayerDefense = true;
    private Animator animator;
    private List<Rigidbody> RigidbodiesList = new List<Rigidbody>();

    private bool isActiveStageTutor = false;
    private bool isFinishTutorial = false;

    //public static Action onMagicDefense;
    public static System.Action onPushEnemyProjectile;

    private void OnEnable()
    {
        //onMagicDefense += EnableDefenseParticle;   

        onPushEnemyProjectile += StartPushAnimation;

        if (tutorTextBlock != null)
        {
            TutorTextBlock.OnTutorObjectsActivate += ShowTutorialTextBlock;
        }
    }


    private void OnDisable()
    {
        //onMagicDefense -= EnableDefenseParticle;

        onPushEnemyProjectile -= StartPushAnimation;

        if (tutorTextBlock != null)
        {
            TutorTextBlock.OnTutorObjectsActivate -= ShowTutorialTextBlock;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        //magicDefense = GetComponent<ParticleSystem>();
        FillRigidbodies();
    }

    private void FillRigidbodies()
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            RigidbodiesList.Add(rigidbodies[i]);
            rigidbodies[i].isKinematic = true;
        }
    }

    //private IEnumerator ActivateDefenseParticle()
    //{
    //    magicDefense.Play();
    //    yield return new WaitForSeconds(1.0f);
    //    magicDefense.Stop();
    //}
    public void StartDisableDefenseParticle()
    {
        StartCoroutine(DisableDefenseParticle());
    }
    private IEnumerator DisableDefenseParticle()
    {
        if (magicDefense != null)
        {
            isEnablePlayerDefense = false;
            //var sphereCollider = gameObject.GetComponent<SphereCollider>().enabled = false;
            magicDefense.Stop();
            breakMagicDefense.Play();
            ChangeKingLayer();
            yield return new WaitForSeconds(1.0f);
            Destroy(magicDefense);
        }
    }

    private void StartPushAnimation()
    {
        animator.SetTrigger("PushProjectile");
    }

    private void ChangeKingLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(LayerList.EnemyWarrior);
        for (int i = 0; i < RigidbodiesList.Count; i++)
        {
            RigidbodiesList[i].gameObject.layer = LayerMask.NameToLayer(LayerList.EnemyWarrior);
            RigidbodiesList[i].velocity /= 5;
        }
    }

    public void StartWinAnimation()
    {
        animator.SetBool("isWin", true);
    }

    private void EnableRegdoll()
    {
        animator.enabled = false;
        for (int i = 0; i < RigidbodiesList.Count; i++)
        {
            RigidbodiesList[i].isKinematic = false;
        }
        winPanel.gameObject.GetComponent<WinLoseBehaviour>().SetWinUI();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == TagList.Projectile)
        {
            HideTutorTextBlock();
            EnableRegdoll();
        }
    }

    private void ShowTutorialTextBlock()
    {
        if (TutorTextBlock.tutorLevelFromObjects == 3 && tutorTextBlock != null)
        {
            tutorTextBlock.ShowTutorText();
            isActiveStageTutor = true;
        }
    }

    private void HideTutorTextBlock()
    {
        if (isActiveStageTutor && !isFinishTutorial)
        {
            tutorTextBlock.HideTutorText();
            isActiveStageTutor = false;
            isFinishTutorial = true;
        }
    }
}
