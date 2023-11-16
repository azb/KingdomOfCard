using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int TeamID = 1;

    public Animator animator;

    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.turn == GameController.Turn.Player1sAttacking)
        {
            if (TeamID == 1)
            {
                animator.SetBool("Attacking", true);
                animator.SetBool("Defending", false);
            }
            else
            {
                animator.SetBool("Defending", true);
                animator.SetBool("Attacking", false);
            }
        }
        else
        if (GameController.turn == GameController.Turn.Player2sAttacking)
        {
            if (TeamID == 2)
            {
                animator.SetBool("Attacking", true);
                animator.SetBool("Defending", false);
            }
            else
            {
                animator.SetBool("Defending", true);
                animator.SetBool("Attacking", false);
            }
        }
        else
        {
            animator.SetBool("Defending", false);
            animator.SetBool("Attacking", false);
        }
    }
}
