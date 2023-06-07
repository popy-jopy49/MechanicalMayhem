using UnityEngine;

namespace SWAssets.Utils.Meshes
{
    public class Mesh2D {
        
        private const int sortingOrderDefault = 5000;

        public GameObject gameObject;
        public Transform transform;
        private Material material;
        private Vector3[] vertices;
        private Vector2[] uv;
        private int[] triangles;
        private Mesh mesh;

		#region Create Methods
		public static Mesh2D CreateEmpty(Vector3 position, Vector3 eulerZ, Material material, int sortingOrderOffset = 0) {
            return new Mesh2D(null, position, Vector3.one, eulerZ, material, new Vector3[0], new Vector2[0], new int[0], sortingOrderOffset);
        }

        // --------------------------------------------
        public static Mesh2D Create(Vector3 position, Vector3 eulerRotation, Material material, Vector3[] vertices, Vector2[] uv, int[] triangles, int sortingOrderOffset = 0)
        {
            return new Mesh2D(null, position, Vector3.one, eulerRotation, material, vertices, uv, triangles, sortingOrderOffset);
        }

        public static Mesh2D Create(Vector3 position, Vector3 eulerRotation, float meshWidth, float meshHeight, Material material, UVCoords uvCoords, int sortingOrderOffset = 0)
        {
            return new Mesh2D(null, position, Vector3.one, eulerRotation, meshWidth, meshHeight, material, uvCoords, sortingOrderOffset);
        }

        public static Mesh2D Create(Vector3 lowerLeftCorner, float width, float height, Material material, UVCoords uvCoords, int sortingOrderOffset = 0)
        {
            return Create(lowerLeftCorner, lowerLeftCorner + new Vector3(width, height), material, uvCoords, sortingOrderOffset);
        }

        public static Mesh2D Create(Vector3 lowerLeftCorner, Vector3 upperRightCorner, Material material, UVCoords uvCoords, int sortingOrderOffset = 0)
        {
            float width = upperRightCorner.x - lowerLeftCorner.x;
            float height = upperRightCorner.y - lowerLeftCorner.y;
            Vector3 localScale = upperRightCorner - lowerLeftCorner;
            Vector3 position = lowerLeftCorner + localScale * .5f;
            return new Mesh2D(null, position, Vector3.one, Vector3.zero, width, height, material, uvCoords, sortingOrderOffset);
        }
        
        // --------------------------------------------
        public static Mesh2D Create(Vector3 position, Material material, Vector3[] vertices, Vector2[] uv, int[] triangles, int sortingOrderOffset = 0)
        {
            return new Mesh2D(null, position, Vector3.one, Vector3.zero, material, vertices, uv, triangles, sortingOrderOffset);
        }

        public static Mesh2D Create(Vector3 lowerLeftCorner, float width, float height, Vector3 eulerRotation, Material material, UVCoords uvCoords, int sortingOrderOffset = 0)
        {
            return Create(lowerLeftCorner, lowerLeftCorner + new Vector3(width, height), eulerRotation, material, uvCoords, sortingOrderOffset);
        }

        public static Mesh2D Create(Vector3 lowerLeftCorner, Vector3 upperRightCorner, Vector3 eulerRotation, Material material, UVCoords uvCoords, int sortingOrderOffset = 0)
        {
            float width = upperRightCorner.x - lowerLeftCorner.x;
            float height = upperRightCorner.y - lowerLeftCorner.y;
            Vector3 localScale = upperRightCorner - lowerLeftCorner;
            Vector3 position = lowerLeftCorner + localScale * .5f;
            return new Mesh2D(null, position, Vector3.one, eulerRotation, width, height, material, uvCoords, sortingOrderOffset);
        }
        #endregion

		public Mesh2D(Transform parent, Vector3 localPosition, Vector3 localScale, Vector3 eulerRotation, float meshWidth, float meshHeight, Material material, UVCoords uvCoords, int sortingOrderOffset) {
            this.material = material;

            vertices = new Vector3[4];
            uv = new Vector2[4];
            triangles = new int[6];

            /* 0,1
             * 1,1
             * 0,0
             * 1,0
             */
            
            float meshWidthHalf  = meshWidth  / 2f;
            float meshHeightHalf = meshHeight / 2f;

            vertices[0] = new Vector3(-meshWidthHalf,  meshHeightHalf);
            vertices[1] = new Vector3( meshWidthHalf,  meshHeightHalf);
            vertices[2] = new Vector3(-meshWidthHalf, -meshHeightHalf);
            vertices[3] = new Vector3( meshWidthHalf, -meshHeightHalf);
            
            if (uvCoords == null) {
                uvCoords = UVCoords.Create(0, 0, material.mainTexture.width, material.mainTexture.height);
            }

            Vector2[] uvArray = GetUVRectangleFromPixels(uvCoords.x, uvCoords.y, uvCoords.width, uvCoords.height, material.mainTexture.width, material.mainTexture.height);

            ApplyUVToUVArray(uvArray, ref uv);

            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            triangles[3] = 2;
            triangles[4] = 1;
            triangles[5] = 3;

            mesh = new Mesh();

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
            gameObject.transform.parent = parent;
            gameObject.transform.localPosition = localPosition;
            gameObject.transform.localScale = localScale;
            gameObject.transform.localEulerAngles = eulerRotation;

            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            gameObject.GetComponent<MeshRenderer>().material = material;

            transform = gameObject.transform;

            SetSortingOrderOffset(sortingOrderOffset);
        }
        
