using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThumbGenExamples
{
    public class ObjectThumbnailActivity
    {
        private GameObject _object;
        private ThumbnailGenerator2 _thumbGen;

        private string _resourceName;

        public ObjectThumbnailActivity(ThumbnailGenerator2 thumbGen, string resourceName)
        {
            _thumbGen = thumbGen;
            _resourceName = resourceName;
        }
        
        public bool Setup()
        {
            if (string.IsNullOrWhiteSpace(_resourceName))
                return false;

            GameObject prefab = Resources.Load<GameObject>(_resourceName);

            GameObject stage = GameObject.Find("Stage");

            _object = GameObject.Instantiate(prefab, stage?.transform);

            Camera cam = _thumbGen.ThumbnailCamera.GetComponent<Camera>();

            cam.transform.LookAt(stage.transform);
            
            return true;
        }

        public bool CanProcess()
        {
            return true;
        }

        public void Process()
        {
            if (_thumbGen == null)
                return;

            string filename = string.Format("render_objectloader_{0}", _resourceName);
            _thumbGen.Render(filename);
        }

        public void Cleanup()
        {
            if (_object != null)
            {
                GameObject.Destroy(_object);

                _object = null;
            }
        }
    }
}
