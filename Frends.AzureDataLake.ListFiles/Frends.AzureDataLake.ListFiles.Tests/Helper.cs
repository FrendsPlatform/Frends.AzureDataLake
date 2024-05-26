using Azure.Storage.Files.DataLake;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Frends.AzureDataLake.ListFiles.Tests
{
    internal class Helper
    {
        internal static async Task CreateContainerAndTestFiles(bool delete, string connString, string containerName)
        {
            var client = new DataLakeServiceClient(connString);
            var container = client.GetFileSystemClient(containerName);
            if (delete)
                await container.DeleteIfExistsAsync();
            else
            {
                await container.CreateIfNotExistsAsync(null, CancellationToken.None);

                var files = new List<string>()
            {
                "TestFile.txt", "TestFile2.txt", "Temp/SubFolderFile", "Temp/SubFolderFile2"
            };

                foreach (var file in files)
                {
                    var fileClient = container.GetFileClient(file);
                    await fileClient.CreateIfNotExistsAsync();
                    byte[] fileContent = Encoding.UTF32.GetBytes($"This is {file}");
                    using (var stream = new MemoryStream(fileContent))
                    {
                        await fileClient.AppendAsync(stream, offset: 0);
                        await fileClient.FlushAsync(stream.Length);
                    }
                }
            }
        }
    }
}
