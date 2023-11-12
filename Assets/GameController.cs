using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Foundry.Networking;

public class GameController : NetworkComponent
{
    public enum Turn { Player1sTurn=0, Player1sAttacking=1, Player2sTurn=2, Player2sAttacking=3 };
    // public static Turn turn;
    public MeshRenderer Player1ButtonRenderer;
    public MeshRenderer Player2ButtonRenderer;
    public Material ButtonDisabledMaterial;
    public Material ButtonEnabledMaterial;
    public NetworkProperty<Turn> _turn = new(0);
    public Turn turn
    {
        get => _turn.Value;
        set => _turn.Value = value;
    }

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
        Instance.GetComponent<NetworkObject>().RequestOwnership();
        if (Instance.turn == Turn.Player1sTurn)
        {
            Instance.turn = Turn.Player1sAttacking;
            // Instance.Invoke("Player1FinishedAttacking", 5f);
            // Instance.Player1ButtonRenderer.material = Instance.ButtonDisabledMaterial;
            // TriggerAttackAnimations();
        }
    }

    public static void Player2NextTurnButtonPressed()
    {
        Instance.GetComponent<NetworkObject>().RequestOwnership();
        if (Instance.turn == Turn.Player2sTurn)
        {
            Instance.turn = Turn.Player2sAttacking;
            // Instance.Invoke("Player2FinishedAttacking", 5f);
            // Instance.Player2ButtonRenderer.material = Instance.ButtonDisabledMaterial;
            // TriggerAttackAnimations();
        }
    }

    static void TriggerAttackAnimations()
    {
        Unit[] units = FindObjectsOfType<Unit>();

        foreach (Unit unit in units)
        {
            if (unit.TeamID == 1 && Instance.turn == Turn.Player1sAttacking)
            {
                unit.animator.SetBool("Attacking", true);
            }

            if (unit.TeamID == 2 && Instance.turn == Turn.Player2sAttacking)
            {
                unit.animator.SetBool("Attacking", true);
            }
        }

        Instance.Invoke("ResetAnimationStates", 4f);
    }

    static void ResetAnimationStates()
    {
        Unit[] units = FindObjectsOfType<Unit>();

        foreach (Unit unit in units)
        {
            if (unit.TeamID == 1 && Instance.turn == Turn.Player1sAttacking)
            {
                unit.animator.SetBool("Attacking", false);
            }

            if (unit.TeamID == 2 && Instance.turn == Turn.Player2sAttacking)
            {
                unit.animator.SetBool("Attacking", false);
            }
        }
    }

    void Player1FinishedAttacking()
    {
        turn = Turn.Player2sTurn;
        // Instance.Player2ButtonRenderer.material = Instance.ButtonEnabledMaterial;
    }

    void Player2FinishedAttacking()
    {
        turn = Turn.Player1sTurn;
        // Instance.Player1ButtonRenderer.material = Instance.ButtonEnabledMaterial;
    }

    public override void RegisterProperties(List<INetworkProperty> props)
    {
        // This callback is called both when the value is set locally and when it is set remotely.
        _turn.OnValueChanged += t =>
        {

            if (t == Turn.Player1sAttacking)
            {
                Player1ButtonRenderer.material = ButtonDisabledMaterial;
                Player2ButtonRenderer.material = ButtonDisabledMaterial;
                TriggerAttackAnimations();
                Invoke("Player1FinishedAttacking", 5f);
            }
            else if (t == Turn.Player2sAttacking)
            {
                Player1ButtonRenderer.material = ButtonDisabledMaterial;
                Player2ButtonRenderer.material = ButtonDisabledMaterial;
                TriggerAttackAnimations();
                Invoke("Player2FinishedAttacking", 5f);
            }
            else if (t == Turn.Player1sTurn)
            {
                Player1ButtonRenderer.material = ButtonEnabledMaterial;
                Player2ButtonRenderer.material = ButtonDisabledMaterial;
            }
            else if (t == Turn.Player2sTurn)
            {
                Player2ButtonRenderer.material = ButtonEnabledMaterial;
                Player1ButtonRenderer.material = ButtonDisabledMaterial;
            }
        };
        props.Add(_turn);
    }
}
