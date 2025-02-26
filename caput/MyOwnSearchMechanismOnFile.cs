using System.Buffers.Text;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;

namespace caput
{
    internal class MyOwnSearchMechanismOnFile
    {
        //Here's an example C# program that demonstrates the partitioning of a file, sorting, and searching using multiple threads:



            static void MainSearch()
            {
                // Define the file path and the number of threads
                string filePath = "large_file.txt";
                int numThreads = 4;

                // Define the number to search for
                int searchNumber = 42;

                // Create a cancellation token source
                CancellationTokenSource cts = new CancellationTokenSource();

                // Partition the file into equal parts
                long[] partitionOffsets = PartitionFile(filePath, numThreads);

                // Create and start the threads
                Task[] tasks = new Task[numThreads];
                for (int i = 0; i < numThreads; i++)
                {
                    int threadId = i;
                    tasks[i] = Task.Run(() =>
                    {
                        SearchPartition(filePath, partitionOffsets[threadId], partitionOffsets[threadId + 1], searchNumber, cts);
                    });
                }

                // Wait for the first thread to find the number
                Task firstTask = Task.WhenAny(tasks);
                firstTask.Wait();

                // Cancel the other threads
                cts.Cancel();

                // Wait for all threads to complete
                Task.WaitAll(tasks);

                Console.WriteLine("Search complete.");
            }

            static long[] PartitionFile(string filePath, int numThreads)
            {
                // Get the file length
                long fileLength = new FileInfo(filePath).Length;

                // Calculate the partition size
                long partitionSize = fileLength / numThreads;

                // Create an array to store the partition offsets
                long[] partitionOffsets = new long[numThreads + 1];

                // Calculate the partition offsets
                for (int i = 0; i < numThreads; i++)
                {
                    partitionOffsets[i] = i * partitionSize;
                }

                // Set the last partition offset to the end of the file
                partitionOffsets[numThreads] = fileLength;

                return partitionOffsets;
            }

            static void SearchPartition(string filePath, long startOffset, long endOffset, int searchNumber, CancellationTokenSource cancellationToken)
            {
                // Open the file stream
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    // Seek to the start of the partition
                    fileStream.Seek(startOffset, SeekOrigin.Begin);

                    // Read the partition into memory
                    byte[] partitionData = new byte[endOffset - startOffset];
                    fileStream.Read(partitionData, 0, partitionData.Length);

                // Convert the partition data to integers
                int[] partitionIntegers = new int[partitionData.Length / 4];

                // Sort the partition integers
                Array.Sort(partitionIntegers);

                    // Search for the number
                    int index = Array.BinarySearch(partitionIntegers, searchNumber);

                    // Check if the number was found
                    if (index >= 0)
                    {
                        Console.WriteLine($"Number {searchNumber} found in partition by thread {Thread.CurrentThread.ManagedThreadId}.");

                        // Cancel the other threads
                        cancellationToken.Cancel();
                    }
                    else
                    {
                        Console.WriteLine($"Number {searchNumber} not found in partition by thread {Thread.CurrentThread.ManagedThreadId}.");
                    }
                }
            }
        }
    /*This program partitions a large file into equal parts based on the number of threads specified.Each thread then sorts its partition and searches for a specific number using binary search.If a thread finds the number, it cancels the other threads using a cancellation token.
    Note that this program assumes that the file contains integers stored in binary format. You may need to modify the program to accommodate a different file format.

    Also, keep in mind that this program uses a simple cancellation mechanism to stop the other threads when one thread finds the number.This may not be the most efficient or robust way to implement cancellation, especially in a real-world application.*/

/*Here's a breakdown of the time complexity:

1. Partitioning: O(n/p) - Each thread processes a partition of size n/p.
2. Sorting: O((n/p) log(n/p)) - Each thread sorts its partition using a comparison-based sorting algorithm(like Array.Sort in C#).
3. Binary Search: O(log(n/p)) - Each thread performs a binary search on its sorted partition.

Since the threads work in parallel, the overall time complexity is the sum of the partitioning and sorting/searching complexities: O(n/p + log(n/p)).
*/


}

