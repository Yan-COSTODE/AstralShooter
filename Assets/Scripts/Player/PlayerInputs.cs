using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    #region Fields & Properties
    #region Fields
    [SerializeField] private float fSpeed = 10.0f;
    [SerializeField] private bool bCanMove = false;
    [SerializeField] private bool bCanShoot = false;
    [SerializeField] private bool bCanSuperShield = false;
    private float fHorizontal = 0.0f;
    private float fVertical = 0.0f;
    private KeyCode left;
    private KeyCode right;
    private KeyCode up;
    private KeyCode down;
    private KeyCode shoot;
    private KeyCode pause;
    private KeyCode superShield;
    private Player player;
    #endregion

    #region Properties
    public KeyCode Left => left;
    public KeyCode Right => right;
    public KeyCode Up => up;
    public KeyCode Down => down;
    public KeyCode Shoot => shoot;
    public KeyCode Pause => pause;
    public KeyCode SuperShield => superShield;
    #endregion
    #endregion
    
    #region Methods
    private void Start()
    {
        UpdateKey();
    }

    private void Update()
    {
        if (bCanMove)
        {
            player.Movement.ResetMove();
            player.Movement.MoveHorizontal(GetHorizontal());
            player.Movement.MoveVertical(GetVertical());
        }

        if (bCanShoot && Input.GetKey(shoot))
            player.Armory.Shoot();
        
        if (bCanSuperShield && Input.GetKeyDown(superShield))
            player.Stats.ActivateSuperShield();
        
        if (Input.GetKeyDown(pause))
            UIManager.Instance.UIPauseMenu.Open();
    }

    public void SetMove(bool _status) => bCanMove = _status;

    public void SetShoot(bool _status) => bCanShoot = _status;

    public void SetSuperShield(bool _status) => bCanSuperShield = _status;
    
    public void Register(Player _player) => player = _player;

    public void UpdateKey()
    {
        left = InputManager.Instance.GetInput("Left");
        right = InputManager.Instance.GetInput("Right");
        up = InputManager.Instance.GetInput("Up");
        down = InputManager.Instance.GetInput("Down");
        shoot = InputManager.Instance.GetInput("Shoot");
        pause = InputManager.Instance.GetInput("Pause");
        superShield = InputManager.Instance.GetInput("SuperShield");
    }
    
    private float GetHorizontal()
    {
        float _horizontal = 0.0f;

        if (Input.GetKey(right))
            _horizontal += 1.0f;
        
        if (Input.GetKey(left))
            _horizontal -= 1.0f;

        fHorizontal = Mathf.Lerp(fHorizontal, _horizontal, fSpeed * Time.deltaTime);
        return fHorizontal;
    }

    private float GetVertical()
    {
        float _vertical = 0.0f;

        if (Input.GetKey(up))
            _vertical += 1.0f;
        
        if (Input.GetKey(down))
            _vertical -= 1.0f;

        fVertical = Mathf.Lerp(fVertical, _vertical, fSpeed * Time.deltaTime);
        return fVertical;
    }
    #endregion
}
