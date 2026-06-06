using UnityEngine;

public class Exercises_08_autoMoveForward : MonoBehaviour
{

    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        //we keep this code for Edward to always move forward
        transform.position += transform.forward * speed * Time.deltaTime;

        //here we should add our raycast
        //if you haven't already, look at the official documentation to find an example of a raycast (https://docs.unity3d.com/ScriptReference/Physics.Raycast.html)
        //if you're wondering why the example is using a raycast declaration in a if statement it's because it's a handy way to say "if the raycast hits something then do this"
        //declaring a raycast with all the arguments can be intimidating but it's possible to make a raycast with just: a position, a direction and a max 


    }
}
