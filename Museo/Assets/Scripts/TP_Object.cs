using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadinator : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoToscene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}
