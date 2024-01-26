using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textMoney;
    [SerializeField] public GameObject ownedTag;
    [SerializeField] public GameObject[] houseTypes;

    public string tileName;
    public bool isProperty = true;
    public bool isRailRoad = false;
    public bool isUltility = false;
    public bool isChance = false;
    public bool isChest = false;
    public bool isIncomeTax = false;
    public bool isLuxuryTax = false;
    public int tileIndex;

    public int houseLevel = 0;
    public int currentPrice = 0;
    public int nextUpgradePrice = 0;
    public int ownerPlayerNumber = -1;

    public int[] propertyPrice;
    public int[] propertyUpgradePrice;
    public int[] propertyFees;

    public TextMeshProUGUI TextName { get => textName; set => textName = value; }
    public TextMeshProUGUI TextMoney { get => textMoney; set => textMoney = value; }

    private void Start()
    {
        this.InitTile();
    }

    public virtual void TileFunction()
    {
        if (isRailRoad)
        {
            if (GameManager.Instance.UsingRailroad)
            {
                GameManager.Instance.UsingRailroad = false;
                return;
            }

            else
            {
                if (ownerPlayerNumber == -1)
                {
                    UIManager.Instance.TogglePanelOn("railroad");
                }

                else
                {
                    TileManager.Instance.PayProperty();
                    if (GameManager.Instance.ActiveRailroad > 1)
                        UIManager.Instance.TogglePanelOn("railroaduse");
                }
            }

        }

        else if (isUltility)
        {
            if (ownerPlayerNumber == -1)
            {
                UIManager.Instance.TogglePanelOn("ultility");
            }

            else
            {
                UIManager.Instance.ToggleCurrentPanelOff();
                GameManager.Instance.ChangePlayerMoney(-DiceManager.Instance.DiceValue * 10, GameManager.Instance.CurrentTurn);
                GameManager.Instance.ChangePlayerMoney(DiceManager.Instance.DiceValue * 10, ownerPlayerNumber);
            }
        }

        else if (isChance)
        {
            ChanceManager.Instance.Draw("chance");
        }

        else if (isChest)
        {
            ChanceManager.Instance.Draw("chest");
        }
        

        else if (isIncomeTax)
        {
            UIManager.Instance.TogglePanelOn("incometax");
        }

        else if (isLuxuryTax)
        {
            Debug.Log("Luxury");
            GameManager.Instance.PayLuxuryTax();
        }

        else
        {
            if (GameManager.Instance.UsingWorldTravel)
            {
                GameManager.Instance.UsingWorldTravel = false;
            }

            if (ownerPlayerNumber != -1)
            {
                if (ownerPlayerNumber == GameManager.Instance.CurrentTurn)
                {
                    if (houseLevel + 1 <= GameManager.MAX_HOUSE_LEVEL)
                    {
                        UIManager.Instance.TogglePanelOn("upgrade");
                    }
                }
                else
                {
                    if (houseLevel == GameManager.MAX_HOUSE_LEVEL)
                    {
                        TileManager.Instance.PayProperty();
                        UIManager.Instance.EndAction();
                    }
                    else
                    {
                        UIManager.Instance.TogglePanelOn("pay");
                    }
                }
            }
            else
            {
                UIManager.Instance.TogglePanelOn("buy");
            }
        }
    }

    public virtual void InitTile()
    {
        ownedTag.SetActive(false);

        if (!isProperty)
        {
            textMoney.gameObject.SetActive(false);
            textName.gameObject.SetActive(false);
        }

        else
        {
            textName.SetText(tileName);
        }
    }

    public void SetTileOwner(int option = 0)
    {
        ownerPlayerNumber = GameManager.Instance.CurrentTurn;

        if(isRailRoad || isUltility)
        {
            ownedTag.GetComponent<Renderer>().material.color = PieceManager.Instance.PlayerMats[GameManager.Instance.CurrentTurn];
            ownedTag.SetActive(true);
        }
        
        if (isProperty)
        {
            houseLevel = option;
            ChangeHouseStatus();
        }
    }

    public void PropertyUpgrade()
    {
        houseTypes[houseLevel].SetActive(false);
        houseLevel++;

        ChangeHouseStatus();
    }

    private void ChangeHouseStatus()
    {
        houseTypes[houseLevel].SetActive(true);
        houseTypes[houseLevel].GetComponent<ChangeHouseColor>().SetColor(PieceManager.Instance.PlayerMats[ownerPlayerNumber]);
        currentPrice = propertyFees[houseLevel];
        if(houseLevel < 4)
        {
            nextUpgradePrice = propertyUpgradePrice[houseLevel];
        }
        
        textMoney.SetText(currentPrice.ToString());
    }
}
