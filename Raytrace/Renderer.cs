namespace RayTrace
{
    public class Renderer
    {
        bool hitSphere(Vector3 center, float radius, Ray r)
        {
            var oc = r.Origin - center;
            var a = Vector3.Dot(r.Direction, r.Direction);
            var b = 2f * Vector3.Dot(oc, r.Direction);
            var c = Vector3.Dot(oc, oc) - radius * radius;
            var discriminant = b * b - 4 * a * c;
            return discriminant > 0;
        }
        Vector3 color(Ray r)
        {
            if (hitSphere(new Vector3(0, 0, -1), 0.5f, r))
            {
                return new Vector3(1, 0, 0);
            }
            var unitDirection = Vector3.UnitVector(r.Direction);
            var t = 0.5f * (unitDirection[1] + 1);
            return (1.0f - t) * new Vector3(1, 1, 1) + t * new Vector3(0.5f, 0.7f, 1f);
        }
        public void Render(IRenderContext renderContext)
        {
            int nx = renderContext.Width;
            int ny = renderContext.Height;
            renderContext.OnInit();

            var lowerLeftCorner = new Vector3(-2, -1, -1);
            var horizontal = new Vector3(4, 0, 0);
            var vertical = new Vector3(0, 2, 0);
            var origin = new Vector3(0, 0, 0);

            for (var j = ny - 1; j >= 0; j--)
            {
                for (var i = 0; i < nx; i++)
                {
                    var u = (float)i / nx;
                    var v = (float)j / ny;

                    var r = new Ray(origin, lowerLeftCorner + u * horizontal + v * vertical);
                    var col = color(r);

                    renderContext.OnRender(i, j, col[0], col[1], col[2], 1);
                }
            }

            renderContext.OnFinalise();
        }
    }
}
