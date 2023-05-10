using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Rigidbody rb;
    public Transform Target;
    public float x, z;
    public bool collected;
    public Safe safe;
    public float timer;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        safe = FindObjectOfType<Safe>();
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector3(x, rb.velocity.y, z);
    }
   



    void Update()
    {
        if (collected)
        {
            timer += Time.deltaTime;
            rb.useGravity = false;
            Vector3 targetPos = ScreenCanvas.Instance.GetIconPos(transform.position);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 5f);
            if (timer>1)
            {
                timer = 0;
                transform.localPosition = new Vector3(0, 0, 0);

                //ScreenCanvas.instance.iconTransform.DOPunchScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f, 4, 0f);
               // ScreenCanvas.Instance.ImageScale();
                collected = false;
                rb.useGravity = true;
                safe.coinPool.Despawn(this.gameObject);


            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MoneyGoUI>())
        {
            collected = true;
            timer = 0;
        }
    }
  
}
