using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{

    public float runSpeed = 14;
    public float rotationSpeed = 250;

    public Animator animator;

    private float x, y;



    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    //Textos del HUD
    public Transform vrCamera;
    Rigidbody rb;
    public float AlturaPersonaje=2f;
    public Text TextVidas;
    public Text TextPociones;
    public Text TextMonedas;
	public Text TextPositionX;
	public Text TextPositionY;
	public Text TextPositionZ;
	public Text TextOrientacion;
	public Text TextAzimut;
    public Text NombreUsuario;
    public Text TextMuertes;
	int contadorVidas = 3;
    int contadorMonedas;
    int contadorPociones;
    int contadorMuertes;

    //////// Teleport Trigger  ////
    public GameObject avatar; 
    public float Xposition=0f;
    public float Yposition=0.0f;
    public float Zposition=0f;
    public string sceneName;
    public string sceneName2;

    ///////// Disparo de arma ///////////
    public GameObject Bala;
    public Transform SpawnPoint;
    public float shotForce = 1500;
    public float shotRate = 0.5f;
    private float shotRateTime = 0;


    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        TextVidas.text = "Vidas: " + contadorVidas;
    }

    public void OnTriggerEnter(Collider other)
	{
		 
        if(other.tag =="Telepuerto_1")
        {
            SceneManager.LoadScene (sceneName);
        }
        if(other.tag =="Telepuerto_2")
        {
            SceneManager.LoadScene (sceneName2);
        }

        if(other.tag =="Teletrans")
        {
            avatar.transform.position = new Vector3(Xposition, Yposition, Zposition);
        }

         if(other.tag == "PortalPrincipal")
         {
           Destroy(other.gameObject);
           //SceneManager.LoadScene ("Animacion");
           
         }

         if(other.tag == "ItemVidas")
         { 
            Destroy(other.gameObject);
            contadorVidas=contadorVidas+1;
            TextVidas.text ="Vidas: "+contadorVidas; 
         }
         if(other.tag == "Enemy")
         {
            contadorVidas = contadorVidas - 1;
            TextVidas.text = "Vidas: " + contadorVidas;
         }

         if(other.tag == "ItemPociones")
         { 
            Destroy(other.gameObject);
            contadorPociones=contadorPociones+1;
            TextPociones.text =""+contadorPociones;
         }

          if(other.tag == "ItemMonedas")
         { 
            Destroy(other.gameObject);
            contadorMonedas=contadorMonedas+1;
            TextMonedas.text =""+contadorMonedas;
             
            //TextPositionX.text="X:   "+Time.deltaTime; 
         }
		//TextPositionX.text="X:   "+Time.deltaTime;
	}
	void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /*void FixedUpdate()
    {
        TextPositionX.text  =""+Mathf.RoundToInt(transform.position.x);
    	TextPositionY.text  =""+Mathf.Round(transform.position.y + AlturaPersonaje);
    	TextPositionZ.text  =""+Mathf.RoundToInt(transform.position.z);
    	TextOrientacion.text=""+Mathf.RoundToInt(vrCamera.eulerAngles.y);
    	TextAzimut.text     =""+Mathf.RoundToInt((vrCamera.eulerAngles.x - 360f)* -1f);
    }*/


    void Update()
    {
        //Disparo
        if(Input.GetButtonDown("Fire1"))
        {
            if(Time.time>shotRateTime)
            {
                GameObject newBala;
                newBala = Instantiate(Bala,SpawnPoint.position, SpawnPoint.rotation);
                newBala.GetComponent<Rigidbody>().AddForce(SpawnPoint.forward*shotForce);
                shotRateTime = Time.time + shotRate;
                Destroy (newBala, 2);
            }
        }

        //Animacion
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, y * Time.deltaTime * runSpeed);
        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);


        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }


    }
}

