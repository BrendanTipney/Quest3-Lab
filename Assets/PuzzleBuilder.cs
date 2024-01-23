using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class PuzzleBuilder : MonoBehaviour
{
    private GameObject piece;
    public GameObject puzzle;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            piece = transform.GetChild(i).gameObject;
            Mesh pieceMesh = piece.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            piece.AddComponent<MeshCollider>();
            MeshCollider mc = piece.GetComponent<MeshCollider>();
            mc.convex = true;
            mc.isTrigger = true;
            mc.sharedMesh = pieceMesh;
            piece.AddComponent<Rigidbody>();
            Rigidbody rb = piece.GetComponent<Rigidbody>();
            rb.useGravity = false;
            piece.AddComponent<Grabbable>();
            Grabbable grab = piece.GetComponent<Grabbable>();
            grab.TransferOnSecondSelection = true;
            piece.AddComponent<HandGrabInteractable>();
            HandGrabInteractable hgi = piece.GetComponent<HandGrabInteractable>();
            hgi.InjectRigidbody(rb);
            hgi.InjectOptionalPointableElement(grab);
            grab.enabled = true;
            hgi.enabled = true;
            piece.AddComponent<PuzzlePiece>();
            piece.GetComponent<PuzzlePiece>().matchedPiece = puzzle.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
