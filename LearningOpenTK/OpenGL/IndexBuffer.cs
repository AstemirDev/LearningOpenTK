using OpenTK.Graphics.OpenGL;

namespace LearningOpenTK.OpenGL;

public class IndexBuffer
{

    private readonly int _id;

    public IndexBuffer(uint[] indices,int count)
    {
        Count = count;
        GL.GenBuffers(1,out _id);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer,_id);
        GL.BufferData(BufferTarget.ElementArrayBuffer,count*sizeof(uint),indices,BufferUsageHint.StaticDraw);
    }

    ~IndexBuffer()
    {
        GL.DeleteBuffer(_id);
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer,_id);
    }

    public void Unbind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer,0);
    }

    public int Count { get; }
}