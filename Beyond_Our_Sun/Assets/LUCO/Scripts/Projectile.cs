using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Lifetime;
    public int layerNumber;

    // Update is called once per frame
    void Update()
    {
        Lifetime -= Time.deltaTime;
        if(Lifetime <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layerNumber)
        {
            //collision.gameObject.GetComponent<Health>();
            Debug.Log(other.gameObject.name);
            Destroy(this.gameObject);
        }
    }
}
