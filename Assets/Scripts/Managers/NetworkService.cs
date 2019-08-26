using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetworkService
{
    // weather api http://openweathermap.org/api
    private const string xmlApi = "api.openweathermap.org/data/2.5/weather?q=Chicago,us&APPID=0a8994dca3365d66ad0b10ee0baafc7e&mode=xml";
    private const string jsonApi = "api.openweathermap.org/data/2.5/weather?q=Chicago,us&APPID=0a8994dca3365d66ad0b10ee0baafc7e";
    private const string webImage = "https://mobalytics.gg/wp-content/uploads/2018/09/Varus_ConquerorSkin.jpg";
    private const string localApi = "http://localhost/ch9/api.php";

    private bool IsResponseValid(WWW www)
    {
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Bad connect");
            return false;
        }
        else if (string.IsNullOrEmpty(www.text))
        {
            Debug.Log("Bad data");
            return false;
        }
        else
        {
            return true;
        }
    }

    private IEnumerator CallAPI(string url, Hashtable args, Action<string> callback)
    {
        WWW www;

        if (args == null)
        {
            www = new WWW(url);
        }
        else
        {
            WWWForm form = new WWWForm();
            foreach (DictionaryEntry arg in args)
            {
                form.AddField(arg.Key.ToString(), arg.Value.ToString());
            }
            www = new WWW(url, form);
        }

        yield return www; // pause while download procces

        if (!IsResponseValid(www))
        {
            yield break; // stop a so-program if have error
        }

        callback(www.text);
    }

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(xmlApi, null, callback);
    }

    public IEnumerator GetWeatherJSON(Action<string> callback)
    {
        return CallAPI(jsonApi, null, callback);
    }

    public IEnumerator LogWeather(string name, float cloudValue, Action<string> callback)
    {
        Hashtable args = new Hashtable();
        args.Add("message", name);
        args.Add("clous_value", cloudValue);
        args.Add("timestamp", DateTime.UtcNow.Ticks);

        return CallAPI(localApi, args, callback);
    }

    public IEnumerator DownloadImage(Action<Texture2D> callback)
    {
        WWW www = new WWW(webImage);
        yield return www;
        callback(www.texture);
    }
}
