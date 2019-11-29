using Enemy.AI.Attack;
using Enemy.AI.Escape;
using Enemy.AI.Follow;
using Enemy.AI.Patrol;
using Helper.Utilities;
using UnityEngine;

namespace Enemy
{
    enum EnemyState
    {
        Patrol,
        Follow,
        Escape,
        Attack,
    }
    public class EnemyController : BaseUnityController
    {
        [SerializeField] private PatrolCommand patrolCommand;
        [SerializeField] private FollowCommand followCommand;
        [SerializeField] private EscapeCommand escapeCommand;
        [SerializeField] private AttackCommand attackCommand;
        [SerializeField] private float patrolDistance;
        private const float patrolDistanceFix = 1.5f;
        [SerializeField] private float minFollowDistance;
        [SerializeField] private float maxFollowDistance;
        [SerializeField] private float escapeDistance;
        [SerializeField] private float attackDistance;
        [SerializeField] private Transform shootSpawnPosition;
        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private LayerMask wallMask;

        [SerializeField] private int enemyScore;
        [SerializeField] private GameObject healthBar;
        [SerializeField] private float dropChance;
        [SerializeField] PickupItem pickupItem;
        [SerializeField] private Equipment headDrop;

        [SerializeField] private Equipment bodyDrop;
        [SerializeField] private Equipment legsDrop;
        [SerializeField] private WeaponBase[] weapons;

        private float baseFollowDistance;
        private EnemyState state;
        private float currentTimeToAttack;
        private new Rigidbody2D rigidbody2D;
        private Vector3 patrolTransform;
        private Vector3 checkPosition = Vector3.zero;
        private Collider2D enemyCollider;
        private Room enemyRoom;

        public Room EnemyRoom { get => enemyRoom; set => enemyRoom = value; }
        public int EnemyScore { get => enemyScore; set => enemyScore = value; }
        public float AttackDistance { get => attackDistance; }

        private void Awake()
        {
            state = EnemyState.Patrol;
            rigidbody2D = GetComponent<Rigidbody2D>();
            patrolTransform = transform.position;
        }

        private void Start()
        {
            UpdateHealth();
            currentTimeToAttack = Weapon.TimeToAttack;
            baseFollowDistance = minFollowDistance;
        }

        private void Update()
        {

            if (GameManager.instance.GameState != GameState.INGAME)
                return;
            HealthRegen();
            currentTimeToAttack -= Time.deltaTime;
            enemyCollider = Physics2D.OverlapCircle(transform.position, baseFollowDistance, enemyMask);
            if (enemyCollider)
            {
                HelperUtilities.LookAt2D(transform, enemyCollider.transform.position, 0);
            }
            else
            {
                HelperUtilities.LookAt2D(transform, patrolTransform, 0);
            }

            if (state == EnemyState.Patrol)
            {
                if (!enemyCollider)
                {
                    baseFollowDistance = minFollowDistance;
                    FindNextPath();
                    patrolCommand.Patrolling(transform, patrolTransform, currentBaseStats.Movespeed);

                }
                if (enemyCollider)
                {
                    state = EnemyState.Follow;
                    baseFollowDistance = maxFollowDistance;
                }
            }
            if (state == EnemyState.Follow)
            {
                if (!enemyCollider)
                {
                    state = EnemyState.Patrol;
                    patrolTransform = transform.position;
                }
                if (enemyCollider && Vector2.Distance(transform.position, enemyCollider.transform.position) > attackDistance)
                {
                    followCommand.Following(transform, enemyCollider.transform, currentBaseStats.Movespeed);
                }
                if (enemyCollider && Vector2.Distance(transform.position, enemyCollider.transform.position) <= attackDistance)
                {
                    state = EnemyState.Attack;
                }
            }
            if (state == EnemyState.Attack)
            {
                if (enemyCollider)
                {
                    if (currentTimeToAttack <= 0)
                    {
                        attackCommand.Attacking(this, shootSpawnPosition, enemyCollider.transform.position, enemyTag, currentBaseStats.Attack);
                        currentTimeToAttack = Weapon.TimeToAttack;
                    }
                    if (Vector2.Distance(transform.position, enemyCollider.transform.position) < baseFollowDistance)
                    {
                        escapeCommand.Escaping(transform, enemyCollider.transform.position, currentBaseStats.Movespeed);
                    }
                }
                if (!enemyCollider)
                {
                    state = EnemyState.Patrol;
                    patrolTransform = transform.position;
                }

            }
        }

        public void DropEquipment()
        {
            int randomDropNumber = Random.Range(0, 100);

            if (randomDropNumber <= dropChance)
            {
                //Drop
                CreateNewEquipment();

            }
        }

        protected override void HealthRegen()
        {
            base.HealthRegen();
            UpdateHealth();
        }

        private void FindNextPath()
        {
            if (Vector2.Distance(patrolTransform, transform.position) < 0.3f)
            {

                RaycastHit2D colliderCheck;
                checkPosition = GetRandomPatrolPosition();

                colliderCheck = Physics2D.Raycast(transform.position, checkPosition, patrolDistance + patrolDistanceFix, wallMask);
                if (!colliderCheck.collider)
                {
                    patrolTransform = transform.position + checkPosition * patrolDistance;
                }

            }
        }

