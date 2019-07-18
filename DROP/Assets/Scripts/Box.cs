using UnityEngine;
using UnityEngine.SceneManagement;

public class Box : MonoBehaviour
{
    public bool set = false;
    [SerializeField]
    Material mat1;
    [SerializeField]
    Material mat2;
    [SerializeField]
    Material mat3;
    [SerializeField]
    Material mat4;
    [SerializeField]
    Material mat5;

    int tolerance = 10;
    public bool fail = false;
    float duration = 2.0f;
    float startTime;
    Color col = Color.black;
    bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        Material[] matarray = { mat1, mat2, mat3, mat4, mat5 };
        GetComponent<MeshRenderer>().material = matarray[Random.Range(0, 4)];
        col = GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(!set)
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * .75f);
        }
        else if(fail)
        {
            float t = (Time.time - startTime) / duration;
            GetComponent<MeshRenderer>().material.color = new Color(Mathf.SmoothStep(col.r, 0, t), Mathf.SmoothStep(col.g, 0, t), Mathf.SmoothStep(col.b, 0, t));
            if (GetComponent<MeshRenderer>().material.color == Color.black)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Set")
        {
            set = true;

            if ((transform.eulerAngles.y <= tolerance && transform.eulerAngles.y >= 0) || (transform.eulerAngles.y <= 360  && transform.eulerAngles.y >= 360 - tolerance)   || (transform.eulerAngles.y <= 90 + tolerance && transform.eulerAngles.y >= 90 - tolerance) || (transform.eulerAngles.y <= 180 + tolerance && transform.eulerAngles.y >= 180 - tolerance) || (transform.eulerAngles.y <= 270 + tolerance && transform.eulerAngles.y >= 270 - tolerance))
            {
                transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            }

            else
            {
                float f = transform.eulerAngles.y;
                startTime = Time.time;
                fail = true;
                //SceneManager.LoadScene(0);
            }
        }
    }
}
