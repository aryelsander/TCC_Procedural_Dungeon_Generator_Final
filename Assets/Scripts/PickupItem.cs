using UnityEngine;
using Procedural.Map;
public class PickupItem : MonoBehaviour
{
    [SerializeField] private Equipment equipment = null;
    [SerializeField] private WeaponBase weapon = null;
    private SpriteRenderer itemSprite;
    private PlayerController playerReference;

    public Equipment Equipment { get => equipment; set => equipment = value; }
    public WeaponBase Weapon { get => weapon; set => weapon = value; }
    public SpriteRenderer ItemSprite { get => itemSprite; set => itemSprite = value; }


    private void Awake()
    {
        itemSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerReference = collision.GetComponent<PlayerController>();
        if (!playerReference)
            return;

        if (equipment)
        {
            if (equipment.slot == EquipmentSlot.Body)
                EquipmentHud.Instance.ShowEquipmentHud(playerReference.BodyEquipment, equipment);

            if (equipment.slot == EquipmentSlot.Helm)
                EquipmentHud.Instance.ShowEquipmentHud(playerReference.HelmEquipment, equipment);
            if (equipment.slot == EquipmentSlot.Legs)
                EquipmentHud.Instance.ShowEquipmentHud(playerReference.LegsEquipment, equipment);
        }
        if (weapon)
            EquipmentHud.Instance.ShowEquipmentHud(playerReference.Weapon, weapon);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        playerReference = collision.GetComponent<PlayerController>();
        if (playerReference && Input.GetKeyDown(KeyCode.Space))
        {
            if (weapon)
            {

                playerReference.EquipWeapon(weapon);

                Destroy(gameObject);
            }
            if (equipment)
            {

                playerReference.EquipItem(equipment, equipment.slot);

                Destroy(gameObject);
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       
            if(collision.GetComponent<PlayerController>())
                EquipmentHud.Instance.HideEquipmentHud();
            playerReference = null;
        
        //playerReference = null;
    }
    public void DestroyPickupItem()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        DungeonGenerator.OnClearDungeon += DestroyPickupItem;
    }
    private void OnDisable()
    {
        DungeonGenerator.OnClearDungeon -= DestroyPickupItem;

    }
}
