using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

public class WeatherTracker : MonoBehaviour
{
    // first we need to know if we are on a moblie device or if we are in desktop mode.
    bool moblie = false; // we asume we are not
    //int lat = 0;
    //int longa  = 0;
    public GameObject popUp;
    public Text popUpText;
    public Text InfoText;
    bool PulledInfo = false;
    public float lat,longer;
    public currentWeather Weather;
    public currentPollen Pollen;
    public currentIpInfo IpInfo;



    private void Start()
    {
        if(Application.platform == RuntimePlatform.Android){
            moblie = true; // we are mobile
            // do pop up request!
            // make sure the pop up is needed
            if(!Input.location.isEnabledByUser){
                // they have not allowed us yet.
                popUp.SetActive(true);
                //popUpText.text = "I have asked you?";
                Input.location.Start();
            }
        }

        // at this point we know we are mobile or not.
        if(moblie){
            
        }else{
            // not mobile code
            // WIP need to get an address or something idk
            // for right now we are just going to pass lat and longer values in

        }
    }
    private IEnumerator getWeatherInfo(float lat, float longer){
        Debug.Log("Hey your checking the weather");
        UnityWebRequest www = UnityWebRequest.Get("https://api.ambeedata.com/weather/latest/by-lat-lng?lat=" + lat + "&lng=" + longer);
        www.SetRequestHeader("x-api-key","326468302619836f959da6647e6576756f435f2e30d1672e23f594aa1b1db699");
        www.SetRequestHeader("content-type","application/json");
        
        yield return www.SendWebRequest();
        Debug.Log("Weather:" + www.responseCode.ToString());
        
        if(www.isNetworkError || www.isHttpError){
            InfoText.text = "broke!";
            yield break;
        }
        //"message":"success","data":{
        string modString = "{\"" + www.downloadHandler.text.Remove(0,30);
        modString = modString.Substring(0, modString.Length - 1);
        //Debug.Log(modString);
        //DebugText.text = www.downloadHandler.text;
        
        
        Weather = JsonUtility.FromJson<currentWeather>(modString);
        InfoText.text = "time:" + Weather.time.ToString() + "\n" + "lat:" + Weather.lat + "\nlng:" + Weather.lng + "\nsummary:" + Weather.summary + "\ntemperature:" + Weather.temperature + "\nApparent temperature:" + Weather.apparentTemperature + "\nDew point:" + Weather.dewPoint + "\nhumidity:" + Weather.humidity + "\npressure:" + Weather.pressure + "\nWind Speed" + Weather.windSpeed + "\nWind gust:" + Weather.windGust +  "\nWind bearing:" + Weather.windBearing + "\nCloud cover:" + Weather.cloudCover + "\nUV index:" + Weather.uvIndex + "\nVisibility:" + Weather.visibility + "\nOzone" + Weather.ozone;
        
    }
    private IEnumerator getPollenInfo(float lat, float longer){
        Debug.Log("Hey your checking the Pollen");
        UnityWebRequest www = UnityWebRequest.Get("https://api.ambeedata.com/latest/pollen/by-lat-lng?lat=" + lat + "&lng=" + longer);
        www.SetRequestHeader("x-api-key","326468302619836f959da6647e6576756f435f2e30d1672e23f594aa1b1db699");
        www.SetRequestHeader("content-type","application/json");
         yield return www.SendWebRequest();
        Debug.Log("Pollen:" + www.responseCode.ToString());
        
        if(www.isNetworkError || www.isHttpError){
            InfoText.text = "broke!";
            yield break;
        }
        //"message":"success","data":{
        string modString = "{\"" + www.downloadHandler.text.Remove(0,70);
        // have to split based on ','
        string[] partsOfModString = modString.Split(',');
        modString = partsOfModString[0] + "," + partsOfModString[1] + "," + partsOfModString[2];
        //modString = modString.Substring(0, modString.Length - 320);
        Debug.Log(modString);
        //DebugText.text = www.downloadHandler.text;
        
        
        Pollen = JsonUtility.FromJson<currentPollen>(modString);
        InfoText.text += 	"\nGrass pollen:" + Pollen.grass_pollen + "\nTree pollen:" + Pollen.tree_pollen + "\nWeed pollen" + Pollen.weed_pollen;
        
    }
    private IEnumerator getALLinfo(){
        Debug.Log("You asked for all the info");
        string ip = new WebClient().DownloadString("http://icanhazip.com");
        Debug.Log("Your Ip is:" + ip);
        UnityWebRequest www = UnityWebRequest.Get("http://ip-api.com/json/" + ip);
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);

        IpInfo = JsonUtility.FromJson<currentIpInfo>(www.downloadHandler.text);

        StartCoroutine(getWeatherInfo(IpInfo.lat,IpInfo.lon));
        StartCoroutine(getPollenInfo(IpInfo.lat,IpInfo.lon));
        yield break;

        
    }
   

    private void Update()
    {
        // keep checking!
        if(moblie){
            if(Input.location.isEnabledByUser && PulledInfo == false){
                // they have not allowed us yet.
                Input.location.Start();
                // hey we have perms to pull info let's do this just once!
                
                // now we have to wait...
                if(Input.location.lastData.latitude != 0){
                    // we have data!
                    popUp.SetActive(false);
                    PulledInfo = true;
                    StartCoroutine(getWeatherInfo(Input.location.lastData.latitude,Input.location.lastData.longitude));
                    StartCoroutine(getPollenInfo(Input.location.lastData.latitude,Input.location.lastData.longitude));
                    // hey now the Info is full of stuff!
                    //DebugText.text = "Current weather:" + Info.summary;
                    //InfoText.text = "testing \n testing more";
                }
            }
        }else{
            if(!PulledInfo){
            PulledInfo = true;
            popUp.SetActive(false);
            //Debug.Log("About to ask for everything");
            StartCoroutine(getALLinfo());
            }
            
        }
    }
}

[System.Serializable]
public class currentIpInfo
{
    public string status;
    public string country;
    public string countryCode;
    public string region;
    public string regionName;
    public string city;
    public int zip;
    public float lat;
    public float lon;
    public string timezone;
    public string isp;
    public string org;
    public string tas;
    public string query;
}
[System.Serializable]
public class currentPollen
{
	public int grass_pollen;
    public int tree_pollen;
    public int weed_pollen;
}
[System.Serializable]
public class currentWeather
{
	public int time;
    public float lat;
    public float lng;
    public string summary;
    public string icon;
    public float temperature;
    public float apparentTemperature;
    public float dewPoint;
    public float humidity;
    public float pressure;
    public float windSpeed;
    public float windGust;
    public int windBearing;
    public float cloudCover;
    public int uvIndex;
    public int visibility;
    public float ozone;
}