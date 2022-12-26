using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KnifeManager : MonoBehaviour
{
    
    public static KnifeManager instance;


   
    public Transform SpawnPosition;
   
    public Text m_text;

  

  
    [Header("--------SETTÝNGS--------")]

    public int Plus;
    public int KnifeCount;
    public int attachedKnife;
    public float Force;
    [Header("--------GAMEOBJECTS--------")]

    public GameObject BossUI;
    public GameObject[] Knifes;
    public List<GameObject> KnifesUI = new List<GameObject>();
    public List<GameObject> KnifesPrefabs = new List<GameObject>();
    public GameObject Wood;
    public GameObject EndGameUI;

    [Header("--------UI--------")]

    public GameObject KnifesUIParent;
    public GameObject KnifeUIprefab;
    public Transform knifeUIPosition;
    public float intervalUI;

    public GameObject[] lights;
    public int LightNumber;

   
    [Header("--------STAGES---------")]
    public List<int> BossStagesNumbers = new List<int>();
    public int StageNum;
    private bool didPass;
    [Header("--------SFX---------")]
    public AudioClip bossWarn;
   
    private void Awake()
    {
        instance = this;
        KnifeCount = Plus;
       
    }
    void Start()
    {
        FilltheKnifeUI();
        LightsCheck();
    }

    
    void Update()
    {   
        Hit();
        GameManage();

    }

    public void GameManage()
    {

        m_text.text = ("Stage" + "  " + StageNum.ToString());
        if (attachedKnife >= Plus && Last() && !didPass)
        {

            StartCoroutine(PassTimer());
            //PassNewStage();
        }
    }
    
    
   public void  Hit()
   {
        if (Input.GetButtonDown("Fire1"))
        {
            if (KnifeCount > 0)
            {          
                KnifeCount--;
                Spawn();              
            }      
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ReleaseAll();
        }
    
    }
    public void Spawn()
    {
        ChangeColorofKnifeUI(KnifeCount);
        int index = KnifeCount > 0 ? 0 : 1;
         
        GameObject m_knife = Instantiate(Knifes[index], SpawnPosition.position,SpawnPosition.rotation);
        m_knife.GetComponent<Rigidbody2D>().AddForce(Vector2.up*Force* Time.fixedDeltaTime, ForceMode2D.Impulse);
        KnifesPrefabs.Add(m_knife);
    }
    public bool Last()
    {
        if (KnifeCount <= 0)
        {
          
            return true;
        }
        return false;
    }
    public void ReGenarate()
    {
        int m_KnifeCount = Random.Range(10, 13);
        KnifeCount = m_KnifeCount;
    }
    public void FilltheKnifeUI()
    {
        Vector2 UIPosition = knifeUIPosition.transform.position;
        Vector3 UIrot = knifeUIPosition.rotation.eulerAngles;
        UIrot = new Vector3(UIrot.x, UIrot.y, 110);
        for (int i = 0; i < KnifeCount; i++)
        {
            GameObject UI = Instantiate(KnifeUIprefab, UIPosition,Quaternion.Euler(UIrot));
            KnifesUI.Add(UI);
            UI.gameObject.transform.SetParent(KnifesUIParent.transform);
            UIPosition.y += intervalUI;  
        }
    }
    //Knife UI RENK DEÐÝÞÝMÝ
    private GameObject SelectedKnifeUI;
    SpriteRenderer sp;
    public void ChangeColorofKnifeUI(int index)
    {
        SelectedKnifeUI = KnifesUI[index];
        sp = SelectedKnifeUI.GetComponent<SpriteRenderer>();
        sp.color = Color.black;
    }
    //Yeni Stage Geçiþ

   
    public void PassNewStage()
    {
         LightsCheck();
      
       ReleaseAll();
        if (BossStage())
        {
          
            Debug.Log("boss geliyor la");
            StartCoroutine(BossOpening());
        }
        else
        {
           
           
            ReGenarate();
            FilltheKnifeUI();
            SpawnWood();
        }
      //  Plus = KnifeCount;
        attachedKnife = 0;
        didPass = false;
    }
    IEnumerator PassTimer()
    {
        didPass = true;
        StageNum++;
      
        
        yield return new WaitForSeconds(1f);
        PassNewStage();
        StopCoroutine(PassTimer());
    }

    IEnumerator BossOpening()
    {
        AudioManager.instance.Source(bossWarn);
        BossUI.SetActive(true);

        yield return new WaitForSeconds(1.15f);

        BossUI.SetActive(false);

        ReGenarate();
        FilltheKnifeUI();
        SpawnWood();
        

    }
    //yenilerini oluþturmka için eskilerini silen Void
    public void ReleaseAll()
    {
        foreach (Transform child in KnifesUIParent.transform)
        {
            KnifesUI.Remove(child.gameObject);
            Destroy(child.gameObject);
        }

        KnifesPrefabs.RemoveAll(items => items != null);
    }
    public void LightsCheck()
    {

        LightNumber++;
            
         if (LightNumber > 5)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].GetComponent<SpriteRenderer>().color = Color.black;
            }
            LightNumber = 1;
        }

        GameObject currentLight = lights[LightNumber-1];
        currentLight.GetComponent<SpriteRenderer>().color = Color.green;

     
     

     

    }
    // BOSS un gelip gelmiçeðini Kontrol eden bool
    public bool BossStage()
    {
        if (BossStagesNumbers.Contains(StageNum))
        {
            BossStagesNumbers.Remove(StageNum);
            return true;
        }
        return false;

    }
    public void SpawnWood()
    {
        //SpawnWood
        GameObject wood = Instantiate(Wood, new Vector2(0, 2.83f), Quaternion.identity);

    }
    public void GameOver()
    {
        EndGameUI.SetActive(true);
        Time.timeScale = 0;
       
       
    }
    public void PlusKnife()
    {
        attachedKnife++;
    }
}
