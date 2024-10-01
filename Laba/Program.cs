using System;
using System.IO;

class RandomFileGenerator
{
    public static void GenerateRandomFile(string path, long sizeInBytes)
    {
        Random rand = new Random();
        using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        {

            long numbersToWrite = sizeInBytes / sizeof(int);

            for (long i = 0; i < numbersToWrite; i++)
            {
                writer.Write(rand.Next());
            }
        }
        Console.WriteLine($"File with random numbers of size {sizeInBytes / (1024 * 1024)} MB created.");
    }
}

class ExternalSort
{
    public static void SortFile(string inputFilePath, string outputFilePath)
    {
        Console.WriteLine("Loading the file into memory...");
        int[] numbers;

        using (BinaryReader reader = new BinaryReader(File.Open(inputFilePath, FileMode.Open)))
        {
            long totalInts = reader.BaseStream.Length / sizeof(int);
            numbers = new int[totalInts]; 

            for (long i = 0; i < totalInts; i++)
            {
                numbers[i] = reader.ReadInt32();
            }
        }

        Console.WriteLine("Sorting the numbers...");
        Array.Sort(numbers);  

        Console.WriteLine("Writing the sorted numbers to the output file...");
        using (BinaryWriter writer = new BinaryWriter(File.Open(outputFilePath, FileMode.Create)))
        {
            foreach (int number in numbers)
            {
                writer.Write(number);
            }
        }

        Console.WriteLine("File successfully sorted.");

        WriteSortedNumbersToTextFile(numbers, outputFilePath);
    }

    public static void WriteSortedNumbersToTextFile(int[] numbers, string outputFilePath)
    {
  
        using (StreamWriter writer = new StreamWriter(outputFilePath + ".txt"))
        {
            foreach (int number in numbers)
            {
                writer.WriteLine(number); 
            }
        }
        Console.WriteLine($"Sorted numbers written to {outputFilePath}.txt");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the file size for sorting (in MB):");
        long sizeInMB = long.Parse(Console.ReadLine());

        string inputFilePath = "largeNumbers.bin";
        string outputFilePath = "sortedNumbers.bin";

        long sizeInBytes = sizeInMB * 1024 * 1024;

        RandomFileGenerator.GenerateRandomFile(inputFilePath, sizeInBytes);

        var watch = System.Diagnostics.Stopwatch.StartNew();

        ExternalSort.SortFile(inputFilePath, outputFilePath);

        watch.Stop();
        Console.WriteLine($"Sorting completed in {watch.Elapsed.TotalSeconds} seconds.");
    }
}
