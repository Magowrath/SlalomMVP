using Unity.VisualScripting;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float blerg;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        blerg = transform.rotation.z * 0.0009f;
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    private void LateUpdate()
    {
        //transform.position = (new Vector3(transform.position.x, transform.position.y + blerg, transform.position.z));
    }
}
