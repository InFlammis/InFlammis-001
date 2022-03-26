namespace FightShipArena.Assets.Scripts.Managers.ScoreManagement
{
    /// <summary>
    /// Interface for a score manager.
    /// A score manager is a component that keeps track of the current player's score
    /// and the top high-scores.
    /// </summary>
    public interface IScoreManager : IMyMonoBehaviour
    {
        /// <summary>
        /// Add the current score to the High Score list
        /// </summary>
        void AddToHighScore();

        /// <summary>
        /// Add a score to the current score
        /// </summary>
        /// <param name="score">Score to add</param>
        void AddToScore(int score);

        /// <summary>
        /// Add a value to the current score multiplier
        /// </summary>
        /// <param name="multiplier">Multiplier value to add</param>
        void AddToMultiplier(int multiplier);

        /// <summary>
        /// Reset the multiplier value to 1
        /// </summary>
        void ResetMultiplier();

        /// <summary>
        /// Reset the current score to 0
        /// </summary>
        void ResetCurrentScore();

        /// <summary>
        /// Clear the list of High scores
        /// </summary>
        void ResetHighScore();
    }
}
