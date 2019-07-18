using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   Animator anim;
    public float speed;
    public float jumpForce;
    public Text countText;
    public Text winText;
    public Text lifeText;

    public AudioClip musicClipOne;
public AudioClip musicClipTwo;
public AudioSource musicSource;

private bool facingRight = true;
     private Rigidbody2D rb2d;
     private int count;
     private int life;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator> ();
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        life = 3;
        winText.text = "";
        SetAllText ();
    }

    // Update is called once per frame
    void Update()
    {
 
        if (Input.GetKey("escape"))
{
     Application.Quit();
}    
    }
    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement * speed);

        //handling the idle - running - idle state change
     if (Input.GetKeyDown (KeyCode.LeftArrow))
{
anim.SetInteger("State", 1);
}   
    if (Input.GetKeyUp (KeyCode.LeftArrow))
    {
anim.SetInteger("State", 0);
    }

    if (Input.GetKeyDown (KeyCode.RightArrow))
{
anim.SetInteger("State", 1);
}   
    if (Input.GetKeyUp (KeyCode.RightArrow))
    {
anim.SetInteger("State", 0);
    } 
    
//handling the jumping state
if (Input.GetKeyDown (KeyCode.UpArrow))
{
    anim.SetInteger("State", 2);
}
    if (facingRight == false && moveHorizontal > 0)
{
Flip();
}
else if (facingRight == true && moveHorizontal < 0)
{
Flip();
}
    }

     void Flip()
{
facingRight = !facingRight;
Vector2 Scaler = transform.localScale;
Scaler.x = Scaler.x * -1;
transform.localScale = Scaler;
}

    void OnTriggerEnter2D(Collider2D other){

        if (count == 3){
            transform.position = new Vector2(20.0f, transform.position.y);
            life = 3;
        }
        
        if (other.gameObject.CompareTag ("PickUp"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetAllText ();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive (false);
            life = life - 1;
            SetAllText();
        }
    }

    void SetAllText ()
    {
        countText.text = "Score: " + count.ToString();
        lifeText.text = "Lives: " + life.ToString();
        if (count >= 8){
            winText.text = "You win!";
        }
        if (life == 0){
            winText.text = "You lost!";
        }
    }

    void OnCollisionStay2D(Collision2D collision){
        if(collision.collider.tag == "Ground") {
      
            if(Input.GetKey(KeyCode.UpArrow)) {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                
                
            }
        }
    }
}
