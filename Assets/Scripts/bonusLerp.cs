using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusLerp : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] GameObject referenceObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Lerping();
    }

    float timeElapsed = 0f;
    void Lerping()
    {
        Vector3 startPos = transform.position;
        if (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, referenceObject.transform.position, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
        }
        else
            transform.position = referenceObject.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
