using UnityEngine;
using System.Collections;

public class EnemyRunaway : EnemyState
{
    private const float runawayBoost = 1.7f;
    private const float detectionTime = 2f;

    public EnemyRunaway(Enemy enemy) : base(enemy) { }

    public override void OnStateEnter()
    {
        Debug.Log("Enemy entered Runaway state");
    }

    // �÷��̾�κ��� ����
    public override void OnStateUpdate()
    {
        if (enemy.Player == null) return;

        Vector3 fleeDirection = (enemy.transform.position - enemy.Player.position).normalized;
        fleeDirection.z = 0;
        enemy.transform.position += runawayBoost * fleeDirection * enemy.enemyData.speed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(fleeDirection);
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, Time.deltaTime * 5f);
        
        enemy.StartCoroutine(ChangeToIdle());
    }

    // Ž�� �ð� �Ŀ� �÷��̾ �þ߰��� ������ �⺻ ���·� ��ȯ
    IEnumerator ChangeToIdle()
    {
        yield return new WaitForSeconds(detectionTime);

        if (!enemy.IsPlayerDetected())
        {
            enemy.stateManager.ChangeState(enemy.stateManager.idleState);
        }
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy exited Runaway state");
    }
}
