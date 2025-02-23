using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int movSpeed = 100;
    public GameObject console;

    Animator animator;

    private bool newarCPU = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 speed = new Vector3(0, this.gameObject.GetComponent<Rigidbody>().velocity.y, 0);
        if (Input.GetKey("d") && console.activeSelf == false)
        {
           
            // speed += Vector3.Cross(new Vector3(1, 0, 0), this.transform.forward) * movSpeed;
            speed += new Vector3(1, 0, 0) * movSpeed;
        }
        if (Input.GetKey("a") && console.activeSelf == false)
        {
            //speed += Vector3.Cross(new Vector3(-1, 0, 0), this.transform.forward) * movSpeed;
            speed += new Vector3(-1, 0, 0) * movSpeed;

        }
        if (Input.GetKey("w") && console.activeSelf == false)
        {
           // speed += Vector3.Cross(new Vector3(0, 0, 1), this.transform.forward) * movSpeed;
            speed += new Vector3(0, 0, 1) * movSpeed;

        }
        if (Input.GetKey("s") && console.activeSelf == false  )
        {
            //speed += Vector3.Cross(new Vector3(0, 0, -1), this.transform.forward) * movSpeed;
            speed += new Vector3(0, 0, -1) * movSpeed;

        }


        this.gameObject.GetComponent<Rigidbody>().velocity = speed;

        if (speed != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(this.gameObject.GetComponent<Rigidbody>().velocity);
            animator.SetBool("isWalking", true);
        }
        else { animator.SetBool("isWalking", false); }

        if (newarCPU && Input.GetKeyDown(KeyCode.Escape))
        {
            console.SetActive(!console.activeSelf);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Computer1"))
        {
            newarCPU = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Computer1"))
        {
            newarCPU = false;
            console.SetActive(false);
        }
    }
}


