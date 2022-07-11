using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_v2 : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Rigidbody2D rigid; 
    private float speed = 3.5f; 
    public float rand_x;
    public float rand_y;
    public float x;
    public float y;
    public Vector2 scale;
    public float size_x;
    public float size_y;
    void Start() 
    { 
        rigid = GetComponent<Rigidbody2D>(); 
        Think();
        scale = Vector2.zero;
        Size_change();
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
        Vector2 position = new Vector2(transform.position.x, transform.position.y); // 현 위치 받아옴 
        rigid.MovePosition(position + direction.normalized * speed * Time.deltaTime); 
        
        if(direction != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 100f);
        } 
    }

    void Size_change()
    {
        if(UI.generation == 1)
        {

        }
        else
        {
            size_x = Random.Range(-1f,1f);
            size_y = Random.Range(-1f,1f);
            transform.localScale = new Vector2(size_x, size_y);
        }
        //else if
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
        Invoke("Think", 4f);
    }

        private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "bob")
        {
            Destroy(other.gameObject);
        }
    }

}
