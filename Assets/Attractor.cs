using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public Rigidbody rb;
    const float G = 667.4f;//gravitional constant

    public static List<Attractor> Attractors;

    private void FixedUpdate() //is called a fixed amount of times per second
    {
        foreach (Attractor attractor in Attractors)
        {
            if(attractor != this)
                Attract(attractor);
        }
    }

    private void OnEnable()
    {
        if (Attractors == null)
            Attractors = new List<Attractor>();

        Attractors.Add(this);
    }

    private void OnDisable()
    {
        Attractors.Remove(this);
    }

    void Attract(Attractor objToAttractor)
    {
        Rigidbody rbToAttract = objToAttractor.rb; //diger attractorlerin rigidbodysine referans

        Vector3 direction = rb.position - rbToAttract.position; //mesafeyi hesapladik
        float distance = direction.magnitude; //mutlak degerini aldik

        if (distance == 0f) //because if we dublicate it in runtime we take error
            return; //we simply want to return out of it

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2); //Mathf.Pow ile karesini aldik

        Vector2 force = -direction.normalized * forceMagnitude; //we'll apply a force in the direction of our object with a strength defined by Newton's equation

        rb.AddForce(force); 
    }
}
