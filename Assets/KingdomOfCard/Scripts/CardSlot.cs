using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Foundry.Networking;

public class CardSlot : NetworkComponent
{
    public GameObject Monster;
    public Material Neutral;
    public Material Selected;
    public MeshRenderer meshRenderer;
    public NetworkProperty<bool> _spawn = new(false);

    public bool spawn
    {
        get => _spawn.Value;
        set => _spawn.Value = value;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    /* RegisterProperties is called once when the component is added to the networked object on Awake, 
     * this is where we connect up all our properties.*/
    public override void RegisterProperties(List<INetworkProperty> props)
    {
        // This callback is called both when the value is set locally and when it is set remotely.
        _spawn.OnValueChanged += b =>
        {
            Monster.SetActive(b);
            if (b)
            {
                meshRenderer.material = Selected;
            }
            else
            {
                meshRenderer.material = Neutral;
            }
        };
        props.Add(_spawn);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter other.transform.name = "+ other.transform.name);

        if (other.name.ToLower().Contains("finger") & !spawn)
        {
            // Change network'ed spawn value
            spawn = true;

            //spawn monster
            // Invoke("ShowMonster", 1f);
            // meshRenderer.material = Selected;
        }
    }

    void ShowMonster()
    {
        Monster.SetActive(true);
        //Invoke("HideMonster", 2f);
    }

    void HideMonster()
    {
        meshRenderer.material = Neutral;
        Monster.SetActive(false);
    }
}
