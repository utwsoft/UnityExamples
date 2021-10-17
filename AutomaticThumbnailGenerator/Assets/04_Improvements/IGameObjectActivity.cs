
namespace ThumbGenExamples
{
    /// <summary>
    /// Interface for any activity to perform
    /// </summary>
    public interface IGameObjectActivity
    {
        /// <summary>
        /// Run any setup, or bail if setup fails
        /// </summary>
        /// <returns></returns>
        bool Setup();

        /// <summary>
        /// Allow the runtime actvity to check
        /// if the process can be performed
        /// </summary>
        /// <returns></returns>
        bool CanProcess();

        /// <summary>
        /// Actually perform the activity.
        /// </summary>
        void Process();

        /// <summary>
        /// Run any cleanup necessary to advance to a next activity
        /// </summary>
        void Cleanup();
    }
}