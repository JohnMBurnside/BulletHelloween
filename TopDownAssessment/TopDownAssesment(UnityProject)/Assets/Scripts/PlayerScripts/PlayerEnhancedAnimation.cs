using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerEnhancedAnimation : MonoBehaviour
{
    //VARIABLES
    private Animator animator;
    public bool shootright = false;
    public bool shootleft = false;
    public bool walkA = false;
    public bool walkD = false;
    public float shootDelay = 1.0f;
    public float timer = 0;
    public float animationTimer = 0;
    public float walkDAnimationTime = 1.5f;
    //START FUNCTION
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    //UPDATE FUNCTION
    void Update()
    {
        timer += Time.deltaTime;
        animationTimer += Time.deltaTime;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToViewportPoint(mousePosition);
        if (Input.GetButton("Fire1") && mousePosition.x > -1 && timer > shootDelay)
        {
            timer = 0;
            animationTimer = 0;
            animator.SetBool("shootright", true);
            shootright = true;
        }
        else if (Input.GetButton("Fire1") && mousePosition.y < 1 && timer > shootDelay)
        {
            timer = 0;
            animationTimer = 0;
            animator.SetBool("shootleft", true);
            shootleft = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            animationTimer = 0;
            animator.SetBool("walkD", true);
            walkD = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            animationTimer = 0;
            animator.SetBool("walkA", true);
            walkD = true;
        }
        if (animationTimer > walkDAnimationTime)
        {
            animationTimer = 0;
            animator.SetBool("walkD", false);
            walkD = false;
            animator.SetBool("walkA", false);
            walkD = false;
            animator.SetBool("ShootLeft", false);
            shootleft = false;
            animator.SetBool("ShootRight", false);
            shootleft = false;
        }
    }
}
