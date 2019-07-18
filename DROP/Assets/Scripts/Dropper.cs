using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public Transform prefab;
    Transform lastBox;
    float vertical = 17;
    public Camera cam;
    bool moveCamera = false;
    float timer = 0;
    bool newBox = false;
    float fallSpeed = 0;
    float Score = 0;
    Box box = null;
    // Start is called before the first frame update
    void Start()
    {       
       lastBox = Instantiate(prefab, new Vector3(0, vertical, 0), Quaternion.Euler(0, Random.Range(0, 45), 0));
        newBox = true;
    }

    // Update is called once per frame
    void Update()
    {
        box = lastBox.GetComponent<Box>();
        if (lastBox.GetComponent<Box>().set && !box.fail)
        {
            Rigidbody rig = lastBox.GetComponent<Rigidbody>();
            Destroy(rig);
            moveCamera = true;
            newBox = false;
            fallSpeed = timer;
            timer = 0;
           
            lastBox = Instantiate(prefab, new Vector3(0, vertical, 0), Quaternion.Euler(0, Random.Range(0, 359), 0));
            Score++;
            lastBox.GetComponent<Rigidbody>().drag = Mathf.Lerp(1.5f, 0f, Score / 25f);
        }
        else if(!box.fail)
        {
            newBox = true;
        }
        if(newBox)
        timer += Time.deltaTime;
    }
    private void LateUpdate()
    {
        if (moveCamera && !box.fail)
        {
            vertical += (Time.deltaTime * (1/fallSpeed));

            cam.transform.position = new Vector3(10, vertical, 10);
        }
    }
}
