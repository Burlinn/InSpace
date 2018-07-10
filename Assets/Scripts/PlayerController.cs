using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    GameObject _shipTravelLine;
    LineRenderer _line;

    // Use this for initialization
    void Start () {
        _shipTravelLine = gameObject.transform.Find("ShipTravelLine").gameObject;
        _line = _shipTravelLine.GetComponent<LineRenderer>();
        _line.enabled = false;
        gameObject.GetComponent<Light>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            StopCoroutine("SetTravelLine");
            StartCoroutine("SetTravelLine");
        }
    }

    IEnumerator SetTravelLine()
    {
        var trans = transform;
        _line.enabled = true;
        _shipTravelLine.GetComponent<Light>().enabled = true;
        while (Input.GetKey("space"))
        {
            _line.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, Time.time);
            Ray ray = new Ray(new Vector3(transform.position.x - .42f, transform.position.y, transform.position.z + .1f), transform.forward);
            RaycastHit hit;

            _line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, 100))
            {
                _line.SetPosition(1, hit.point);
                if (hit.rigidbody)
                {
                    hit.rigidbody.AddForceAtPosition(transform.forward * 5, hit.point);
                }
            }
            else
            {
                _line.SetPosition(1, ray.GetPoint(100));
            }


            yield return null;
        }
        _shipTravelLine.GetComponent<Light>().enabled = false;
        _line.enabled = false;
    }
}
