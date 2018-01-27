using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private PlayerData playerData;
    private bool isGamePaused = false;

    // ANIMATIONS
    Animator _animator;

    // MOVEMENT
    private Vector3 mousePosition;
    private float moveSpeed;
    Vector2 targetPosition;
    private bool isMoving = false;

    #region CHARACTER PANEL
    public bool hasUnspentSkillpoints = false;
    private bool isCharPanelOpen = false;
    [SerializeField]
    GameObject charPanel;

    [SerializeField]
    GameObject charPanelOnButton;

    [SerializeField]
    GameObject charPanelOffButton;

    [SerializeField]
    GameObject levelUpButton;

    [SerializeField]
    GameObject levelUpStrenButton;

    [SerializeField]
    GameObject levelUpIntButton;

    [SerializeField]
    GameObject levelUpDexButton;

    #region SKILL TEXT
    [SerializeField]
    Text levelText;

    [SerializeField]
    Text strenText;

    [SerializeField]
    Text intText;

    [SerializeField]
    Text dexText;
    #endregion

    #region STAT TEXT
    [SerializeField]
    Text lifeText;

    [SerializeField]
    Text meleeDamageText;

    [SerializeField]
    Text maxManaText;

    [SerializeField]
    Text manaRegenText;

    [SerializeField]
    Text moveSpeedText;

    [SerializeField]
    Text projeSpeedText;
    #endregion

    #endregion

    #region OTHER UI
    [SerializeField]
    GameObject pauseUI;
    #endregion

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        playerData = gameObject.GetComponent<PlayerData>();
        moveSpeed = playerData.movementSpeed;
        Debug.Log("Player move speed: " + moveSpeed);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
          //  Debug.Log("Collision with enemey!");
            AIController collisionEnemy = coll.gameObject.GetComponent<AIController>();
            if (collisionEnemy.isTargetted)
            {
                _animator.SetTrigger("attack");
                collisionEnemy.Wound(playerData.damage);
            }
        }
    }

    void CastSpell(Vector2 target)
    {
        playerData.mana = playerData.mana - playerData.spellCost;
        GameObject projectile = Instantiate(playerData.fireball,
            gameObject.transform.position, Quaternion.identity, gameObject.transform);
        FireBall fireballProjectile = projectile.GetComponent<FireBall>();
        fireballProjectile.target = target;
        fireballProjectile.Fly();
    }

    void ShowCharPanel()
    {
        isCharPanelOpen = !isCharPanelOpen;
        if(isCharPanelOpen)
        {
            charPanel.SetActive(true);
            charPanelOnButton.SetActive(false);
            charPanelOffButton.SetActive(true);
            levelUpButton.SetActive(!hasUnspentSkillpoints);
        }
        else if (!isCharPanelOpen)
        {
            charPanel.SetActive(false);
            charPanelOnButton.SetActive(true);
            charPanelOffButton.SetActive(false);
            levelUpButton.SetActive(hasUnspentSkillpoints);
         }
    }

    public void ShowLevelUpButtons()
    {
        if (isCharPanelOpen)
        {
            levelUpButton.SetActive(!hasUnspentSkillpoints);
        }
        else if (!isCharPanelOpen)
        {
            levelUpButton.SetActive(hasUnspentSkillpoints);
        }
        levelUpStrenButton.SetActive(hasUnspentSkillpoints);
        levelUpIntButton.SetActive(hasUnspentSkillpoints);
        levelUpDexButton.SetActive(hasUnspentSkillpoints);
    }

    public void RefreshSkillGUIText()
    {
        intText.text = "Intelligence: " + playerData.intelligence;
        strenText.text = "Strength: " + playerData.stren;
        dexText.text = "Dexterity: " + playerData.dex;
    }

    public void RefreshStatsGUIText()
    {
        lifeText.text = "Max Life: " + playerData.maxHp;
        meleeDamageText.text = "Melee Damage: " + playerData.damage;
        maxManaText.text = "Max Mana: " + playerData.maxMana;
        manaRegenText.text = "Mana Regen: " + playerData.manaRegenAmount + "/s";
        moveSpeedText.text = "Movement speed bonus: " + playerData.totalMovementSpeedBonus + "%";
        projeSpeedText.text = "Projectile speed bonus: " + playerData.totalProjSpeedBonus + "%";
    }

    public void RefreshLevelText()
    {
        levelText.text = "Level " + playerData.level;
    }

    public void UpdateMoveSpeed()
    {
        moveSpeed = playerData.movementSpeed;
        Debug.Log("Player move speed: " + moveSpeed);
    }

    private void PauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            pauseUI.SetActive(true);
        }
        else if (!isGamePaused)
        {
            Time.timeScale = 1f;
            pauseUI.SetActive(false);
        }
    }

    void Update()
    {
        if (!playerData.isDead && !isGamePaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                targetPosition = Input.mousePosition;
                targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);
                if (transform.position.x != targetPosition.x && transform.position.y != targetPosition.y)
                {
                    isMoving = true;
                    _animator.SetBool("isMoving", isMoving);
                }
            }

            if (isMoving)
            {
                transform.position = Vector2.Lerp(transform.position, targetPosition, moveSpeed);
            }

            if (transform.position.x == targetPosition.x && transform.position.y == targetPosition.y)
            {
                isMoving = false;
                _animator.SetBool("isMoving", isMoving);
            }

            if (Input.GetMouseButtonDown(1))
            {
                // check if enough mana
                if (playerData.mana >= playerData.spellCost)
                {
                    CastSpell(Input.mousePosition);
                } 
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                ShowCharPanel();    
            }
        }

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
}
