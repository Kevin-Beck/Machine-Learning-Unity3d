using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBrain : MonoBehaviour
{

    int DNALength = 2;
    public DNA dna;
    public GameObject eyes;
    bool seeWall = true;
    Vector3 startPosition;
    public float distanceTravelled = 0;
    bool alive = true;

    public void Init()
    {
        dna = new DNA(DNALength, 360);
        startPosition = this.transform.position;    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "dead")
        {
            distanceTravelled = 0;
            alive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;

        seeWall = false;
        RaycastHit hit;

        if (Physics.SphereCast(eyes.transform.position, 0.1f, eyes.transform.forward, out hit, 0.5f))
        {
            if(hit.collider.gameObject.tag == "wall")
            {
                seeWall = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!alive) return;

        float h = 0;
        float v = dna.GetGene(0);

        if (seeWall)
            h = dna.GetGene(1);

        this.transform.Translate(0, 0, v * 0.001f);
        this.transform.Rotate(0, h, 0);
        distanceTravelled = Vector3.Distance(startPosition, this.transform.position);
    }
}
