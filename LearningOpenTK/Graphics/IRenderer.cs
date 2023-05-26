using LearningOpenTK.OpenGL;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace LearningOpenTK.Graphics;

public interface IRenderer
{

    void Render(float delta);

    void EnableBlend()
    {
        GL.Enable(EnableCap.Blend);
    }

    void DisableBlend()
    {
        GL.Disable(EnableCap.Blend);
    }

    void BlendFunc(BlendingFactor src,BlendingFactor dest)
    {
        GL.BlendFunc(src,dest);
    }
    
    void DrawElements(VertexArray va,IndexBuffer ib,Shader shader)
    {
        shader.Bind();
        va.Bind();
        ib.Bind();
        GL.DrawElements(PrimitiveType.Triangles,ib.Count,DrawElementsType.UnsignedInt,(IntPtr)null);
    }

    void Clear(Color4 color,ClearBufferMask mask)
    {
        GL.ClearColor(color);
        GL.Clear(mask);
    }
}