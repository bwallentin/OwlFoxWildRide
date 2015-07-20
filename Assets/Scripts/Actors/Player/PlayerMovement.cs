using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private float camRaylength = 100f;
    private int navMask;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        navMask = LayerMask.GetMask("NavMesh");
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Fire2"))
        {
            Move();
        }
    }

    void Update()
    {
        animator.speed = agent.speed;
        animator.SetBool("IsWalking", agent.velocity != Vector3.zero);
    }

    private void Move()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Cast a ray from camera to any object on our navMask layer, which is our NavMesh Quad
        if (Physics.Raycast(ray, out hit, camRaylength, navMask))
        {
            Vector3 diff = hit.point - transform.position;
            diff.y = 0.0f;

            // If we're further away than 0.5 units, we'll set the movement.
            if (diff.magnitude > 1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(diff);

                targetRotation.x = 0.0f;
                targetRotation.z = 0.0f;

                // Rotate player towards the mouse position
                rb.MoveRotation(targetRotation);

                // Send the agent on its way
                agent.SetDestination(hit.point);
            }
        }
    }

    public void Stop()
    {
        agent.Stop();
    }
}
