using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    #region Fields & Properties
    #region Fields
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private string nextScene;
    [SerializeField] private GameObject rockets;
    [SerializeField] private GameObject bigSpaceGun;
    [SerializeField] private GameObject autoCannon;
    [SerializeField] private GameObject superShield;
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text text;
    private float fCooldown;
    private float fCooldownTimer;
    private bool bCooldown;
    private int iStage;
    #endregion

    #region Properties
    #endregion
    #endregion
    
    #region Methods
    private void Start()
    {
        SetText("Welcome to the tutorial !", 3.0f);
        iStage = 0;
    }

    private void Update()
    {
        UpdateTimer();
        
        switch (iStage)
        {
            case 0: TryTakeDamage();
                break;
            case 1: TryMove();
                break;
            case 2: TryShoot();
                break;
            case 3: TrySuperShield();
                break;
            case 4: TryChangeLevel();
                break;
            case 5: ChangeLevel();
                break;
        }
    }

    private void UpdateTimer()
    {
        if (!bCooldown)
            return;
        
        fCooldownTimer += Time.deltaTime;

        if (fCooldownTimer < fCooldown)
            return;
        
        SetText("");
        bCooldown = false;
    }
    
    private void SetText(string _text)
    {
        text.gameObject.SetActive(_text.Length > 0);
        text.text = _text;
    }
    
    private void SetText(string _text, float _time)
    {
        SetText(_text);
        fCooldown = _time;
        fCooldownTimer = 0.0f;
        bCooldown = true;
    }
    
    private void TryTakeDamage()
    {
        if (bCooldown)
            return;
        
        SetText("When taking damage, only your shield will regen", 5.0f);
        player.TakeDamage(50.0f);
        player.TakeDamage(10.0f);
        iStage++;
    }
    
    private void TryMove()
    {
        if (bCooldown)
            return;
        
        SetText($"Press <color={InputManager.Instance.InputColor}>[{player.Inputs.Up}][{player.Inputs.Left}][{player.Inputs.Down}][{player.Inputs.Right}]</color> to move", 0.1f);
        player.Inputs.SetMove(true);

        if (!player.Movement.Moving)
            return;
        
        SetText($"Press <color={InputManager.Instance.InputColor}>[{player.Inputs.Up}][{player.Inputs.Left}][{player.Inputs.Down}][{player.Inputs.Right}]</color> to move", 1.0f);
        iStage++;
    }

    private void TryShoot()
    {
        if (!rockets && autoCannon && bigSpaceGun)
        {
            Destroy(autoCannon);
            Destroy(bigSpaceGun);
        }
        else if (rockets && !autoCannon && bigSpaceGun)
        {
            Destroy(rockets);
            Destroy(bigSpaceGun);
        }
        else if (rockets && autoCannon && !bigSpaceGun)
        {
            Destroy(rockets);
            Destroy(autoCannon);
        }
        
        if (bCooldown)
            return;

        if (rockets && autoCannon && bigSpaceGun)
        {
            player.Movement.SetPosition(playerPosition);
            rockets.SetActive(true);
            autoCannon.SetActive(true);
            bigSpaceGun.SetActive(true);
            SetText($"Move to a pickUp to collect it, you can only choose one", 3.0f);
        }
        else
        {
            SetText($"Press <color={InputManager.Instance.InputColor}>[{player.Inputs.Shoot}]</color> to shoot", 3.0f);
            player.Inputs.SetShoot(true);
            iStage++;
        }
    }
    
    private void TrySuperShield()
    {
        if (bCooldown)
            return;

        if (superShield)
        {
            player.Movement.SetPosition(playerPosition);
            superShield.SetActive(true);
            SetText($"Move to the loot to collect it", 3.0f);
        }
        else
        {
            SetText($"Press <color={InputManager.Instance.InputColor}>[{player.Inputs.SuperShield}]</color> to become invincible for 5s", 3.0f);
            player.Inputs.SetSuperShield(true);

            if (player.Stats.SuperShield)
                iStage++;
        }
        
    }

    private void TryChangeLevel()
    {
        if (bCooldown)
            return;
        
        SetText("You will start the real game", 3.0f);
        iStage++;
    }

    private void ChangeLevel()
    {
        if (bCooldown)
            return;
        
        player.Health.MaxOut();
        player.Shield.MaxOut();
        SceneManager.LoadScene(nextScene);
    }
    #endregion
}
