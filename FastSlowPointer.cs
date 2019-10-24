using System;
using System.Collections.Generic;
using System.Text;  
using System.Linq;

class FastSlowPointer {
  public static void Main (string[] args) 
  {
    // Test CircularArrayLoop
    var arr1 = new int[] {1, 2, -1, 2, 2};
    var arr2 = new int[] { 2, 2, -1, 2};
    var arr3 = new int[] {2,1,-1,-2};
    Console.WriteLine($"CircularArrayLoop [{string.Join(", ", arr1)}]: {CircularArrayLoop(arr1)}");
    Console.WriteLine($"CircularArrayLoop [{string.Join(", ", arr2)}]: {CircularArrayLoop(arr2)}");
    Console.WriteLine($"CircularArrayLoop [{string.Join(", ", arr3)}]: {CircularArrayLoop(arr3)}"); 
    // Test IsHappyNumber
    Console.WriteLine($"Is {232345544} Happy? {IsHappyNumber(232345544)}");
    Console.WriteLine($"*******************");
    Console.WriteLine($"Is {12} Happy? {IsHappyNumber(12)}");
    Console.WriteLine($"*******************");
    Console.WriteLine($"Is {0} Happy? {IsHappyNumber(0)}");
    Console.WriteLine($"*******************");
    Console.WriteLine($"Is {1} Happy? {IsHappyNumber(1)}");

    Console.WriteLine("end");
  }

  /* We are given an array containing positive and negative numbers. Suppose the array contains a number ‘M’ at a particular index. Now, if ‘M’ is positive we will move forward ‘M’ indices and if ‘M’ is negative move backwards ‘M’ indices. Write a method to determine if the array has a cycle. The cycle should have more than one element and should follow one direction which means the cycle should not contain both forward and backward movements.
  Time Complexity O(n^2)
  Space Complexity O(1)*/
  public static bool CircularArrayLoop(int[] arr)
  {
    var length = arr.Length;
    for(int i=0; i<length; i++)
    { 
      int slow =i;
      int fast =i;
      bool isForward = arr[i] >=0? true: false;   
      do
      { 
        slow = GetNextPosition(arr, slow, isForward);
        fast = GetNextPosition(arr, fast, isForward);

        if(fast != -1)
        {
          fast = GetNextPosition(arr, fast, isForward);
        }
      }
      while(slow !=-1 && fast !=-1 && slow!=fast);

      if(slow!= -1 && slow==fast)
      {
        return true;
      }
    }

    return false;
  }

  /* Faster Version by memorizing calculated indexes
  Time Complexity O(n)
  Space Complexity O(n)*/
  public static bool CircularArrayLoopFast(int[] arr)
  {
    Dictionary<int, bool> visited = new Dictionary<int,bool>();
    var length = arr.Length;
    for(int i=0; i<length; i++)
    {
      if(!visited.ContainsKey(i))
      {
        int slow =i;
        int fast =i;
        bool isForward = arr[i] >=0? true: false;    
        do
        { 
          slow = GetNextPosition(arr, slow, isForward);
          fast = GetNextPosition(arr, fast, isForward);
          if(fast != -1)
          {
            fast = GetNextPosition(arr, fast, isForward);
          }
        }
        while(slow !=-1 && fast !=-1 && slow!=fast);

        if(slow!= -1 && slow==fast)
        {
          return true;
        }
        if(!visited.ContainsKey(slow))
        {
           visited.Add(slow, false);
        }
        if(!visited.ContainsKey(fast))
        {
           visited.Add(fast, false);
        }

        visited.Add(i, false);
      }
    }

    return false;
  }

  private static int GetNextPosition(int[] arr, int index, bool isForward)
  {
    bool nextDir = arr[index]>=0 ? true: false;
    if(nextDir != isForward)
    {
      return -1;
    }

    int nextIndex = index+arr[index];
    if(nextIndex<0)
    {
      nextIndex = arr.Length + nextIndex;
    }
    else if(nextIndex> arr.Length-1)
    {
      nextIndex = nextIndex - arr.Length;
    }
    
    if(nextIndex == index)
    {
      nextIndex= -1;
    }
    
    return nextIndex;
  }

  /* Any number will be called a happy number if, after repeatedly replacing it with a number equal to the sum of the square of all of its digits, leads us to number ‘1’. All other (not-happy) numbers will never reach ‘1’. Instead, they will be stuck in a cycle of numbers which does not include ‘1’.
  
  Run Time: (Iterations to get to 1 or loop) x digits at each iteration =~ log(N)
  Space: log(n) for saving data in dictionary*/
  public static bool IsHappyNumber(int num)
  {
    bool continueLoop = true;
    Dictionary<int, int> digitsSum = new Dictionary<int,int>();
    while(continueLoop)
    {
      // get each digit
      char[] digits = num.ToString().ToCharArray();
      var sum =0;
      for(int i=0; i<digits.Length;i++)
      {
        // calculate sum = digit1^2+digit2^2
        sum+= (int)Math.Pow(Convert.ToInt32(digits[i].ToString()),2); 
      }

      if(sum ==1)
      {
        // if sum ==1 return happy
        return true;
      }
      else
      {  
        // if sum exist in dictionary return false , found cycle
        if(digitsSum.ContainsKey(sum))
        {
          return false;
        }
        else
        {
          digitsSum.Add(num, sum);
        }
      }
     
      num = sum;
    }

   return false;
  } 
}
