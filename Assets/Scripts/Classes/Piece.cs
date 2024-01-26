using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Piece : MonoBehaviour
{
    public void InitPiece(int index)
    {
        Renderer pieceRenderer = GetComponent<Renderer>();
        pieceRenderer.material.color = PieceManager.Instance.PlayerMats[index];
        gameObject.name = "P" + (index + 1);
        transform.position = PieceManager.Instance.PieceSpawnpointObject.transform.position;
        
    }
}
