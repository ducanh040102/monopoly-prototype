using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerMoney;
    public TextMeshProUGUI playerNet;

    private void Start()
    {
        playerName.SetText("name");
    }

    public void RefreshInfo(int money, int net)
    {
        playerMoney.SetText(money.ToString());
        playerNet.SetText(net.ToString());
    }
}
