using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class WheelBonusLerp : MonoBehaviour
{
    [SerializeField] float duration;
    GameObject referenceObject;
    [SerializeField] TextMeshProUGUI text;
    Vector3 startPos;
    int scoreBonus;


    // Start is called before the first frame update
    void Start()
    {
        referenceObject = GameObject.FindGameObjectWithTag("Player");
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Lerping();
    }

    float timeElapsed = 0f;
    void Lerping()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, referenceObject.transform.position, timeElapsed / duration);

        }
        else
        {
            transform.position = referenceObject.transform.position;
            GameManager.instance.AddToScore(scoreBonus);
            Destroy(gameObject);
        }
    }

    public void SetBonus(int score)
    {
        scoreBonus = score;
        text.SetText("+" + score.ToString());
    }


}
