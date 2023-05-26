using LearningOpenTK.Common;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using GL = OpenTK.Graphics.OpenGL.GL;

namespace LearningOpenTK.OpenGL;

public class Shader
{
    private readonly int _id;

    private Dictionary<string, int> _uniformLocs = new ();
    
    public Shader(ResourceLoc shaderLoc)
    {
        var text = shaderLoc.ReadAllText();
        var vertexShader = StringUtils.GetBetween(text,
            "VertexShader_Begin;\n",
            "VertexShader_End;\n");
        var fragmentShader = StringUtils.GetBetween(text,
            "FragmentShader_Begin;\n",
            "FragmentShader_End;\n");
        _id = CreateShader(vertexShader, fragmentShader);
    }

    ~Shader()
    {
        GL.DeleteProgram(_id);
    }

    private int CreateShader(string vertexShader,string fragmentShader)
    {
        var program = GL.CreateProgram();
        var vs = CompileShader(ShaderType.VertexShader, vertexShader);
        var fs = CompileShader(ShaderType.FragmentShader, fragmentShader);
        GL.AttachShader(program, vs);
        GL.AttachShader(program, fs);
        GL.LinkProgram(program);
        GL.ValidateProgram(program);
        GL.DeleteShader(vs);
        GL.DeleteShader(fs);
        return program;
    }
    
    private int CompileShader(ShaderType shaderType,string source)
    {
        var id = GL.CreateShader(shaderType);
        GL.ShaderSource(id,source);
        GL.CompileShader(id);
        GL.GetShader(id,ShaderParameter.CompileStatus,out var result);
        if (result == 0)
        {
            var log = GL.GetShaderInfoLog(id);
            Logger.Instance.Error("Failed to compile ",Enum.GetName(shaderType)," shader: \n",log);
            GL.DeleteShader(id);
            return 0;
        }
        return id;
    }

    public void Bind()
    {
        GL.UseProgram(_id);
    }

    public void Unbind()
    {
        GL.UseProgram(0);
    }

    private int GetUniformLoc(string name)
    {
        if (_uniformLocs.TryGetValue(name, out var loc))
        {
            return loc;
        }
        var location = GL.GetUniformLocation(_id, name);
        if (location == -1)
        {
            Logger.Instance.Warning("Uniform ",name," doesn't exist.");
        }
        return location;
    }

    public void SetUniform1i(string name, int value)
    {
        GL.Uniform1(GetUniformLoc(name),value);   
    }
    
    public void SetUniform1f(string name, float value)
    {
        GL.Uniform1(GetUniformLoc(name),value);   
    }
    
    public void SetUniform4(string name, float v0, float v1, float v2, float v3)
    {
        GL.Uniform4(GetUniformLoc(name),v0,v1,v2,v3);
    }
    
    public void SetUniform4(string name,Vector4 vector4)
    {
        GL.Uniform4(GetUniformLoc(name),vector4);
    }
    
    public void SetUniform4(string name,Color4 color)
    {
        GL.Uniform4(GetUniformLoc(name),color);
    }

    public void SetUniformMat4(string name,Matrix4 matrix)
    {
        GL.UniformMatrix4(GetUniformLoc(name),false,ref matrix);
    }
}