using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Megaman : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundLayer;

    Animator myAnimator;
    Rigidbody2D myBody;
    Collider2D myCollider;
    

    public GameObject bullet;
    public bool shooting;
    private float shoot_time;
    public float time;
    public GameObject point;//punto de disparo


    bool isGrounded;
    bool canDoubleJump;
    public float doubleJumpSpeed; //fuerza del segundo salto.

    public bool Dash;
    public float Dash_T;
    public float Speed_Dash;

    public float cooldown = 2; //cooldown para ritmo que salen las balas cada cierto tiempo
    private float nextFire;
 

    void Start()
    {

        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();       
    }

    
    void Update()
    {
        
        //MoverMegaman(); no usar por como se va a usar el dash
        Saltar();
        Caer();
        
        if (Time.time > nextFire)
        {
            Disparar();
        }

    }


    private void FixedUpdate()
    {
        Move();
        CollisionSalto();
        
    }


    void CollisionSalto ()
    {
        if (isGrounded)
        {
            DashAbility();
        }
    }



    void DashAbility()
    {
        if (Input.GetKey(KeyCode.X))
        {
            Dash_T += 1 * Time.deltaTime;
            if (Dash_T < 0.35f)
            {
                Dash = true;
                myAnimator.SetBool("isDash", true);
                transform.Translate(Vector3.right * Speed_Dash * Time.fixedDeltaTime);
                myAnimator.SetBool("takeof", false);
                myAnimator.SetBool("isRunning", false);
               
            }

            else
            {
                Dash = false;
                myAnimator.SetBool("isDash", false);

            }
        }

        else
        {
            Dash = false;
            myAnimator.SetBool("isDash", false);
            Dash_T = 0;
        }

    }

    void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) && !Dash)
        {
            myAnimator.SetBool("isRunning", true);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
            
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) && !Dash)
        {
            myAnimator.SetBool("isRunning", true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
            
        }
        
    }


    void Disparar ()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            nextFire = Time.time + cooldown;
            shoot_time = 0.1f;
            GameObject Firepoint = Instantiate(bullet, point.transform.position, transform.rotation) as GameObject;
            
            if (!shooting)
            {
                shooting = true;
            }
        }

        if (shooting)
        {
            shoot_time += 1 * Time.deltaTime;
            myAnimator.SetLayerWeight(0, 0);
            myAnimator.SetLayerWeight(1, 1);
        }

        

        if (shoot_time >=time)
        {
            myAnimator.SetLayerWeight(0, 1);
            myAnimator.SetLayerWeight(1, 0);
            shooting = false;
            shoot_time = 0;
        }


    }

    
    void terminarSaltar ()
    {
        myAnimator.SetBool("isFalling", true);
        myAnimator.SetBool("takeof", false);
    }

    void Saltar ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        

            if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && !myAnimator.GetBool("takeof"))
            {
                
                myAnimator.SetBool("takeof", false);
                myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                myAnimator.SetTrigger("jumping");
                myAnimator.SetBool("takeof", true);
                canDoubleJump = true;

            }
            else
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    if (canDoubleJump)
                    {
                        DoubleJump();
                        canDoubleJump = false;
                    }
                }
                
            } 
            
         
            
        }
        
    }

    void DoubleJump()
    {
        myBody.AddForce(new Vector2(0, doubleJumpSpeed), ForceMode2D.Impulse);
        myAnimator.SetTrigger("jumping");
        myAnimator.SetBool("takeof", true);
    }

    void Caer ()
    {
        if (!myAnimator.GetBool("takeof") && myBody.velocity.y < 0 && !myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("isFalling", true);
        }
        else
        {
            myAnimator.SetBool("isFalling", false);
        }

    }

   
    /*void MoverMegaman()
    {
        float movH = Input.GetAxis("Horizontal");
        

        Vector2 movimiento = new Vector2(movH * Time.deltaTime * speed, 0);


        transform.Translate(movimiento);

        if (movH !=0)
        {
            myAnimator.SetBool("isRunning", true);
            if (movH < 0)
            {
                transform.localScale = new Vector2(-1, 1);

            }
            
            else
            {
                transform.localScale = new Vector2(1, 1);

            }

        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }

    } */// se cambia para poder poner condiciones al dash

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3 && !isGrounded) //confirmar si esta en un terreno o no
        {
            isGrounded = true;
            
            
        }
    }  //revisar si esta pisando terreno o no
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer ==3 && isGrounded)
        {
            isGrounded = false;
        }
    }


    /*IEnumerator Attack ()
    {

        yield return new WaitForSeconds(0.25f);
        GameObject A = Instantiate(bullet, point.transform.position, Quaternion.identity);
        A.GetComponent<Rigidbody2D>().velocity = transform.right * 10f;
        Destroy(A, 2f);
    }//no se usa.*/

}
