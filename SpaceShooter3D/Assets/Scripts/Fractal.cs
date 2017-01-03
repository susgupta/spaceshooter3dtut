using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

    //reference for mesh array to use in fractal
    [SerializeField]
    Mesh mesh;

    //reference for material to use in fractal
    [SerializeField]
    Material material;

    //max depth count to control fractal children
    [SerializeField]
    int maxDepth;

    //reference to move children fractal scale by some amount
    [SerializeField]
    float childScale;

    //reference for simple fractal twist effect
    [SerializeField]
    float maxTwist;

    //reference for rotation speed
    [SerializeField]
    float maxRotationSpeed;

    //factor for spawn probability of children
    [SerializeField]
    float spawnProbability;

    //stores current fractal depth
    int depth;

    //stores current rotation speed
    float rotationSpeed;

    //inidcator to begin fractal generation
    bool beginFractalGeneration = false;

    //directions to create children
    static Vector3[] childDirections = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };

    //rotations to create children
    static Quaternion[] childOrientations = {
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(90f, 0f, 0f),
        Quaternion.Euler(-90f, 0f, 0f)
    };
    
    void Start()
    {
        StartCoroutine(CreateFractals());
    }

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);        
    }

    void OnEnable()
    {
        //add delegate to subscribe
        EventManager.onStartGame += EnableFractalGeneration;
    }

    void OnDisable()
    {
        //remove delegate to unsubscribe
        EventManager.onStartGame -= DisableFractalGeneration;
    }

    void EnableFractalGeneration()
    {
        beginFractalGeneration = true;        
    }

    void DisableFractalGeneration()
    {
        beginFractalGeneration = false;
    }

    //method to create fractals
    IEnumerator CreateFractals()
    {
        //add twist effect
        transform.Rotate(Random.Range(-maxTwist, maxTwist), 0f, 0f);
        //set rotation speed
        rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        
        //add materials and mesh to fractal
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;

        //only begin spawning if fractal generation is enabled
        yield return new WaitUntil(() => beginFractalGeneration == true);

        if (depth < maxDepth)
        {
            //create fractal children with properties of this parent
            StartCoroutine(CreateChildren());
        }
    }

    //create children Fractals from parent properties
    void Initialize(Fractal parent, int childIndex)
    {
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        spawnProbability = parent.spawnProbability;
        maxTwist = parent.maxTwist;
        maxRotationSpeed = parent.maxRotationSpeed;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
        transform.localRotation = childOrientations[childIndex];

        //allow begin fractal indicator from parent as well
        beginFractalGeneration = parent.beginFractalGeneration;
    }

    //create children fractals in coutine
    IEnumerator CreateChildren()
    {
        for (int i = 0; i < childDirections.Length; i++)
        {
            //use probability to create child or not
            if (Random.value <= spawnProbability)
            {
                //this effect to simulate random growth effect of fractal
                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
                new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, i);
            }
        }
    }
}
