using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotController : MonoBehaviour {

    public Image resourceImage;
    public Text amountText;
    public Text amountUpdateText;
    public Animator animator;

    public Color emptyColor;

    public bool IsAnimating { get; private set; }

    private ResourceType resourceType = ResourceType.None;
    public ResourceType ResourceType {
        get {
            return resourceType;
        }
        set {
            resourceType = value;
            LoadSprite();
        }
    }

    public ResourceOptions spriteBundles;

    private int itemAmount = 0;
    public int ItemCount {
        get {
            return itemAmount;
        }
        set {
            // Only update if not setting the new value to 0
            if (value != 0)
            {
                int diff = value - itemAmount;
                ShowAmountUpdate(diff, diff > 0);
            }

            UpdateAmountText(value);

            itemAmount = value;
        }
    }

	// Use this for initialization
	void Start () {
        IsAnimating = false;
        ItemCount = 0;

        if (ResourceType != ResourceType.None) {
            LoadSprite();
        }
	}

    private void ShowAmountUpdate(int amount, bool positive) {
        string displayText = (positive ? "+" : "-") + System.Math.Abs(amount).ToString("00");
        amountUpdateText.text = displayText;
        // Start animation
        animator.Play("UpdateCountTextAnim");
    }

    private void UpdateAmountText(int amount) {
        amountText.text = amount.ToString("00");
        resourceImage.color = amount == 0 ? emptyColor : Color.white;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (Time.timeSinceLevelLoad > 4 && System.Math.Abs(Time.timeSinceLevelLoad % 4) < 0.00001)
        //{
        //    ItemCount = Random.Range(10, 20);
        //}
    }

    private void OnValidate()
    {
        LoadSprite();
    }

    private void LoadSprite() {
        if (ResourceType != ResourceType.None)
        {
            resourceImage.sprite = spriteBundles.GetSprite(ResourceType);
        }
    }
}
