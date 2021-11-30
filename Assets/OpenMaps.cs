using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMaps : MonoBehaviour
{
    // Start is called before the first frame update
    public void OpenMap() {
        Application.OpenURL("http://maps.google.com/maps?q=hospital-near-me");
    }
}
