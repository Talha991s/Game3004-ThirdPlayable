using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public GameObject bullet;

    // running speed
    public float speed;
    // jumping power
    public float jumpForce;

    // joystic object
    public Joystick joystick;

    public Rigidbody rb;
    public bool grounded;
    public float sensitivity;

    
    Vector3 direction;
    float hor;
    float vert;
    public float turnSmoothVelocity;
    public float turnSmoothTime;
    public float angle;
    Animator anim;
    [SerializeField] bool isMoving;

    public enum ControlSettings
    {
        DESKTOP,
        MOBILE
    }

    public ControlSettings controlSettings;
    public bool inventory;
    

    /// Events
    void Start()
    {
       // FindObjectOfType<SoundManager>().Play("Theme");

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (controlSettings == ControlSettings.DESKTOP)
        {
            Controls();
        }
        else if (controlSettings == ControlSettings.MOBILE)
        {
            ControlsMobile();
        }
    }

    void Controls()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (!inventory)
            {
                GameObject.FindGameObjectWithTag("Inventory").GetComponent<PlayerInventory>().OpenInvetory();
                inventory = true;
            }
            else
            {
                GameObject.FindGameObjectWithTag("Inventory").GetComponent<PlayerInventory>().CloseInvetory(true);
                inventory = false;
            }

        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

   

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            angle -= 125 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            angle += 125 * Time.deltaTime;
        }
        // Side to Side Movemnt
        if (Input.GetKey(KeyCode.A))
        {
            hor = -1;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            hor = 1;
        }
        else
        {
            hor = 0;
        }

        // Forward and Back movement
        if(Input.GetKey(KeyCode.W))
        {
            vert = 1;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            vert = -1;
        }
        else
        {
            vert = 0;
        }

        direction = new Vector3(hor, 0, vert);

        float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0, smoothedAngle, 0);

        if (isMoving)
        {
            anim.SetInteger("AnimationPar", 1);
            

            Vector3 moveDir = gameObject.transform.forward * vert;
            transform.position += moveDir * speed * Time.deltaTime;
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);
        }
    }

    void ControlsMobile()
    {
        Vector2 dir = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;


        if (dir.magnitude >= sensitivity)
        {
            anim.SetInteger("AnimationPar", 1);
            angle = Mathf.Atan2(dir.x, dir.y) * 180 / Mathf.PI;
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, smoothedAngle, 0);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);
        }


       

    }

    public void Shoot()
    {
        anim.SetTrigger("Shoot");
        Vector3 offset = transform.forward * 0.3f;
        GameObject b = Instantiate(bullet, transform.position + offset, new Quaternion(0, 0, 0, 0));
        b.GetComponent<LazerScript>().direction = transform.forward;
    }

    public void Jump()
    {
        if (grounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            FindObjectOfType<SoundManager>().Play("jump");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        grounded = true;
    }
    private void OnCollisionExit(Collision other)
    {
        grounded = false;
    }

    // Added OnCollisionStay as I was getting inconsistent collision with imported assets
    // Also added Ground tag so you aren't able to jump along walls
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            grounded = true;
        }
    }
}
