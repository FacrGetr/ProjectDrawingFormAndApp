using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DrawingModel
{
    class FileHandler
    {
        const string SAVE_FILE_TITLE = "test.txt";
        const string CONTENT_TYPE = "text/plain";
        const string DOWNLOAD_PATH = "..\\\\";
        const string APPLICATION_NAME = "Windows Project#2";
        const string CLIENT_SECRET_FILE_NAME = "client_secret.json";
        GoogleDriveService _service = new GoogleDriveService(APPLICATION_NAME, CLIENT_SECRET_FILE_NAME);

        public GoogleDriveService Service
        {
            get
            {
                return _service;
            }
        }

        public string SaveFileName
        {
            get
            {
                return SAVE_FILE_TITLE;
            }
        }

        string ExistingSave
        {
            get
            {
                foreach (Google.Apis.Drive.v2.Data.File file
                    in _service.ListRootFileAndFolder())
                {
                    if (file.Title == SAVE_FILE_TITLE)
                    {
                        return file.Id;
                    }
                }
                return null;

            }
        }

        //非同步上傳（請用 task 來 run 它
        public void Upload()
        {
            if (ExistingSave != null)
            {
                _service.DeleteFile(ExistingSave);
            }

            _service.UploadFile(SAVE_FILE_TITLE, CONTENT_TYPE);
        }

        //讀檔
        public void LoadFile(Model model)
        {
            Google.Apis.Drive.v2.Data.File saveFile = FindSaveFile();

            DownloadSaveFile(saveFile);
            StreamReader streamReader = new StreamReader(SAVE_FILE_TITLE);
            string aLine = streamReader.ReadLine();
            while (aLine != null)
            {
                model.AddShape(aLine);
                aLine = streamReader.ReadLine();
            }
        }

        //找存檔
        Google.Apis.Drive.v2.Data.File FindSaveFile()
        {
            const string EXCEPTION_MESSAGE = "找不到檔案";
            List<Google.Apis.Drive.v2.Data.File> rootFiles = _service.ListRootFileAndFolder();

            foreach (Google.Apis.Drive.v2.Data.File file in rootFiles)
            {
                if (file.Title == SAVE_FILE_TITLE)
                {
                    return file;
                }
            }
            throw new Exception(EXCEPTION_MESSAGE);
        }

        //下載檔案
        void DownloadSaveFile(object selectedItem)
        {
            Google.Apis.Drive.v2.Data.File file = selectedItem as Google.Apis.Drive.v2.Data.File;
            _service.DownloadFile(file, DOWNLOAD_PATH);
        }

    }
}
