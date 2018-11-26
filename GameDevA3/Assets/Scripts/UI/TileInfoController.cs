using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInfoController : MonoBehaviour {

    public static TileInfoController main;

    public Animator animator;
    public Text tileText;

    public Text peopleRequiredText;
    public Text resourceAmountText;
    public Image resourceImage;

    public ResourceOptions spriteBundles;

    private Tile currentTile;

    public Tile CurrentTile {
        get {
            return currentTile;
        }
        set {
            if (value != null && currentTile == null) {
                animator.Play("SlideInAnimationTileInfo");
            }
            else if (value == null && currentTile != null) {
                animator.Play("SlideOutAnimationTileInfo");
            }

            if (value != null) {
                SetTileInfo(value);
            }

            currentTile = value;
        }
    }

    private void SetTileInfo(Tile tile) {
        tileText.text = GetBiomeName(tile.Biome);

        if (tile.ChildObject is NaturalResource) {
            NaturalResource naturalResource = (NaturalResource)tile.ChildObject;
            resourceAmountText.text = naturalResource.Amount.ToString();
            peopleRequiredText.text = "0 / " + naturalResource.PeopleRequired;

            resourceImage.sprite = spriteBundles.GetSprite(naturalResource.resource.ResourceType);
        }
    }

    private string GetBiomeName(Biome biome) {
        switch (biome) {
            case Biome.Grasslands:
                return "Grassy Plains";

            case Biome.Dirt:
                return "Just Dirt";

            case Biome.Desert:
                return "Sand Lands";

            case Biome.Rocky:
                return "Rocky Roads";

            case Biome.RedDesert:
                return "Red Desert";

            default:
                return "Debug";
        }
    }

	// Use this for initialization
	void Start () {
        if (main == null) {
            main = this;
        }
        else if (main != this) {
            Destroy(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HarvestResources() {
        Tile highlightedTile = HighlightManager.current_target;

        if (highlightedTile != null && highlightedTile.ChildObject is NaturalResource) {
            NaturalResource naturalResource = (NaturalResource) highlightedTile.ChildObject;
            naturalResource.StartHarvest();
        }
    }
}
