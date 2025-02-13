using UnityEngine;
using System;

public class Growth : MonoBehaviour
{

    private static readonly int[] expTable = { 0, 300, 1000 };
    private static readonly int MAX_EXP = expTable[expTable.Length - 1];
    private static readonly int MAX_LEVEL = expTable.Length;

    public int CurrentExp { get; private set; } = 0;
    public int CurrentLevel { get; private set; } = 1;

    [SerializeField] private GameObject[] characterPrefabs;
    private GameObject currentCharacterInstance;
    private Vector3 baseScale = Vector3.one;

    private void Start()
    {
        ChangePrefab();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            AddExp(40);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            AddExp(50);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            AddExp(60);
        }
    }

    // ����ġ ����
    public void AddExp(int expAmount)
    {
        if (CurrentLevel >= MAX_LEVEL)
        {
            return;
        }
        else
        {
            CurrentExp = Math.Min(MAX_EXP, CurrentExp + expAmount);
            Debug.Log("Exp: " + CurrentExp);
            CheckLevelUp();
        }
    }

    // ���� �� ���� Ȯ��
    private void CheckLevelUp()
    {
        if(CurrentExp >= expTable[CurrentLevel])
        {
            ApplyLevelUP();
        }
    }

    // ���� �� ����
    private void ApplyLevelUP()
    {
        CurrentLevel += 1;
        ChangePrefab();
        Debug.Log("Level: " + CurrentLevel);
    }

    // ĳ���� ������ ����
    private void ChangePrefab()
    {
        if(currentCharacterInstance != null)
        {
            Destroy(currentCharacterInstance);
        }

        currentCharacterInstance = Instantiate(
            characterPrefabs[CurrentLevel-1],
            transform.position,
            transform.rotation,
            transform
        );

        if (CurrentLevel != 1)
        {
            IncreaseScale();
        }
    }

    // ĳ���� ũ�� ����
    private void IncreaseScale()
    {
        transform.localScale = baseScale + (Vector3.one * (CurrentLevel * 0.8f));
    }
}
