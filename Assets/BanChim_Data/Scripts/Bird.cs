using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird :MonoBehaviour
    { 
    public float xSpeed;

    public float minYSpeed;

    public float maxYSpeed;

    Rigidbody2D rb;

    bool moveLeft;

    public bool isDead;

    public GameObject deathVFX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        RandomMovingDirection();

        Flip();
    }
    private void Update()
    {

        rb.velocity = moveLeft ?  new Vector2(-xSpeed, Random.Range(minYSpeed, maxYSpeed)) : new Vector2(xSpeed, Random.Range(minYSpeed, maxYSpeed));
    }

    public void RandomMovingDirection()
    {
        //nếu vị trí x của chim > 0 thì nó di chuyển sang bên trái còn không thì sẽ di chuyển sang bên phải 
        moveLeft = transform.position.x > 0 ? true : false;
    }

    void Flip()
    {
        if (moveLeft)
        {
            if(transform.localScale.x < 0)
            {
                return;
            } else if(transform.localScale.x>0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        } else
        {
            if (transform.localScale.x > 0)
            {
                return;
            }
            else if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    public void Die()
    {
        isDead = true;
        
        Destroy(gameObject);


        //Khởi tạo hoạt ảnh nổ bùm máu cho con chim 
        if (deathVFX)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
        }

        GameManager.Ins.BirdKilled++;

        UIManager.Ins.UpdateKillin(GameManager.Ins.BirdKilled);

        
    }
    
}


