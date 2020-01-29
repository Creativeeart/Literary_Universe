using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public Transform parent; 
    public Camera mainCam;
    public float speed = 3f;
    public float fireRate = 3f;
    public float force = 10f;
    public GameObject bulletPrefab;

    private GameObject bullet;
    //private float distance = 50f;
    private Vector3 mousePos;
    private bool canShoot = true;
    private Vector3 aim;

    void Update()
    {
        //Get mouse position. You need to call this every frame not just in start
        mousePos = Input.mousePosition;
        mousePos += mainCam.transform.forward * 10f; // Make sure to add some "depth" to the screen point 
        aim = mainCam.ScreenToWorldPoint(mousePos);
        transform.LookAt(aim);
        //get mouse click and asks if we can shoot yet
        if (Input.GetKey(KeyCode.Mouse0) && canShoot)
        {
            //StartCoroutine(shoot());
        }
    }

    public IEnumerator shoot()
    {
        //instantiates the bullet
        transform.LookAt(aim);
        bullet = Instantiate(bulletPrefab, parent.position, Quaternion.identity);
        bullet.transform.LookAt(aim);
        Rigidbody b = bullet.GetComponent<Rigidbody>();
        b.AddRelativeForce(Vector3.forward * force);
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
