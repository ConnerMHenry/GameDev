using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Tile : MonoBehaviour {

    public TileUI ProgressBarPreFab;

    public string Name;
    public BiomeOptions biomeOptions;
    public Biome Biome = Biome.Grasslands;
    public SpriteRenderer terrainImage;
    public bool IsClear {
        get {
            return ChildObject == null;
        }
    }

    private TileObject _childObject;
    public TileObject ChildObject {
        get {
            return _childObject;
        }
        set {
            if (value != null)
            {
                value.SetTile(this);
            }
            _childObject = value;
        }
    }

    private bool isHighlighted = false;
    public bool IsHighlighted {
        get {
            return isHighlighted;
        }

        set {

            if (value) {
                if (_childObject is NaturalResource) {
                    ((NaturalResource)_childObject).OnHarvest(null);
                }
            }

            isHighlighted = value;
        }
    }

    public TileUI ProgressBar { get; protected set; }

    // Use this for initialization
    void Start () {
        SetupBiome();
	}

    public void CreateProgressBar() {
        ProgressBar = Instantiate(ProgressBarPreFab) as TileUI;
        if (ProgressBar != null) {
            ProgressBar.SetPosition(this);
        }
    }

    public void RemoveProgressBar() {
        if (ProgressBar != null)
        {
            Destroy(ProgressBar.gameObject);
            ProgressBar = null;
        }
    }

    public void OnValidate()
    {
        SetupBiome();
    }

    public void SetupBiome() {
        BiomeOptions options = biomeOptions;

        switch(Biome) {
            case Biome.Grasslands:
                terrainImage.sprite = options.grassBiome;
                break;

            case Biome.Dirt:
                terrainImage.sprite = options.dirtBiome;
                break;

            case Biome.Rocky:
                terrainImage.sprite = options.rockBiome;
                break;

            case Biome.Desert:
                terrainImage.sprite = options.desertBiome;
                break;

            case Biome.RedDesert:
                terrainImage.sprite = options.redDesertBiome;
                break;

            case Biome.Debug:
                terrainImage.sprite = options.debugBiome;
                break;
        }
    }

}
