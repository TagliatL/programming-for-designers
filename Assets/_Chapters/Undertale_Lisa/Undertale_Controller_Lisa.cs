using UnityEngine;

public class Undertale_Controller_Lisa : MonoBehaviour
{
    public float speed = 1f;
    public int HP = 10;
    public Transform arenaTransform;
    float leftCheck;
    float rightCheck;
    float downCheck;
    float upCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void UpdateChecks()
    {
        rightCheck = arenaTransform.position.x + arenaTransform.lossyScale.x / 2f;
        leftCheck = arenaTransform.position.x - arenaTransform.lossyScale.x / 2f;
        upCheck = arenaTransform.position.z + arenaTransform.lossyScale.z / 2f;
        downCheck = arenaTransform.position.z - arenaTransform.lossyScale.z / 2f;
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if(HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateChecks();

        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"),0f, Input.GetAxis("Vertical"));
        transform.position += inputDirection * speed * Time.deltaTime;

        if(transform.position.x > rightCheck)
        {
            transform.position = new Vector3(rightCheck, 0f, transform.position.z);
        }
        if (transform.position.x < leftCheck)
        {
            transform.position = new Vector3(leftCheck, 0f, transform.position.z);
        }
        if (transform.position.z > upCheck)
        {
            transform.position = new Vector3(transform.position.x, 0f, upCheck);
        }
        if (transform.position.z < downCheck)
        {
            transform.position = new Vector3(transform.position.x, 0f, downCheck);
        }
    }
}
