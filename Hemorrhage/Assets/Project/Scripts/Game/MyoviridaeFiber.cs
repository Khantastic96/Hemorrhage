/**
 * Created 09/12/2020
 * By: Sharek Khan
 * Last Modified 09/12/2020
 * By: Sharek Khan
 *
 * Contributors: Sharek Khan
 */

using UnityEngine;

/*
 * MyoviridaeFiber handles all the logic and functionality required to "animate" the Myoviridae's fiber limbs
 */
public class MyoviridaeFiber : MonoBehaviour
{
    public float positiveRotDelta = 10.0f;
    public float negativeRotDelta = 10.0f;
    public float rotSpeed = 10.0f;
    private Quaternion m_initialRot;
    private float m_rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_initialRot = transform.rotation;
        if (gameObject.tag == "RightFiber")
        {
            m_rotSpeed = rotSpeed * -1;
        }
        else if (gameObject.tag == "LeftFiber")
        {
            m_rotSpeed = rotSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, m_rotSpeed * Time.deltaTime);
        if (transform.rotation.eulerAngles.y >= m_initialRot.eulerAngles.y + positiveRotDelta)
        {
            m_rotSpeed *= -1;
        }
        else if (transform.rotation.eulerAngles.y <= m_initialRot.eulerAngles.y - negativeRotDelta)
        {
            m_rotSpeed *= -1;
        }
    }
}