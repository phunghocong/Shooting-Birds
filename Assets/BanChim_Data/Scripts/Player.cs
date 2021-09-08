using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //thời gian nạp đạn
    public float fireRate;

    float curfireRate;

    public GameObject viewFinder;

    GameObject viewFinderClone;


    bool isShooted;

    private void Start()
    {
        if (viewFinder)
        {
           viewFinderClone = Instantiate(viewFinder, Vector3.zero, Quaternion.identity);
        }
    }

    //trước khi ctrinh bắt đầu chạy 
    private void Awake()
    {
        //lưu dữ liệu firerate lên curfirerate
        curfireRate = fireRate;
    }
    private void Update()
    {
        //chuyển đổi tọa độ của người chơi nhấn chuột thành tọa độ trong unity
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && isShooted == false)
        {
            Shoot(mousePos);
        }

        if (isShooted)
        {
            curfireRate = curfireRate - Time.deltaTime;

            if(curfireRate <=0)
            {
                isShooted = false;

                curfireRate = fireRate;
            }
            UIManager.Ins.GetFireRate(curfireRate / fireRate);
        }

        if (viewFinderClone)
        {
            viewFinderClone.transform.position = new Vector3(mousePos.x,mousePos.y,0f);
        }
    }
    void Shoot(Vector3 mousePos)
    {

        isShooted = true;

        Vector3 shootDir = Camera.main.transform.position - mousePos;

        shootDir.Normalize();

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, shootDir);

        if(hits != null && hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {

                //phần tử trong mảng hits
                RaycastHit2D hit = hits[i];


                //nếu khoảng cách giữa vật mà đường raycast chạm phải với con trỏ chuột mà nhỏ hơn 0,4
                if(hit.collider && (Vector3.Distance((Vector2)hit.collider.transform.position,(Vector2)mousePos) <= 0.4f))
                {
                    Bird bird = hit.collider.GetComponent<Bird>();

                    if (bird)
                    {
                        bird.Die();


                    }
                } 
            }
        }

        CineController.Ins.ShakeTrigger();


        Audio.Ins.PlaySound(Audio.Ins.shooting);
    }
}
