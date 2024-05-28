using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanPartsScript : MonoBehaviour
{
    public List<GameObject> ObjectsOnSnowman = new List<GameObject>();
    public List<GameObject> ObjectsOnTheGround = new List<GameObject>();
    [SerializeField] private LayerMask layer;
    public ParticleSystem particles;
    bool isLooking = false;

    void Start()
    {
        Stopparticles();
    }


    void Update()
    {
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 70, layer);
        //Debug.DrawLine(transform.position, hit.point, Color.green);
        if (hit.collider != null && !isLooking)
        {
            isLooking = true;
            for(int i = 0; i < ObjectsOnTheGround.Count; i++)
            {
                if (ObjectsOnTheGround[i] == hit.transform.gameObject)
                {
                    ObjectsOnSnowman[i].SetActive(true);
                    particles.transform.position = ObjectsOnTheGround[i].transform.position;
                    particles.Play();
                    Invoke("Stopparticles", 1.5f);
                    ObjectsOnTheGround[i].SetActive(false);
                }
            }
        }
        else if (hit.collider == null && isLooking)
        {
            isLooking = false;

        }
    }

    private void Stopparticles()
    {
        particles.Stop();
    }
}
