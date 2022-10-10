using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int rutina;
    public float cronometro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;
    public int vel = 4;

    public GameObject tarjet;

    public AudioSource rugido;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();

        tarjet = GameObject.Find("Player");
    }


    public void Comportamiento_Enemigo()
    {

        if (Vector3.Distance(transform.position, tarjet.transform.position) > 20)
        {

            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {

                case 0:
                    ani.SetBool("walk", false);
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(vel * Time.deltaTime * Vector3.forward);
                    ani.SetBool("walk", true);
                    break;


            }

        }
        else
        {
            rugido.Play();
            ani.SetBool("walk", true);

            var lookPos = tarjet.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);

            transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        }

        

    }



    // Update is called once per frame
    void Update()
    {
        Comportamiento_Enemigo();
    }
}
