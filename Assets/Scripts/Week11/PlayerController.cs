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
        }
    }

    public void AttackHasFinishedd()
    {

        Debug.Log("The attack animation has been finished");
        canRun = true;

    }

    public void FootSteps()
    {
        Debug.Log("step on the groud.");
        int R = Random.Range(0, 9); // 随机选择一个索引
        audioSource.clip = footsteps[R]; // 设置AudioSource要播放的clip
        audioSource.Play(); // 播放音频

    }
}
