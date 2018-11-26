using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UI.T

public class ResourceRowController : MonoBehaviour {

    public Image ResourceImage;
    public TextMeshProUGUI ResourceNameText;
    public TextMeshProUGUI ResourceAmountText;

    public ResourceOptions SpriteBundle;
    public ResourceType ResourceType;
    public int Amount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetInfo(ResourceType resourceType, int amount) {
        this.ResourceType = resourceType;
        this.Amount = amount;

        ResourceImage.sprite = SpriteBundle.GetSprite(resourceType);
        ResourceNameText.text = resourceType.ToString();
        ResourceAmountText.text = amount.ToString();
    }
}
