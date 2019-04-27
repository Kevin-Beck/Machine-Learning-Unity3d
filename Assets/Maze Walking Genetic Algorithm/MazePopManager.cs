using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazePopManager : MonoBehaviour
{
    public GameObject botPrefab;
    public GameObject startingPos;
    public int populationSize = 50;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    public float trialTime = 5;
    int generation = 1;

    GUIStyle guiStyle = new GUIStyle();

    private void OnGUI()
    {
        guiStyle.fontSize = 25;
        guiStyle.normal.textColor = Color.white;
        GUI.BeginGroup(new Rect(10, 10, 250, 150));
        GUI.Box(new Rect(0, 0, 1430, 140), "States", guiStyle);
        GUI.Label(new Rect(10, 25, 200, 30), "Gen: " + generation, guiStyle);
        GUI.Label(new Rect(10, 50, 200, 30), string.Format("time: {0:0.00}", elapsed), guiStyle);
        GUI.Label(new Rect(10, 75, 200, 30), "Population: " + population.Count, guiStyle);
        GUI.EndGroup();
    }

    private void Start()
    {
        for(int i = 0; i < populationSize; i++)
        {
            GameObject b = Instantiate(botPrefab, startingPos.transform.position, this.transform.rotation);
            b.GetComponent<MazeBrain>().Init();
            population.Add(b);
        }
    }

    GameObject Breed(GameObject p1, GameObject p2)
    {
        GameObject offspring = Instantiate(botPrefab, startingPos.transform.position, this.transform.rotation);
        MazeBrain b = offspring.GetComponent<MazeBrain>();
        if(Random.Range(0, 100) == 1)
        {
            b.Init();
            b.dna.Mutate();
        }
        else
        {
            b.Init();
            b.dna.Combine(p1.GetComponent<MazeBrain>().dna, p2.GetComponent<MazeBrain>().dna);
        }
        return offspring;
    }

    void BreedNewPopulation()
    {
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<MazeBrain>().distanceTravelled).ToList();
        population.Clear();

        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        for (int i = 0; i < sortedList.Count; i++)
            Destroy(sortedList[i]);

        generation++;
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed >= trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
    }
}
