using StbImageSharp;

namespace LearningOpenTK.Common;

public class ResourceLoc
{
    private readonly string _path;
    
    private ResourceLoc(string path)
    {
        _path = path;
    }

    public string Name => _path[new Range(_path.LastIndexOf("/") + 1, _path.LastIndexOf(".", StringComparison.Ordinal))];
    
    public string Extension => _path[(_path.LastIndexOf(".")+1)..];

    public string Path => _path;
    
    
    public static ResourceLoc Open(string path)
    {
        return new ResourceLoc(path);
    }

    public string ReadAllText()
    {
        return File.ReadAllText(_path);
    }

    public ImageResult ReadImage()
    {
        StbImage.stbi_set_flip_vertically_on_load(1);
        using Stream stream = File.OpenRead(_path);
        var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        return image;
    }
}