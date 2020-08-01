using OpenGL;

namespace TestOpenGL.Rendering
{
    public class Camera
    {
        private const float Near = 0.1f;
        private const float Far = 100f;

        public Vector3 Position { get; set; }
        public Vector3 Target { get; set; }
        private readonly float aspect;
        
        public Matrix4 ViewMatrix => Matrix4.LookAt(Position, Target, new Vector3(0, 1, 0));

        public Matrix4 ProjectionMatrix => Matrix4.CreatePerspectiveFieldOfView(0.45f, aspect, Near, Far);

        
        public Camera(float aspect)
        {
            this.aspect = aspect;
            Position = new Vector3(0, 0, 10);
            Target = new Vector3(0,0,0);
        }
    }
}