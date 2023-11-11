using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public GameObject Monster;
    public Material Neutral;
    public Material Selected;
    public MeshRenderer meshRenderer;

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
            Invoke("ShowMonster", 1f);
            meshRenderer.material = Selected;
        }
    }

    void ShowMonster()
    {
        Monster.SetActive(true);
        Invoke("HideMonster", 2f);
    }

    void HideMonster()
    {
        meshRenderer.material = Neutral;
        Monster.SetActive(false);
    }
}
