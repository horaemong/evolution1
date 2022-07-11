using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class black_v1 : MonoBehaviour
{
    // Start is called before the first frame update    private Rigidbody2D rigid; 
    int enemySpawnLimit;
    // 생성된 객체를 받아올 배열
    public GameObject[] enemies;


    public float[] gene;
    private int degree;
    public int n = 1;
    public static GameObject black;
    private Rigidbody2D rigid; 
    private float speed = 2f; 
    public float rand_x;
    public float rand_y;
    public float x;
    public float y;
    public Vector2 scale;
    public float size_x;
    public float size_y;
    public GameObject prefab;
    public Vector2 position;
    public float think = 3f;
    void Start() 
    { 
        rigid = GetComponent<Rigidbody2D>(); 
        Genetic();
        Think();
        scale = Vector2.zero;
        Devide();
        Find_obj();
    } 
    void FixedUpdate() 
    { 
        Moving(); 
    } 
    void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position); //캐릭터의 월드 좌표를 뷰포트 좌표계로 변환해준다.
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos); //다시 월드 좌표로 변환한다.
        transform.position = worldPos; //좌표를 적용한다.
    }
    void Moving() 
    { 
        Vector2 direction = new Vector2(rand_x,rand_y); 
        // x = Input.GetAxis("Horizontal");
        // y= Input.GetAxis("Vertical");
        // Vector2 direction = new Vector2(x, y); 
        position = new Vector2(transform.position.x, transform.position.y); // 현 위치 받아옴 
        rigid.MovePosition(position + direction.normalized * speed * Time.deltaTime); 
        
        if(direction != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 100f);
        } 
    }

    void Think()
    {
        while(true)
        {
            rand_x = Random.Range(-1f,1f);
            rand_y = Random.Range(-1f,1f);
            if(rand_x ==0 & rand_y ==0)
            {
                continue;
            }
            break;
        }
        Invoke("Think", think);
    }

    void Find_obj()
    {
        enemies = GameObject.FindGameObjectsWithTag("bob");
        Invoke("Find_obj", 13);
    }

    void Devide()
    {
        enemySpawnLimit = 250;
        if(n<UI.generation)
        {
            n+=1;
            if(enemies.Length < enemySpawnLimit)
            {
            
                Instantiate (prefab, position, transform.rotation);
                Instantiate (prefab, position, transform.rotation);
            }


        }
        Invoke("Devide", 0.1f);

    }

    void Change_Pro()
    {
        int a = Random.Range(0,100);
        if(a>=0 & a<90)
        {
            degree = 1;
        }
        else if(a>=90 & a<99)
        {
            degree = 2;
        }
        else
        {
            degree = 3;
        }
    }

    void Genetic()
    {
        gene = new float[4];
        if(UI.generation == 1)
        {
            gene[0] = speed;
            gene[1] = transform.localScale.x;
            gene[2] = transform.localScale.y;
            gene[3] = think;
        }
        else
        {
            int a = Random.Range(0, 3);
            
            switch(a)
            {
                case 0:
                    Size_change_x();
                    Size_change_y();
                    Think_Change();
                    break;
                case 1:
                    Speed_change();
                    Size_change_y();
                    Think_Change();
                    break;
                case 2:
                    Speed_change();
                    Size_change_x();
                    Think_Change();
                    break;
                case 3:
                    Speed_change();
                    Size_change_x();
                    Size_change_y();
                    break;

            }



        }
    }
    void Size_change_x()
    {
            int sign = Random.Range(0,2);
            Change_Pro();
            
            switch (degree)
            {
                case 1:
                    if(sign == 0)
                    {
                        size_x = transform.localScale.x + Random.Range(0, 0.2f);

                    }
                    else
                    {
                        size_x = transform.localScale.x - Random.Range(0, 0.2f);

                    }
                    break;

                case 2:
                    if(sign == 0)
                    {
                        size_x = transform.localScale.x + Random.Range(0.2f, 2f);

                    }
                    else
                    {
                        size_x = transform.localScale.x - Random.Range(0.2f, 2f);

                    }
                    break;
                case 3:
                    if(sign == 0)
                    {
                        size_x = transform.localScale.x + Random.Range(2f, 10f);

                    }
                    else
                    {
                        size_x = transform.localScale.x - Random.Range(2f, 10f);

                    }
                    break;
            }
            transform.localScale = new Vector2(size_x, transform.localScale.y);

    }

    void Size_change_y()
    {
            int sign = Random.Range(0,2);
            Change_Pro();
            
            switch (degree)
            {
                case 1:
                    if(sign == 0)
                    {

                        size_y = transform.localScale.y + Random.Range(0, 0.2f);
                    }
                    else
                    {

                        size_y = transform.localScale.y - Random.Range(0, 0.2f);
                    }
                    break;

                case 2:
                    if(sign == 0)
                    {

                        size_y = transform.localScale.y + Random.Range(0.2f, 2f);
                    }
                    else
                    {

                        size_y = transform.localScale.y - Random.Range(0.2f, 2f);
                    }
                    break;
                case 3:
                    if(sign == 0)
                    {

                        size_y = transform.localScale.y + Random.Range(2f, 10f);
                    }
                    else
                    {

                        size_y = transform.localScale.y - Random.Range(2f, 10f);
                    }
                    break;
            }
            transform.localScale = new Vector2(transform.localScale.x, size_y);

    }
    void Speed_change()
    {
        Change_Pro();
        int a = Random.Range(0,2);
        if (a==0)
        {
            switch(degree)
            {
                case 1:
                    speed += Random.Range(0, 0.2f);
                    break;
                case 2:
                    speed += Random.Range(0.2f, 1f);
                    break;
                case 3:
                    speed += Random.Range(1f, 2.3f);
                    break; 
            }
        }
        else
        {
            switch(degree)
            {
                case 1:
                    speed -= Random.Range(0, 0.2f);
                    break;
                case 2:
                    speed -= Random.Range(0.2f, 1f);
                    break;
                case 3:
                    speed -= Random.Range(1f, 2.3f);
                    break; 
            } 
        }
    }
    void Think_Change()
    {
        Change_Pro();
        switch(degree)
        {
            case 1:
                speed = Random.Range(0, 1f);
                break;
            case 2:
                speed = Random.Range(1f, 3f);
                break;
            case 3:
                speed = Random.Range(3f, 7f);
                break; 
        }
    }

}
