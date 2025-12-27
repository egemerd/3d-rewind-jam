using System.Collections;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    [SerializeField] private GameObject numberFlashEffectPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FLashNumber());
        }
    }

    private IEnumerator FLashNumber()
    {
        numberFlashEffectPrefab.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        numberFlashEffectPrefab.SetActive(false);

        yield return new WaitForSeconds(0.2f);
        numberFlashEffectPrefab.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        numberFlashEffectPrefab.SetActive(false);
    }
}
