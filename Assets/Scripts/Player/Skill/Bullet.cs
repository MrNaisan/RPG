using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Bullet : MonoBehaviour
{
    public float speed = 30;
    public float slowDownRate = 0.01f;
    public float detectionDistance = 0.1f;
    public float destroyDelay = 5f;
    public float objectsToDetachDelay = 2;
    public List<GameObject> objectsToDetach = new List<GameObject>();
    [Space]
    public float erodeInRate = 0.06f;
    public float erodeOutRate = 0.03f;
    public float erodeRefreshRate = 0.01f;
    public float erodeAwayDelay = 1.25f;
    public List<SkinnedMeshRenderer> objectsToErode = new List<SkinnedMeshRenderer>();
    public VisualEffect Particle;

    private Rigidbody rb;
    private bool stopped;
    private bool isParticlePlay;

    private void Start() 
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);  

        if(GetComponent<Rigidbody>() != null)
        {
            rb = GetComponent<Rigidbody>();
            StartCoroutine(SlowDown());
        }

        if(objectsToDetach != null)
            StartCoroutine(DetachObjects());
        
        if(objectsToErode != null)
            StartCoroutine(ErodeObjects());

    }

    private void FixedUpdate() 
    {
        if(!stopped)
        {
            RaycastHit hit;
            Vector3 distance = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

            if(Physics.Raycast(distance, transform.TransformDirection(-Vector3.up), out hit, detectionDistance, 2))
            {
                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }

            Debug.DrawRay(distance, transform.TransformDirection(-Vector3.up * detectionDistance), Color.red);
        }    
    }

    IEnumerator SlowDown()
    {
        float t = 1;

        while(t > 0)
        {
            rb.velocity = Vector3.Lerp(Vector3.zero, rb.velocity, t);
            t -= slowDownRate;
            yield return new WaitForSeconds(0.1f);
        }

        stopped = true;
    }

    IEnumerator DetachObjects()
    {
        yield return new WaitForSeconds(objectsToDetachDelay);

        for(int i = 0; i < objectsToDetach.Count; i++)
        {
            objectsToDetach[i].transform.parent = null;
            Destroy(objectsToDetach[i], objectsToDetachDelay);
        }
    }

    IEnumerator ErodeObjects()
    {
        for(int i = 0; i < objectsToErode.Count; i++)
        {
            float t = 1;

            while(t > 0)
            {
                t -= erodeInRate;
                objectsToErode[i].material.SetFloat("_Erode", t);
                yield return new WaitForSeconds(erodeRefreshRate);
            }
        }

        yield return new WaitForSeconds(erodeAwayDelay);

        for(int i = 0; i < objectsToErode.Count; i++)
        {
            float t = 0;

            while(t < 1)
            {
                t += erodeOutRate;
                objectsToErode[i].material.SetFloat("_Erode", t);
                yield return new WaitForSeconds(erodeRefreshRate);
                if(t > 0.3 && !isParticlePlay)
                {
                    isParticlePlay = true;
                    Particle.Play();
                }
            }
        }
        Destroy(gameObject);
    }
}
