using UnityEngine;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    public const float fovAngle = 120f;

    public Transform player;
    public Transform Player => player;

    private bool isOutOfBoundary = false;

    public EnemyStateManager stateManager;
    public Growth growth;

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        stateManager = GetComponent<EnemyStateManager>();

        if (player != null)
        {
            growth = player.GetComponent<Growth>();
            LookAtPlayer();
        }
    }

    public bool IsPlayerDetected()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float verticalAngle = Vector3.Angle(transform.forward, directionToPlayer);

        return distanceToPlayer <= enemyData.sightRange && verticalAngle <= Enemy.fovAngle / 2;
    }

    private void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stateManager != null && stateManager.currentState != null)
        {
            stateManager.currentState.OnTriggerEnter(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            if (!isOutOfBoundary)
            {
                isOutOfBoundary = true;
                Invoke(nameof(OnTriggerDeath), 3f);
            }
        }
    }

    public void OnTriggerDeath()
    {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Color _blue = new Color(0f, 0f, 1f, 0.2f);
        Color _red = new Color(1f, 0f, 0f, 0.2f);

        if (enemyData == null) return;
        Handles.color = IsPlayerDetected() ? _red : _blue;
        Handles.DrawSolidArc(transform.position, transform.right, transform.forward, fovAngle / 2, enemyData.sightRange);
        Handles.DrawSolidArc(transform.position, transform.right, transform.forward, -fovAngle / 2, enemyData.sightRange);
    }
}
