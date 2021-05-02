using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Fruit : MonoBehaviour
{
    public List<Transform> positions;
    public NavMeshAgent agent;
    public Animator animator;

    private int minDuration = 3;
    private int maxDuration = 5;

    private int lastIndex;

    void Start()
    {
        agent.speed = Random.Range(3, 5);
        Invoke(nameof(MoveToNextPosition), Random.Range(minDuration, maxDuration));
    }

    private void MoveToNextPosition()
    {
        int index = Random.Range(0, positions.Count);

        if (index == lastIndex)
            index = (index + 1) % positions.Count;

        agent.SetDestination(positions[index].position);
        lastIndex = index;
        animator.SetBool("Walk", true);
    }

    private void Update()
    {
        if (agent.hasPath && agent.remainingDistance <= 1)
        {
            agent.ResetPath();
            animator.SetBool("Walk", false);
            Invoke(nameof(MoveToNextPosition), Random.Range(minDuration, maxDuration));
            Debug.Log(name + " est arrive a sa destination");
        }
    }
}