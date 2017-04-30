using UnityEngine;
using System.Collections;

public class GoogleMap : MonoBehaviour
{

	private float[] lat, lon;
	public enum MapType
	{
		RoadMap,
		Satellite,
		Terrain,
		Hybrid
	}
	public bool loadOnStart = true;
	public bool autoLocateCenter = true;
	public GoogleMapLocation centerLocation;
	public int zoom = 13;
	public MapType mapType;
	public int size = 512;
	public bool doubleResolution = false;
	public GoogleMapMarker[] markers;
	public GoogleMapPath[] paths;
	string jsonDataString;
	LatLongCollection jsonData;
	bool DataFetched = false, DataFetchedLoop = true;
	public GameObject dot;

	void Start() {
		if(loadOnStart) Refresh();	
		StartCoroutine (GetData ());
	}

	void Update()
	{
		if (Time.time>=5.0){
		if (jsonData.info [0].lat != null) {
			DataFetched = true;
		}

			if (DataFetched && DataFetchedLoop) {
				Debug.Log ("Data agya");
				DataFetchedLoop = false;
				Debug.Log (jsonData.info.Length);
				lat = new float[jsonData.info.Length];
				lon = new float[jsonData.info.Length];

				for (int i = 0; i < jsonData.info.Length; i++) {
					lat [i] = float.Parse(jsonData.info [i].lat);
					lon [i] = float.Parse(jsonData.info [i].lon);
				}
			}	
		}
	}

	public void Refresh() {
		if(autoLocateCenter && (markers.Length == 0 && paths.Length == 0)) {
			Debug.LogError("Auto Center will only work if paths or markers are used.");	
		}
		StartCoroutine(_Refresh());
	}

	IEnumerator GetData()
	{
		var dataReq = new WWW ("https://api.myjson.com/bins/18029l");		
		yield return dataReq;
		jsonDataString = dataReq.text;
		jsonData = JsonUtility.FromJson<LatLongCollection> (jsonDataString);
	}

	IEnumerator _Refresh ()
	{
		var url = "http://maps.googleapis.com/maps/api/staticmap";
		var qs = "";
		if (!autoLocateCenter) {
			if (centerLocation.address != "")
				qs += "center=" + WWW.UnEscapeURL (centerLocation.address);
			else {
				qs += "center=" + WWW.UnEscapeURL (string.Format ("{0},{1}", centerLocation.latitude, centerLocation.longitude));
			}

			qs += "&zoom=" + zoom.ToString ();
		}
		qs += "&size=" + WWW.UnEscapeURL (string.Format ("{0}x{0}", size));
		qs += "&scale=" + (doubleResolution ? "2" : "1");
		qs += "&maptype=" + mapType.ToString ().ToLower ();
		var usingSensor = false;
		#if UNITY_IPHONE
		usingSensor = Input.location.isEnabledByUser && Input.location.status == LocationServiceStatus.Running;
		#endif
		qs += "&sensor=" + (usingSensor ? "true" : "false");

		foreach (var i in markers) {
			qs += "&markers=" + string.Format ("size:{0}|color:{1}|label:{2}", i.size.ToString ().ToLower (), i.color, i.label);
			foreach (var loc in i.locations) {
				if (loc.address != "")
					qs += "|" + WWW.UnEscapeURL (loc.address);
				else
					qs += "|" + WWW.UnEscapeURL (string.Format ("{0},{1}", loc.latitude, loc.longitude));
			}
		}

//		string templat, templon;
//
//		for(int i = 0; i < lat.Length; i++)
//		{
//			templat = lat[i].ToString();
//			templon = lon[i].ToString();
//			qs += "&markers=" + "size:8|" + "color:2|" + "label:dummy|" + templat + "," + templon; 
//		}

		foreach (var i in paths) {
			qs += "&path=" + string.Format ("weight:{0}|color:{1}", i.weight, i.color);
			if(i.fill) qs += "|fillcolor:" + i.fillColor;
			foreach (var loc in i.locations) {
				if (loc.address != "")
					qs += "|" + WWW.UnEscapeURL (loc.address);
				else
					qs += "|" + WWW.UnEscapeURL (string.Format ("{0},{1}", loc.latitude, loc.longitude));
			}
		}


		var req = new WWW (url + "?" + qs);
		yield return req;
		GetComponent<Renderer>().material.mainTexture = req.texture;
	}


}

public enum GoogleMapColor
{
	black,
	brown,
	green,
	purple,
	yellow,
	blue,
	gray,
	orange,
	red,
	white
}

[System.Serializable]
public class GoogleMapLocation
{
	public string address;
	public float latitude;
	public float longitude;
}

[System.Serializable]
public class GoogleMapMarker
{
	public enum GoogleMapMarkerSize
	{
		Tiny,
		Small,
		Mid
	}
	public GoogleMapMarkerSize size;
	public GoogleMapColor color;
	public string label;
	public GoogleMapLocation[] locations;

}

[System.Serializable]
public class GoogleMapPath
{
	public int weight = 5;
	public GoogleMapColor color;
	public bool fill = false;
	public GoogleMapColor fillColor;
	public GoogleMapLocation[] locations;	
}

[System.Serializable]
public struct LatLongCollection
{
	[System.Serializable]
	public class Infos
	{
		public string lat;
		public string lon;
	}

	public Infos[] info;
}