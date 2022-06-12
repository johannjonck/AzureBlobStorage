using Application.Dtos;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace AzureBlobStorage
{
    internal class Program
    {
        private static IConfigurationRoot? builder;

        private static void Main(string[] args)
        {
            builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            MenuOptions();
        }

        private static void MenuOptions()
        {
            int userChoice;

            do
            {
                Console.Clear();
                Console.WriteLine("\nChoose one of the following options:\n");
                Console.WriteLine("[ 1 ] Upload Document");
                Console.WriteLine("[ 2 ] Download Document");
                Console.WriteLine("[ 0 ] Exit\n");

            } while (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < 0 || userChoice > 2);

            Console.Clear();

            switch (userChoice)
            {
                case 1:
                    AddDocumentMenu();
                    break;
                case 2:
                    if (GetDocumentList())
                    {
                        SelectDocument();
                    }
                    Console.WriteLine("Enter a File name to download\n");
                    Console.ReadLine();
                    break;
                case 0:
                    Console.WriteLine("Exit");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Try again!!");
                    break;
            }
        }

        private static void AddDocumentMenu()
        {
            var name = "";
            bool fileValidation = false;
            do
            {
                Console.WriteLine("Please enter the file path to upload a document");
                name = Console.ReadLine();
            } while (fileValidation = !FileExist(name));


            if (!fileValidation)
            {
                Console.WriteLine("Completed\n");

                int userChoice;

                do
                {
                    Console.Clear();
                    Console.WriteLine("\nChoose 0 to exit or 1 to go back to the main menu:\n");

                } while (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < 0 || userChoice > 1);

                Console.Clear();

                switch (userChoice)
                {
                    case 1:
                        MenuOptions();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Try again!!");
                        break;
                }
            }
        }

        private static void SelectDocument()
        {
            var name = "";
            bool completed = false;
            do
            {
                Console.WriteLine("Type the FileFullName name to download");
                name = Console.ReadLine();

            } while (name.Length == 0);

            completed = DownloadDocument(name);
            //TODO: Return to main menu...
        }

        private static bool FileExist(string name)
        {
            AddDocumentMetadata(name);
            Console.WriteLine("File selected: " + name);
            return true;
        }

        private static void AddDocumentMetadata(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (HttpClient client = new HttpClient())
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    var fileSizeibBytes = fileInfo.Length;
                    var fileName = Path.GetFileName(filePath);
                    var newDocument = new Document { FileName = fileName, FileFullName = filePath, FileSize = fileSizeibBytes };
                    var relativeURI = builder.GetSection("DocumentEndPoints").GetSection("UploadDocument").Value;
                    using (HttpResponseMessage response = client.PostAsJsonAsync(relativeURI, newDocument).Result)
                    {
                        var jsonString = response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            else
            {
                Console.WriteLine("FILE DOES NOT EXIST => Make sure the path is correct");
            }
        }

        private static bool GetDocumentList()
        {
            var getData = "";
            var relativeUri = builder.GetSection("DocumentEndPoints").GetSection("ListDocument").Value;
            var request = new HttpRequestMessage(HttpMethod.Get, relativeUri);

            HttpClient client = new HttpClient();

            using (var response = client.SendAsync(request))
            {
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    getData = response.Result.Content.ReadAsStringAsync().Result;
                    if (string.IsNullOrEmpty(getData))
                    {
                        Console.WriteLine($"ERROR => Empty response");
                    }
                    else
                    {
                        var result = JsonConvert.DeserializeObject<IEnumerable<DocumentDto>>(getData);
                        if (result != null)
                        {
                            result.ToList().ForEach(x => Console.WriteLine(String.Format("FileName {0} | FileFullPath {1} | FileSize{2} | DateModified {3}", x.FileName, x.FileFullName, x.FileSize, x.DateModified.ToString())));
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("EMPTY RESULT => No documents to display");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"RESULT ERROR=> {response.Result.StatusCode} - {response.Result.RequestMessage}");
                }

                return false;
            }
        }

        private static bool DownloadDocument(string filePath)
        {
            using (HttpClient client = new HttpClient())
            {
                FileInfo fileInfo = new FileInfo(filePath);
                var fileSizeInBytes = fileInfo.Length;
                var fileName = Path.GetFileName(filePath);
                var newDocument = new Document { FileName = fileName, FileFullName = filePath, FileSize = fileSizeInBytes };
                var relativeURI = builder.GetSection("DocumentEndPoints").GetSection("DownloadDocument").Value;

                using (HttpResponseMessage response = client.PostAsJsonAsync(relativeURI, newDocument).Result)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"{filePath} downloaded with result: {jsonString}");
                    return true;
                }
            }
        }
      
    }

}