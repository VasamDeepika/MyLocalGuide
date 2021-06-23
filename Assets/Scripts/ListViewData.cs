using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;

public class ListViewData : MonoBehaviour
{
    string url = "https://tools.learningcontainer.com/sample-json.json";

    string templateName = "Template";
    // Start is called before the first frame update
    void Start()
    {
        if (SceneController.KeyWord != null)
        {
            StartCoroutine(GetRequest(url));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
            CreateListOfData(webRequest.downloadHandler.text);
        }

    }
    private void CreateListOfData(string jsonText)
    {
        if (jsonText != null)
        {
            Root root = new Root();
            Newtonsoft.Json.JsonConvert.PopulateObject(jsonText, root);
            //Debug.Log(root.Name);
            string theWord = SceneController.KeyWord;

            switch (theWord)
            {
                case "firstName":
                    templateName = "Template";
                    break;
                case "":
                    break;
                default:
                    break;
            }
            for (int i = 0; i < root.lastName.Length; i++)
            {
                GameObject thePrefab = Instantiate(Resources.Load(templateName)) as GameObject;
                GameObject contentHolder = GameObject.FindGameObjectWithTag("contentHolder");
                thePrefab.transform.parent = contentHolder.transform;
                Text[] theText = thePrefab.GetComponentsInChildren<Text>();
                theText[0].text = root.firstName;
                theText[1].text = root.age.ToString();

            }

        }
    }

    public class Address
    {
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
    }

    public class PhoneNumber
    {
        public string type { get; set; }
        public string number { get; set; }
    }

    public class Root
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public Address address { get; set; }
        public List<PhoneNumber> phoneNumbers { get; set; }
    }
}