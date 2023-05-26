
using OpenTK.Graphics.OpenGL;

namespace LearningOpenTK.OpenGL;

public class VertexBufferLayout
{

    private readonly List<VertexBufferElement> _elements = new List<VertexBufferElement>();

    public VertexBufferLayout()
    {
        Stride = 0;
    }

    private void Push(VertexAttribPointerType type,int count)
    {
        _elements.Add(new VertexBufferElement(type,count,false));
        Stride += count*SizeOfType(type);
    }
    
    public void PushFloat(int count)
    {
        Push(VertexAttribPointerType.Float,count);
    }
    
    public void PushInt(int count)
    {
        Push(VertexAttribPointerType.UnsignedInt,count);
    }
    
    public void PushByte(int count)
    {
        Push(VertexAttribPointerType.UnsignedByte,count);
    }

    public static int SizeOfType(VertexAttribPointerType type)
    {
        switch (type)
        {
            case VertexAttribPointerType.Byte:
            {
                return sizeof(byte);
            }
            case VertexAttribPointerType.Double:
            {
                return sizeof(double);
            }
            case VertexAttribPointerType.Float:
            {
                return sizeof(float);
            }
            case VertexAttribPointerType.Int:
            {
                return sizeof(int);
            }
            case VertexAttribPointerType.Short:
            {
                return sizeof(short);
            }
            case VertexAttribPointerType.UnsignedByte:
            {
                return sizeof(byte);
            }
            case VertexAttribPointerType.UnsignedInt:
            {
                return sizeof(uint);
            }
            case VertexAttribPointerType.UnsignedShort:
            {
                return sizeof(ushort);
            }
        }
        return 0;
    }
    
    public int Stride { get; set; }

    public List<VertexBufferElement> GetElements() => _elements;
    
    public struct VertexBufferElement
    {

        public VertexBufferElement(VertexAttribPointerType type, int count, bool normalized)
        {
            Type = type;
            Count = count;
            Normalized = normalized;
        }


        public VertexAttribPointerType Type { get; }
        
        public int Count { get; }

        public bool Normalized { get; }
    }
}