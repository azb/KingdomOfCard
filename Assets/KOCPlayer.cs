using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOCPlayer : MonoBehaviour
{
    public int ID;
    public float Health;
    public float Mana;

    public float DisplayHealth;
    public float DisplayMana;

    public Transform healthBar;
    public Transform manaBar;

    float healthBarWidth = 2f;
    float manaBarWidth = 2f;

    void Start()
    {

    }

    void Update()
    {
        DisplayHealth = (Health + DisplayHealth) / 2f * Time.deltaTime;
        DisplayMana = (Mana + DisplayMana) / 2f * Time.deltaTime;
        if (healthBar != null)
        {
            healthBar.transform.localScale = new Vector3(
                healthBar.transform.localScale.x,
                healthBar.transform.localScale.y,
                healthBar.transform.localScale.z
            );
        }
    }
}
