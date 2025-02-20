using UnityEngine;

public class EnemyAttack : EnemyState
{
    public EnemyAttack(Enemy enemy) : base(enemy) { }

    public override void OnStateEnter()
    {
        Debug.Log("Enemy entered EnemyAttack state");

        Debug.Log("���� ��ġ�� �׾����ϴ�.");
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy exited EnemyAttack state");
    }
}