        public void CreateNewEquipment()
        {
            Equipment equipment;
            WeaponBase weapon;
            int randomChoice = Random.Range(0, 4);


            if (randomChoice == 0)
            {
                equipment = ScriptableObject.CreateInstance<Equipment>();

                equipment.name = $"Body +{GameManager.instance.stage}";
                equipment.SetEquipmentStats(equipment, bodyDrop);
                equipment.MinimumEquipmentStats = bodyDrop.MinimumEquipmentStats;

                equipment.MaximumEquipmentStats = bodyDrop.MaximumEquipmentStats;
                equipment.slot = EquipmentSlot.Body;
                equipment.SetRandomStats();
                PickupItem pickup = Instantiate(pickupItem, transform.position, Quaternion.identity);
                pickup.ItemSprite.sprite = equipment.EquipmentSprite;
                pickup.Equipment = equipment;
            }
            if (randomChoice == 1)
            {
                equipment = ScriptableObject.CreateInstance<Equipment>();

                equipment.name = $"Head +{GameManager.instance.stage}";
                equipment.SetEquipmentStats(equipment, headDrop);
                equipment.slot = EquipmentSlot.Helm;

                equipment.MinimumEquipmentStats = headDrop.MinimumEquipmentStats;

                equipment.MaximumEquipmentStats = headDrop.MaximumEquipmentStats;
                equipment.SetRandomStats();
                PickupItem pickup = Instantiate(pickupItem, transform.position, Quaternion.identity);
                pickup.ItemSprite.sprite = equipment.EquipmentSprite;
                pickup.Equipment = equipment;
            }
            if (randomChoice == 2)
            {
                equipment = ScriptableObject.CreateInstance<Equipment>();
                equipment.name = $"Legs +{GameManager.instance.stage}";
                equipment.SetEquipmentStats(equipment, legsDrop);
                equipment.slot = EquipmentSlot.Legs;

                equipment.MinimumEquipmentStats = legsDrop.MinimumEquipmentStats;

                equipment.MaximumEquipmentStats = legsDrop.MaximumEquipmentStats;
                equipment.SetRandomStats();
                PickupItem pickup = Instantiate(pickupItem, transform.position, Quaternion.identity);
                pickup.ItemSprite.sprite = equipment.EquipmentSprite;
                pickup.Equipment = equipment;
            }
            if (randomChoice == 3)
            {
                WeaponBase choosedWeapon = weapons[Random.Range(0, weapons.Length)];
                weapon = (WeaponBase)ScriptableObject.CreateInstance(choosedWeapon.GetType());
                weapon.name = $"{choosedWeapon.name} +{GameManager.instance.stage}";

                weapon.SetEquipmentStats(weapon, choosedWeapon);
                weapon.slot = EquipmentSlot.Weapon;

                weapon.MinimumEquipmentStats = choosedWeapon.MinimumEquipmentStats;
                weapon.MaximumEquipmentStats = choosedWeapon.MaximumEquipmentStats;
                weapon.Projectile = choosedWeapon.Projectile;
                weapon.ShootForce = choosedWeapon.ShootForce;
                weapon.TimeToAttack = choosedWeapon.TimeToAttack;
                weapon.TimeToDestroy = choosedWeapon.TimeToDestroy;
                weapon.SetRandomStats();
                PickupItem pickup = Instantiate(pickupItem, transform.position, Quaternion.identity);
                pickup.ItemSprite.sprite = weapon.EquipmentSprite;
                pickup.Weapon = weapon;

            }

        }

        public void ApplyStatsByPower()
        {
            currentBaseStats.Health = currentBaseStats.Health * GameManager.instance.stage;
            currentBaseStats.CurrentHealth = currentBaseStats.Health;
            currentBaseStats.Attack = currentBaseStats.Attack * GameManager.instance.stage;
            currentBaseStats.Attack = currentBaseStats.Defense * GameManager.instance.stage;
            currentBaseStats.HealthRegen = currentBaseStats.HealthRegen * GameManager.instance.stage;
            currentBaseStats.Movespeed = currentBaseStats.Movespeed;
        }

        public override void OnTakeDamage(float attack)
        {
            currentBaseStats.CurrentHealth -= attack - currentBaseStats.Defense >= 0.1f ? attack - currentBaseStats.Defense : 0.1f;
            PlayerUI.Instance.ShowDamage(transform, attack - currentBaseStats.Defense >= 0.1f ? attack - currentBaseStats.Defense : 0.1f);

            UpdateHealth();
            if (currentBaseStats.CurrentHealth <= 0)
            {
                GameManager.instance.score += enemyScore;
                PlayerUI.Instance.UpdateScore();
                DropEquipment();
                enemyRoom.EnemyDeath();
                Destroy(gameObject);
            }
        }

        public Vector3 GetRandomPatrolPosition()
        {

            Vector3 randomDistance = new Vector3(Random.Range(-patrolDistance, patrolDistance), Random.Range(-patrolDistance, patrolDistance), 0).normalized;

            return randomDistance;
        }
        public void UpdateHealth()
        {
            healthBar.transform.localScale = new Vector3(currentBaseStats.CurrentHealth / currentBaseStats.Health, healthBar.transform.localScale.y, 1);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!enemyCollider)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, baseFollowDistance);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, patrolTransform);
        }
#endif
    }
}