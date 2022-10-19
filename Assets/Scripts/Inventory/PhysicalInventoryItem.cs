using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;


    private void AddItemToInventory()
    {
        if ((playerInventory) && (thisItem))
        {
            if (playerInventory.myInventory.Contains(thisItem))
            {
                thisItem.numberHeld += 1;
            }
            else
            {
                playerInventory.myInventory.Add(thisItem);
                thisItem.numberHeld += 1;
            }
        }
        else
        {
            Debug.Log("Physical item in world is missing inventory or item reference!");
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && (!other.isTrigger))
        {
            AddItemToInventory();
            Destroy(this.gameObject);
        }
    }
}
