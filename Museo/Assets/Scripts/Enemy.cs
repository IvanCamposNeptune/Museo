﻿using System.Collections;
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

    public AudioSource rugido;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }


    public void Comportamiento_Enemigo()
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
                rugido.Play();
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



    // Update is called once per frame
    void Update()
    {
        Comportamiento_Enemigo();
    }
}
