using UnityEngine;
using System.Collections;

public class EnemyChase : EnemyState
{
    private const float chaseBoost = 2f;
    private const float detectionTime = 5f;

    public EnemyChase(Enemy enemy) : base(enemy) { }

    public override void OnStateEnter()
    {
        Debug.Log("Enemy entered Chase state");
    }

    // �÷��̾� �߰�
    public override void OnStateUpdate()
    {
        if (enemy.Player == null) return;

        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.Player.position, chaseBoost * enemy.enemyData.speed * Time.deltaTime);

        Vector3 direction = (enemy.Player.position - enemy.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, Time.deltaTime * 5f);

        enemy.StartCoroutine(ChangeToIdle());
    }

    // Ž�� �ð� �Ŀ� �÷��̾ �þ߰��� ������ �⺻ ���·� ��ȯ
    IEnumerator ChangeToIdle()
    {
        yield return new WaitForSeconds(detectionTime);

        if (!enemy.IsPlayerDetected())
        {
            enemy.ChangeState(new EnemyIdle(enemy));
        }
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy exited Chase state");
    }
}
