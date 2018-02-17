using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

        public float movementSpeed = 5f;
        public float strafeSpeed = 3f;

        public Plane playerPlane;
        public Transform Player;
        public Ray ray;

        void Start()
        {

        }

        void Update()
        {

            if (Input.GetKey("w"))
            {
                transform.position += transform.forward * Time.deltaTime * movementSpeed;
            }

            if (Input.GetKey("s"))
            {
                transform.position -= transform.forward * Time.deltaTime * movementSpeed;
            }

            if (Input.GetKey("d"))
            {
                transform.position += transform.right * Time.deltaTime * strafeSpeed;
            }

            if (Input.GetKey("a"))
            {
                transform.position -= transform.right * Time.deltaTime * strafeSpeed;
            }

            playerPlane = new Plane(Vector3.up, transform.position);
            // ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist;

            if (playerPlane.Raycast(ray, out hitdist))
            {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

                transform.rotation = targetRotation;

            }

        }
}