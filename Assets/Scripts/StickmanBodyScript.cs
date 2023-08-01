using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StickmanBodyScript : MonoBehaviour
{
    public Animator anim;
    private bool backTheAngle;
    private Transform from;
    private Transform to;
    public float percentOfAngle;
    // Start is called before the first frame update
    void Start()
    {
        to = gameObject.transform;
        Debug.Log(to.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //проверяем, коснулась ли кукла кубов
        if(collision.gameObject.name == "Cube(Clone)" && gameObject.GetComponent<Rigidbody>().isKinematic == false)
        {
            //включаем анимацию
            gameObject.GetComponent<Animator>().enabled = true;
            //фиксируем куклу
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            backTheAngle = true;
            anim.SetTrigger("standUp");
            backTheAngle = true;
            from = gameObject.transform;
            Debug.Log(from.rotation);
        }
    }

    void Update()
    {
        if(backTheAngle)
        {
            //возвращаем стикмена смотреть в камеру
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(from.transform.rotation.x, from.transform.rotation.y, from.transform.rotation.z), Quaternion.Euler(0f, -168f, 0f), percentOfAngle);
            percentOfAngle += Time.deltaTime* 0.2f;
        }

    }

}
