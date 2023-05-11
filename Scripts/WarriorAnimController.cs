using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAnimController : MonoBehaviour
{
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAtackTime", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
