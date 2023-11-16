using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public GameObject Monster;
    public Material Neutral;
    public Material Selected;
    public MeshRenderer meshRenderer;


    NetworkedObject _networkedObject;
    public NetworkedObject networkedObject
    {
        get
        {
            if (_networkedObject == null)
            {
                _networkedObject = GetComponent<NetworkedObject>();
            }
            return _networkedObject;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Invoke("CheckForUpdate", 1);
    }

    void CheckForUpdate()
    {
        bool characterSpawned = networkedObject.GetSyncedBool("CharacterSpawned");

        if (characterSpawned)
        {
            Monster.SetActive(true);
        }

        Invoke("CheckForUpdate", 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter other.transform.name = "+ other.transform.name);

        if (other.name.ToLower().Contains("finger"))
        {
            CardClicked();
        }
    }

    public void CardClicked()
    {
        networkedObject.SetSyncedBool("CharacterSpawned", true);
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
