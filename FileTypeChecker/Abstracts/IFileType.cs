namespace FileTypeChecker.Abstracts
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IFileType
    {
        /// <summary>
        /// Returns the name of the current <see cref="IFileType"/>.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns the extension of the current <see cref="IFileType"/> without dot.
        /// </summary>
        string Extension { get; }
        
        /// <summary>
        /// Checks if current <see cref="IFileType"/> matches with file from stream.
        /// </summary>
        /// <param name="stream">File as stream.</param>
        /// <param name="resetPosition"></param>
        /// <returns>Does file from stream match with current.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        Task<bool> DoesMatchWithAsync(Stream stream, bool resetPosition = true);
    }
}