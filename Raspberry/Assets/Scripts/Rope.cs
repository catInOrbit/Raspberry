using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{

    public int ropeNodeCount;
    //public float nodeHeight;
    //public float nodeWidth;
    public float connectedAnchorPadding;
    public float spawnPosPadding;

    public float attachObjectMassReconfigure;

    public GameObject rope;
    public GameObject ropeParent;
    public GameObject attachmentPoint;

    public bool rigidAttachment; // attach end of rope to attachmentPoint;

    private bool ropeSpawnHasFinished = false;

	// Use this for initialization
	void Start ()
    {
        GenerateRope(ropeNodeCount);
	}


    void GenerateRope(int nodeCount)
    {
        GameObject ropeBase = ropeParent.transform.GetChild(0).gameObject;


        for (int i = 0; i < nodeCount; i++)
        {
            GameObject nextRopeChildPos = ropeParent.transform.GetChild(i).gameObject;

            float anchorYPos = nextRopeChildPos.GetComponent<BoxCollider2D>().size.y / 2;
            if (i >= 1)
            {
                Vector3 instantiatePos = new Vector3(nextRopeChildPos.transform.position.x, nextRopeChildPos.transform.position.y + spawnPosPadding, 0);
                // Vector3 anchorPos = Camera.main.ScreenToWorldPoint(nextRopeChildPos.GetComponent<SpriteRenderer>().sprite.pivot);

                GameObject previousChild = ropeParent.transform.GetChild(i).gameObject;
                //float anchorYPos = (rope.transform.position.y + previousChild.transform.position.y);
                Debug.Log("Rope " + rope.transform.position.y);
                Debug.Log("Previous rope y: " + previousChild.transform.position.y);

                rope.GetComponent<HingeJoint2D>().connectedBody = previousChild.GetComponent<Rigidbody2D>();
                rope.GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = false;
                rope.GetComponent<HingeJoint2D>().anchor = new Vector2(0, +anchorYPos - connectedAnchorPadding);
                rope.GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, -anchorYPos + connectedAnchorPadding);

                Debug.Log(anchorYPos);

                rope.GetComponent<DistanceJoint2D>().connectedBody = previousChild.GetComponent<Rigidbody2D>();
                rope.GetComponent<DistanceJoint2D>().autoConfigureDistance = true;
                rope.GetComponent<DistanceJoint2D>().maxDistanceOnly = true;
                rope.GetComponent<DistanceJoint2D>().autoConfigureConnectedAnchor = false;
                rope.GetComponent<DistanceJoint2D>().anchor = new Vector2(0, -anchorYPos + connectedAnchorPadding);
                rope.GetComponent<DistanceJoint2D>().connectedAnchor = new Vector2(0, 0);

                Instantiate(rope, instantiatePos, Quaternion.identity, ropeParent.transform);


            }

            else //first rope instance, first child in parent is base
            {
                Vector3 instantiatePos = new Vector3(ropeBase.transform.position.x, ropeBase.transform.position.y + spawnPosPadding, 0);
                //Vector3 anchorPos = Camera.main.ScreenToWorldPoint(ropeBase.GetComponent<SpriteRenderer>().sprite.pivot);
                float anchorYPosBase = ropeBase.GetComponent<BoxCollider2D>().size.y / 2;



                Debug.Log("Base anchor " + anchorYPos);
                Debug.Log("Rope " + rope.transform.position.y);


                rope.GetComponent<HingeJoint2D>().connectedBody = ropeBase.GetComponent<Rigidbody2D>();
                rope.GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = false;
                rope.GetComponent<HingeJoint2D>().anchor = new Vector2(0, +anchorYPosBase - connectedAnchorPadding);
                rope.GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, -anchorYPosBase + connectedAnchorPadding);


                rope.GetComponent<DistanceJoint2D>().connectedBody = ropeBase.GetComponent<Rigidbody2D>();
                rope.GetComponent<DistanceJoint2D>().autoConfigureDistance = true;
                rope.GetComponent<DistanceJoint2D>().maxDistanceOnly = true;
                rope.GetComponent<DistanceJoint2D>().autoConfigureConnectedAnchor = false;
                rope.GetComponent<DistanceJoint2D>().anchor = new Vector2(0, -anchorYPos + connectedAnchorPadding);
                rope.GetComponent<DistanceJoint2D>().connectedAnchor = new Vector2(0, 0);

                Instantiate(rope, instantiatePos, Quaternion.identity, ropeParent.transform);

            }

        }

        ropeSpawnHasFinished = true;



    }

    void Update()
    {
        if (ropeSpawnHasFinished)
        {
            ropeParent.transform.GetChild(ropeNodeCount).GetComponent<BoxCollider2D>().isTrigger = true;
            attachmentPoint.GetComponent<HingeJoint2D>().connectedBody = ropeParent.transform.GetChild(ropeNodeCount).GetComponent<Rigidbody2D>();
            attachmentPoint.GetComponent<DistanceJoint2D>().connectedBody = ropeParent.transform.GetChild(ropeNodeCount).GetComponent<Rigidbody2D>();
        }

        if(rigidAttachment == true)
        {
            ropeParent.transform.GetChild(ropeNodeCount).transform.position = attachmentPoint.transform.position;
        }
    }
}
