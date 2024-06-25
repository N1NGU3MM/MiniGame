using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    public float Delay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BeginSelfDestructon());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator BeginSelfDestructon()
    {
        yield return new WaitForSeconds(Delay);
        Destroy(gameObject);
    }
}
