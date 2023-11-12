using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public GameObject Monster;
    public Material Neutral;
    public Material Selected;
    public MeshRenderer meshRenderer;

    public bool spawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter other.transform.name = "+ other.transform.name);

        if (other.name.ToLower().Contains("finger"))
        {
            // Change network'ed spawn value
            spawn = !spawn;

            //spawn monster
            // Invoke("ShowMonster", 1f);
            // meshRenderer.material = Selected;
        }
    }

    public void ShowMonster()
    {
        meshRenderer.material = Selected;
        Monster.SetActive(true);
        Invoke("SetNeutral", 2f);
    }

    void SetNeutral()
    {
        meshRenderer.material = Neutral;
    }

    void HideMonster()
    {
        meshRenderer.material = Neutral;
        Monster.SetActive(false);
    }
}
