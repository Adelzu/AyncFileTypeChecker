namespace FileTypeChecker.Tests
{
    using FileTypeChecker.Types;
    using FileTypeChecker.Extensions;
    using NUnit.Framework;
    using System.IO;
    using System.Threading.Tasks;

    [TestFixture]
    public class StreamExtensionsTests
    {
        [Test]
        public async Task Is_ShouldReturnTrueIfTheTypesMatch()
        {
            using var fileStream = File.OpenRead("./files/test.bmp");
            var expected = true;
            var actual = await fileStream.IsAsync<Bitmap>();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Is_ShouldReturnFalseIfTypesDidNotMatch()
        {
             using var fileStream = File.OpenRead("./files/test.bmp");
            var expected = false;
            var actual = await fileStream.IsAsync<Gzip>();

            Assert.AreEqual(expected, actual);
        }
    }
}
