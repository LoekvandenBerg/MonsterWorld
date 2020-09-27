using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room
{
    public Vector2Int roomCoordinate;
    public Dictionary<string, Room> neighbors;

    private Dictionary<string, GameObject> name2Prefab;
    private int tilemapWidth = 18;
    private int tilemapHeight = 10;

    private string[,] population;

    public Room(int xCoordinate, int yCoordinate)
    {
        this.roomCoordinate = new Vector2Int(xCoordinate, yCoordinate);
        this.neighbors = new Dictionary<string, Room>();
        this.population = new string[tilemapWidth, tilemapHeight];
        for (int xIndex = 0; xIndex < tilemapWidth; xIndex++)
        {
            for (int yIndex = 0; yIndex < tilemapHeight; yIndex++)
            {
                this.population[xIndex, yIndex] = "";
            }
        }
        this.population[8, 5] = "Player";
        this.name2Prefab = new Dictionary<string, GameObject>();
    }

    public Room(Vector2Int roomCoordinate)
    {
        this.roomCoordinate = roomCoordinate;
        this.neighbors = new Dictionary<string, Room>();
        this.population = new string[tilemapWidth, tilemapHeight];
        for (int xIndex = 0; xIndex < tilemapWidth; xIndex++)
        {
            for (int yIndex = 0; yIndex < tilemapHeight; yIndex++)
            {
                this.population[xIndex, yIndex] = "";
            }
        }
        this.population[8, 5] = "Player";
        this.name2Prefab = new Dictionary<string, GameObject>();
    }

    public List<Vector2Int> NeighborCoordinates()
    {
        List<Vector2Int> neighborCoordinates = new List<Vector2Int>();
        neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x, this.roomCoordinate.y - 1));
        neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x + 1, this.roomCoordinate.y));
        neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x, this.roomCoordinate.y + 1));
        neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x - 1, this.roomCoordinate.y));

        return neighborCoordinates;
    }

    public void Connect(Room neighbor)
    {
        string direction = "";
        if(neighbor.roomCoordinate.y < this.roomCoordinate.y)
        {
            direction = "N";
        }
        if (neighbor.roomCoordinate.x > this.roomCoordinate.x)
        {
            direction = "E";
        }
        if (neighbor.roomCoordinate.y > this.roomCoordinate.y)
        {
            direction = "S";
        }
        if (neighbor.roomCoordinate.x < this.roomCoordinate.x)
        {
            direction = "W";
        }

        this.neighbors.Add(direction, neighbor);
    }

    public string PrefabName()
    {
        string name = "Room_";
        foreach (KeyValuePair<string, Room> neighborPair in neighbors)
        {
            name += neighborPair.Key;
        }
        return name;
    }

    public Room Neighbor(string direction)
    {
        return neighbors[direction];
    }

    public void PopulateObstacles(int numberOfObstacles, Vector2Int[] possibleSizes)
    {
        for (int obstacleIndex = 0; obstacleIndex < numberOfObstacles; obstacleIndex++)
        {
            int sizeIndex = Random.Range(0, possibleSizes.Length);
            Vector2Int regionSize = possibleSizes[sizeIndex];
            List<Vector2Int> region = FindFreeRegion(regionSize);
            foreach (Vector2Int coordinate in region)
            {
                this.population[coordinate.x, coordinate.y] = "Obstacle";
            }
        }
    }

    public void PopulatePrefabs(int amountOfPrefabs, GameObject[] possiblePrefabs)
    {
        for (int prefabIndex = 0; prefabIndex < amountOfPrefabs; prefabIndex++)
        {
            int choiceIndex = Random.Range(0, possiblePrefabs.Length);
            GameObject prefab = possiblePrefabs[choiceIndex];
            List<Vector2Int> region = FindFreeRegion(new Vector2Int(1,1));
            
            this.population[region[0].x, region[0].y] = prefab.name;
            this.name2Prefab[prefab.name] = prefab;
        }
    }

    private List<Vector2Int> FindFreeRegion(Vector2Int sizeInTiles)
    {
        List<Vector2Int> region = new List<Vector2Int>();
        do
        {
            region.Clear();

            Vector2Int centerTile = new Vector2Int(Random.Range(2, tilemapWidth - 3), Random.Range(2, tilemapHeight - 3));

            region.Add(centerTile);

            int initialXCoordinate = (centerTile.x - (int)Mathf.Floor(sizeInTiles.x / 2));
            int initialYCoordinate = (centerTile.y - (int)Mathf.Floor(sizeInTiles.y / 2));
            for (int xCoordinate = initialXCoordinate; xCoordinate < initialXCoordinate + sizeInTiles.x; xCoordinate++)
            {
                for (int yCoordinate = initialYCoordinate; yCoordinate < initialYCoordinate + sizeInTiles.y; yCoordinate++)
                {
                    region.Add(new Vector2Int(xCoordinate, yCoordinate));
                }
            }
        } while (!IsFree(region));
        return region;
    }

    private bool IsFree(List<Vector2Int> region)
    {
        foreach (Vector2Int tile in region)
        {
            if(this.population[tile.x, tile.y] != "")
                return false;        
        }
        return true;
    }

    public void AddPopulationToTilemap(Tilemap tilemap, TileBase obstacleTile)
    {
        int amountOfEnemies = 0;
        for (int xIndex = 0; xIndex < tilemapWidth; xIndex++)
        {
            for (int yIndex = 0; yIndex < tilemapHeight; yIndex++)
            {
                if (this.population[xIndex, yIndex] == "Obstacle")
                {
                    tilemap.SetTile(new Vector3Int(xIndex - (tilemapWidth / 2), yIndex - (tilemapHeight / 2), 0) ,obstacleTile);
                }
                else if(this.population[xIndex, yIndex] != "" && this.population[xIndex,yIndex] != "Player"){
                    GameObject prefab = GameObject.Instantiate(this.name2Prefab[this.population[xIndex, yIndex]]);
                    amountOfEnemies++;
                    prefab.transform.position = new Vector2(xIndex - (tilemapWidth / 2) + 0.5f, yIndex - (tilemapHeight / 2) + 0.5f);
                    prefab.GetComponent<BattleLaunchCharacter>().id = amountOfEnemies;
                }
            }
        }
    }

}
