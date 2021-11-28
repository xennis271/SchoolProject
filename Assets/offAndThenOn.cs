using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offAndThenOn : MonoBehaviour
{
    public GameObject objectToTurnOffAndOnAgain;
    
    // Start is called before the first frame update
    public void offAndOnMethod()
    {
        if(objectToTurnOffAndOnAgain.activeSelf == true)
        {
            objectToTurnOffAndOnAgain.SetActive(false);
        }else{
            objectToTurnOffAndOnAgain.SetActive(true);
        }
    }
}
