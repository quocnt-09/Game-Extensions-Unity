using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class GameExtensions
{
    public static readonly string TIME_FORMAT = "yyyy-MM-dd HH:mm:ss tt";

    #region Encrypt && Decrypt

    public static string Encrypt(string input, string password)
    {
        byte[] data = UTF8Encoding.UTF8.GetBytes(input);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(password));
            using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() {Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7})
            {
                ICryptoTransform tr = trip.CreateEncryptor();
                byte[] result = tr.TransformFinalBlock(data, 0, data.Length);
                return Convert.ToBase64String(result, 0, result.Length);
            }
        }
    }

    public static string Decrypt(string input, string password)
    {
        byte[] data = Convert.FromBase64String(input);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(password));
            using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() {Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7})
            {
                ICryptoTransform tr = trip.CreateDecryptor();
                byte[] result = tr.TransformFinalBlock(data, 0, data.Length);
                return UTF8Encoding.UTF8.GetString(result);
            }
        }
    }

    #endregion

    private static string _strName;

    public static string TrimName(string name, string extent, int numCharacter = 8)
    {
        if (name.Length > numCharacter)
        {
            _strName = $"{name.Substring(0, numCharacter)}{extent}";
        }
        else
        {
            _strName = name;
        }

        return _strName;
    }

    #region ENUM

    public static T IntToEnum<T>(int value)
    {
        return (T) Enum.ToObject(typeof(T), value);
    }

    public static T ParseEnum<T>(string value)
    {
        return (T) Enum.Parse(typeof(T), value, true);
    }

    #endregion

    #region INT

    public static int EnumToIn(Enum value)
    {
        return Convert.ToInt32(value);
    }

    public static int BoolToIn(bool value)
    {
        return value ? 1 : 0;
    }

    public static int StringToInt(string str)
    {
        str = str.Replace(",", "");

        int rs = 0;
        try
        {
            rs = int.Parse(str);
        }
        catch (Exception e)
        {
            Debug.LogError("Parse To Int Error: " + e);
        }

        return rs;
    }

    #endregion

    #region FLOAT

    public static float StringToFloat(string str)
    {
        str = str.Replace(",", "");

        float rs = 0;
        try
        {
            rs = float.Parse(str);
        }
        catch (Exception e)
        {
            Debug.LogError("Parse To Float Error: " + e);
        }

        return rs;
    }

    #endregion

    #region BOOL

    public static bool IntToBool(int value)
    {
        return value == 1 ? true : false;
    }

    public static bool StringToBool(string str)
    {
        bool rs = false;
        try
        {
            rs = bool.Parse(str);
        }
        catch (Exception e)
        {
            Debug.LogError("Parse To Bool Error: " + e);
        }

        return rs;
    }

    #endregion

    #region FOR UI && GAMEOBJECT

    public static bool LayerInLayerMask(int layer, LayerMask layerMask)
    {
        return ((1 << layer) & layerMask) != 0;
    }

    public static LayerMask AddLayerMask(this LayerMask mask, string layer)
    {
        mask = mask | (1 << LayerMask.NameToLayer(layer));
        return mask;
    }

    public static LayerMask RemoveLayerMask(this LayerMask mask, string layer)
    {
        mask = mask & ~(1 << LayerMask.NameToLayer(layer));
        return mask;
    }

    public static void SetSafeEnable(this SpriteRenderer sp, bool value)
    {
        if (sp.enabled != value)
        {
            sp.enabled = value;
        }
    }

    public static void SetSafeActive(this GameObject obj, bool value)
    {
        if (obj == null) return;

        if (obj.activeSelf != value)
        {
            obj.SetActive(value);
        }
    }

    public static bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current) {position = Input.mousePosition};
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        if (results.Count > 0)
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.GetComponent<Image>())
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static void SetTimeText(this TextMeshProUGUI obj, int totalSeconds)
    {
        var time = new TimeSpan(0, 0, totalSeconds);

        var strTime = "";
        if ((int) time.TotalHours > 0)
        {
            strTime = $"{time.TotalHours:00}:{time.Minutes:00}:{time.Seconds:00}";
        }
        else
        {
            if (time.Minutes > 0)
            {
                strTime = $"{time.Minutes:00}:{time.Seconds:00}";
            }
            else
            {
                strTime = $"{time.Seconds:00}";
            }
        }

        obj.SetText(strTime);
    }

    #endregion
}