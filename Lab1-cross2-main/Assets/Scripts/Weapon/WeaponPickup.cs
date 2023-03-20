using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponPickup : MonoBehaviour
{

    private Weapon weapon;
    public Transform weaponAttachPoint;

    public float weaponDropForce;
    
    // Start is called before the first frame update
    void Start()
    {
        weapon = null;

        if (!weaponAttachPoint)
            weaponAttachPoint = GameObject.FindGameObjectWithTag("Attachpoint").transform;

        if (weaponDropForce <= 0)
            weaponDropForce = 10f;


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!weapon && collision.gameObject.CompareTag("Weapon"))
        {
            weapon = collision.gameObject.GetComponent<Weapon>();

            if(weapon)
            {
                weapon.rb.isKinematic = true;
                weapon.gameObject.transform.position = weaponAttachPoint.position;
                weapon.gameObject.transform.SetParent(weaponAttachPoint);
                weapon.gameObject.transform.rotation = weaponAttachPoint.rotation;
                Physics.IgnoreCollision(weapon.gameObject.GetComponent<Collider>(),GetComponent<Collider>(),true);

                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame() && weapon)
        {
            weapon.Fire();
        }
    }

 
    public void Throw(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame() && weapon)
        {
            weaponAttachPoint.DetachChildren();

            StartCoroutine(EnableCollisions(2));

            weapon.rb.isKinematic = false;

            weapon.rb.AddForce(weapon.transform.forward * weaponDropForce, ForceMode.Impulse);


        }
    }

    IEnumerator EnableCollisions(float timeToDisable)
    {
        yield return new WaitForSeconds(timeToDisable);
        Physics.IgnoreCollision(weapon.gameObject.GetComponent<Collider>(), GetComponent<Collider>(), false);
       
        weapon = null;
    }

}
