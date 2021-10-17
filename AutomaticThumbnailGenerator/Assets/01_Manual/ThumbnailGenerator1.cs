using System.Collections;
using UnityEngine;

namespace ThumbGenExamples
{
    public class ThumbnailGenerator1 : MonoBehaviour
    {
        public RenderTexture TargetRenderTexture;

        public Camera ThumbnailCamera;

        public Transform ObjectPosition;

        public int ThumbnailWidth;

        public int ThumbnailHeight;

        public Texture2D TextureResult { get; private set; }

        /// <summary>
        /// If this is not null or empty, the texture is exported as a png to the file system.
        /// </summary>
        public string ExportFilePath;

        void Start()
        {
            ThumbnailWidth = 256;
            ThumbnailHeight = 256;
            
            Render("render_manual");
        }

        private void AssignRenderTextureToCamera()
        {
            if (ThumbnailCamera != null)
            {
                ThumbnailCamera.targetTexture = TargetRenderTexture;
            }
        }

        private void Render(string filename)
        {
            StartCoroutine(DoRender(filename));
        }

        IEnumerator DoRender(string filename)
        {
            yield return new WaitForEndOfFrame();

            ExecuteRender(filename);
        }

        private void ExecuteRender(string filename)
        {
            if (ThumbnailCamera == null)
            {
                throw new System.InvalidOperationException("ThumbnailCamera not found. Please assign one to the ThumbnailGenerator.");
            }

            if (TargetRenderTexture == null)
            {
                throw new System.InvalidOperationException("RenderTexture not found. Please assign one to the ThumbnailGenerator.");
            }

            AssignRenderTextureToCamera();

            Texture2D tex = null;

            {   // Create the texture from the RenderTexture

                RenderTexture.active = TargetRenderTexture;

                tex = new Texture2D(ThumbnailWidth, ThumbnailHeight);

                tex.ReadPixels(new Rect(0, 0, ThumbnailWidth, ThumbnailHeight), 0, 0);
                tex.Apply();

                TextureResult = tex;
            }

            // Export to the file system, if ExportFilePath is specified.
            if (tex != null && !string.IsNullOrWhiteSpace(ExportFilePath) && !string.IsNullOrWhiteSpace(filename))
            {
                string dir = System.IO.Path.GetDirectoryName(ExportFilePath);
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }

                foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                {
                    filename = filename.Replace(c, '_');
                }


                string finalPath = string.Format("{0}/{1}.png", ExportFilePath, filename);

                byte[] bytes = tex.EncodeToPNG();
                System.IO.File.WriteAllBytes(ExportFilePath + "/" + filename + ".png", bytes);
            }
        }
    }
}