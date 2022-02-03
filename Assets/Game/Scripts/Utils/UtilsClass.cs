using UnityEngine;
using System;

/// <summary>
/// My Library
/// </summary>
public static class UtilsClass
{
    private static Camera mainCamera;

    #region Mouse or Object Position
    public static Vector3 GetScreenToViewportPoint(Vector3? objectPosition = null, float posZ = 0f)
    {
        // sol-alt köşe 0.0f | sağ-üst 1.0f | kamera hareket etse de hep böyle
        mainCamera ??= Camera.main;

        Vector3 position = objectPosition == null ?
            mainCamera.ScreenToViewportPoint(Input.mousePosition) : mainCamera.ScreenToViewportPoint((Vector3)objectPosition);

        position.z = posZ;
        return position;
    }
    public static Vector3 GetScreenToWorldPoint(Vector3? objectPosition = null, float posZ = 0f)
    {
        // sol-alt Dünyada neyse o -299 bile olabilir kamera konumu önemsiz. Dünyadaki 0,0,0 noktası origin olur
        mainCamera ??= Camera.main;

        Vector3 position = objectPosition == null ?
            mainCamera.ScreenToWorldPoint(Input.mousePosition) : mainCamera.ScreenToWorldPoint((Vector3)objectPosition);

        Debug.DrawRay(position, mainCamera.transform.forward * 1000, Color.red);
        position.z = posZ;
        return position;
    }
    public static Vector3 GetWorldToScreenPoint(Vector3? objectPosition = null, float posZ = 0f)
    {
        // aynı Vec3'ten döndürmez hiç.İlk girdiğinde kameranın sol altı 0,0 olur.70k'lara kadar çıkar ilerledikçe çok artar
        mainCamera ??= Camera.main;

        Vector3 position = objectPosition == null ?
            mainCamera.WorldToScreenPoint(Input.mousePosition) : mainCamera.WorldToScreenPoint((Vector3)objectPosition);

        position.z = posZ;
        return position;
    }
    #endregion

    #region Mouse ile obje arasındaki yön vektörü
    public static Vector3 GetMouseDirection(Transform bulletSource, float posZ = 0f)
    {
        //ok atarken gitceði yön vektörünü bulmak için
        Vector3 mouseVector = GetScreenToWorldPoint() - bulletSource.position;
        mouseVector.z = posZ;
        return mouseVector;
    }
    public static Vector3 GetNormalizeMouseDirection(Transform bulletSource, float posZ = 0f)
    {
        // ok atarken gideceği yön vektörünü bulmak için
        Vector3 mouseVector = GetMouseDirection(bulletSource, posZ);
        mouseVector.Normalize();
        return mouseVector;
    }
    #endregion

    #region Random Directions
    //Random Directions
    public static Vector3 GetRandomDirection2D()
    {
        // random x ve y değerleri, z=0f
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    public static Vector3 GetRandomUpDirection2D()
    {
        // nesneyi havaya fırlatma
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), 1f).normalized;
    }
    #endregion

    #region Açılar, diferansiyeller mmm..
    public static float GetAngleFromVector(Vector3 vector)
    {
        // okun yönü düşmana göre dönsün diye
        float radians = Mathf.Atan2(vector.y, vector.x);
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
