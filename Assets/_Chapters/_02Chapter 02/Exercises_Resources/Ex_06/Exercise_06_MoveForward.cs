using UnityEngine;

public class Exercises_06_MoveForward : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //whatever method you use, always multiply the speed of the movement you're applying by Time.deltatime to make it independant from the framerate
        //for example, mving towards the right would look like this: Vector.right * Time.deltatime (and then you can multiply again by another float to control the speed)
        //don't forget to apply that direction to Edward's position
    }
}
