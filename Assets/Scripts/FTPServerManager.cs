using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;
using System.Data;

public class FTPServerManager : MonoBehaviour
{

    public static FTPServerManager Instance;
    public string DownloadURL;
    public string UploadURL;
    public Text notes;

    private void Awake()
    {
        if (Instance != null)
        {
            GameObject.Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void DownloadWithFTP()
    {
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(DownloadURL)); //Start the FTP request with dummy URL.

        request.Credentials = new NetworkCredential("demo", "password"); //Add credentials username and password of the server.

        request.Method = WebRequestMethods.Ftp.DownloadFile; //Download.

        DownloadAndSave(request.GetResponse(), "D:\\DownloadedFile.txt"); //Save the file as a text file named "DownloadedFile.txt".

    }

    void DownloadAndSave(WebResponse request, string savePath)
    {
        Stream reader = request.GetResponseStream();

        if (!Directory.Exists(Path.GetDirectoryName(savePath))) //Create Directory if it does not exist
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
        }

        FileStream fileStream = new FileStream(savePath, FileMode.Create);


        int bytesRead = 0;
        byte[] buffer = new byte[2048];

        while (true)
        {
            bytesRead = reader.Read(buffer, 0, buffer.Length); //Read bytes from reader stream.

            if (bytesRead == 0)
                break;

            fileStream.Write(buffer, 0, bytesRead); //Save the file from the bytes array.
        }
        fileStream.Close();
        notes.text = "Please note that the file was downloaded to the path D:\\ under the name of DownloadedFile.txt \n\nThis file is a dummy readme file downloaded from this path ftp://test.rebex.net/readme.txt";
    }

    public void UploadWithFTP()
    {
        try
        {

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(UploadURL)); //Start the FTP request with dummy URL.

            request.Credentials = new NetworkCredential("dlpuser", "rNrKYTX9g7z3RgJRmxWuGHbeu"); //Add credentials username and password of the server.
            request.KeepAlive = true;
            request.UseBinary = true;

            request.Method = WebRequestMethods.Ftp.UploadFile; 

            using (Stream fileStream = File.OpenRead(@"D:\\DownloadedFile.txt"))
            using (Stream ftpStream = request.GetRequestStream())
            {
                byte[] buffer = new byte[10240];
                int read;
                while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ftpStream.Write(buffer, 0, read);
                }
            }
            notes.text = "Please note that the file was Uploaded to the path ftp://ftp.dlptest.com/1/ under the name of test.txt \n\nThis file is a dummy readme file that was downloaded before";
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

  
  
}

