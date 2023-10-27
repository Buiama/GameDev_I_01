using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    Text finalScore;

    // Start is called before the first frame update
    void Start()
    {
        if (LevelController.instance != null)
        {
            Destroy(LevelController.instance.gameObject);
            finalScore = GameObject.Find("FinalScore").GetComponent<Text>();
            finalScore.text = "Your final Score: " + LevelController.instance.GetScoreValue().ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
