using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    private static PieceManager instance;

    [SerializeField] private List<GameObject> pieces;

    [SerializeField] private GameObject playPiece;
    [SerializeField] private GameObject pieceSpawnpointObject;

    [SerializeField] private Color[] playerMats;

    public Color[] PlayerMats { get => playerMats; }
    public GameObject PieceSpawnpointObject { get => pieceSpawnpointObject;}
    public static PieceManager Instance { get => instance; }
    public List<GameObject> Pieces { get => pieces; set => pieces = value; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreatePlayer();
    }

    public virtual void CreatePlayer()
    {
        for (int i = 0; i < GameManager.Instance.NumberOfPlayer; i++)
        {
            GameObject pieceIntantiate = Instantiate(playPiece, Vector3.zero, gameObject.transform.rotation);

            pieceIntantiate.GetComponent<Piece>().InitPiece(i);

            Pieces.Add(pieceIntantiate);

            GameManager.Instance.PlayersTileIndex.Add(0);
        }
    }
}
