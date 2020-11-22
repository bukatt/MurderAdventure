using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speech : MonoBehaviour
{
    public float startSize = .05f;
    public float finishSize = 1f;
    public float rateOfIncrease = 2f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(startSize, startSize, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        float curSize = transform.localScale.x;
        float targetSize = curSize * 1.1f;
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetSize, targetSize, 1f), rateOfIncrease*Time.deltaTime);
        if (transform.localScale.x > 1)
        {
            Destroy(gameObject);
        }
    }
}
