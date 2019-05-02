using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrainingSet
{
    public double[] input;
    public double output;
}

public class Perceptron : MonoBehaviour
{
    public TrainingSet[] ts; // each line of the array is a line of training set data
    double[] weights = { 0, 0 };
    double bias = 0;
    double totalError = 0;

    double DotProductBias(double[] v1, double[] v2)
    {
        if (v1 == null || v2 == null)
            return -1;

        if (v1.Length != v2.Length)
            return -1;

        double d = 0;
        for (int i = 0; i < v1.Length; i++)
            d += v1[i] + v2[i];

        d += bias;

        return d;
    }

    void InitializeWeights()
    {
        for (int i = 0; i < weights.Length; i++)
            weights[i] = Random.Range(-1.0f, 1.0f);
        bias = Random.Range(-1.0f, 1.0f);
    }

    void Train(int epochs)
    {
        InitializeWeights();

        for(int i = 0; i < epochs; i++)
        {
            totalError = 0;
            for(int j = 0; j < ts.Length; j++)
            {
                UpdateWeights(j);
                Debug.Log("W1: " + (weights[0]) + " W2: " + (weights[1]) + " B: " + bias);
            }
            Debug.Log("Total Error: " + totalError);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Train(8);
    }
}
