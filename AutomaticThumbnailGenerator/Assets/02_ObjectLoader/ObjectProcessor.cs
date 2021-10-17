using System.Collections;
using UnityEngine;

namespace ThumbGenExamples
{
    public class ObjectProcessor : MonoBehaviour
    {
        private ObjectThumbnailActivity _activity;

        void Start()
        {

        }

        void Awake()
        {
            ThumbnailGenerator2 thumbGen = GetComponent<ThumbnailGenerator2>();

            //String resourcePath = “Resources / cube”;
            _activity = new ObjectThumbnailActivity(thumbGen, "objects/Cube");
            StartCoroutine(DoProcess());
        }

        IEnumerator DoProcess()
        {
            yield return new WaitForEndOfFrame();

            _activity.Setup();

            if(!_activity.CanProcess())
    
            yield return null;

            _activity.Process();

            StartCoroutine(DoCleanup());
        }

        IEnumerator DoCleanup()
        {
            yield return new WaitForEndOfFrame();

            _activity.Cleanup();
        }
    }
}
