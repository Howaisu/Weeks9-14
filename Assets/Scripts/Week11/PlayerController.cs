using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer sr;
    Animator animator;
    public float speed = 2;
    public bool canRun = true;
    public AudioClip[] footsteps; // 存储所有footstep音频的数组
    private AudioSource audioSource; // 用于播放音频的组件
    float t = 0;
    bool isJumping;

    public AnimationCurve jump;
   

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = sr.GetComponent<Animator>();
        
        //
        audioSource = GetComponent<AudioSource>(); // 获取当前GameObject的AudioSource组件
        

    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        sr.flipX = (direction < 0);
        animator.SetFloat("movement", Mathf.Abs(direction));




        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
            canRun = false;
        }

        if (canRun == true)
        {
            transform.position += transform.right * direction * speed * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space)&&!isJumping)
            {

                StartCoroutine(Jumping());

            }
        }
    }

    IEnumerator Jumping()
    {
        animator.SetBool("jump",true);
        
        //CALL
        Debug.Log("jumping");
        //start jump
        isJumping = true;  
        t = 0;  
        // A loop that make the player jump
        while (t < 1)
        {
            t += Time.deltaTime;
            Vector2 position = transform.position;
            position.y = jump.Evaluate(t);
            transform.position = position; // return
            yield return null;  // go out from the coroutine
        }
        isJumping = false;
        animator.SetBool("jump", false);

    }

    public void AttackHasFinishedd()
    {

        Debug.Log("The attack animation has been finished");
        canRun = true;

    }

    public void FootSteps()
    {
       
        int R = Random.Range(0, 9); // 随机选择一个索引
        Debug.Log("step out the sound "+ R);
        audioSource.clip = footsteps[R]; // 设置AudioSource要播放的clip
        audioSource.Play(); // 播放音频

    }
}
