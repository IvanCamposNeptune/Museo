using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Disparo : MonoBehaviour
{

    public GameObject enemy;


    public int vida = 3;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Disparo")
        {
            vida -= 1;
            if (vida == 0)
            {
                Destroy(enemy.gameObject);
            }
            
        }

    }



        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
