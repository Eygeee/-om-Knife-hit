using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameranScript : MonoBehaviour
{
    public static CameranScript instance;

    Animator anim;
    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    public void CameraAnimation(string m_name,bool m_bool = false,bool m_animtrue=false)
    {

        if (m_bool) { anim.SetBool(m_name, m_animtrue); }
        else
        { anim.SetTrigger(m_name);  }
       


    }
}
