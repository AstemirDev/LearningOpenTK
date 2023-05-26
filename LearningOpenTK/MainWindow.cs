using System.Numerics;
using ImGuiNET;
using LearningOpenTK.Graphics;
using LearningOpenTK.OpenGL;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace LearningOpenTK;

public class MainWindow : GameWindow
{

    private readonly GameRenderer _gameRenderer = new();
    
    private ImGuiController _imGuiController;

    
    public MainWindow(string title,int width,int height) : 
        base(GameWindowSettings.Default, ConfigureSettings(title,width,height))
    {
        Size = new Vector2i(width, height);
        GL.Enable(EnableCap.Texture2D);
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        _gameRenderer.Load();
        _imGuiController = new ImGuiController(ClientSize.X, ClientSize.Y);
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        _imGuiController.WindowResized(ClientSize.X, ClientSize.Y);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);
        _imGuiController.Update(this,(float)e.Time);
        if (KeyboardState.IsKeyDown(Keys.Escape))
            Close();
    }
        
    protected override void OnRenderFrame(FrameEventArgs e)
    {
        _gameRenderer.Render((float)e.Time);
        _imGuiController.Render();
        ImGuiController.CheckGLError("End of frame");
        SwapBuffers();
    }

    protected override void OnTextInput(TextInputEventArgs e)
    {
        base.OnTextInput(e);
        _imGuiController.PressChar((char)e.Unicode);
    }
    
    protected override void OnMouseWheel(MouseWheelEventArgs e)
    {
        base.OnMouseWheel(e);
        _imGuiController.MouseScroll(e.Offset);
    }

    private static NativeWindowSettings ConfigureSettings(string title,int width,int height)
    {
        return new NativeWindowSettings
        {
            Size = new Vector2i(width, height),
            APIVersion = new Version(3, 3),
            API = ContextAPI.OpenGL,
            Flags = ContextFlags.ForwardCompatible,
            Profile = ContextProfile.Core,
            Title = title
        };
    }

}