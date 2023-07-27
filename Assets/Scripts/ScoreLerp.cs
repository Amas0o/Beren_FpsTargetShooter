using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreLerp : MonoBehaviour
{


    [SerializeField] float duration;
    [SerializeField] float lerpDistance;
    [SerializeField] TextMeshProUGUI text;
    
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
            transform.position = Vector3.Lerp(startPos, new Vector3(transform.position.x, transform.position.y + lerpDistance, transform.position.z), timeElapsed / duration);
            timeElapsed += Time.deltaTime;
        }
        else
            Destroy(gameObject);
    }
    public void setText(int score) 
    {
        if (score < 0)
        {
            text.SetText(score.ToString());
            text.color = Color.red;
        }
        else text.SetText("+" + score.ToString());
    }
}
