using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public GameObject[] Monster;
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
        int characterSpawned = networkedObject.GetSyncedInt("CharacterSpawned");

        UpdateCharactersVisibility();

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
        networkedObject.SetSyncedInt("CharacterSpawned", -1);
        networkedObject.SetSyncedBool("ParticleEffectPlayed", false);
        cardClicked = false;
        particleEffectPlayedPrev = false;
    }

    bool cardClicked = false;

    public void CardClicked()
    {
        if (GameController.turn == turnThisCardSlotCanBeUsed && !cardClicked)
        {
            if (GameController.Instance.CurrentPlayerHasMana(75))
            {
                GameController.Instance.SubtractManaFromCurrentPlayer(75);
                cardClicked = true;
                networkedObject.SetSyncedBool("ParticleEffectPlayed", true);
                Invoke("SpawnCharacter", 1);
            }
        }
    }

    void SpawnCharacter()
    {
        networkedObject.SetSyncedInt("CharacterSpawned", Random.Range(1, Monster.Length));
    }

    public void ShowMonster()
    {
        meshRenderer.material = Selected;

        UpdateCharactersVisibility();

        Invoke("SetNeutral", 2f);
    }

    void UpdateCharactersVisibility()
    {
        int characterSpawned = networkedObject.GetSyncedInt("CharacterSpawned");

        for (int i = 0; i < Monster.Length; i++)
        {
            Monster[i].SetActive(characterSpawned == (i + 1));
        }
    }

    void SetNeutral()
    {
        meshRenderer.material = Neutral;
    }

    void HideMonster()
    {
        meshRenderer.material = Neutral;

        for (int i = 0; i < Monster.Length; i++)
        {
            Monster[i].SetActive(false);
        }
    }
}
