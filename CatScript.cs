using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    [SerializeField] GameObject CatAnimObj;
    private Animator anim_cat;
    [SerializeField] GameObject Path_following_obj;
    private Follow_the_path_script path_script;

    public CatInputHandler CatInput;

    public Path_Script PathToCircle;
    public Path_Script PathToThePlayer;

    void Start()
    {
        anim_cat = CatAnimObj.GetComponent<Animator>();
        path_script = Path_following_obj.GetComponent<Follow_the_path_script>();
    }

    void Update()
    {
        if (CatInput.lookingAtCat)
        {
            path_script.PathToFollow = PathToThePlayer;

        }
        else if (!CatInput.pettingTheCat && !path_script.Cat_is_moving)
        {
            path_script.CurrentWayPointID = 0;
            path_script.PathToFollow = PathToCircle;
            Invoke("CatMoveChangeToTrue", 1f);
            anim_cat.SetBool("PlayerNerby", false);
        }
        else if (!CatInput.pettingTheCat && path_script.Cat_is_moving)
        {
            path_script.PathToFollow = PathToCircle;
        }


        if (path_script.PathToFollow == PathToThePlayer && path_script.CurrentWayPointID >= path_script.PathToFollow.path_objects.Count)
        {
            path_script.Cat_is_moving = false;
            anim_cat.SetBool("PlayerNerby", true);
            CancelInvoke();
        }

        if(!path_script.Cat_is_moving && CatInput.pettingTheCat)
            anim_cat.SetBool("Pet", true);
        else
            anim_cat.SetBool("Pet", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("PlayerIn");
            path_script.Cat_is_moving = false;
            anim_cat.SetBool("PlayerNerby", true);
            CancelInvoke();
        } 

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("PlayerOut");
            Invoke("CatMoveChangeToTrue", 1f);
            anim_cat.SetBool("PlayerNerby", false);
        }
    }

    private void CatMoveChangeToTrue()
    {
        path_script.Cat_is_moving = true;
    }
}
