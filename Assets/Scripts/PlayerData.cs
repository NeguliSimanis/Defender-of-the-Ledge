using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour {

    public int exp = 0;
    public int nextLevelExp = 100;
    public int level = 1;
    public int attack = 10;
    public int hp = 100;
    public int maxHp = 100;

    #region Player UI elements
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Image expBar;
    #endregion

    public void Wound(int damage)
    {
        Debug.Log("wounded!");
        hp = hp - damage;
    }

    void Update ()
    {
        healthBar.fillAmount = (hp * 1f) / maxHp;
        expBar.fillAmount = (exp * 1f) / nextLevelExp;
    }
}
