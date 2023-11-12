using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum Turn { Player1sTurn, Player2sTurn };
    public static Turn turn;
    public GameObject Player1NextTurnButton;
    public GameObject Player2NextTurnButton;
    public MeshRenderer Player1ButtonRenderer;
    public MeshRenderer Player2ButtonRenderer;
    public Material ButtonDisabledMaterial;
    public Material ButtonEnabledMaterial;

    public static GameController Instance;

    private void Start()
    {
        Instance = this;
        Invoke("SetButtonColors", 1f);
    }

    void SetButtonColors()
    {
        Player1ButtonRenderer.material = ButtonEnabledMaterial;
        Player2ButtonRenderer.material = ButtonDisabledMaterial;
    }

    public static void Player1NextTurnButtonPressed()
    {
        if (turn == Turn.Player1sTurn)
        {
            turn = Turn.Player2sTurn;
            Instance.Player1ButtonRenderer.material = Instance.ButtonDisabledMaterial;
            Instance.Player2ButtonRenderer.material = Instance.ButtonEnabledMaterial;
            //Instance.Player1NextTurnButton.SetActive(false);
            //Instance.Player2NextTurnButton.SetActive(true);
        }
    }
    public static void Player2NextTurnButtonPressed()
    {
        if (turn == Turn.Player2sTurn)
        {
            turn = Turn.Player1sTurn;
            Instance.Player1ButtonRenderer.material = Instance.ButtonEnabledMaterial;
            Instance.Player2ButtonRenderer.material = Instance.ButtonDisabledMaterial;
            //Instance.Player2NextTurnButton.SetActive(false);
            //Instance.Player1NextTurnButton.SetActive(true);
        }
    }
}
