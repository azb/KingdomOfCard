using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public GameObject Monster;
    public Material Neutral;
    public Material Selected;
    public MeshRenderer meshRenderer;
    public GameObject glow;
    public ParticleSystem spawnParticleSystem;
    bool particleEffectPlayedPrev = false;

    public GameController.Turn turnThisCardSlotCanBeUsed;

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
        Invoke("CheckForUpdate", .5f);
    }

    private void Update()
    {
        glow.SetActive(GameController.turn == turnThisCardSlotCanBeUsed);

        if (networkedObject.GetSyncedBool("ParticleEffectPlayed") == true && !particleEffectPlayedPrev)
        {
            particleEffectPlayedPrev = true;
            spawnParticleSystem.Play();
        }
    }

    void CheckForUpdate()
    {
        bool characterSpawned = networkedObject.GetSyncedBool("CharacterSpawned");

        Monster.SetActive(characterSpawned);

        Invoke("CheckForUpdate", .5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter other.transform.name = " + other.transform.name);

        if (other.name.ToLower().Contains("finger"))
        {
            CardClicked();
        }
    }

    public void Reset()
    {
        networkedObject.SetSyncedBool("CharacterSpawned", false);
        cardClicked = false;
        particleEffectPlayedPrev = false;
    }

    bool cardClicked = false;

    public void CardClicked()
    {
        if (GameController.turn == turnThisCardSlotCanBeUsed && !cardClicked)
        {
            cardClicked = true;
            networkedObject.SetSyncedBool("ParticleEffectPlayed", true);
            Invoke("SpawnCharacter", 2);
        }
    }

    void SpawnCharacter()
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
