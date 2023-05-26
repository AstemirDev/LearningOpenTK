using OpenTK.Graphics.OpenGL;

namespace LearningOpenTK.OpenGL;

public class VertexArray
{
    private readonly int _id;
    
    public VertexArray()
    {
        GL.GenVertexArrays(1,out _id);
    }

    ~VertexArray()
    {
        GL.DeleteVertexArray(_id);
    }


    public void AddBuffer<T>(VertexBuffer<T> vb,VertexBufferLayout layout) where T: struct
    {
        Bind();
        vb.Bind();
        var offset = 0;
        for (var i = 0; i < layout.GetElements().Count; i++)
        {
            var element = layout.GetElements()[i];
            GL.EnableVertexAttribArray(i);
            GL.VertexAttribPointer(i,element.Count,element.Type,element.Normalized,layout.Stride,offset);
            offset += element.Count*VertexBufferLayout.SizeOfType(element.Type);
        }
    }

    public void Bind()
    {
        GL.BindVertexArray(_id);
    }

    public void Unbind()
    {
        GL.BindVertexArray(0);
    }
}