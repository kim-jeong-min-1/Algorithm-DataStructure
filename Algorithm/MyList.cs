using System.Diagnostics;

public class MyList<T> 
{
    const int DEFAULT_SIZE = 1;
    
    T[] data = new T[DEFAULT_SIZE];

    public int Count 
    {
        get { return data.Length; }
    } // 현재 사용중인 배열의 개수

    public int Capacity; // 예약된 배열의 개수

   

}