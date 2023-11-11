using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public GameObject Monster;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter other.transform.name = "+ other.transform.name);

        if (other.name.ToLower().Contains("finger"))
        {
            //spawn monster
            Monster.SetActive(true);
            Invoke("HideMonster", 5f);
        }
    }

    void HideMonster()
    {
        Monster.SetActive(false);
    }
}
