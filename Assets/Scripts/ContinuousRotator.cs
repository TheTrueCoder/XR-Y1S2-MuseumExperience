using UnityEngine;

public class ContinuousRotator : MonoBehaviour
{
    public Vector3 axisVector = new Vector3(0, 1, 0);
    public float degreesPerSecond = 10.0f;

    public bool randomStartAngle = false;
    //public Vector3 turnAxis = ;

    private void Start()
    {
        if (randomStartAngle)
        {
            transform.Rotate(axisVector * Random.Range(0.0f, 360.0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotAmount = Time.deltaTime * axisVector * degreesPerSecond;
        transform.Rotate(rotAmount);
    }
}
