using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using static UnityEngine.GraphicsBuffer;
using System;

public class SuperChance : MonoBehaviour
{
    private const int MAX_OF_CHOSEN = 3;

    [SerializeField] private Button superChanceButton1;
    [SerializeField] private Button superChanceButton2;
    [SerializeField] private Button superChanceButton3;

    [SerializeField] private TextMeshProUGUI superChanceText1Name;
    [SerializeField] private TextMeshProUGUI superChanceText2Name;
    [SerializeField] private TextMeshProUGUI superChanceText3Name;

    [SerializeField] private TextMeshProUGUI superChanceText1Des;
    [SerializeField] private TextMeshProUGUI superChanceText2Des;
    [SerializeField] private TextMeshProUGUI superChanceText3Des;

    [SerializeField] private int numberOfChosen = 0;
    [SerializeField] private int[] chosenSuperID = new int[MAX_OF_CHOSEN];
    

    Upgrade[] _Upgrades = new Upgrade[]
    {
        new Upgrade { Id = 1, Name = "Attack speed (projectiles)", Description = "Increases shooting speed of projectiles by X%", Rarity = "Common", Increase = 20 },
        new Upgrade { Id = 2, Name = "Projectile damage", Description = "Increases projectile damage by X%", Rarity = "Common", Increase = 20 },
        new Upgrade { Id = 3, Name = "Projectile size", Description = "Increases size of projectiles by X%", Rarity = "Common", Increase = 30 },
        new Upgrade { Id = 4, Name = "Pierce", Description = "Increases number of enemies that projectile can pierce through +X", Rarity = "Rare", Increase = 1 },
        new Upgrade { Id = 5, Name = "Precision", Description = "Your projectiles are more accurate +X%", Rarity = "Common", Increase = 20},
        new Upgrade { Id = 6, Name = "Greater view", Description = "You will see in greater distance by X", Rarity = "Rare", Increase = 30 },
        new Upgrade { Id = 7, Name = "Crit chance", Description = "Greater chance to do crit by X%", Rarity = "Rare", Increase = 10 },
        new Upgrade { Id = 8, Name = "Crit multiplier", Description = "Crit does more damage by X", Rarity = "Common", Increase = 15 },
        new Upgrade { Id = 9, Name = "Area damage", Description = "Greater area damage by X%", Rarity = "Rare", Increase = 25 },
        new Upgrade { Id = 10, Name = "Train health", Description = "Increases hitpoints of the train by X%", Rarity = "Rare", Increase = 15 },
        new Upgrade { Id = 11, Name = "Train repair", Description = "Repairs your train by X", Rarity = "Rare", Increase = 5 },
        new Upgrade { Id = 12, Name = "Level up faster", Description = "Level up faster by X%", Rarity = "Epic", Increase = 5 }
};

    [SerializeField] List<int> IDSuper = new List<int>();

    Upgrade Superchance_1 = null;
    Upgrade Superchance_2 = null;
    Upgrade Superchance_3 = null;

    private void Start()
    {
        MakeList();
        ButtonSet();
    }

    private void OnEnable()
    {
        if(numberOfChosen >= MAX_OF_CHOSEN)
            UIManager.Instance.ToggleCurrentPanelOff();

        ShuffleChance();
    }

    private void MakeList()
    {
        for (int i = 0; i < _Upgrades.Length; i++)
        {
            IDSuper.Add(_Upgrades[i].Id);
        }

        ShuffleList(IDSuper);
    }

    private void ButtonSet()
    {
        superChanceText1Name = superChanceButton1.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        superChanceText2Name = superChanceButton2.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        superChanceText3Name = superChanceButton3.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        superChanceText1Des = superChanceButton1.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        superChanceText2Des = superChanceButton2.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        superChanceText3Des = superChanceButton3.transform.GetChild(1).GetComponent<TextMeshProUGUI>();


        for (int i = 0; i < IDSuper.Count; i++)
        {
            if (_Upgrades[i].Id == IDSuper[0])
            {
                Debug.Log(IDSuper[0]);
                Superchance_1 =(Upgrade)_Upgrades[i];
                break;
            }
        }
        
        for (int i = 0; i < IDSuper.Count; i++)
        {
            if (_Upgrades[i].Id == IDSuper[1])
            {
                Debug.Log(IDSuper[1]);
                Superchance_2 =(Upgrade)_Upgrades[i];
                break;
            }
        }
        
        for (int i = 0; i < IDSuper.Count; i++)
        {
            if (_Upgrades[i].Id == IDSuper[2])
            {
                Debug.Log(IDSuper[2]);
                Superchance_3 =(Upgrade)_Upgrades[i];
                break;
            }
        }

        superChanceText1Name.text = Superchance_1.Name;
        superChanceText2Name.text = Superchance_2.Name;
        superChanceText3Name.text = Superchance_3.Name;
        
        superChanceText1Des.text = Superchance_1.Description;
        superChanceText2Des.text = Superchance_2.Description;
        superChanceText3Des.text = Superchance_3.Description;

        superChanceButton1.transform.GetChild(2).name = Superchance_1.Id.ToString();
        superChanceButton2.transform.GetChild(2).name = Superchance_2.Id.ToString();
        superChanceButton3.transform.GetChild(2).name = Superchance_3.Id.ToString();

        superChanceText1Des.text = Superchance_1.Description.Replace("X", Superchance_1.Increase.ToString());
        superChanceText1Des.text = Superchance_2.Description.Replace("X", Superchance_2.Increase.ToString());
        superChanceText1Des.text = Superchance_3.Description.Replace("X", Superchance_3.Increase.ToString());

        Dictionary<string, Color> rarityColors = new Dictionary<string, Color>();
        rarityColors.Add("Common", new Color(1, 1, 1, 1));
        rarityColors.Add("Rare", new Color(0f, 0.5f, 1f, 1));
        rarityColors.Add("Epic", new Color(0.75f, 0.25f, 0.75f, 1));


        superChanceButton1.GetComponent<Image>().color = rarityColors[Superchance_1.Rarity];
        superChanceButton2.GetComponent<Image>().color = rarityColors[Superchance_2.Rarity];
        superChanceButton3.GetComponent<Image>().color = rarityColors[Superchance_3.Rarity];
    }

    private void ShuffleList(List<int> list)
    {
        
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private void SCEffect(int id)
    {
        if(id == 0)
        {

        }
    }

    public class Upgrade
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rarity { get; set; }
        public float Increase { get; set; }
    }

    // Btn Function

    public void ShuffleChance()
    {
        ShuffleList(IDSuper);
        ButtonSet();
    }

    public void PickChance(string position)
    {
        Button clickedButton = null;

        if (position == "middle")
        {
            clickedButton = superChanceButton2;
        }
        
        else if (position == "right")
        {
            clickedButton = superChanceButton1;
        }
        
        else if (position == "left")
        {
            clickedButton = superChanceButton3;
        }

        int SCId = int.Parse(clickedButton.transform.GetChild(2).name);

        if(numberOfChosen < MAX_OF_CHOSEN)
        {
            chosenSuperID[numberOfChosen] = SCId;
            numberOfChosen++;
        }
        SCEffect(SCId);
        IDSuper.Remove(SCId);
    }
}
