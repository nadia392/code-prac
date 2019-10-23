using System;
using System.Collections.Generic;
using System.Text;  
using System.Linq;

class MainClass {
  public static void Main (string[] args) {
  
    // Test PairWithTargetSumHash
    int[] arrInt = {2, 5, 9, 11};
    int target = 11;
    var result = PairWithTargetSumHash(arrInt,target);
    Array.ForEach(result, Console.WriteLine);
    
    // Test RemoveDuplicates
    int[] dups = {2, 3, 3, 3, 6, 9, 9};
    Console.WriteLine($"RemoveDuplicates: {RemoveDuplicates(dups)}");
    
    // Test RemoveElement
    int[] elements = {3, 2, 3, 6, 3, 10, 9, 3};
    int key = 3;
    Console.WriteLine($"RemoveElement: {RemoveElement(elements, key)}");
    
    // Test SortedArraySquares
    int[] baseArr = {-2, -1, 0, 2, 3};
    int[] r = SortedArraySquares(baseArr);
    Array.ForEach(r , Console.WriteLine);
    
    // Test TripletSumToZero
    int[] tri = new int[] {-5, 2, -1, -2, 3 };
    var triplesResult = TripletSumToZero(tri);
    foreach(List<int> l in triplesResult)
    {
      foreach(int i in l)
      {
        Console.Write(i+", ");
      }
      Console.WriteLine();
    }
    Console.WriteLine("Result Count: "+triplesResult.Count);
    Console.WriteLine("end");
  }
  
  /* Given an array of unsorted numbers, find all unique triplets in it that add up to zero. 
  Sorting the array will take O(N * logN). The searchPair() function will takeO(N). As we are calling searchPair() for every number in the input array, this means that overall searchTriplets() will take O(N * logN + N^2), which is asymptotically equivalent to O(N^2).
  Ignoring the space required for the output array, the space complexity of the above algorithm will be O(N) which is required for sorting.*/
  public static List<List<int>> TripletSumToZero(int[] arr)
  {
    Array.Sort(arr);
    List<List<int>> result = new List<List<int>>();

    for(int i=0; i<arr.Length-2; i++)
    {
      if (i > 0 && arr[i] == arr[i - 1])
      {
        continue;
      }
      var r = SearchPair(arr, -arr[i], i+1);
      if(r.Count>0)
      {
        result.Add(r);
      }
    }

    return result;
  }
  
  public static List<int> SearchPair(int[] arr, int targetDiff, int start) 
  {
    List<int> r = new List<int>();
    var end = arr.Length-1;
    while(start<end)
    {
      var diff = targetDiff- arr[start];
      if(diff == arr[end])
      {
        r.Add(-targetDiff);
        r.Add(arr[start]);
        r.Add(arr[end]);
        end--;
        start++;
        while (start < end && arr[start] == arr[start - 1])
        {
          start++;
        }
        while (start < end && arr[end] == arr[end + 1])
        {
          end--;
        }
      }
      else if (diff > arr[end])
      {
        start++;
      }
      else
      {
        end--;
      }
    }
    
    return r;
  }
  
  /* Given a sorted array, create a new array containing squares of all the number of the input array in the sorted order.
  The time complexity of the above algorithm will be O(N)
  The space complexity of the above algorithm will be O(N) */
  public static int[] SortedArraySquares(int[] arr) 
  {
    int l = arr.Length;
    int[] result = new int[l];
    int left =0;
    int right = l-1;
    int maxsqr = l-1;
    while(left<=right)
    {
      int leftVal = (int)Math.Pow(arr[left],2);
      int rightVal =  (int)Math.Pow(arr[right],2);
      if(rightVal>leftVal)
      {
        result[maxsqr] = rightVal;
        maxsqr--;
        right--;
      }
      else
      {
        result[maxsqr] = leftVal;
        maxsqr--;
        left++;
      }
    }
    return result;
  }

  /* Given an array of sorted numbers, remove all duplicates from it. You should not use any extra space; after removing the duplicates in-place return the new length of the array.
  The time complexity of the above algorithm will be O(N)
  The algorithm runs in constant space O(1)*/
  public static int RemoveDuplicates(int[] arr)
  {
    if(arr.Length<=1)
    {
      return arr.Length;
    }
    int next =1;
    for(int i=1; i<arr.Length;i++)
    {
      if(arr[next-1]!=arr[i])
      {
        arr[next]=arr[i];
        next++;
      }
    }

    return next;
  }

  /* Given an unsorted array of numbers and a target ‘key’, remove all instances of ‘key’ in-place and return the new length of the array.
  The time complexity of the above algorithm will be O(N)
  The algorithm runs in constant space O(1)*/
  public static int RemoveElement(int[] arr, int key) 
  {
    int next =0;
     for(int i=0; i<arr.Length; i++)
     {
       if(arr[i]!= key)
       {
         arr[next]=arr[i];
         next++;
         // shift the array to left
       }
     }

    return next;
  }

  /*Given an array of sorted numbers and a target sum, find a pair in the array whose sum is equal to the given target.
  The time complexity of the above algorithm will be O(N)
  The algorithm runs in constant space O(1)*/
  public static int[] PairWithTargetSum(int[] arr, int targetSum) 
  {
    int start =0;
    int end = arr.Length-1;
    while(start<end)
    {
      var sum = arr[start]+arr[end];
      if(sum == targetSum)
      {
        return new int[] {arr[start],arr[end]};
      }
      else if (sum > targetSum)
      {
        end--;
      }
      else
      {
        // is sum < targetSum
        start++;
      }
    }

    return new int[] {-1,-1};
  }

  /*The time complexity of the above algorithm will be O(N)
  The algorithm runs in constant space O(N)*/
  public static int[] PairWithTargetSumHash(int[] arr, int targetSum) 
  {
    Dictionary<int,int> pairs = new Dictionary<int,int>();

    foreach (int i in arr)
    {
      if(pairs.ContainsKey(targetSum-i))
      {
        return new int[] {i,targetSum-i};
      }
      else
      {
        pairs.Add(i,targetSum-i);
      }
    }

    return new int[] {-1,-1};
  }
  
}
