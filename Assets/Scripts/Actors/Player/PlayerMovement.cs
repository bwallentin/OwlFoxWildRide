using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour, IMovable
{
    // Controlling our objects animations
    private Animator animator;

    // Responsible for moving the object along the nav mesh
    private NavMeshAgent agent;

    // Our rigid body, allowing us to interact with physics
    private Rigidbody rb;

    // The length our camera will cast ray
    private float camRaylength = 100f;

    // The layer our navigation mesh resides on
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

    public void IsMovable(bool isMovable)
    {
       
        // stop the agent from moving anymore
        agent.Stop();

        // release the current path
        agent.ResetPath();

        // Reset animation to idle
        animator.SetBool("IsWalking", false);

        // disable further click to move instructions
        this.enabled = isMovable;
    }
}
