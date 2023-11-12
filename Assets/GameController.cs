using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum Turn { Player1sTurn, Player1sAttacking, Player2sTurn, Player2sAttacking };
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
            turn = Turn.Player1sAttacking;
            Instance.Invoke("Player1FinishedAttacking", 5f);
            Instance.Player1ButtonRenderer.material = Instance.ButtonDisabledMaterial;
            TriggerAttackAnimations();
            //Instance.Player2ButtonRenderer.material = Instance.ButtonEnabledMaterial;
            //Instance.Player1NextTurnButton.SetActive(false);
            //Instance.Player2NextTurnButton.SetActive(true);
        }
    }

    public static void Player2NextTurnButtonPressed()
    {
        if (turn == Turn.Player2sTurn)
        {
            turn = Turn.Player2sAttacking;
            Instance.Invoke("Player2FinishedAttacking", 5f);
            Instance.Player2ButtonRenderer.material = Instance.ButtonDisabledMaterial;
            TriggerAttackAnimations();
            //Instance.Player2NextTurnButton.SetActive(false);
            //Instance.Player1NextTurnButton.SetActive(true);
        }
    }

    static void TriggerAttackAnimations()
    {
        Unit[] units = FindObjectsOfType<Unit>();

        foreach (Unit unit in units)
        {
            if (unit.TeamID == 1 && turn == Turn.Player1sAttacking)
            {
                unit.animator.SetBool("Attacking", true);
            }

            if (unit.TeamID == 2 && turn == Turn.Player2sAttacking)
            {
                unit.animator.SetBool("Attacking", true);
            }
        }

        Instance.Invoke("ResetAnimationStates", 4f);
    }

    void ResetAnimationStates()
    {
        Unit[] units = FindObjectsOfType<Unit>();

        foreach (Unit unit in units)
        {
            if (unit.TeamID == 1 && turn == Turn.Player1sAttacking)
            {
                unit.animator.SetBool("Attacking", false);
            }

            if (unit.TeamID == 2 && turn == Turn.Player2sAttacking)
            {
                unit.animator.SetBool("Attacking", false);
            }
        }
    }

    void Player1FinishedAttacking()
    {
        turn = Turn.Player2sTurn;
        Instance.Player2ButtonRenderer.material = Instance.ButtonEnabledMaterial;
    }

    void Player2FinishedAttacking()
    {
        turn = Turn.Player1sTurn;
        Instance.Player1ButtonRenderer.material = Instance.ButtonEnabledMaterial;
    }
}
