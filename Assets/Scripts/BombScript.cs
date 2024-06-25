using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScripts : MonoBehaviour
{

    public float explosionDelay = 5f;
    public GameObject ExplosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        

    }


    private IEnumerator ExplosionCoroutine()
    {
        // wait
        yield return new WaitForSeconds(explosionDelay);


        // explode
        Explode();
    }


    private void Explode()
    {

        Instantiate(ExplosionPrefab, transform.position, ExplosionPrefab.transform.rotation);

        Destroy(gameObject);        
    
    }
}

