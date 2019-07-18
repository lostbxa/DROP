using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIControls : MonoBehaviour
{
    public TextMeshPro Drop;
    public TextMeshPro Options;
    public TextMeshPro Credits;
    public Transform pos1;
    public Transform pos2;
    public Camera cam;
    public GameObject game;

    int Selection = 1;
    float timer = 0;
    float duration = 5.0f;
    float startTime;
    bool moving = false;
    bool playing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(!playing)
        {
            //Move selection
            if (Input.GetAxis("Mouse X") > .2 && timer > 0.1f)
            {
                if (Selection < 3)
                {
                    Selection++;
                }
                else
                {
                    Selection = 0;
                }
                timer = 0;
            }
            if (Input.GetAxis("Mouse X") < -.2 && timer > 0.1f)
            {
                if (Selection > 1)
                {
                    Selection--;
                }
                else
                {
                    Selection = 3;
                }
                timer = 0;

            }
            switch (Selection)
            {
                case 1:
                    Drop.color = new Color(128f / 255f, 255f / 255f, 232f / 255f);
                    Options.color = new Color(255f / 255f, 128f / 255f, 159f / 255f);
                    Credits.color = new Color(255f / 255f, 128f / 255f, 159f / 255f);
                    break;

                case 2:
                    Drop.color = new Color(255f / 255f, 128f / 255f, 159f / 255f);
                    Options.color = new Color(128f / 255f, 255f / 255f, 232f / 255f);
                    Credits.color = new Color(255f / 255f, 128f / 255f, 159f / 255f);
                    break;

                case 3:
                    Drop.color = new Color(255f / 255f, 128f / 255f, 159f / 255f);
                    Options.color = new Color(255f / 255f, 128f / 255f, 159f / 255f);
                    Credits.color = new Color(128f / 255f, 255f / 255f, 232f / 255f);
                    break;
            }
        }
        if (moving)
        {
            float t = (Time.time - startTime) / duration;
            cam.transform.position = new Vector3(pos2.transform.position.x, Mathf.SmoothStep(pos1.transform.position.y, pos2.transform.position.y, t), pos2.transform.position.z);
            cam.transform.rotation = new Quaternion(Mathf.SmoothStep(pos1.transform.rotation.x, pos2.transform.rotation.x, t), Mathf.SmoothStep(pos1.transform.rotation.y, pos2.transform.rotation.y, t), Mathf.SmoothStep(pos1.transform.rotation.z, pos2.transform.rotation.z, t), Mathf.SmoothStep(pos1.transform.rotation.w, pos2.transform.rotation.w, t));
            cam.orthographicSize = Mathf.SmoothStep(1, 5, t);

            if (cam.transform.position == pos2.position)
            {
                moving = false;
                game.SetActive(true);
                Destroy(Drop.gameObject);
                Destroy(Options.gameObject);
                Destroy(Credits.gameObject);
            }
        }
        if (Input.GetMouseButton(0) && !playing)
        {
            switch (Selection)
            {
                case 1:
                    startTime = Time.time;
                    moving = true;
                    Drop.GetComponent<Rigidbody>().useGravity = true;
                    Options.GetComponent<Rigidbody>().useGravity = true;
                    Credits.GetComponent<Rigidbody>().useGravity = true;
                    playing = true;
                    break;

                case 2:
                    Debug.Log("Options");
                    break;

                case 3:
                    Debug.Log("Credits");
                    break;
            }

        }
    }
}
