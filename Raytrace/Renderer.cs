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
                    var col = new Vector3((float)i / nx, (float)j / ny, 0.2f);

                    renderContext.OnRender(i, j, col[0], col[1], col[2], 1);
                }
            }

            renderContext.OnFinalise();
        }
    }
}
