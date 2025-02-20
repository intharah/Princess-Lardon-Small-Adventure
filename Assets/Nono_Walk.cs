using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nono_Walk : StateMachineBehaviour
{
    public float speed = 1.0f;
    private float contactRange = 7.0f;
    Transform player;
    Rigidbody2D rb;
    NonoController nono;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        nono = animator.GetComponent<NonoController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        nono.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        if (Vector2.Distance(player.position, rb.position) <= contactRange)
        {
            rb.MovePosition(newPos);
        }
    }
}
