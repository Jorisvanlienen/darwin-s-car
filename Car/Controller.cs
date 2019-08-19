using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public List<GameObject> cars;
    public int population = 20;
    public int generation = 0;
    public GameObject car;
    [HideInInspector]
    public DNA winner;
    public DNA secWinner;
    private int carsCreated = 0;

    public Controller(int carsCreated)
    {
        this.carsCreated = carsCreated;
    }

    // Use this for initialization
    void Start()
    {
        newPopulation();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public List<GameObject> getCars()
    {
        return cars;
    }
    public void newPopulation()
    {
        cars = new List<GameObject>();
        for (int i = 0; i < population; i++)
            NewMethod();
        generation++;
        Debug.Log(generation);
    }

    private void NewMethod()
    {
        GameObject carObj = (Instantiate(car));
        cars.Add(carObj);
        carObj.GetComponent<Car>().Initialize();
    }

    public void newPopulation(bool geneticManipulation)
    {
        if (geneticManipulation)
        {
            Debug.Log("aa");
            cars = new List<GameObject>();
            for (int i = 0; i < population; i++)
            {
                DNA dna = winner.crossover(secWinner);
                DNA mutated = dna.mutate();
                GameObject carObj = Instantiate(car);
                cars.Add(carObj);
                carObj.GetComponent<Car>().Initialize(mutated);
            }
        }
        generation++;
        carsCreated = 0;
        GameObject.Find("Camera").GetComponent<CameraMov>().Follow(cars[0]);
    }
    public void restartGeneration()
    {
        cars.Clear();
        newPopulation();
    }

    public override bool Equals(object obj)
    {
        var controller = obj as Controller;
        return controller != null &&
               base.Equals(obj) &&
               carsCreated == controller.carsCreated;
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
