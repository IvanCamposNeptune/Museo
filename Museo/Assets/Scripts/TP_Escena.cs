using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TP_Escena : MonoBehaviour
{
    public string sceneName;
    public string sceneName2;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void OnTriggerEnter(Collider Other)
    {
        if(Other.tag == "Player"){
            SceneManager.LoadScene(sceneName);
        }
        if(Other.tag == "Player"){
            SceneManager.LoadScene(sceneName2);
        }
    }
}
