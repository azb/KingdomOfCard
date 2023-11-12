
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviourPunCallbacks
{
    public AudioSource HitAudioSource;
    public AudioClip HitAudioClip;

    public UnityEvent onDeath;

    NetworkedObject _networkedObject;
    NetworkedObject networkedObject
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

    float _health;
    public float health
    {
        get
        {
            float h = networkedObject.GetSyncedFloat("Health");
            Debug.Log("health = "+ h);
            return networkedObject.GetSyncedFloat("Health");
        }

        set
        {
            _health = Mathf.Clamp(value, -1f, maxHealth);

            if (PhotonNetwork.OfflineMode && PhotonNetwork.IsMasterClient)
            {
                if (_health >= this.maxHealth || _health <= 0)
                {
                    networkedObject.SetSyncedFloat("Health", _health);
                }
                /*
                else
                {
                    Debug.Log("Not updating health because Time.fixedUnscaledTime - timeOfLastHealthUpdate = "+(Time.fixedUnscaledTime - timeOfLastHealthUpdate));
                }*/
            }
            else
            {
                if (_health < 0)
                {
                    Kill();
                }
            }
        }
    }

    public float maxHealth;
    public bool underConstruction = false;
    float timeAlive;
    bool queueDestroy;
    public bool destroyed;
    public bool invincible;

    int UpdatesPerSecond = 1;
    float TimeToNextUpdate = 0;

    // Use this for initialization
    void Start()
    {
        _health = maxHealth;
        if (NetworkSync.IsMasterClient || NetworkSync.PlayingOffline)
        {
            networkedObject.SetSyncedFloat("Health", _health);
        }
    }
    /*
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        networkedObject.SetSyncedFloat("Health", _health);
    }*/

    // Update is called once per frame
    void Update()
    {
        if (!NetworkSync.IsMasterClient)
        {
            SetHealth(networkedObject.GetSyncedFloat("Health"));
        }

        if (timeAlive < 10f)
        {
            timeAlive += Time.deltaTime;
        }

        //Later replace this check with a single IEnumerator

        if (queueDestroy && !destroyed)
        {
            Kill();
        }

        if (networkedObject.GetSyncedFloat("Health") < 0)
        {
            Kill();
        }

        if (NetworkSync.IsMasterClient)
        {
            if (TimeToNextUpdate <= 0)
            {
                networkedObject.SetSyncedFloat("Health", _health);
                TimeToNextUpdate = 1f / ((float)UpdatesPerSecond);
            }
            else
            {
                TimeToNextUpdate -= Time.deltaTime;
            }
        }
    }

    private void OnEnable()
    {
        if (destroyed)
        {
            gameObject.SetActive(false);
        }
    }

    public void Kill()
    {
        destroyed = true;
        gameObject.SetActive(false);
        queueDestroy = false;
        onDeath.Invoke();
        Destroy(gameObject);
    }

    //Adds a value healthToAdd to current health value, this can be a positive or negative number
    public void AddHealth(float healthToAdd)
    {
        if (destroyed)
            return;

        if (NetworkSync.IsMasterClient)
        {
            if (invincible)
            {
                return;
            }

            health += healthToAdd;
            networkedObject.SetSyncedFloat("Health", _health);
        }
    }

    public void SetHealth(float newHealth)
    {
        if (NetworkSync.IsMasterClient || NetworkSync.PlayingOffline || NetworkSync.PlayerCount == 1)
        {
            _health = newHealth;

            networkedObject.SetSyncedFloat("Health", _health);
        }

        if (_health < 0)
        {
            Kill();
        }
    }
}
