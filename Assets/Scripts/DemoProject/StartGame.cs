using SUIFW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Log.Write(GetType() + " Start()/");
        UIManager.GetInstance().ShowUIForms("LoginUIForm");
    }

   
}
