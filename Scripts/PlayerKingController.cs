//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKingController : MonoBehaviour
{

    [SerializeField] private ParticleSystem magicDefense;
    [SerializeField] private ParticleSystem breakMagicDefense;
    [HideInInspector] public bool isEnablePlayerDefense = true;
    private Animator animator;
    private List<Rigidbody> RigidbodiesList = new List<Rigidbody>();
    [SerializeField] private GameObject losePanel;


    //public static Action onMagicDefense;
    public static System.Action onPushProjectile;

    private void OnEnable()
    {
        //onMagicDefense += EnableDefenseParticle;   

        onPushProjectile += StartPushAnimation;
    }


    private void OnDisable()
    {
        //onMagicDefense -= EnableDefenseParticle;

        onPushProjectile -= StartPushAnimation;
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
    public void StartDisableDefenseParticle ()
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
        gameObject.layer = LayerMask.NameToLayer(LayerList.PlayerWarrior);
        for (int i = 0; i < RigidbodiesList.Count; i++)
        {
            RigidbodiesList[i].gameObject.layer = LayerMask.NameToLayer(LayerList.PlayerWarrior);
        }
    }

    public void StartWinAnimation()
    {
        animator.SetBool("isWin", true);
    }

    private void EnableRegdoll()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.left * 5);
        animator.enabled = false;
        for (int i = 0; i < RigidbodiesList.Count; i++)
        {
            RigidbodiesList[i].isKinematic = false;
        }
        losePanel.gameObject.GetComponent<WinLoseBehaviour>().SetLoseUI();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == TagList.EnemyProjectile)
        {
            EnableRegdoll();
        }
    }

   













}
