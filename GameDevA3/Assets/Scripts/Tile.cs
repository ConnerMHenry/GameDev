using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Tile : MonoBehaviour {

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

    // Use this for initialization
    void Start () {
        SetupBiome();
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
