using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class KnifeScript : MonoBehaviour
{
    public AudioClip onhitwood;
    public AudioClip onhitknife;

    public ParticleSystem Particle;

    public UnityEvent WoodHitEvent;
    public UnityEvent onTriggerEvent;
    public GameObject Wood;

    Rigidbody2D rb;
    void Start()
    {
       
    }
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        Wood = GameObject.FindWithTag("Wood");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        
        
        
        if (collision.gameObject.CompareTag("knife"))
        {

            foreach (ContactPoint2D hitpos in collision.contacts)
            {
                if (hitpos.normal.y < 0)
                {

                    AudioManager.instance.Source(onhitknife);
                    KnifeManager.instance.GameOver();

                }

            }
           
        }
        if (collision.gameObject.CompareTag("Wood"))
        {
           
            foreach (ContactPoint2D hitpos in collision.contacts)
            {
                if (hitpos.normal.y < 0)
                {
          
               
                    Wood.GetComponent<Animator>().SetTrigger("hit");
                    WoodHit(0);
                   
                }
               
            }
        }

      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wood"))
        {
            WoodHit(1);
        }
    }
    void WoodHit(int value)
    {
        Particle.Play();
        KnifeManager.instance.PlusKnife();
        AudioManager.instance.Source(onhitwood);
        // CameranScript.instance.CameraAnimation("shake");
        switch (value)
        {
           
           
            case 0:

               WoodHitEvent.Invoke();
                break;
            case 1:
                onTriggerEvent.Invoke();
                break;
            default:
                break;
        }
       
    }

    public void Stick()
    {
        this.gameObject.transform.SetParent(Wood.transform);
        
    }
    public void ResetForce()
    {
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
    }
    public void Break()
    {
        
        Wood.GetComponent<Wood>().Break();

    }

}
