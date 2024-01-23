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

public class PuzzlePiece : MonoBehaviour
{
    // Start is called before the first frame update
    public float threshold = 0.2f;
    public float angleThreshold = 5f;
    private string debugString;
    public bool debugEnable = false;
    public UnityEngine.UI.Text debugText;
    public GameObject matchedPiece;
    private bool Locked = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(matchedPiece != null)
        {
            if (!Locked)
            {
                float dist = Vector3.Distance(this.transform.position, matchedPiece.transform.position);
                //this.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_normalAdd", dist);
                //debugString += "Dist = "+dist+"\n";
                if (dist<threshold)
                {
                    float angleDifference = Quaternion.Angle(this.transform.rotation, matchedPiece.transform.rotation);
                    //debugString += "angle = "+angleDifference+"\n";
                    if (angleDifference < angleThreshold)
                    {
                        Locked = true;
                        this.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_normalAdd", 0.0f);
                        this.GetComponent<Grabbable>().enabled = false;
                        this.GetComponent<HandGrabInteractable>().enabled = false;
                        //this.enabled = false;

                        this.transform.SetPositionAndRotation(matchedPiece.transform.position, matchedPiece.transform.rotation);
                    }
                }
            }
            //Debug.Log(debugString);
            //debugText.text = debugString;
            debugString = "";
        }
    }
}
