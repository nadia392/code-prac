using System;
using System.Collections.Generic;
using System.Text;  
using System.Linq;

class SlidingWindow {
  public static void Main (string[] args) {
    Console.WriteLine ("Hello World, these are Array Sliding Window Pattern Questions:");
    
    // Test CharacterReplacement
    string common = "abbcb";
    int allowed = 1;
    Console.WriteLine("CharacterReplacement: "+CharacterReplacement(common,allowed));
    
    // Test NoRepeatSubstring
    string uniqStr = "abbccad";
    Console.WriteLine("Unique chars: " + NoRepeatSubstring(uniqStr));
    
    // Test Longest Substring K distinct
    string str = "arcaniacaaaaca";
    int distinct = 2;
    Console.WriteLine ("Longest Substring K distinct: " + LongestSubstringKDistinct(str,distinct));
    
    // Test MaxFruitCountOf2Types
    char[] arrChar = {'A', 'B', 'C', 'B', 'B', 'C'};
    Console.WriteLine ("max fruit = " + MaxFruitCountOf2Types(arrChar));
    
    // Test FindMaxSumSubArray
    int[] arr = { 2, 1, 5, 2, 3, 2 };
    int k = 7;
    Console.WriteLine("FindMaxSumSubArray = " + FindMaxSumSubArray(k,arr));
    
    // Test AverageSubArray
    double[] r = AverageSubArray(k, arr);
    Console.WriteLine("AverageSubArray: ");
    Array.ForEach( r, Console.WriteLine);
    
    // Test FindMinSubArray  
    int[] array = {3, 4, 1, 1, 6 };
    int sum = 8;
    Console.WriteLine("FindMinSubArray = " + FindMinSubArray(sum,array));
  }
  
  /*Given a string with lowercase letters only, if you are allowed to replace no more than ‘k’ letters with any letter, 
  find the length of the longest substring having the same letters after replacement.
  Run time in O(n).*/
  public static int CharacterReplacement(string str, int k)
  {
    Dictionary<char,int> dic = new Dictionary<char, int>();
    int maxrepeating =0;
    int start =0;
    int maxLen = 0;
    for(int i=0; i<str.Length; i++)
    {
      if(dic.ContainsKey(str[i]))
      { 
        dic[str[i]]++;
        maxrepeating = Math.Max(dic[str[i]],maxrepeating);
         
      }
      else
      {
        if(i-start+1-maxrepeating>k)
        {
          char startChar = str[start];
          if(dic.ContainsKey(startChar))
          {
            dic[startChar]--;
          }
        }
        dic.Add(str[i],1);
        start++;
      }
      maxLen = Math.Max(maxLen, maxrepeating+k);
    }
    return maxLen;
  }
   
  /*Given a string, find the length of the longest substring which has no repeating characters.
  The time complexity of the above algorithm will be O(N)
  The space complexity of the algorithm will be O(K) where K is the number of distinct characters in the input string.*/
    public static int NoRepeatSubstring(string str)
    {
      HashSet<char> uniqueChars = new HashSet<char>();
      int maxLength =0;
      int start =0;
      int end =0;
      for(int i=0; i<str.Length ; i++)
      {
        if(!uniqueChars.Contains(str[i]))
        {
            uniqueChars.Add(str[i]);
            end++;
        }
        else
        {
          maxLength= Math.Max(uniqueChars.Count,maxLength);
          uniqueChars.Remove(str[start]);
          start++;
          i--;
        }
      }
      maxLength = Math.Max(uniqueChars.Count,maxLength);
      return maxLength;
    }
  
  /*Given an array of characters where each character represents a fruit tree, you are given two baskets and your goal is to put maximum number of fruits in each basket. The only restriction is that each basket can have only one type of fruit. 
   Time Complexity O(n)
   Space Complexity O(1) since we can max have 2 letters in dictionary*/
  public static int MaxFruitCountOf2Types(char[] arr)
  {
    Dictionary<char,int> treeMap = new Dictionary<char,int>();
    int maxLength =0;
    int start =0;
    int end =0;
    for(int i=0; i<arr.Length && start<arr.Length; i++)
    {
      if(!treeMap.ContainsKey(arr[i]))
      {
        if(treeMap.Count == 2)
        {
          maxLength = Math.Max(GetMapLength(treeMap), maxLength);
          SlideWindow(treeMap, arr[start]);
          start ++;
          i--;
        }
        else
        {
          treeMap.Add(arr[i],1);
        }
      }
      else
      {
        treeMap[arr[i]]++;
        end++;
      }
    }
    maxLength = Math.Max(GetMapLength(treeMap), maxLength);
    return maxLength;
  }
  
  /*
  Given a string, find the length of the longest substring in it with no more than K distinct characters.
  Time Complexity O(n)
  */
  public static int LongestSubstringKDistinct(string str, int k)
  {
    Dictionary<char, int> lettersMap = new Dictionary<char,int>();
    int maxLength =0;
    int start =0;
    int end =0;
    for(int i=0; i<str.Length && start<str.Length; i++)
    {
      char c = str[i];
      if(!lettersMap.ContainsKey(c))
      {
        // check Length
        if(lettersMap.Count == k)
        {
          // get length and remove start
          maxLength = Math.Max(GetMapLength(lettersMap), maxLength);
          SlideWindow(lettersMap, str[start]);
          start ++;
          i--;
        }
        else
        {
          end++;
          lettersMap.Add(c, 1);
        }
      }
      else
      {
        end++;
        lettersMap[c]++;
      }
    }

    maxLength = Math.Max(GetMapLength(lettersMap), maxLength);
    return maxLength;
  }
  
  // Helper method to decrease the char c occurrance count or remove it.
  private static Dictionary<char,int> SlideWindow(Dictionary<char,int> map, char c)
  {
    if(map.ContainsKey(c))
    {
      if(map[c]>1)
      {
        map[c]--;
      }
      else
      {
        map.Remove(c);
      }
    }
    return map;
  }
  
  // Helper Method to get Dictionary Values Sum
  private static int GetMapLength(Dictionary<char, int> map)
  {
    int result =0;
    foreach(int i in map.Values)
    {
      result+=i;
    }
    return result;
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
  
  /* Given an array of positive numbers and a positive number ‘S’, 
     find the length of the smallest subarray whose sum is greater than or equal to ‘S’. 
     Return 0, if no such subarray exists.
     The time complexity of this algorithm will be O(N). */
  public static int FindMinSubArray(int S, int[] arr) 
  {
    if(arr.Max()>=S)
    {
      return 1;
    }
    var sum =0;
    var l =0; 
    var minLength =arr.Length+1;
  
    for(int i=0; i<arr.Length; i++)
    {
      if(sum>=S)
      {
        if(l<minLength)
        {
          minLength=l;
        }
        sum-=arr[i-(l-1)];
        l--;
      }
      else
      {
        l++;
        sum+=arr[i];
      }
      
    }
    return minLength == arr.Length+1 ? 0 : minLength;
  }
}
