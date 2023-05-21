using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using UnityEngine.UI;

public class MakeSkyBox : MonoBehaviour
{
    struct StreetViewApiParam
    {
       string location; // (위도,경도)
        string size; //Street View Static API에서 가져올 이미지의 크기입니다. 기본값은 640x640입니다.

        //시야각을 설정합니다. 범위는 0~360입니다. 0이면 정면을 바라보는 것이며, 90이면 오른쪽을 바라보는 것, 180이면 뒤쪽을 바라보는 것입니다.
        //헤딩은 위치 정보(location)와 함께 조합하여 원하는 방향으로 이미지를 가져오는데 사용됩니다.
        string heading;

        //fov(기본값 90)는 이미지의 시야각을 설정합니다. 시야각이 넓을수록 큰 범위를 포착할 수 있습니다.
        //큰 이미지를 가져올 경우 시야각을 넓게 설정하고, 작은 이미지를 가져올 경우 기본값으로 설정합니다.
        string fov;

        //pitch(기본값 0)은 이미지를 수직으로 회전시키는 각도입니다. 위를 바라볼 경우 양수 값, 아래를 바라볼 경우 음수 값으로 설정합니다.
        string pitch;

        //return_error_code를 true로 설정하면 이미지가 존재하지 않거나 오류(404 Not Found 등)가 발생했을 때,
        //API가 오류를 반환하도록 합니다. 오류가 발생하면 해당 오류 메시지를 받을 수 있습니다. 기본값은 true입니다.
        string error_code;


        public string GetUrl()
        {
            string DevleoperToken = "api key";
            return "https://maps.googleapis.com/maps/api/streetview?" + $"&location={location}&size={size}&heading={heading}&fov={fov}&pitch={pitch}&return_error_code={error_code}&key={DevleoperToken}";
        }

        public StreetViewApiParam(string location = "0,0", string size = "500x500", string heading = "0", string fov = "90",
            string pitch = "0", string error_code = "true")
        {
            this.heading = heading;
            this.location = location;
            this.size = size;
            this.fov = fov;
            this.pitch = pitch;
            this.error_code = error_code;
        }
    }


    private void Start()
    {

        string location = "37.298,126.8355";
        // 동
        StreetViewApiParam eastParam = new StreetViewApiParam(location: location, heading: "90");
        Texture2D eastTexture = GetImg(eastParam.GetUrl(), "동쪽");

        // 서
        StreetViewApiParam westParam = new StreetViewApiParam(location: location, heading: "270");
        Texture2D westTexture = GetImg(westParam.GetUrl(), "서쪽");

        // 남
        StreetViewApiParam southParam = new StreetViewApiParam(location: location, heading: "180");
        Texture2D southTexture = GetImg(southParam.GetUrl(), "남쪽");

        // 북
        StreetViewApiParam northParam = new StreetViewApiParam(location: location, heading: "0");
        Texture2D northTexture = GetImg(northParam.GetUrl(), "북쪽");

        // 상
        StreetViewApiParam upParam = new StreetViewApiParam(location: location, heading: "0", pitch: "90");
        Texture2D upTexture = GetImg(upParam.GetUrl(), "상쪽");

        // 하
        StreetViewApiParam downParam = new StreetViewApiParam(location: location, heading: "180", pitch: "-90");
        Texture2D downTexture = GetImg(downParam.GetUrl(), "하쪽");
    }
    Texture2D GetImg(string url, string name)
    {
        try
        {
            WebClient webClient = new WebClient();
            byte[] imageData = webClient.DownloadData(url);
            File.WriteAllBytes(name+".jpg", imageData);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);
            return texture;
        }
        catch (WebException ex)
        {
            if (ex.Response is HttpWebResponse response && response.StatusCode == HttpStatusCode.NotFound)
            {
               // 예외처리
            }
            return null;
        }
    }
}