using System;
using System.Collections.Generic;
using System.Text;  
using System.Linq;

class SlidingWindow {
  public static void Main (string[] args) {
    Console.WriteLine ("Hello World, these are Array Sliding Window Pattern Questions:");
    
    int[] arr = { 2, 1, 5, 2, 3, 2 };
    int k =7;
    // Test FindMaxSumSubArray
    Console.WriteLine(FindMaxSumSubArray(k,arr));
    
    // Test AverageSubArray
    double[] r = AverageSubArray(k, arr);
    Array.ForEach( r, Console.WriteLine);
  }
  
  /* Given an array of positive numbers and a positive number ‘k’, 
     find the maximum sum of any contiguous subarray of size ‘k’. 
     The time complexity of this algorithm will be O(N) */
  public static int FindMaxSumSubArray(int k, int[] arr) 
  {
    List<int> result = new List<int>();
    var sum=0;
    for(int i=0; i<k; i++)
    {
      sum+=arr[i];
    }
    result.Add(sum);
    for(int i=k; i<arr.Length; i++)
    {
      sum-=arr[i-k];
      sum+=arr[i];
      result.Add(sum);
    }
    return result.Max();
  }
  
  /* Given an array, find the average of all subarrays of size ‘K’ in it.
     The time complexity of this algorithm will be O(N) */
  public static double[] AverageSubArray(int k, int[] arr)
  {
    List<double> result = new List<double>();
    double sum =0;
    for(int i=0; i<k; i++)
    {
      sum+=arr[i];
    }
    double average = sum/k;
    result.Add(average);
    for(int j=k; j<arr.Length; j++)
    {
      sum-= arr[j-k];
      sum+=arr[j];
      average = sum/k;
      result.Add(average);
    }
    return result.ToArray();
  }
}
