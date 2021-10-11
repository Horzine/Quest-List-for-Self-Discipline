using System;
using UnityEngine;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Framework
{
    public class Archive
    {
        public static void WriteValue<T>(string key, T value)
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Int32:
                    {
                        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
                        break;
                    }

                case TypeCode.Single:
                    {
                        PlayerPrefs.SetFloat(key, Convert.ToSingle(value));
                        break;
                    }

                case TypeCode.String:
                    {
                        PlayerPrefs.SetString(key, Convert.ToString(value));
                        break;
                    }

                case TypeCode.Boolean:
                    {
                        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
                        break;
                    }

                default:
                    {
                        Debug.LogError("PlayerPrefs only support 'int', 'float', 'string', 'bool'");
                        return;
                    }
            }
            PlayerPrefs.Save();
        }

        public static T ReadValue<T>(string key)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                Debug.LogError("PlayerPrefs don't have this key");
                return default;
            }

            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Int32:
                    {
                        return (T)(object)PlayerPrefs.GetInt(key);
                    }

                case TypeCode.Single:
                    {
                        return (T)(object)PlayerPrefs.GetFloat(key);
                    }

                case TypeCode.String:
                    {
                        return (T)(object)PlayerPrefs.GetString(key);
                    }

                case TypeCode.Boolean:
                    {
                        return (T)(object)Convert.ToBoolean(PlayerPrefs.GetInt(key));
                    }

                default:
                    {
                        Debug.LogError("PlayerPrefs only support 'int', 'float', 'string', 'bool'");
                        return default;
                    }
            }
        }

        public static void DeleteAllKeys()
        {
            PlayerPrefs.DeleteAll();
        }

        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
    }
}