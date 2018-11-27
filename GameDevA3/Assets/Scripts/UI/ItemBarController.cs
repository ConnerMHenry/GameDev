using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBarController : MonoBehaviour {

    public static ItemBarController main { get; private set; }
    public ItemSlotController ItemSlotPrefab;

    private List<ItemSlotController> itemSlots;

	// Use this for initialization
	void Start () {
        if (main == null) {
            main = this;
        }
        else if (main != this) {
            Destroy(this.gameObject);
        }

        itemSlots = new List<ItemSlotController>();

        SetupSlots();
    }


    private void SetupSlots() {
        int slotAmount = 8;

        if (itemSlots == null) {
            itemSlots = new List<ItemSlotController>();
        }
        else {
            itemSlots.Clear();
        }

        ResourceType[] resourceTypes = (ResourceType[]) System.Enum.GetValues(typeof(ResourceType));
        // Create slotAmount many ItemSlots, each spaced 40 pixels(?) between each other.
        for (int i = 0; i < slotAmount; i++) {
            ItemSlotController itemSlot = Instantiate(ItemSlotPrefab) as ItemSlotController;
            itemSlot.transform.parent = this.transform;
            itemSlot.ResourceType = resourceTypes[i];
            //itemSlot.transform.localPosition = Vector3.zero;

            int yPos = 0; // Should be 0, but for some reason the item slot is appearing below the itemBar with -50 as it's y...
            int xPos = -138 + (40 * i);

            itemSlot.transform.localPosition = new Vector2(xPos, yPos);
            itemSlot.transform.localScale = new Vector3(1, 1, 1);
            itemSlots.Add(itemSlot);
        }
    }

    public int AmountOf(ResourceType type) {

        foreach (ItemSlotController itemSlot in itemSlots) {
            if (itemSlot.ResourceType == type) {
                return itemSlot.ItemCount;
            }
        }

        return 0;
    }
	
    public void Add(ResourceType resource, int amount) {
        foreach(ItemSlotController itemSlot in itemSlots) {
            if (itemSlot.ResourceType == resource) {
                itemSlot.ItemCount += amount;
            }
        }
        // Update Building window availability on the different building options
        BuildingWindowController.main.UpdateSlots();
    }


}
