using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_Coins_Counter_Script : MonoBehaviour
{
    // public static Score_Coins_Counter_Script instance;

    // private void Awake()
    // {
    //     if (instance == null)
    //     {
    //         isntance = this;
    //     }
    // }

    [SerializeField] TextMeshProUGUI total_coins_text;

    [SerializeField] TextMeshProUGUI currunt_score_text;

    [SerializeField] Transform Player;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        total_coins_text.text = PlayerPrefs.GetInt("total_coins",0).ToString("00");
        currunt_score_text.text = Player.transform.position.z.ToString("00.0") + "m";
    


        if (Player.transform.position.z >= PlayerPrefs.GetFloat("high_score", 0f))
        {
            PlayerPrefs.SetFloat("high_score", Player.transform.position.z);
        }
    }

  
}
