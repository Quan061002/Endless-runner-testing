using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pallet_random_script : MonoBehaviour
{

    public GameObject[] difficults;
    public bool isDefault;



    // Start is called before the first frame update
    void Start()
    {
        if (!isDefault)
        {
            int random_value = Random.Range(0, difficults.Length);

            difficults[random_value].gameObject.SetActive(true);
        }
    }

   
}
