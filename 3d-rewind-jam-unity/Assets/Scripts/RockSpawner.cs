using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [Header("Ayarlar")]
    public GameObject rockPrefab; // Prefab yaptýðýn kayayý buraya sürükle
    public float spawnInterval = 3f; // Kaç saniyede bir taþ çýksýn?
    public float rockLifeTime = 15f; // Taþ kaç saniye sonra yok olsun (Killzone yapmazsan diye)

    [Header("Fýrlatma Gücü")]
    public float pushForce = 500f; // Ýlk çýkýþta biraz ittirelim

    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnRock();
            timer = spawnInterval;
        }
    }

    void SpawnRock()
    {
        // Taþý spawner'ýn olduðu yerde yarat
        GameObject newRock = Instantiate(rockPrefab, transform.position, Random.rotation);

        // Biraz ileri doðru ittir ki maðaradan fýrlasýn
        if (newRock.GetComponent<Rigidbody>())
        {
            newRock.GetComponent<Rigidbody>().AddForce(transform.forward * pushForce);
        }

        // Performans için: Taþ 15 saniye sonra otomatik silinsin (Opsiyonel)
        Destroy(newRock, rockLifeTime);
    }
}