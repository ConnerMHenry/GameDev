﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Tile : MonoBehaviour {

    public TileUI ProgressBarPreFab;

    private string displayName = "Dustlands";
    public string Name {
        get
        {
            if (ChildObject != null) {
                return ChildObject.Name;
            }

            return name;
        }
        set {
            name = value;
        }
    }
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
                Name = "Grass Plain";
                break;

            case Biome.Dirt:
                terrainImage.sprite = options.dirtBiome;
                Name = "Dirt Field";
                break;

            case Biome.Rocky:
                terrainImage.sprite = options.rockBiome;
                Name = "Bedrock";
                break;

            case Biome.Desert:
                terrainImage.sprite = options.desertBiome;
                Name = "Desert Dune";
                break;

            case Biome.RedDesert:
                terrainImage.sprite = options.redDesertBiome;
                Name = "Dustlands";
                break;

            case Biome.Debug:
                terrainImage.sprite = options.debugBiome;
                Name = "Debug";
                break;
        }
    }

}
