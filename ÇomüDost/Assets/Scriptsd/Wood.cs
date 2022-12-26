using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    // ikinci Script 18.12.22  22.50

    public float Angle = 0f;

    public float TurnSpeed,TurnMin,TurnMax;
    public float Timer, TimerMin, TimerMax;

    Animator anim;

    public GameObject Particle;
    public GameObject pofuduk;
    public Transform[] Poses;
    public List<int> list = new List<int>();
    public List<int> Randomlist = new List<int>();

    private bool Reject;
    private bool canBreak;
    void Start()
    {
        int m_appleCount = Random.Range(0, 4);
        IList();
        GenerateApplePrefab(m_appleCount);
        StartCoroutine(ChangeDirection());
        anim = GetComponent<Animator>();
       
    }

    public void Test()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            GameObject pof = Instantiate(pofuduk,Poses[getRandomValue()].position,Quaternion.identity);
            pof.transform.SetParent(gameObject.transform);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Reject = !Reject;
        }

    }
     public void GenerateApplePrefab(int m_count)
    {

        for (int i = 0; i < m_count; i++)
        {
             GameObject pof = Instantiate(pofuduk,Poses[getRandomValue()].position,Quaternion.identity);
         
            pof.transform.SetParent(gameObject.transform);
            pof.transform.localScale = new Vector2(0.123696f, 0.11709f);
        }   

    }
    void IList()
    {
        for (int i = 0; i < 8; i++)
        {
            list.Add(i);
        }
    }
    void Update()
    {
        Turn();
        Test();
        if (Reject)
        {
            foreach (Transform childs in transform)
            {
                childs.SetParent(null);
                if (childs.GetComponent<Rigidbody2D>() != null)
                {
                    childs.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Break();
        }
    }
    public void Turn()
    {
        Angle += TurnSpeed *  Time.deltaTime;
        transform.SetEulerAnglesZ(Angle);

    }
    public int getRandomValue()
    {
       
        int random = Random.Range(0, list.Count);
        int value = list[random];
        list.RemoveAt(random);
        Randomlist.Add(value);
        return value;

    }
   
    public void Break()
    {
        Reject = true;
        GameObject par = Instantiate(Particle, transform.position, Quaternion.identity);
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.gameObject);
    }

    IEnumerator ChangeDirection()
    {
        TurnSpeed = Random.Range(TurnMin, TurnMax);
        Timer = Random.Range(TimerMin, TimerMax);
        yield return new WaitForSeconds(Timer);
        StartCoroutine(ChangeDirection());
    }
}
