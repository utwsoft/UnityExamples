using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThumbGenExamples
{
    public class MultiObjectProcessor : MonoBehaviour
    {
        private List<ObjectThumbnailActivity> _activities;

        private ObjectThumbnailActivity _curActivity;

        void Start()
        {
        }

        void Awake()
        {
            _curActivity = null;
            _activities = new List<ObjectThumbnailActivity>();

            ThumbnailGenerator2 thumbGen = GetComponent<ThumbnailGenerator2>();

            string[] objectResourceNames =
            {
                "objects/Cube",
                "objects/Cylinder",
                "objects/Capsule",
                "objects/Sphere"
            };

            foreach (var name in objectResourceNames)
            {
                var thumbActivity = new ObjectThumbnailActivity(thumbGen, name);
                _activities.Add(thumbActivity);
            }
        }

        void Update()
        {
            if (_curActivity == null)
            {
                if (_activities.Count > 0)
                {
                    _curActivity = _activities[0];
                    
                    _curActivity.Setup();

                    StartCoroutine("DoProcess");

                    _activities.RemoveAt(0);
                }
            }
        }

        IEnumerator DoProcess()
        {
            yield return new WaitForEndOfFrame();

            if (_curActivity == null)
                yield return null;

            if(!_curActivity.CanProcess())
                yield return null;

            _curActivity.Process();

            StartCoroutine(DoCleanup());
        }

        IEnumerator DoCleanup()
        {
            yield return new WaitForEndOfFrame();

            if (_curActivity != null)
            {
                _curActivity.Cleanup();
                _curActivity = null;
            }
        }
    }
}
