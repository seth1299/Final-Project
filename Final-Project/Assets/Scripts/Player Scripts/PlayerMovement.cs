using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  [Tooltip("This is the Character Controller component that is attached to this Player game object.")]
  public CharacterController controller;

  [Tooltip("This is the speed at which the player moves. It is very unclear how this unit is measured in, so arbitrary values are used for it.")]
  public float speed;

  private Rigidbody rb;

  private float gravity = 9.8f;

  void Start()
  {
    rb = gameObject.GetComponent<Rigidbody>();
  }

  void Update()
  {
      float x = Input.GetAxis ("Horizontal");
      float z = Input.GetAxis ("Vertical");

      
      Vector3 move = transform.right * x - transform.up * gravity + transform.forward * z;
      controller.Move(move * speed * Time.deltaTime);
  }

}