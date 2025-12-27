using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CodeLock"))
        {
            
        }
    }
}
