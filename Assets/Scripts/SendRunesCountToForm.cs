using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class SendRunesCountToForm : MonoBehaviour
{
    private string formUrl = "https://docs.google.com/forms/u/1/d/e/1FAIpQLSeEsAjJbPRcczNOHFPipHO-HEiI0Sx58M4mC_H5YFZzBElbpg/formResponse";

    public void SendData()
    {
        StartCoroutine(DataSenderCoroutine());
    }

    private void Start()
    {
        SendData();
    }

    private IEnumerator DataSenderCoroutine()
    {
        WWWForm form = new();
        form.AddField("entry.559169516", "1");
        form.AddField("entry.533910644", "1");
        form.AddField("entry.37363679", "1");
        form.AddField("entry.31424790", "1");
        form.AddField("entry.1858200441", "1");
        form.AddField("entry.1333815808", "1");
        form.AddField("entry.1684768483", "1");
        form.AddField("entry.364721132", "1");
        form.AddField("entry.90358466", "1");
        form.AddField("entry.268208002", "1");
        form.AddField("entry.1720844628", "1");
        form.AddField("entry.100832720", "1");
        form.AddField("entry.481534036", "1");
        form.AddField("entry.1288215816", "1");
        form.AddField("entry.1989236674", "1");
        form.AddField("entry.115930605", "1");
        form.AddField("entry.1887223463", "1");
        form.AddField("entry.877640917", "1");
        form.AddField("entry.329159137", "1");
        form.AddField("entry.1019093456", "1");
        form.AddField("entry.534428674", "1");
        form.AddField("entry.1192414128", "1");
        form.AddField("entry.337151586", "1");
        form.AddField("entry.824119169", "1");
        form.AddField("entry.1539196450", "1");
        form.AddField("entry.353861353", "1");
        form.AddField("entry.985701260", "1");
        form.AddField("entry.288290614", "1");
        form.AddField("entry.720159647", "1");
        form.AddField("entry.2113552632", "1");
        form.AddField("entry.1427604265", "1");
        form.AddField("entry.999489226", "1");
        form.AddField("entry.391606568", "1");
        using (UnityWebRequest www = UnityWebRequest.Post(formUrl, form))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success) 
            {
                Debug.Log("OKEY");
            }
            else
            {
                Debug.LogError(www.error);
            }
        }
    }
}
