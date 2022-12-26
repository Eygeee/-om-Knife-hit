using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Util
{ 
    
    // ilk Script 18.12.22  22:44

    public static readonly Vector3 VECTOR3_ZERO = Vector3.zero;
    public static readonly Vector3 VECTOR3_ONE = Vector3.one;

    public static void SetPosition(this Transform self, float x, float y, float z)
    {
       VECTOR3_ZERO.Set(x, y, z);
        self.position = VECTOR3_ZERO;
    }

  

    public static void SetPositionX(this Transform self, float x)
    {
        self.SetPosition(x, self.position.y, self.position.z);
    }

    public static void SetPositionY(this Transform self, float y)
    {
        self.SetPosition(self.position.x, y, self.position.z);
    }
    public static void SetEulerAnglesZ(this Transform self, float z)
    {
        Vector3 selfAngles = self.eulerAngles;
        self.rotation = Quaternion.Euler(selfAngles.x, selfAngles.y, z);
    }

}
