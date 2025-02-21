using UnityEngine;
using System.Collections;

public class EnemyChase : EnemyState
{
    private const float chaseBoost = 1.7f;
    private const float detectionTime = 5f;
    private bool isChangingState = false;

    public EnemyChase(Enemy enemy) : base(enemy) { }

    public override void OnStateEnter()
    {
        Debug.Log("Enemy entered Chase state");
        isChangingState = false;
        enemy.StartCoroutine(ChangeToIdle());
    }

    // �÷��̾� �߰�
    public override void OnStateUpdate()
    {
        if (enemy.Player == null) return;

        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.Player.position, chaseBoost * enemy.enemyData.speed * Time.deltaTime);

        Vector3 direction = (enemy.Player.position - enemy.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    // �÷��̾�� �浹 �� ���
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HungerSystem playerHungerSystem = other.GetComponent<HungerSystem>();

            if (playerHungerSystem != null)
            {
                playerHungerSystem.TriggerDeath();
            }
        }
    }

    // Ž�� �ð� �Ŀ� �÷��̾ �þ߰��� ������ �⺻ ���·� ��ȯ
    IEnumerator ChangeToIdle()
    {
        if (isChangingState) yield break;
        isChangingState = true;

        yield return new WaitForSeconds(detectionTime);

        if (!enemy.IsPlayerDetected())
        {
            enemy.stateManager.ChangeState(enemy.stateManager.idleState);
        }

        isChangingState = false;
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy exited Chase state");
    }
}
