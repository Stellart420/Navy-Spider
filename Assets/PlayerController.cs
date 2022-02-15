using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance;

    [SerializeField] float speed;
    

    Camera cam;
    NavMeshAgent agent;
    Animator animator;
    public delegate void OnSpeedChangeDelegate(float speed);
    public event OnSpeedChangeDelegate OnSpeedChangeChange;

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            if (speed == value)
                return;

            speed = value;
            agent.speed = speed;
        }
    }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = speed;
    }

    private void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                transform.LookAt(agent.destination);
            }
        }
        animator.SetBool("IsRun", agent.remainingDistance > 0.2f);
    }
}
