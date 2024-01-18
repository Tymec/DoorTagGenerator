using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Platform;

namespace App;

public static class Utils {
    private static readonly HttpClient httpClient = new();

    public static byte[] LoadFromResource(string resourceName) {
        var uri = new Uri($"avares://App/Assets/{resourceName}");
        var readStream = AssetLoader.Open(uri);
        var memoryStream = new MemoryStream();
        readStream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }

    public static async Task<byte[]?> LoadFromUrl(Uri uri) {
        try {
            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            // ensure that the response is an image
            var contentType = response.Content.Headers.ContentType;
            if (contentType is null) return null;
            if (contentType.MediaType != "image/jpeg" && contentType.MediaType != "image/png") return null;

            var data = await response.Content.ReadAsByteArrayAsync();
            return data;
        } catch (HttpRequestException ex) {
            Console.WriteLine($"An error occurred while downloading image '{uri}': {ex.Message}");
        }

        return null;
    }
}
