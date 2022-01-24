using UnityEngine;
using System;

/// <summary>
/// Çok kullanılan metodlar static olarak burada
/// </summary>
public static class UtilsClass
{
    private static Camera mainCamera;

    #region GetPosition, Direction Stuff
    /// <summary>
    /// sol-alt köşe 0.0f | sağ-üst 1.0f | kamera hareket etse de hep böyle
    /// </summary>
    /// <param name="transform">default null</param>
    /// <returns></returns>
    public static Vector3 GetScreenToViewportPosition(Transform transform = null, float posZ = 0f)
    {
        if (mainCamera == null) mainCamera = Camera.main;
        Vector3 mousePos = transform == null ? mainCamera.ScreenToViewportPoint(Input.mousePosition) :
            Camera.main.ScreenToViewportPoint(transform.position);
        mousePos.z = posZ;
        return mousePos;
    }
    /// <summary>
    /// sol-alt Dünyada neyse o -299 bile olabilir kamera konumu önemsiz
    /// Dünyadaki 0,0,0 noktası origin olur
    /// </summary>
    /// <param name="transform">default null</param>
    /// <returns></returns>
    public static Vector3 GetScreenToWorldPosition(Transform transform = null, float posZ = 0f)
    {
        if (mainCamera == null) mainCamera = Camera.main;
        Vector3 mouseWorldPosition = transform == null ? mainCamera.ScreenToWorldPoint(Input.mousePosition) :
            Camera.main.ScreenToWorldPoint(transform.position);
        //mouseWorldPosition.z = posZ;
        return mouseWorldPosition;
    }
    /// <summary>
    /// aynı Vector3'ten döndürmez hiç. ilk girdiğinde kameranın sol altı 0,0 olur
    /// 70k'lara kadar çıkar ilerledikçe çok artar
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static Vector3 GetWorldToScreenPosition(Transform transform = null, float posZ = 0f)
    {
        if (mainCamera == null) mainCamera = Camera.main;
        Vector3 mouseScreenPosition = transform == null ? Camera.main.WorldToScreenPoint(Input.mousePosition) :
            Camera.main.WorldToScreenPoint(transform.position);
        mouseScreenPosition.z = posZ;
        return mouseScreenPosition;
    }
    /// <summary>
    /// ok atarken gitceði yön vektörünü bulmak için
    /// </summary>
    /// <param name="bulletSource"></param>
    /// <returns></returns>
    public static Vector3 GetMouseDirection(Transform bulletSource, float posZ = 0f)
    {
        Vector3 mouseVector = GetScreenToWorldPosition() - bulletSource.position;
        mouseVector.z = posZ;
        return mouseVector;
    }

    /// <summary>
    /// ok atarken gideceği yön vektörünü bulmak için
    /// </summary>
    /// <param name="bulletSource"></param>
    /// <returns></returns>
    public static Vector3 GetNormalizedMouseDirection(Transform bulletSource, float posZ = 0f)
    {
        Vector3 mouseVector = GetMouseDirection(bulletSource, posZ);
        mouseVector.Normalize();
        return mouseVector;
    }
    /// <summary>
    /// x ekseninde random direction normalized
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetRandomDirection2D()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }
    /// <summary>
    /// nesneyi havaya fırlatma
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetRandomUpDirection2D()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), 1f).normalized;
    }
    /// <summary>
    /// okun yönü düşmana göre dönsün diye
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);    //diferansiyel.. en sevdiðim mmm..
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }
    #endregion

    #region Colors

    // Get Hex Color FF00FF
    public static string GetStringFromColor(Color color)
    {
        string red = Dec01_to_Hex(color.r);
        string green = Dec01_to_Hex(color.g);
        string blue = Dec01_to_Hex(color.b);
        return red + green + blue;
    }
    // Returns a hex string based on a number between 0->1
    public static string Dec01_to_Hex(float value)
    {
        return Dec_to_Hex((int)Mathf.Round(value * 255f));
    }
    // Returns 00-FF, value 0->255
    public static string Dec_to_Hex(int value)
    {
        return value.ToString("X2");
    }

    // Get Color from Hex string FF00FFAA
    public static Color GetColorFromString(string color)
    {
        float red = Hex_to_Dec01(color.Substring(0, 2));
        float green = Hex_to_Dec01(color.Substring(2, 2));
        float blue = Hex_to_Dec01(color.Substring(4, 2));
        float alpha = 1f;
        if (color.Length >= 8)
        {
            // Color string contains alpha
            alpha = Hex_to_Dec01(color.Substring(6, 2));
        }
        return new Color(red, green, blue, alpha);
    }

    // Returns a float between 0->1
    public static float Hex_to_Dec01(string hex)
    {
        return Hex_to_Dec(hex) / 255f;
    }

    // Returns 0-255
    public static int Hex_to_Dec(string hex)
    {
        return Convert.ToInt32(hex, 16);
    }
    #endregion

}
