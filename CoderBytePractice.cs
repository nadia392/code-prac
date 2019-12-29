using System;
using System.Collections.Generic;
using System.Text;

class CoderBytePractice {
  public static void Main (string[] args) {
    Console.WriteLine ("Coder Byte Practice Questions:");
    TestFirstNonRep();
    TestValidParanthesis();
    TestRemoveChars();
    TestMapFunction();
    TestClockAngle();
    TestSumNestedArray();
    FizzBuzz();
    TestTwoSum();
  }
  public static void TestFirstNonRep()
  {
    Console.WriteLine(FirstNonRep("acdscabsdb"));
    Console.WriteLine(FirstNonRep("acdscabsd"));
    Console.WriteLine(FirstNonRep("acdscbsdbc"));
  }
  public static char FirstNonRep(string str)
  {
    Dictionary<char,int> strChars = new Dictionary<char,int>();
    foreach(char c in str)
    {
      if(strChars.ContainsKey(c))
      {
        strChars[c]++;
      }
      else{
        strChars.Add(c,1);
      }
    }
    foreach(char c in str)
    {
      if (strChars[c]==1)
      {
        return c;
      }
    }
    return '0';
  }
  public static void TestValidParanthesis()
  {
    string p = "{ad[a]s}((),())f";
    string p1 = "{ad[a]s}((),()f";
    Console.WriteLine(ValidParanthesis(p));
    Console.WriteLine(ValidParanthesis(p1));
  }
  public static bool ValidParanthesis(string str)
  {
    Dictionary<char,char> paran = new Dictionary<char,char>();
    Stack<char> paranStack = new Stack<char>();
    paran.Add('{', '}');
    paran.Add('[', ']');
    paran.Add('(',')');
    foreach(char c in str)
    {
      if(paran.ContainsKey(c))
      {
        paranStack.Push(paran[c]);
      }
      else if(paran.ContainsValue(c))
      {
        if (paranStack.Count == 0)
        {
          return false;
        }
        else if (paranStack.Pop()!=c)
        {
          return false;
        }
      }
    }
    if (paranStack.Count == 0)
    {
      return true;
    }
    else
    {
      return false;
    }
  }
  public static void TestRemoveChars()
  {
    char[] chars = {'b','a'};
    string str = "bbaloonbacon";
    Console.WriteLine(RemoveChars(chars,str));
  }
  public static string RemoveChars(char[] chr, string str)
  {
    HashSet<char> charsMap = new HashSet<char>();
    foreach(char c in chr)
    {
      charsMap.Add(c);
    }
    StringBuilder strbldr = new StringBuilder();
    foreach(char c in str)
    {
      if(!charsMap.Contains(c))
      {
        strbldr.Append(c);
      }
    }
    
    return strbldr.ToString();
  }
  public static void TestMapFunction()
  {
    int[] arr = {1,2,3,4};
    Action<int> act = new Action<int>(ActionFunction);
    MapFunction(arr,act);
  }
  public static void MapFunction(int[] arr, Action<int> func)
  {
    Array.ForEach(arr, func);
  }
  public static void ActionFunction(int n)
  {
    Console.WriteLine(n*n);
  }
  public static bool IsPrime(int n)
  {
    var sqrt = Math.Sqrt(n);
    for(int i =2; i<= sqrt; i++)
    {
      if(n%i==0)
      {
        return false;
      }
    }
    return true;
  }
  public static void TestClockAngle()
  {
    Console.WriteLine(ClockAngle(12,5));
    Console.WriteLine(ClockAngle(13,17));
    Console.WriteLine(ClockAngle(9,27));
    Console.WriteLine(ClockAngle(5,35));
  }
  public static int ClockAngle(int hour, int min)
  {
    // clock in 360 degrees
    // 180 right + 180 left
    // 360 Degrees / 60 Minutes = 6 degrees per Minutes
    // 360 Degrees / (12 hours * 60 minutes) = 0.5 degrees per minutes
    // hour degree 3 o'clock = 0.5 * (3*60) = 90
    // hour degree 3:15 = 0.5 * (3*60 + 15) = 3*60*0.5 + 15*0.5 = 97
    // minute degreee 3:15 = 15*6 = 80
    // clockAngel = 97-80 = 17
    // if degree > 180 ==> 360 - clockAngel 
    var hour_degree = 0.5;
    var min_degree = 6;
    var h_angle = hour_degree*(hour*60+min);
    var m_angle = min_degree*min;
    var angle = (int)Math.Abs(h_angle-m_angle);
    if(angle>180)
    {
      return 360-angle;
    }
    else{
      return angle;
    }
  }
  public static void TestSumNestedArray()
  {
    int[][] nesArray = new int[][] {
      new int[] {1},
      new int[] {2},
      new int[] {1,2,3},
      new int[] {4},
      new int[] {5}};
    Console.WriteLine(SumNestedArray(nesArray));
  }
  public static int SumNestedArray(int[][] arr)
  {
    var sum =0;
    foreach(int[] a in arr)
    {
      sum+= SumArray(a);
    }

    return sum;
  }

  public static int SumArray(int[] arr)
  {
    var sum =0;
    foreach(int n in arr)
    {
      sum+=n;
    }
    return sum;
  }
  public static void FizzBuzz()
  {
    for(int i=1; i<=100; i++)
    {
      if(i%3==0)
      {
        if(i%5 ==0)
        {
          Console.WriteLine("FizzBuzz");
        }
        else
        {
          Console.WriteLine("Fizz");
        }
      }
      else if(i%5 ==0)
      {
          Console.WriteLine("Buzz");
      }
      else
      {
          Console.WriteLine(i);
      }
    }
  }

  public static void TestTwoSum()
  {
    int[] arr = {6,11,4,15,0,-3,12};
    int sum = 1;
    var r = TwoSum(sum,arr);
    Array.ForEach(r, Console.WriteLine);
  }

  public static int[] TwoSum(int sum, int[] arr)
  {
    Dictionary<int, int> sumsTable = new Dictionary<int,int>();

    for(int i =0; i<arr.Length; i++)
    {
      if(sumsTable.ContainsKey(sum-arr[i]))
      {
        return new int[] {i, sumsTable[sum-arr[i]]};
      }
      else{
        if(!sumsTable.ContainsKey(arr[i]))
        {
          sumsTable.Add(arr[i],i);
        }
      }
    }
    return new int[] {-1,-1};
  }
}
