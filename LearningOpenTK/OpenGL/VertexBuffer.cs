using OpenTK.Graphics.OpenGL;

namespace LearningOpenTK.OpenGL;

public class VertexBuffer<T> where T : struct
{

    private readonly int _id;
    
    public VertexBuffer(T[] data,int size)
    {
        GL.GenBuffers(1,out _id);
        GL.BindBuffer(BufferTarget.ArrayBuffer,_id);
        GL.BufferData(BufferTarget.ArrayBuffer,size,data,BufferUsageHint.StaticDraw);
    }

    ~VertexBuffer()
    {
        GL.DeleteBuffer(_id);
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer,_id);
    }

    public void Unbind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer,0);
    }
}