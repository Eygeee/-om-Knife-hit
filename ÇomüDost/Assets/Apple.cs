using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public GameObject particle;
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("knife"))
        {
            Slash();
        }
    }

    public void Slash()
    {

        GameObject sl = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(sl, 1f);
        Destroy(this.gameObject);
    }
}
