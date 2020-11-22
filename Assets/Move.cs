using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed;
    private float x;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float curSize = transform.localScale.x;
        float targetSize = curSize * 1.1f;
        x += Time.deltaTime; 
        float y = Mathf.Exp(x);
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(x, y, 1f), moveSpeed * Time.deltaTime);
        if (transform.localPosition.x > 1)
        {
            Destroy(gameObject);
        }
    }
}
