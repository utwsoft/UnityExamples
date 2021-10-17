using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThumbGenExamples
{
    public class QueueableMultiObjectProcessor : MonoBehaviour
    {
        private List<IGameObjectActivity> _activities;

        private IGameObjectActivity _curActivity;

        void Start()
        {
        }

        void Awake()
        {
            _curActivity = null;
            _activities = new List<IGameObjectActivity>();

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
                var thumbActivity = new ThumbnailActivity(thumbGen, name);
                QueueActivity(thumbActivity);
            }
        }

        public void QueueActivity(IGameObjectActivity activity)
        {
            StartCoroutine(DoQueueActivity(activity));
        }

        IEnumerator DoQueueActivity(IGameObjectActivity activity)
        {
            yield return new WaitUntil(() => _curActivity == null);

            _activities.Add(activity);
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

            if (!_curActivity.CanProcess())
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
