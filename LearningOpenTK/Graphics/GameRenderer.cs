using System.Numerics;
using ImGuiNET;
using LearningOpenTK.Common;
using LearningOpenTK.OpenGL;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace LearningOpenTK.Graphics;

public class GameRenderer : IRenderer
{

    private Shader shader;
    private VertexArray vertexArray;
    private VertexBuffer<float> vertexBuffer;
    private IndexBuffer indexBuffer;
    private Texture texture;

    
    public void Load()
    {
        var positions = new []
        {
            -50f,-50f, 0.0f,0.0f,
             50f,-50f, 1.0f,0.0f,
             50f, 50f, 1.0f,1.0f,
            -50f, 50f, 0.0f,1.0f
        };
        var indices = new uint[]
        {
            0,1,2,
            2,3,0
        };
   
        shader = new Shader(ResourceLoc.Open("Resources/Shaders/default.shader"));
        shader.Bind();
        shader.SetUniform1i("u_Texture",0);
        
        
        texture = new Texture(ResourceLoc.Open("Resources/Textures/morgan.png").ReadImage());
        texture.Bind();
        
        vertexArray = new VertexArray();
        vertexBuffer = new VertexBuffer<float>(positions,  positions.Length* sizeof(float));
        indexBuffer = new IndexBuffer(indices,indices.Length);
        var layout = new VertexBufferLayout();
        layout.PushFloat(2);
        layout.PushFloat(2);
        vertexArray.AddBuffer(vertexBuffer,layout);
        vertexArray.Bind();
        indexBuffer.Bind();
        
        vertexArray.Unbind();
        vertexBuffer.Unbind();
        indexBuffer.Unbind();
        shader.Unbind();
        Logger.Instance.Log("Current version of OpenGL is: ",GL.GetString(StringName.Version));
    }
    

    
    public void Render(float delta)
    {
        Time += delta;
        Renderer.Clear(Color4.Black,ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        Renderer.EnableBlend();
        Renderer.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        {
            Matrix4.CreateTranslation(0.75f, 1, 0, out var modelMatrix);
            Matrix4.CreateTranslation(0,0,0,out var viewMatrix);
            Matrix4.CreateOrthographicOffCenter(0.0f, 640.0f, 0.0f, 480f, -1.0f, 1.0f,out var projectionMatrix);
            shader.Bind();
            shader.SetUniformMat4("u_ModelViewProj",projectionMatrix*viewMatrix*modelMatrix);
            Renderer.DrawElements(vertexArray,indexBuffer,shader);
        }
        
               
        {
            Matrix4.CreateTranslation(1.25f, 1, 0, out var modelMatrix);
            Matrix4.CreateTranslation(0,0,0,out var viewMatrix);
            Matrix4.CreateOrthographicOffCenter(0.0f, 640.0f, 0.0f, 480f, -1.0f, 1.0f,out var projectionMatrix);
            shader.Bind();
            shader.SetUniformMat4("u_ModelViewProj",projectionMatrix*viewMatrix*modelMatrix);
            Renderer.DrawElements(vertexArray,indexBuffer,shader);
        }
        
        
        Renderer.DisableBlend();
    }
    

    private IRenderer Renderer => this;

    private float Time { get; set; }
}