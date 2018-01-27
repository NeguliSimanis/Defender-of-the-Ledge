using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour {

    PlayerController playerController;

    #region Levelling stuff
    public int exp = 0;
    public int nextLevelExp = 100;
    public int level = 1;
    public int unspentSkillPoints = 0;
    #endregion

    #region MELEE
    public int damagePerStren = 1;
    public int damage = 10;
    #endregion

    #region LIFE
    public int hpPerStren = 10;

    public int hp = 100;
    public int maxHp = 100;
    #endregion

    #region MANA
    public int maxManaPerInt = 10;
    public float manaRegenMultiplierPerInt = 1.2f;

    public int mana = 100;
    public int maxMana = 100;
    public int manaRegenAmount;
    public float manaRegenCoefficient = 0.05f;
    public float manaRegenCooldown = 2f;
    #endregion

    #region SKILLS
    public int intelligence = 10;
    public int stren = 10;
    #endregion

    #region SPELLS
    public int spellCost = 30;
    public int spellDamage = 9;
    [SerializeField]
    public GameObject fireball;
    #endregion

    #region states
    public bool isDead = false;
    #endregion 

    [SerializeField]
    private AudioClip woundSFX;
    AudioSource audioSource;

    #region Player UI elements
    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private Image manaBar;

    [SerializeField]
    private Image expBar;

    [SerializeField]
    private GameObject deathScreen;
    #endregion


    void Start()
    {
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        manaRegenAmount = Mathf.RoundToInt(maxMana * manaRegenCoefficient);
        StartRegenMana();
    }

    public void Wound(int damage)
    {
        //Debug.Log("wounded!");
        audioSource.PlayOneShot(woundSFX, 0.9F);
        hp = hp - damage;

        #region DIE
        if (hp <= 0)
        {
            deathScreen.SetActive(true);
            isDead = true;
        }
        #endregion
    }

    public void AddStren()
    {
        stren++;
        unspentSkillPoints--;

        maxHp = maxHp + hpPerStren;
        damage = damage + damagePerStren;

        playerController.RefreshSkillGUIText();
        if (unspentSkillPoints == 0)
        {
            playerController.hasUnspentSkillpoints = false;
            playerController.ShowLevelUpButtons();
        }
    }

    public void AddInt()
    {
        intelligence++;
        unspentSkillPoints--;

        maxMana = maxMana + maxManaPerInt;
        manaRegenAmount = Mathf.RoundToInt(manaRegenAmount *manaRegenMultiplierPerInt);

        playerController.RefreshSkillGUIText();
        if (unspentSkillPoints == 0)
        {
            playerController.hasUnspentSkillpoints = false;
            playerController.ShowLevelUpButtons();
        }
    }

    public void AddExp (int amount)
    {
        exp = exp + amount;

        #region LEVEL UP
        if (exp >= nextLevelExp)
        {
            #region set experience stuff
            Debug.Log("Level up!");
            exp = exp - nextLevelExp;
            level++;
            nextLevelExp = nextLevelExp + 10*level;
            #endregion

            #region replenish health and other stuff
            hp = maxHp;
            mana = maxMana;
            #endregion

            unspentSkillPoints = unspentSkillPoints + 1;
            playerController.hasUnspentSkillpoints = true;
            playerController.ShowLevelUpButtons();
            playerController.RefreshLevelText();
        }
        #endregion
    }

    private void StartRegenMana()
    {
        StartCoroutine(RegenMana());
    }

    IEnumerator RegenMana()
    {
        yield return new WaitForSeconds(manaRegenCooldown);
        if (mana < maxMana)
        {
            mana = mana + manaRegenAmount;
        }
        StartRegenMana();
    }

    void Update()
    {
        healthBar.fillAmount = (hp * 1f) / maxHp;
        expBar.fillAmount = (exp * 1f) / nextLevelExp;
        manaBar.fillAmount = (mana * 1f) / maxMana;
    }
}
