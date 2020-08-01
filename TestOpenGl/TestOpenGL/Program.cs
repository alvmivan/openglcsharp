using System;
using OpenGL;
using Tao.FreeGlut;
using TestOpenGL.Rendering;

namespace TestOpenGL
{
    class Program
    {


        static void Main()
        {
            var renderer = new Renderer();
            renderer.Run();
            
        }
    }
}
