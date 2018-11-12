using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCreator : MonoBehaviour
{

    public Tile TilePrefab;
    public ResourceGenerator resourceGenerator;

    public int gridXLength;
    public int gridYLength;

    public float xIncrement;
    public float yIncrement;

    public Tile[,] tileGrid;
    private Dictionary<Biome, List<Tile>> biomeTiles;

	public CameraController camera;

    public void Generate()
    {
        tileGrid = new Tile[gridXLength, gridYLength];
        biomeTiles = new Dictionary<Biome, List<Tile>>();

        bool indent = false;
        for (int y = 0; y < this.gridYLength; y++)
        {

            for (int x = 0; x < this.gridXLength; x++)
            {

                Tile tile = Instantiate(TilePrefab) as Tile;
                tile.Name = "(" + x + ", " + y + ")";
                tile.Biome = Biome.Grasslands;
                float xInc = xIncrement * 2 * x;
                float yInc = yIncrement * y;

                if (indent)
                {
                    xInc += xIncrement;
                }

                tile.transform.position = new Vector3(xInc, yInc);
                tileGrid[x, y] = tile;

                // Add new tile to the biome dictionary
                if (biomeTiles.ContainsKey(tile.Biome))
                {
                    biomeTiles[tile.Biome].Add(tile);
                }
                else
                {
                    List<Tile> tiles = new List<Tile> { tile };
                    biomeTiles.Add(tile.Biome, tiles);
                }

            }

            indent = !indent;
        }

        GenerateResources();
		camera.boundaries = new Rect(- 6 * xIncrement, -2 * yIncrement, (gridXLength * 2 + 13.0f) * xIncrement, (gridYLength + 3.0f ) * yIncrement);
    }

    public void GenerateResources()
    {
        foreach (Biome biome in biomeTiles.Keys)
        {
            List<Tile> tiles = biomeTiles[biome];
            int resourceAmount = resourceGenerator.RandomizeResourceAmount(tiles.Count, tiles.Count/4, tiles.Count/3);

            for (int i = 0; i < resourceAmount; i++)
            {
                int tileIndex = Random.Range(0, tiles.Count);
                Tile tile = tiles[tileIndex];

                TileObject resource = Instantiate(resourceGenerator.CreateResource(biome)) as TileObject;
                tile.ChildObject = resource;
                tiles.RemoveAt(tileIndex);
                Debug.Log("Resource added at tile " + tile.Name);
            }
        }
    }
}
