using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInputHandler : MonoBehaviour
{
    private TeleportationScript TpScript;
    [SerializeField] private LayerMask layer;

    public bool lookingAtCat = false;
    public bool pettingTheCat = false;

    void Start()
    {
        TpScript = this.gameObject.GetComponent<TeleportationScript>();
    }


    void Update()
    {
        if (Physics.Raycast(TpScript.PlayerCam.transform.position, TpScript.PlayerCam.transform.TransformDirection(Vector3.forward), 
            out RaycastHit hit, 100, layer, QueryTriggerInteraction.Collide))
        {
            //Debug.Log(hit.transform.gameObject.name);
            
            if (hit.transform.gameObject.name == "CatHolder")
                lookingAtCat = true;
        }
        else
            lookingAtCat = false;


        if (Physics.Raycast(TpScript.PlayerCam.transform.position, TpScript.PlayerCam.transform.TransformDirection(Vector3.forward),
            out RaycastHit hit2, 100, layer, QueryTriggerInteraction.Ignore))
        {
            //Debug.Log(hit2.transform.gameObject.name);

            if (hit2.transform.gameObject.name == "PetCollider")
                pettingTheCat = true;
        }
        else
            pettingTheCat = false;
    }
}
