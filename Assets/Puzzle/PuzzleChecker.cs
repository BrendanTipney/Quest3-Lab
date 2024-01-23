using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleChecker : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject puzzle;
    public float threshold = 0.1f;
    public float angleThreshold = 5f;
    private string debugString;
    public UnityEngine.UI.Text debugText;
    private GameObject matchedPiece;
    private bool Locked = false;
    void Start()
    {
        for (int i = 0; i < puzzle.transform.childCount; i++)
        {
            Debug.Log(puzzle.transform.GetChild(i).name);
            if(puzzle.transform.GetChild(i).name == this.name)
            {
                matchedPiece = puzzle.transform.GetChild(i).gameObject;
                break;
            }
        }
        debugText.text = matchedPiece.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Locked)
        {
            float dist = Vector3.Distance(this.transform.position, matchedPiece.transform.position);
            debugString += "Dist = "+dist+"\n";
            if (dist<threshold)
            {
                float angleDifference = Quaternion.Angle(this.transform.rotation, matchedPiece.transform.rotation);
                debugString += "angle = "+angleDifference+"\n";
                if (angleDifference < angleThreshold)
                {
                    debugString += "MatchFound";
                    this.transform.SetPositionAndRotation(matchedPiece.transform.position, matchedPiece.transform.rotation);
                    Locked = true;
                    this.GetComponent<Grabbable>().enabled = false;
                    this.GetComponent<HandGrabInteractable>().enabled = false;
                }
            }
        } else {
            debugString += "MatchFound";
        }
        Debug.Log(debugString);
        debugText.text = debugString;
        debugString = "";
    }
}
