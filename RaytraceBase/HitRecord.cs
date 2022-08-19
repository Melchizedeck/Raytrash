namespace RayTrace
{
    public struct HitRecord
    {
        public double t;
        public Vector3 p;
        public Vector3 normal;
        public bool frontFace;
        public IHitable Hit;
        public void SetFaceNormal(Ray r, Vector3 outwardNormal)
        {
            frontFace = Vector3.Dot(r.Direction, outwardNormal) < 0;
            normal = frontFace ? outwardNormal : -outwardNormal;
        }
    }    
}
