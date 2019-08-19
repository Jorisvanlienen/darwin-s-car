using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private DNA dna;
    private NeuralNetwork network;
    private Vector3 initialPoint;
    private float distance;
    private Camera Camera;

    private bool initialized = false;

    public Car(float distance)
    {
        this.distance = distance;
    }

    void Start()
    {

    }
    public void Initialize()
    {
        network = new NeuralNetwork();
        dna = new DNA(network.getWeights());
        initialPoint = transform.position;
        initialized = true;
    }
    public void Initialize(DNA dna)
    {
        network = new NeuralNetwork(dna);
        this.dna = dna;
        initialPoint = transform.position;
        initialized = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (initialized)
        {
            //Get inputs of distances lasers
            float[] inputs = GetComponent<Lasers>().getDistances();

            //Execute feed-forward
            network.feedForward(inputs);

            List<float> outputs = network.getOutputs();
            GetComponent<Movement>().updateMovement(outputs);
            distance = Vector3.Distance(transform.position, initialPoint);
        }
    }
    void OnTriggerEnter(Collider col)
    {

        changeCamera();
    }
    public DNA getDNA()
    {
        return dna;
    }
    public void changeCamera()
    {
        Controller controller = GameObject.Find("CarController").GetComponent<Controller>();
        List<GameObject> cars = controller.getCars();
        if (cars.Count == 2)
        {
            controller.winner = cars[0].GetComponent<Car>().getDNA();
            controller.secWinner = cars[1].GetComponent<Car>().getDNA();
        }
        if (cars.Count == 1)
        {
            if (!controller.winner.Equals(cars[0].GetComponent<Car>().getDNA()))
            {
                DNA inter = controller.secWinner;
                controller.secWinner = controller.winner;
                controller.winner = inter;
            }
            cars.Remove(gameObject);
            controller.newPopulation(true);
            Destroy(gameObject);
        }
        else
        {
            int rand = Random.Range(0, (int)cars.Count);
            if (cars[rand] == gameObject)
            {
                changeCamera();
            }
            else
            {
                if (gameObject == GameObject.Find("Camera").GetComponent<CameraMov>().getFollowing())
                {
                    GameObject.Find("Camera").GetComponent<CameraMov>().Follow(cars[rand]);
                }
                cars.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }

    public override bool Equals(object obj)
    {
        var car = obj as Car;
        return car != null &&
               base.Equals(obj) &&
               distance == car.distance;
    }

    public override int GetHashCode()
    {
        var hashCode = 1407510264;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + distance.GetHashCode();
        return hashCode;
    }
}
