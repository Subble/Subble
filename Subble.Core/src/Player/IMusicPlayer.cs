using Subble.Core.Func;
using System.IO;

namespace Subble.Core.Player
{
    public interface IMusicPlayer
    {
        /// <summary>
        /// Start playing a file
        /// </summary>
        /// <param name="file">file path to play</param>
        void Play(FileInfo file);

        /// <summary>
        /// Resume playing the last file
        /// </summary>
        void Play();

        /// <summary>
        /// Stop playing
        /// </summary>
        void Stop();

        /// <summary>
        /// Pause playing
        /// </summary>
        void Pause();

        /// <summary>
        /// Current playing status
        /// </summary>
        PlayerStatus Status { get; }

        /// <summary>
        /// Current file in memory
        /// </summary>
        Option<FileInfo> CurrentFile { get; }

        /// <summary>
        /// Check if player can play file
        /// </summary>
        /// <param name="file">file to play</param>
        /// <returns>true, if is valid</returns>
        bool ValidateFile(FileInfo file);
    }
}
