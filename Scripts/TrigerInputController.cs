using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrigerInputController : MonoBehaviour
{
    private BoxCollider triggerCollider;
    private LayerMask ignoreLayerMask;

    private void Start()
    {
        triggerCollider = GetComponent<BoxCollider>();
        ignoreLayerMask = LayerMask.NameToLayer(LayerList.PlayerWarrior);
    }
    private void OnMouseDown()
    {
        //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        //if (hit.collider != null && hit.collider == triggerCollider)
        //{
            InputControllerBase.onTriggerTap.Invoke();
        //}

        //Vector3 viewportPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //Ray ray = Camera.main.ViewportPointToRay(viewportPoint);
        //RaycastHit hit;

        //if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayerMask))
        //{
        //    Debug.Log("trigger");
        //}
    }
}
