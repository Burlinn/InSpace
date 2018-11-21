using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    
    public Camera _camera;
    GameObject _shipTravelLine;
    LineRenderer _line;
    public float _rotationSpeed = 3f;
    public float _driftSpeed = 5f;
    public GameObject _shotPrefab;
    public Transform _shotSpawn;
    public float _shotSpeed = 6f;
    public float _shotCooldown = .5f;
    public GameObject _explosionPrefab;

    private bool _isDrifting = false;
    private Vector3 _driftTo = new Vector3();
    private float _resetTimer;
    private int _shotsFired = 0;

    // Use this for initialization
    void Start () {
        _resetTimer = Time.time + 3;
        _shipTravelLine = gameObject.transform.Find("ShipTravelLine").gameObject;
        _line = _shipTravelLine.GetComponent<LineRenderer>();
        _line.enabled = false;
        gameObject.GetComponent<Light>().enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("space"))
        {
            StopCoroutine("SetTravelLine");
            StartCoroutine("SetTravelLine");
            _isDrifting = false;
        }
        if (Input.GetMouseButton(1))
        {
            RotateShip();
        }
        if (Input.GetMouseButton(0))
        {
            if (Time.time > _resetTimer)
            {
                _resetTimer = Time.time + _shotCooldown;
                Fire();
            }

            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SetDriftLane();
        }
        if (_isDrifting)
        {
            Drift();

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
            //Ray ray = new Ray(new Vector3(transform.position.x - .42f, transform.position.y, transform.position.z + .1f), transform.forward);
            Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.forward);
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

    void RotateShip()
    {

        Vector3 rotation = new Vector3();
        float mousePositionX = _camera.ScreenToViewportPoint(Input.mousePosition).x;
        float mousePositionY = _camera.ScreenToViewportPoint(Input.mousePosition).y;

        if(mousePositionX > .4 && mousePositionX < .6)
        {
            mousePositionX = 0;
        }
        else if(mousePositionX < .5)
        {
            mousePositionX = (mousePositionX * -1f) - .5f;
        }
        if (mousePositionY > .4 && mousePositionY < .6)
        {
            mousePositionY = 0;
        }
        else if (mousePositionY < .5)
        {
            mousePositionY = (mousePositionY * -1f) - .5f;
        }

        rotation = new Vector3( -1 * mousePositionY * _rotationSpeed, mousePositionX * _rotationSpeed, 0);
        transform.Rotate(rotation * Time.deltaTime);
    }

    void SetDriftLane()
    {
        _isDrifting = true;
        _driftTo = _line.bounds.center;
        //if(Vector3.Distance(transform.position, _line.bounds.min) > Vector3.Distance(transform.position, _line.bounds.max)){
        //    _driftTo = _line.bounds.min;
        //}
        //else
        //{
        //    _driftTo = _line.bounds.max;
        //}

    }

    void Drift()
    {
        transform.position = Vector3.MoveTowards(transform.position, _driftTo, Time.deltaTime * _driftSpeed);
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var shot = (GameObject)Instantiate(
            _shotPrefab,
            _shotSpawn.position,
            _shotSpawn.rotation);

        // Add velocity to the bullet
        shot.GetComponent<Rigidbody>().velocity = shot.transform.forward * _shotSpeed;

        // Destroy the bullet after 2 seconds
        Destroy(shot, 4.0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        var explosion = (GameObject)Instantiate(
            _explosionPrefab,
            transform.position,
            transform.rotation);
        Destroy(collision.gameObject);
        Destroy(this.gameObject);
    }
}
