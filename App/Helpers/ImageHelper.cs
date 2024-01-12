using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace App.Helpers;

public static class ImageHelper {
    private static readonly HttpClient httpClient = new();

    public static Bitmap LoadFromResource(string resourceName) {
        var uri = new Uri($"avares://App/Assets/{resourceName}");
        return new Bitmap(AssetLoader.Open(uri));
    }

    public static async Task<Bitmap?> LoadFromUrl(Uri uri) {
        try {
            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            // ensure that the response is an image
            var contentType = response.Content.Headers.ContentType;
            if (contentType is null) return null;
            if (contentType.MediaType != "image/jpeg" && contentType.MediaType != "image/png") return null;

            var data = await response.Content.ReadAsByteArrayAsync();
            return new Bitmap(new MemoryStream(data));
        } catch (HttpRequestException ex) {
            Console.WriteLine($"An error occurred while downloading image '{uri}': {ex.Message}");
        }

        return null;
    }

    public static async Task<Bitmap?> LoadFromUrl(string url) {
        if (Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
            (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
        ) return await LoadFromUrl(uri);

        return null;
    }
}
