using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace Dinco
{
    class SimpleWindow3D : GameWindow
    {

        const float rotation_speed = 180.0f;
        float angle;
        bool showCube = true;
        int counter = 20;
        KeyboardState lastKeyPress;

        // Constructor.
        public SimpleWindow3D() : base(800, 600)
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Blue);
            //GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            MouseState mouse = OpenTK.Input.Mouse.GetState();

            if (keyboard[OpenTK.Input.Key.Escape])
            {
                Exit();
                return;
            }
            else if (keyboard[OpenTK.Input.Key.P] && !keyboard.Equals(lastKeyPress))
            {

                if (showCube == true)
                {
                    showCube = false;
                }
                else
                {
                    showCube = true;
                }
            }
            lastKeyPress = keyboard;


            if (mouse[OpenTK.Input.MouseButton.Left])
            {

                if (showCube == true)
                {
                    showCube = false;
                }
                else
                {
                    showCube = true;
                }
            }

            /////Modificare program pentru Lab 2. Pe tasta D cubul se va misca spre dreapta cu 20 de unitati iar cu tasta A
            ///cu 20 de unitati spre stanga. Prin apasarea scroll-ului mouse-ului se va centra obiectul.

            if (keyboard[Key.D])
            {
                base.OnResize(e);

                GL.Viewport(0 + counter, 0, Width, Height);


            }

            if (keyboard[Key.A])
            {
                base.OnResize(e);

                GL.Viewport(0 - counter, 0, Width, Height);

            }

            if (mouse[OpenTK.Input.MouseButton.Middle])
            {
                base.OnResize(e);

                GL.Viewport(0, 0, Width, Height);
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(15, 50, 15, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            //angle += rotation_speed * (float)e.Time;
            //GL.Rotate(angle, 0.0f, 1.0f, 0.0f);

            if (showCube == true)
            {
                DrawCube();
                DrawAxes_OLD();

            }

            SwapBuffers();
            //Thread.Sleep(1);
        }

        private void DrawAxes_OLD()
        {
            GL.Begin(PrimitiveType.Lines);

            // X
            GL.Color3(Color.PapayaWhip);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(-20, 0, 0);

            // Y
            GL.Color3(Color.PaleVioletRed);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 20, 0);

            // Z
            GL.Color3(Color.Gainsboro);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 20);


            GL.End();
        }

        private void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.BurlyWood);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(Color.FloralWhite);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(Color.MediumAquamarine);

            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(Color.Peru);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(Color.Thistle);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(Color.SteelBlue);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();
        }

        [STAThread]
        static void Main(string[] args)
        {

            using (SimpleWindow3D example = new SimpleWindow3D())
            {

                example.Run(30.0, 0.0);
            }
        }
    }

}
