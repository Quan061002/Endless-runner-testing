using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] Transform center_pos;
    [SerializeField] Transform left_pos;
    [SerializeField] Transform right_pos;

    int currunt_pos = 0;

    public float side_speed;
    public float running_Speed;
    public float jump_Force;
    public float roll_Force;

    [SerializeField] Rigidbody rb;

    bool isGameStarted;
    bool isGameOver;

    [SerializeField] Animator player_Animator;

    [SerializeField] GameObject GameOverPanle;

    [SerializeField] GameObject Tap_To_Start_Canvas;
    // Start is called before the first frame update
    void Start()
    {
        isGameStarted = false;
        isGameOver = false;
        currunt_pos = 0; // 0=center, 1=left, 2=right
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted || !isGameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Game is Started");
                isGameStarted = true;
                player_Animator.SetInteger("isRunning",1);
                player_Animator.speed = 1.3f;
                Tap_To_Start_Canvas.SetActive(false);
            }
        }

        if(isGameStarted)
        {   
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + running_Speed * Time.deltaTime);

            if (currunt_pos == 0)
            {
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    currunt_pos = 1;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    currunt_pos = 2;
                }
            }
            else if (currunt_pos == 1)
            {
                if(Input.GetKeyDown(KeyCode.RightArrow))
                {
                    currunt_pos = 0;
                }
            }
            else if (currunt_pos == 2)
            {
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    currunt_pos = 0;
                }
            }

            if (currunt_pos == 0)
            {
                if ( Vector3.Distance(transform.position, new Vector3(center_pos.position.x, transform.position.y, transform.position.z)) >= 0.1f)
                {
                    Vector3 dir = new Vector3(center_pos.position.x, transform.position.y, transform.position.z) - transform.position;
                    transform.Translate(dir.normalized * side_speed * Time.deltaTime, Space.World);
                } 
            }
            else if (currunt_pos == 1)
            {
                if ( Vector3.Distance(transform.position, new Vector3(left_pos.position.x, transform.position.y, transform.position.z)) >= 0.1f)
                {
                    Vector3 dir = new Vector3(left_pos.position.x, transform.position.y, transform.position.z) - transform.position;
                    transform.Translate(dir.normalized * side_speed * Time.deltaTime, Space.World);
                }
            }
            else if (currunt_pos == 2)
            {
                if ( Vector3.Distance(transform.position, new Vector3(right_pos.position.x, transform.position.y, transform.position.z)) >= 0.1f)
                {
                    Vector3 dir = new Vector3(right_pos.position.x, transform.position.y, transform.position.z) - transform.position;
                    transform.Translate(dir.normalized * side_speed * Time.deltaTime, Space.World);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // rb.AddForce(Vector3.up * jump_Force);
                rb.velocity = Vector3.up * jump_Force;
                StartCoroutine(Jump());
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                rb.velocity = Vector3.down * roll_Force;
                StartCoroutine(Roll());
            }

        }

        if (isGameOver)
        {
            if (!GameOverPanle.gameObject.active)
            {
                GameOverPanle.SetActive(true);
            }
        }
    
    }

    IEnumerator Jump()
    {
        player_Animator.SetInteger("isJump",1);
        yield return new WaitForSeconds(0.1f);
        player_Animator.SetInteger("isJump",0);
    }

    IEnumerator Roll()
    {
        player_Animator.SetInteger("isRoll",1);
        yield return new WaitForSeconds(0f);
        player_Animator.SetInteger("isRoll",0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "object")
        {
            isGameStarted = false;
            player_Animator.applyRootMotion = true;
            isGameOver = true;
            player_Animator.SetInteger("isDied",1);
            
        }
    }
}
