using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TileManager : MonoBehaviour
{
    public static int NUMBER_OF_TILES = 40;
    public static int JAIL_TILE_INDEX = 10;

    private static TileManager instance; 

    [SerializeField] private List<GameObject> tiles;
    [SerializeField] private List<Tile> railroadTiles;

    public static TileManager Instance { get => instance; }
    public List<GameObject> Tiles { get => tiles; }

    public Vector3 row1FocusPoint = new Vector3(6f, 20, -3.5f);
    public Vector3 row2FocusPoint = new Vector3(-5f, 20, -3.5f);
    public Vector3 row3FocusPoint = new Vector3(6f, 20, -3.5f);
    public Vector3 row4FocusPoint = new Vector3(-5f, 20, -3.5f);

    [SerializeField] private string row1Name = "Row_1";
    [SerializeField] private string row2Name = "Row_2";
    [SerializeField] private string row3Name = "Row_3";
    [SerializeField] private string row4Name = "Row_4";

    private void Awake()
    {
        instance = this;
        LoadAllTile();
    }

    private void Start()
    {
        LoadAllTile();
    }

    private void Reset()
    {
        this.LoadAllTile();
    }

    protected virtual void LoadAllTile()
    {
        if (this.tiles.Count > 0) return;

        Transform boardObj = transform.Find("Board");

        foreach (Transform row in boardObj)
        {
            foreach (Transform tile in row)
            {
                Transform focusPoint = tile.Find("FocusPoint");
                if (row.name == row1Name)
                    focusPoint.localPosition = row1FocusPoint;
                else if (row.name == row2Name)
                    focusPoint.localPosition = row2FocusPoint;
                else if (row.name == row3Name)
                    focusPoint.localPosition = row3FocusPoint;
                else if (row.name == row4Name)
                    focusPoint.localPosition = row4FocusPoint;

                tiles.Add(tile.gameObject);
                
            }
        }
    }

    public void BuyProperty(int option = 0)
    {
        Tile thisTile = GameManager.Instance.GetPlayerCurrentTileObject().GetComponent<Tile>();

        GameManager.Instance.ChangePlayerMoney(-thisTile.propertyPrice[option], GameManager.Instance.CurrentTurn);

        thisTile.SetTileOwner(option);

        UIManager.Instance.ToggleCurrentPanelOff();
    }

    public void PayProperty()
    {
        Tile thisTile = GameManager.Instance.GetPlayerCurrentTileObject().GetComponent<Tile>();

        GameManager.Instance.ChangePlayerMoney(-thisTile.currentPrice, GameManager.Instance.CurrentTurn);
        GameManager.Instance.ChangePlayerMoney(thisTile.currentPrice, thisTile.ownerPlayerNumber);
        
        UIManager.Instance.ToggleCurrentPanelOff();
    }

    public void UpgradeProperty()
    {
        Tile thisTile = GameManager.Instance.GetPlayerCurrentTileObject().GetComponent<Tile>();

        GameManager.Instance.ChangePlayerMoney(-thisTile.nextUpgradePrice, GameManager.Instance.CurrentTurn);

        thisTile.PropertyUpgrade();

        UIManager.Instance.ToggleCurrentPanelOff();

    }

    public void RebuyProperty()
    {
        Tile thisTile = GameManager.Instance.GetPlayerCurrentTileObject().GetComponent<Tile>();

        GameManager.Instance.ChangePlayerMoney(-thisTile.currentPrice, GameManager.Instance.CurrentTurn);
        GameManager.Instance.ChangePlayerMoney(thisTile.currentPrice, thisTile.ownerPlayerNumber);

        PayProperty();

        thisTile.SetTileOwner(thisTile.houseLevel);
    }

}

