using UnityEngine;

public static class CameraExtensions {
  public static Vector3 MouseWorldPosition(this Camera camera) => camera.ScreenToWorldPoint(Input.mousePosition);
  public static Vector3 MouseWorldPosition(this Camera camera, float depth) => camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth));
}