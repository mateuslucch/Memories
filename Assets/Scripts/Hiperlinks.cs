using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiperlinks : MonoBehaviour
{

    public void OpenPlayStore()
    {
        Application.OpenURL("market://details?id=com.MateusLucchese.com.unity.template.mobile2D");
    }

}
