using UnityEngine;
using Cinemachine;
using Procedural.Map;
using Helper.Utilities;

public class PlayerController : BaseUnityController
{

    [SerializeField] private Equipment helmEquipment;
    [SerializeField] private Equipment bodyEquipment;
    [SerializeField] private Equipment legsEquipment;
    [SerializeField] private Transform shootSpawnPosition;
    private float currentTimeToShoot;
    private CinemachineVirtualCamera virtualCamera;
    private Vector2 inputAxes;
    private Rigidbody2D rigibody2D;
    private Camera mainCamera;

    public Equipment LegsEquipment { get => legsEquipment;}
    public Equipment BodyEquipment { get => bodyEquipment;}
    public Equipment HelmEquipment { get => helmEquipment;}

    private void Awake()
    {
        mainCamera = Camera.main;
        rigibody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        currentBaseStats.CurrentHealth = currentBaseStats.Health;
    }
    private void Update()
    {
        if(GameManager.instance.GameState == GameState.INGAME)
        {
            PlayerAttack();
            GetInputAxes();
            HealthRegen();
            HelperUtilities.LookAt2D(transform, GetMousePosition(), 0);
        }
    }
    
    public Vector3 GetMousePosition()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0));

        return mousePosition;
    }
    private void PlayerAttack()
    {
        currentTimeToShoot -= Time.deltaTime;
        if (Input.GetMouseButton(0) && currentTimeToShoot <= 0)
        {
            Weapon.Attack(shootSpawnPosition, GetMousePosition(),enemyTag,currentBaseStats.Attack);
            currentTimeToShoot = Weapon.TimeToAttack;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.GameState == GameState.INGAME)
        {
            Move();
        }else if (GameManager.instance.GameState == GameState.PAUSED)
        {
            rigibody2D.velocity = Vector2.zero;
        }
    }

    private void Move()
    {
        rigibody2D.velocity = new Vector2(inputAxes.x, inputAxes.y) * currentBaseStats.Movespeed;
    }

    public void SetupCamera()
    {
        
        virtualCamera = DungeonGenerator.rooms[0].GetComponent<Room>().virtualCameraRoom;
        virtualCamera.gameObject.SetActive(true);
        virtualCamera.Follow = transform;
    }
    private void GetInputAxes()
    {
        inputAxes.x = Input.GetAxisRaw("Horizontal");
        inputAxes.y = Input.GetAxisRaw("Vertical");
        inputAxes.Normalize();
    }

    public void ChangeCamera(CinemachineVirtualCamera newCamera)
    {

        virtualCamera.gameObject.SetActive(false);
        virtualCamera = newCamera;
        virtualCamera.gameObject.SetActive(true);
        virtualCamera.Follow = transform;
    }
    public override void OnTakeDamage(float attack)
    {
        currentBaseStats.CurrentHealth -= attack - currentBaseStats.Defense >= 0.1f ? attack - currentBaseStats.Defense : 0.1f;
        PlayerUI.Instance.ShowDamage(transform, attack - currentBaseStats.Defense >= 0.1f ? attack - currentBaseStats.Defense : 0.1f);
        PlayerUI.Instance.UpdateHealth(currentBaseStats.Health, currentBaseStats.CurrentHealth);
        if(currentBaseStats.CurrentHealth <= 0)
        {
            GameManager.instance.SetScoreText();
            Destroy(gameObject);
        }
    }
    protected override void HealthRegen()
    {
        base.HealthRegen();
        PlayerUI.Instance.UpdateHealth(currentBaseStats.Health, currentBaseStats.CurrentHealth);
    }
    protected void SetEquipmentStats(Equipment currentEquip,Equipment equip)
    {
        currentBaseStats.Health -= currentEquip.Health;
        currentBaseStats.CurrentHealth -= currentEquip.Health;
        currentBaseStats.Attack -= currentEquip.Attack;
        currentBaseStats.HealthRegen -= currentEquip.HealthRegen;
        currentBaseStats.Movespeed -= currentEquip.Movespeed;
        currentBaseStats.Defense -= currentEquip.Defense;

        currentBaseStats.Health += equip.Health;
        currentBaseStats.CurrentHealth += equip.Health;
        currentBaseStats.Attack += equip.Attack;
        currentBaseStats.HealthRegen += equip.HealthRegen;
        currentBaseStats.Movespeed += equip.Movespeed;
        currentBaseStats.Defense += equip.Defense;
        PlayerUI.Instance.UpdateHealth(currentBaseStats.Health, currentBaseStats.CurrentHealth);
    }
    public void EquipItem(Equipment equip,EquipmentSlot slot)
    {
        if(slot == EquipmentSlot.Body)
        {
            SetEquipmentStats(bodyEquipment, equip);
            bodyEquipment = equip;
        }
        if(slot == EquipmentSlot.Helm)
        {
            SetEquipmentStats(helmEquipment, equip);
            helmEquipment = equip;
        }
        if(slot == EquipmentSlot.Legs)
        {
            SetEquipmentStats(legsEquipment, equip);
            legsEquipment = equip;
        }
    }
    public void EquipWeapon(WeaponBase weapon)
    {
        SetEquipmentStats(Weapon, weapon);
        Weapon = weapon;
    }
    

}
