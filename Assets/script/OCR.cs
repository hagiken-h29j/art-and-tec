using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


public class OCR : MonoBehaviour
{
    static readonly string apiKey = Env.GetEnv("GOOGLE_API_KEY");
    static readonly string url = "https://vision.googleapis.com/v1/images:annotate?key="+apiKey;

    [Serializable]
    public class requestBody
    {
        public List<AnnotateImageRequest> requests;
    }

    [Serializable]
    public class AnnotateImageRequest
    {
        public Image image;
        public List<Feature> features;
        //public string imageContext;
    }

    [Serializable]
    public class Image
    {
        public string content;
        //public ImageSource source;
    }

    [Serializable]
    public class ImageSource
    {
        public string imageUri;
    }

    [Serializable]
    public class Feature
    {
        public string type;
        public int maxResults;
    }

    public enum FeatureType
    {
        TYPE_UNSPECIFIED,
        FACE_DETECTION,
        LANDMARK_DETECTION,
        LOGO_DETECTION,
        LABEL_DETECTION,
        TEXT_DETECTION,
        SAFE_SEARCH_DETECTION,
        IMAGE_PROPERTIES
    }

    [Serializable]
    public class ImageContext
    {
        public LatLongRect latLongRect;
        public string languageHints;
    }

    [Serializable]
    public class LatLongRect
    {
        public LatLng minLatLng;
        public LatLng maxLatLng;
    }

    [Serializable]
    public class LatLng
    {
        public float latitude;
        public float longitude;
    }



    [Serializable]
    public class ResponseBody
    {
        public List<AnnotateImageResponse> responses;
    }

    [Serializable]
    public class AnnotateImageResponse
    {
        public List<EntityAnnotation> labelAnnotations;
    }

    [Serializable]
    public class EntityAnnotation
    {
        public string mid;
        public string locale;
        public string description;
        public float score;
        public float confidence;
        public float topicality;
        public BoundingPoly boundingPoly;
        public List<LocationInfo> locations;
        public List<Property> properties;
    }

    [Serializable]
    public class BoundingPoly
    {
        public List<Vertex> vertices;
    }

    [Serializable]
    public class Vertex
    {
        public float x;
        public float y;
    }

    [Serializable]
    public class LocationInfo
    {
        LatLng latLng;
    }

    [Serializable]
    public class Property
    {
        string name;
        string value;
    }

    [Obsolete]
    static public IEnumerator GetCharOfImage(string base64Image)
    {
        // requestBodyを作成
        var requests = new requestBody();
        requests.requests = new List<AnnotateImageRequest>();

        var request = new AnnotateImageRequest();
        request.image = new Image();
        request.image.content = base64Image;

        request.features = new List<Feature>();
        var feature = new Feature();
        feature.type = FeatureType.TEXT_DETECTION.ToString();
        feature.maxResults = 10;
        request.features.Add(feature);

        requests.requests.Add(request);

        // JSONに変換
        string jsonRequestBody = JsonUtility.ToJson(requests);

        // ヘッダを"application/json"にして投げる
        var webRequest = new UnityWebRequest(url, "POST");
        byte[] postData = Encoding.UTF8.GetBytes(jsonRequestBody);
        webRequest.uploadHandler = new UploadHandlerRaw(postData);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.Send();

        if (webRequest.isError)
        {
            // エラー時の処理
            Debug.Log(webRequest.error);
        }
        else
        {
            // 成功時の処理
            Debug.Log(webRequest.downloadHandler.text);
            //var responses = JsonUtility.FromJson<ResponseBody>(webRequest.downloadHandler.text);
            //Debug.Log(responses);

        }
    }

}