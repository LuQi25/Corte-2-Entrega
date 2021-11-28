using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Megaman : MonoBehaviour
{
    [SerializeField] AudioClip sfx_jumpMegaman;
    [SerializeField] AudioClip sfx_dashMegaman;
    [SerializeField] AudioClip sfx_fallMegaman;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject Bullet;
    [SerializeField] Text Reloj;

    Animator myAnimator;
    Rigidbody2D myBody;
    Collider2D myCollider;

    public float life = 10f;
    //public GameObject bullet;
    public bool shooting;
    private float shoot_time;
    public float time;
    public GameObject point;//punto de disparo

    bool doublejumpcan = false;//pfsor

    bool isGrounded;
    bool canDoubleJump;
    public float doubleJumpSpeed; //fuerza del segundo salto.

    public bool Dash;
    public float Dash_T;
    public float Speed_Dash;

    public float cooldown = 2; //cooldown para ritmo que salen las balas cada cierto tiempo
    private float nextFire;

    float dashTime = 0.6f;//pfsor
    float dashstarttime;//pfsor
    bool isdashing = false;//pfsor
    float animationCooldown = 0.4f;//pfsor
    float nextFireBefore;//pfsor
    float fireRate = 0.2f;//pfsor
    float nextFireIn;//pfsor
    float dir = 1; //pfsor
    float timeT = 0; //pfsor
    float count = 0; //pfsor


    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();

        StartCoroutine(TimerCorutina());//pfsor
    }

    
    void Update()
    {
        
        MoverMegaman(); //pfsor
        //Saltar();
        Caer();
        Jump(); //pfsor
        dashh();//pfsor
        Shoot();//pfsor
        /*if (Time.time > nextFire)
        {
            Disparar();
        }*/

    }


    private void FixedUpdate()
    {
        //Move();
        //CollisionSalto();
        
    }

    /*bool EstaTocandoParedIZ()
    {
        RaycastHit2D colision_parediz = Physics2D.Raycast(myCollider.bounds.center, Vector2.left,
                                     myCollider.bounds.extents.x + 0.1f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(myCollider.bounds.center, Vector2.left * (myCollider.bounds.extents.x + 0.1f), Color.red);
        return (colision_parediz.collider != null);
    }*/
    /*bool EstaTocandoParedDER()
    {
        RaycastHit2D colision_paredder = Physics2D.Raycast(myCollider.bounds.center, Vector2.right,
                                     myCollider.bounds.extents.x + 0.1f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(myCollider.bounds.center, Vector2.right * (myCollider.bounds.extents.x + 0.1f), Color.blue);
        return (colision_paredder.collider != null);
    }*/


    
    bool EstaEnSuelo()//pfsor
    {
        //return myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        RaycastHit2D colision_suelo = Physics2D.Raycast(myCollider.bounds.center, Vector2.down,
                                                  myCollider.bounds.extents.y + 0.2f, LayerMask.GetMask("Ground"));

        Debug.DrawRay(myCollider.bounds.center, Vector2.down * (myCollider.bounds.extents.y + 0.1f), Color.green);
        return (colision_suelo.collider !=null);
    }

    bool MirandoPared()//pfsor
    {
        //return myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        RaycastHit2D raycast_pared = Physics2D.Raycast(myCollider.bounds.center, new Vector2(dir,0),
                                                  myCollider.bounds.extents.x + 0.1f, LayerMask.GetMask("Ground"));

        Debug.DrawRay(myCollider.bounds.center, new Vector2(dir, 0) * (myCollider.bounds.extents.x + 0.1f), Color.green);
        return (raycast_pared.collider != null);
    }

    void Timernormal()
    {
        timeT += Time.deltaTime;
        if (timeT >= 1)
        {
            count++;
            Reloj.text = count.ToString();
            timeT = 0;
        }
    }

    IEnumerator TimerCorutina()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            count++;
            Reloj.text = count.ToString();
        }
    }


    void CollisionSalto ()
    {
        if (isGrounded)
        {
            DashAbility();
        }
    }

    void dashh()
    {
        if (EstaEnSuelo())
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                AudioSource.PlayClipAtPoint(sfx_dashMegaman, Camera.main.transform.position);
                dashstarttime = Time.time;
                myAnimator.SetBool("isDash", true);
                isdashing = true;
            }

            if (Input.GetKey(KeyCode.X))
            {
                
                if (Time.time <= dashstarttime+dashTime)
                {
                    myBody.velocity = new Vector2(speed * 2 * transform.localScale.x, myBody.velocity.y);
                }
                else
                {
                    myAnimator.SetBool("isDash", false);
                    isdashing = false;
                }
            }
            else
            {
                isdashing = false;
                myAnimator.SetBool("isDash", false);
            }

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

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= nextFireIn)
        {
            GameObject bala = Instantiate(Bullet, point.transform.position, point.transform.rotation);
            bool direccion = transform.localScale.x == 1;
            (bala.GetComponent<Bullet>()).Shoot(direccion,speed*2);

            myAnimator.SetLayerWeight(1, 1);
            nextFireBefore = Time.time + animationCooldown;
            nextFireIn = Time.time + fireRate;
        }
        else
        {
            if (Time.time>nextFireBefore)
            {
                myAnimator.SetLayerWeight(1, 0);
            }
        }
    }

    void Disparar ()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            nextFire = Time.time + cooldown;
            shoot_time = 0.1f;
            GameObject Firepoint = Instantiate(Bullet, point.transform.position, transform.rotation) as GameObject;
            
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

    void Jump()
    {
       
          if (EstaEnSuelo())
          {
            if (!myAnimator.GetBool("takeof"))
            {
                doublejumpcan = false;
                myAnimator.SetBool("isFalling", false);
                myAnimator.SetBool("takeof", false);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    AudioSource.PlayClipAtPoint(sfx_jumpMegaman, Camera.main.transform.position);
                    if (isdashing) 
                    { 
                        myBody.AddForce(new Vector2(0, jumpForce*1.5f), ForceMode2D.Impulse);
                    }
                    else 
                    { 
                    myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    myAnimator.SetTrigger("jumping");
                    myAnimator.SetBool("takeof", true);
                    doublejumpcan = true;
                    }
                }
            }  
          }

        if (!EstaEnSuelo() && doublejumpcan)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioSource.PlayClipAtPoint(sfx_jumpMegaman, Camera.main.transform.position);
                myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                doublejumpcan = false;
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
        if (!myAnimator.GetBool("takeof") && myBody.velocity.y < -0.05f && !myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("isFalling", true);
        }
        else
        {
            myAnimator.SetBool("isFalling", false);
        }

    }

    void caerSound()
    {
        AudioSource.PlayClipAtPoint(sfx_fallMegaman, Camera.main.transform.position);
    }
   
    void MoverMegaman()
    {
        float movH = Input.GetAxis("Horizontal");
        
        if (movH !=0 )
        {
            
            if (movH < 0 )
            {
                transform.localScale = new Vector2(-1, 1);
            }
            
            else 
            {
                transform.localScale = new Vector2(1, 1);
            }
            if (MirandoPared() && movH==dir)
            {
                myAnimator.SetBool("isRunning", false);
                myBody.velocity = new Vector2(0, myBody.velocity.y);
            }
            else
            {
            dir = transform.localScale.x;
            myAnimator.SetBool("isRunning", true);
            myBody.velocity = new Vector2(movH * speed, myBody.velocity.y);
            }
            /*if (!EstaTocandoParedIZ() && !EstaTocandoParedDER())
            {
                myAnimator.SetBool("isRunning", true);
            }
            else
            {
                myAnimator.SetBool("isRunning", false);
            }*/

        }
        else
        {
            myBody.velocity = new Vector2(0 * speed, myBody.velocity.y);
            myAnimator.SetBool("isRunning", false);
        }
        

    } // version mejorada

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


}
