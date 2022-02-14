using UnityEngine;

public class InputService {
    public static bool moveRight => Input.GetKey(KeyCode.D);
    public static bool moveLeft => Input.GetKey(KeyCode.A);
    public static bool rewind => Input.GetKey(KeyCode.R);
    public static bool interact => Input.GetKey(KeyCode.E);
}