        public Mesh2D(Transform parent, Vector3 localPosition, Vector3 localScale, Vector3 eulerRotation, Material material, Vector3[] vertices, Vector2[] uv, int[] triangles, int sortingOrderOffset) {
            this.material = material;
            this.vertices = vertices;
            this.uv = uv;
            this.triangles = triangles;

            mesh = new Mesh();

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
            gameObject.transform.parent = parent;
            gameObject.transform.localPosition = localPosition;
            gameObject.transform.localScale = localScale;
            gameObject.transform.localEulerAngles = eulerRotation;

            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            gameObject.GetComponent<MeshRenderer>().material = material;

            transform = gameObject.transform;

            SetSortingOrderOffset(sortingOrderOffset);
        }

        private Vector2 ConvertPixelsToUVCoordinates(int x, int y, int textureWidth, int textureHeight) {
            return new Vector2((float)x / textureWidth, (float)y / textureHeight);
        }

		#region Setters
		private void ApplyUVToUVArray(Vector2[] uv, ref Vector2[] mainUV) {
            if (uv == null || uv.Length < 4 || mainUV == null || mainUV.Length < 4) throw new System.Exception();
            mainUV[0] = uv[0];
            mainUV[1] = uv[1];
            mainUV[2] = uv[2];
            mainUV[3] = uv[3];
        }

        public void SetUVCoords(UVCoords uvCoords) {
            Vector2[] uvArray = GetUVRectangleFromPixels(uvCoords.x, uvCoords.y, uvCoords.width, uvCoords.height, material.mainTexture.width, material.mainTexture.height);
            ApplyUVToUVArray(uvArray, ref uv);
            mesh.uv = uv;
        }

        public void SetSortingOrderOffset(int sortingOrderOffset) {
            SetSortingOrder(GetSortingOrder(gameObject.transform.position, sortingOrderOffset));
        }

        public void SetSortingOrder(int sortingOrder) {
            gameObject.GetComponent<Renderer>().sortingOrder = sortingOrder;
        }

        public void SetLocalScale(Vector3 localScale) {
            transform.localScale = localScale;
        }

        public void SetPosition(Vector3 localPosition) {
            transform.localPosition = localPosition;
        }

        public void AddPosition(Vector3 addPosition) {
            transform.localPosition += addPosition;
        }
		#endregion

		#region Getters
		private Vector2[] GetUVRectangleFromPixels(int x, int y, int width, int height, int textureWidth, int textureHeight)
        {
            /* 0, 1
             * 1, 1
             * 0, 0
             * 1, 0
             * */
            return new Vector2[] {
                ConvertPixelsToUVCoordinates(x, y + height, textureWidth, textureHeight),
                ConvertPixelsToUVCoordinates(x + width, y + height, textureWidth, textureHeight),
                ConvertPixelsToUVCoordinates(x, y, textureWidth, textureHeight),
                ConvertPixelsToUVCoordinates(x + width, y, textureWidth, textureHeight)
            };
        }

        public Vector3 GetPosition() {
            return transform.localPosition;
        }

        public int GetSortingOrder() {
            return gameObject.GetComponent<Renderer>().sortingOrder;
        }

        public Mesh GetMesh() {
            return mesh;
        }

        private static int GetSortingOrder(Vector3 position, int offset, int baseSortingOrder = sortingOrderDefault)
        {
            return (int)(baseSortingOrder - position.y) + offset;
        }
		#endregion

		#region Basic Methods
		public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }

        public void DestroySelf() {
            Object.Destroy(gameObject);
        }
		#endregion

		public class UVCoords
        {
            public int x, y, width, height;

            private UVCoords(int x, int y, int width, int height)
            {
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
            }

            public static UVCoords Create() => new UVCoords(0, 0, 1, 1);
            public static UVCoords CreatePos(int x, int y) => new UVCoords(x, y, 1, 1);
            public static UVCoords CreateSize(int width, int height) => new UVCoords(0, 0, width, height);
            public static UVCoords Create(int x, int y, int width, int height) => new UVCoords(x, y, width, height);
        }

    }

}