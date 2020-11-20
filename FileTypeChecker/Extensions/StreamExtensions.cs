namespace FileTypeChecker.Extensions
{
    using FileTypeChecker.Abstracts;
    using FileTypeChecker.Common;
    using FileTypeChecker.Types;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public static class StreamExtensions
    {
        /// <summary>
        /// Validates that the file is from certain type
        /// </summary>
        /// <typeparam name="T">Type that implements FileType</typeparam>
        /// <param name="fileContent">File as stream</param>
        /// <returns>True if file match the desired type otherwise returns false.</returns>
        public static async Task<bool> IsAsync<T>(this Stream fileContent) where T : FileType, IFileType
        {
            var instance = Activator.CreateInstance(typeof(T)) as FileType;

            DataValidator.ThrowIfNull(instance, nameof(FileType));

            return await instance.DoesMatchWithAsync(fileContent).ConfigureAwait(false);
        }
        /// <summary>
        /// Validates that the current file is image.
        /// </summary>
        /// <param name="fileContent">File to check as stream.</param>
        /// <returns>Returns true if the provided file is image otherwise returns false. Supported image types are: Bitmap, JPEG, GIF, PNG, and TIF.</returns>
        public static async Task<bool> IsImageAsync(this Stream fileContent)
            => await fileContent.IsAsync<Bitmap>().ConfigureAwait(false)
            || await fileContent.IsAsync<JointPhotographicExpertsGroup>().ConfigureAwait(false)
            || await fileContent.IsAsync<GraphicsInterchangeFormat87>().ConfigureAwait(false)
            || await fileContent.IsAsync<GraphicsInterchangeFormat89>().ConfigureAwait(false)
            || await fileContent.IsAsync<PortableNetworkGraphic>().ConfigureAwait(false)
            || await fileContent.IsAsync<TaggedImageFileFormat>().ConfigureAwait(false);
        /// <summary>
        /// Validates that the current file is archive.
        /// </summary>
        /// <param name="fileContent"File to check as stream.></param>
        /// <returns>Returns true if the provided file is archive otherwise returns false. Supported archive types are: Extensible archive, Gzip, Rar, 7Zip, Tar, Zip, BZip2, LZip, and Xz.</returns>
        public static async Task<bool> IsArchiveAsync(this Stream fileContent)
            => await fileContent.IsAsync<ExtensibleArchive>().ConfigureAwait(false)
            || await fileContent.IsAsync<Gzip>().ConfigureAwait(false)
            || await fileContent.IsAsync<RarArchive>().ConfigureAwait(false)
            || await fileContent.IsAsync<SevenZipFile>().ConfigureAwait(false)
            || await fileContent.IsAsync<TarArchive>().ConfigureAwait(false)
            || await fileContent.IsAsync<ZipFile>().ConfigureAwait(false)
            || await fileContent.IsAsync<BZip2File>().ConfigureAwait(false)
            || await fileContent.IsAsync<LZipFile>().ConfigureAwait(false)
            || await fileContent.IsAsync<XzFile>().ConfigureAwait(false);
        /// <summary>
        /// Validates taht the current file is executable or executable and linkable.
        /// </summary>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        public static async Task<bool> IsExecutableAsync(this Stream fileContent)
            => await fileContent.IsAsync<Executable>().ConfigureAwait(false)
            || await fileContent.IsAsync<ExecutableAndLinkableFormat>().ConfigureAwait(false);
    }
}
