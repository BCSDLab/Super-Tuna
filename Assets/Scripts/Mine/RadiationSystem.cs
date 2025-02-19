using System;
using System.Collections;
using UnityEngine;

public class RadiationSystem : MonoBehaviour
{ 
    private const float RadiationDamage = 5f;
    private const float RadiationSpreadSpeed = 5f;
    private const float RadiationSpreadDuration = 300f;
        
    [SerializeField] private bool isPlayerInRadiation;
    [SerializeField] private float currentRadiationDamage = RadiationDamage;
    private Coroutine _radiationCoroutine;
        
    public event Action<float> OnRadiationDamage;

    private void Start()
    { 
        StartCoroutine(SpreadRadiation());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRadiation = true; 
            HungerSystem playerHunger = other.GetComponent<HungerSystem>();
            if (playerHunger != null && _radiationCoroutine == null)
            {
                _radiationCoroutine = StartCoroutine(ApplyRadiationDamage(playerHunger));
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    { 
        if (other.CompareTag("Player")) 
        { 
            isPlayerInRadiation = false; 
            if (_radiationCoroutine != null) 
            { 
                StopCoroutine(_radiationCoroutine); 
                _radiationCoroutine = null;
            }
        }
    }
    private IEnumerator ApplyRadiationDamage(HungerSystem playerHunger) 
    { 
        while (isPlayerInRadiation) 
        { 
            playerHunger.DecreaseHunger(currentRadiationDamage); 
            OnRadiationDamage?.Invoke(currentRadiationDamage); 
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator SpreadRadiation() 
    { 
        float elapsedTime = 0f; 
        while (elapsedTime < RadiationSpreadDuration) 
        { 
            transform.localScale += new Vector3(0, 0, RadiationSpreadSpeed) * Time.deltaTime; 
            elapsedTime += Time.deltaTime; 
            yield return null;
        }
    }
}