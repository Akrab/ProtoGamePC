using UnityEngine;

namespace ProtoGame.Extensions
{
    public static class VectorExt
    {

        public static Vector3 IsoVectorConvert(this Vector3 input)
        {
            Quaternion rotation = Quaternion.Euler(0, 45, 0);
            Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotation);
            return isoMatrix.MultiplyPoint3x4(input);
        }

    }
}
