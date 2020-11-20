﻿namespace FileTypeChecker.App
{
    using FileTypeChecker.Abstracts;
    using FileTypeChecker.App.MyCustomTypes;
    using FileTypeChecker.Types;
    using FileTypeChecker.Extensions;
    using System;
    using System.IO;

    public static class StartUp
    {
        public static void Main()
        {
            // You can register your own custom types validation if its needed.
            FileTypeValidator.RegisterCustomTypes(typeof(MyCustomFileType).Assembly);

            for (int i = 1; i <= 12; i++)
            {
                using var fileStream = File.OpenRead($".\\files\\{i}");
                var isRecognizableType = FileTypeValidator.IsTypeRecognizable(fileStream);

                if (!isRecognizableType)
                {
                    Console.WriteLine("Unknown file");
                    Console.WriteLine(new string('=', 10));
                    continue;
                }

                IFileType fileType = FileTypeValidator.GetFileType(fileStream);
                                
                Console.WriteLine("Is Image?: {0}", fileStream.IsImageAsync());
                Console.WriteLine("Is Bitmap?: {0}", fileStream.IsAsync<Bitmap>());
                Console.WriteLine("Type Name: {0}", fileType.Name);
                Console.WriteLine("Type Extension: {0}", fileType.Extension);
                Console.WriteLine(new string('=', 10));
            }
        }
    }
}
