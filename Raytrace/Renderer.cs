namespace RayTrace
{
    public class Renderer
    {
        public void Render(IRenderContext renderContext)
        {
            int nx = renderContext.Width;
            int ny = renderContext.Height;
            renderContext.OnInit();

            for (var j = ny - 1; j >= 0; j--)
            {
                for (var i = 0; i < nx; i++)
                {
                    var r = (float)i / nx;
                    var g = (float)j / ny;
                    var b = 0.2f;
                    renderContext.OnRender(i, j, r, g, b, 1);
                }
            }

            renderContext.OnFinalise();
        }
    }
}
