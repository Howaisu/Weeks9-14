using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer sr;
    Animator animator;
    public float speed = 2;
    public bool canRun = true;
    public AudioClip[] footsteps; // �洢����footstep��Ƶ������
    private AudioSource audioSource; // ���ڲ�����Ƶ�����
   

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = sr.GetComponent<Animator>();
        
        //
        audioSource = GetComponent<AudioSource>(); // ��ȡ��ǰGameObject��AudioSource���
        

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
        int R = Random.Range(0, 9); // ���ѡ��һ������
        audioSource.clip = footsteps[R]; // ����AudioSourceҪ���ŵ�clip
        audioSource.Play(); // ������Ƶ

    }
}
