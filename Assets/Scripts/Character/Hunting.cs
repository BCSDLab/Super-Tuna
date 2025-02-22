using UnityEngine;

public class Hunting : MonoBehaviour
{
    private bool isHunting;
    private const float huntingTime = 0.1f;

    [SerializeField] HungerSystem hungerSystem;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�浹�߽��ϴ�.");
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            AttemptHunt(enemy);
        }
    }

    private void AttemptHunt(Enemy target)
    {
        if (!isHunting)
        {
            Debug.Log("����� �õ��մϴ�.");
            isHunting = true;
            ExecuteHunt(target);
        }
    }

    private void ExecuteHunt(Enemy target)
    {
        if (target != null)
        {
            Debug.Log("��ɿ� �����߽��ϴ�.");
            hungerSystem.IncreaseHunger(100);
            target.gameObject.SetActive(false);
            Invoke(nameof(ResetIsHunting), huntingTime);
        }
    }

    private void ResetIsHunting()
    {
        isHunting = false;
    }
}
