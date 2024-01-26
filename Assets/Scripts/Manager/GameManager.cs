using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int MAX_HOUSE_LEVEL = 4;

    private static GameManager instance;

    [Header("Main Info")]
    [SerializeField] private int numbersOfPlayers = 4;
    [SerializeField] private int currentTurn = 0;
    [SerializeField] private int initialPlayerMoney = 200;
    [SerializeField] private int passGOTileMoney = 200;

    [Space(10)]
    [Header("Tax")]
    [SerializeField] private int incomeTaxMoney = 200;
    [SerializeField] private int luxuryTaxMoney = 200;
    [SerializeField] private int luxuryTaxStackMoney = 50;
    [SerializeField] private int luxuryTaxCurrentStack = 0;
    [SerializeField] private int luxuryTaxMaxStack = 3;

    [Space(10)]
    [Header("Railroad")]
    [SerializeField] private int ownedRailroads = 0;
    [SerializeField] private bool isRailroadInUse = false;
    [SerializeField] private bool isWorldTravelerInUse = false;

    [Space(10)]
    [SerializeField] private List<int> playersMoney;

    [SerializeField] private List<int> playersTileIndex;
    [SerializeField] private List<bool> isPlayersInJail;

    public static GameManager Instance { get => instance; }
    public int NumberOfPlayer { get => numbersOfPlayers; }
    public int CurrentTurn { get => currentTurn; }
    public int StartMoney { get => passGOTileMoney; }
    public int ActiveRailroad { get => ownedRailroads; }
    public List<bool> IsInJail { get => isPlayersInJail; }
    public List<int> PlayersTileIndex { get => playersTileIndex; set => playersTileIndex = value; }
    public bool UsingRailroad { get => isRailroadInUse; set => isRailroadInUse = value; }
    public bool UsingWorldTravel { get => isWorldTravelerInUse; set => isWorldTravelerInUse = value; }

    private void Awake()
    {
        instance = this;
        InitialPlayerData();
    }

    public int GetCurrentPlayerTileIndex()
    {
        return playersTileIndex[CurrentTurn];
    }

    public int GetPlayerMoney(int i)
    {
        return playersMoney[i];
    }

    public GameObject GetPlayerCurrentTileObject()
    {
        return TileManager.Instance.Tiles[playersTileIndex[CurrentTurn]];
    }

    public void SetPlayerTileIndex(int value)
    {
        playersTileIndex[CurrentTurn] = value;
    }

    public void SetIsInJail(bool status)
    {
        isPlayersInJail[CurrentTurn] = status;
        playersTileIndex[CurrentTurn] = TileManager.JAIL_TILE_INDEX;
    }

    public bool IsCurrentPlayerInJail()
    {
        return isPlayersInJail[currentTurn];
    }

    public void InitialPlayerData()
    {
        for (int i = 0; i < numbersOfPlayers; i++)
        {
            playersMoney.Add(initialPlayerMoney);
            isPlayersInJail.Add(false);
        }
    }

    public void GameLogic()
    {
        GameObject thisTile = GetPlayerCurrentTileObject();

        if (thisTile.GetComponent<SquareTile>() != null)
            thisTile.GetComponent<SquareTile>().SquareTileFunction();
        else
            thisTile.GetComponent<Tile>().TileFunction();
    }

    public void ChangePlayerMoney(int value, int i)
    {
        playersMoney[i] += value;
        UIManager.Instance.RefreshPlayerPanel();
    }

    public void AddMoneyToCurrentPlayer(int value)
    {
        ChangePlayerMoney(value, CurrentTurn);
    }

    public void PayLuxuryTax()
    {
        if (luxuryTaxCurrentStack >= luxuryTaxMaxStack)
            luxuryTaxCurrentStack = 0;

        AddMoneyToCurrentPlayer(-(luxuryTaxMoney + luxuryTaxCurrentStack * luxuryTaxStackMoney));

        luxuryTaxCurrentStack++;
    }


    // Button Function

    public void AddActiveRailroadCount()
    {
        ownedRailroads++;
    }

    public void SetUseRailroad(bool value)
    {
        UsingRailroad = value;
    }
    
    public void SetUseWorldTravel(bool value)
    {
        UsingWorldTravel = value;
    }

    public void IncomeTaxPay(int option)
    {
        if (option == 1)
            AddMoneyToCurrentPlayer(-(int)(playersMoney[currentTurn] * 0.1f));
        else
            AddMoneyToCurrentPlayer(-incomeTaxMoney);
    }

    public void TurnLogic()
    {
        if (CurrentTurn == NumberOfPlayer - 1)
        {
            currentTurn = 0;
        }
        else
        {
            currentTurn++;
        }

        if (IsInJail[currentTurn])
        {
            UIManager.Instance.TogglePanelOn("injail");
        }
    }
}
