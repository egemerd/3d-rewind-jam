 using UnityEngine;

public class CubeDeneme : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;

    void Update()
    {
        transform.position += new Vector3(1f, 0f, 0f) * Time.deltaTime * speed;
        transform.Rotate(new Vector3(0.5f, 1f, 0.75f) * Time.deltaTime * rotationSpeed * 10f);
        
    }
}
