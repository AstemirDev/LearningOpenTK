using LearningOpenTK.Graphics;
using OpenTK.Graphics.ES11;
using StbImageSharp;

namespace LearningOpenTK.OpenGL;

public class Texture
{

    private int _id;


    public Texture(ImageResult image)
    {
        Image = image;
        _id = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D,_id);
        GL.TexParameter(TextureTarget.Texture2D,TextureParameterName.TextureMinFilter,(int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D,TextureParameterName.TextureMagFilter,(int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D,TextureParameterName.TextureWrapS,(int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D,TextureParameterName.TextureWrapT,(int)TextureWrapMode.ClampToEdge);
        GL.TexImage2D(TextureTarget.Texture2D,0,InternalFormat.Rgba8,Image.Width,Image.Height,0,PixelFormat.Rgba,PixelType.UnsignedByte,Image.Data);
        GL.BindTexture(TextureTarget.Texture2D,0);
    }

    ~Texture()
    {
        GL.DeleteTexture(_id);
    }


    
    public void Bind(uint slot = 0)
    {
        GL.ActiveTexture((TextureUnit)((uint)TextureUnit.Texture0+slot));
        GL.BindTexture(TextureTarget.Texture2D,_id);
    }

    public void Unbind()
    {
        GL.BindTexture(TextureTarget.Texture2D,0);
    }
    
    private ImageResult Image { get; }

    public int Width => Image.Width;

    public int Height => Image.Height;
}