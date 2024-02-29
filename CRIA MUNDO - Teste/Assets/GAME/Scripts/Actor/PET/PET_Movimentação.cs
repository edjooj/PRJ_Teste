using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PET_Movimentação : MonoBehaviour
{
    public Transform playerTransform;
    public float speed = 5f;
    public float stoppingDistance = 1f;
    public float teleportDistance = 15f;
    private Vector3 targetPosition;
    private Vector3 lastMovingDirection;
    public float rotationSpeed = 5f;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Teletransporta o PET se estiver muito longe
        if (distanceToPlayer > teleportDistance)
        {
            transform.position = playerTransform.position + (Vector3.back * stoppingDistance);
            targetPosition = transform.position;
        }
        else if (distanceToPlayer > stoppingDistance)
        {
            targetPosition = playerTransform.position;
            MoveTowardsTarget(distanceToPlayer);
        }
    }

    void MoveTowardsTarget(float currentDistance)
    {
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;

        // Determina a direção e atualiza a última direção de movimento
        if (directionToTarget != Vector3.zero)
        {
            lastMovingDirection = directionToTarget;
        }

        if (currentDistance > stoppingDistance)
        {
            // Movimenta o PET
            transform.position += lastMovingDirection * speed * Time.deltaTime;

            // Rotaciona o PET para alinhar com a direção do movimento
            RotateTowardsMovementDirection(lastMovingDirection);
        }
    }

    void RotateTowardsMovementDirection(Vector3 movementDirection)
    {
        if (movementDirection.magnitude > 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }
    }
}