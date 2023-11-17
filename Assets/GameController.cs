using UnityEngine;

public class GameController : MonoBehaviour
{
    public float Player1Health
    {
        get
        {
            return networkedObject.GetSyncedFloat("Player1Health");
        }
        set
        {
            networkedObject.SetSyncedFloat("Player1Health", value);
        }
    }
    public float Player2Health
    {
        get
        {
            return networkedObject.GetSyncedFloat("Player2Health");
        }
        set
        {
            networkedObject.SetSyncedFloat("Player2Health", value);
        }
    }


    public float Player1Mana
    {
        get
        {
            return networkedObject.GetSyncedFloat("Player1Mana");
        }
        set
        {
            networkedObject.SetSyncedFloat("Player1Mana", value);
        }
    }

    public float Player2Mana
    {
        get
        {
            return networkedObject.GetSyncedFloat("Player2Mana");
        }
        set
        {
            networkedObject.SetSyncedFloat("Player2Mana", value);
        }
    }

    public Transform Player1HealthBar;
    public Transform Player2HealthBar;
    public Transform Player1ManaBar;
    public Transform Player2ManaBar;

    public enum Turn
    {
        Player1sTurn = 0,
        Player1sAttacking = 1,
        Player2sTurn = 2,
        Player2sAttacking = 3
    };
    public static Turn turn
    {
        get
        {
            Turn newTurn = (Turn)Instance.networkedObject.GetSyncedInt("Turn");
            Debug.Log("Getting turn which is " + newTurn);
            return newTurn;
        }
        set
        {
            Debug.Log("Setting turn to " + value);
            Turn newTurn = value;
            Instance.networkedObject.SetSyncedInt("Turn", (int)newTurn);
            Instance.UpdateButtonColors();
        }
    }

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

    public GameObject Player1NextTurnButton;
    public GameObject Player2NextTurnButton;
    public MeshRenderer Player1ButtonRenderer;
    public MeshRenderer Player2ButtonRenderer;
    public Material ButtonDisabledMaterial;
    public Material ButtonEnabledMaterial;

    public static GameController Instance;

    Vector3 StartBarScale;

    private void Start()
    {
        Player1Health = 100;
        Player2Health = 100;
        Player1Mana = 100;
        Player2Mana = 100;

        Instance = this;
        InvokeRepeating("UpdateButtonColors", .5f, .5f);

        StartBarScale = Player1HealthBar.localScale;
    }

    private void Update()
    {
        Player1HealthBar.localScale = Vector3.Scale(
            StartBarScale,
            new Vector3(Player1Health / 100f, 1, 1)
        );

        Player2HealthBar.localScale = Vector3.Scale(
            StartBarScale,
            new Vector3(Player2Health / 100f, 1, 1)
        );

        Player1ManaBar.localScale = Vector3.Scale(
            StartBarScale,
            new Vector3(Player1Mana / 100f, 1, 1)
        );

        Player2ManaBar.localScale = Vector3.Scale(
            StartBarScale,
            new Vector3(Player2Mana / 100f, 1, 1)
        );
    }

    void UpdateButtonColors()
    {
        if (turn == Turn.Player1sTurn)
        {
            Instance.Player1ButtonRenderer.material = Instance.ButtonEnabledMaterial;
        }
        else
        {
            Instance.Player1ButtonRenderer.material = Instance.ButtonDisabledMaterial;
        }

        if (turn == Turn.Player2sTurn)
        {
            Instance.Player2ButtonRenderer.material = Instance.ButtonEnabledMaterial;
        }
        else
        {
            Instance.Player2ButtonRenderer.material = Instance.ButtonDisabledMaterial;
        }
    }

    public static void Player1NextTurnButtonPressed()
    {
        if (turn == Turn.Player1sTurn)
        {
            turn = Turn.Player1sAttacking;
            Instance.Invoke("Player1FinishedAttacking", 3f);
        }
    }

    public static void Player2NextTurnButtonPressed()
    {
        if (turn == Turn.Player2sTurn)
        {
            turn = Turn.Player2sAttacking;
            Instance.Invoke("Player2FinishedAttacking", 3f);
        }
    }

    void Player1FinishedAttacking()
    {
        turn = Turn.Player2sTurn;
        Player2Health -= 10;
    }

    void Player2FinishedAttacking()
    {
        turn = Turn.Player1sTurn;
        Player1Health -= 10;
    }
}
