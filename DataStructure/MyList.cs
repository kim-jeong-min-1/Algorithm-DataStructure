/// <summary>
/// 동적배열 클래스
/// </summary>
/// <typeparam name="T"></typeparam>
namespace DataStructure
{
    public class MyList<T>
    {
        const int DEFAULT_SIZE = 1;
        T[] data = new T[DEFAULT_SIZE];

        public int Count = 0; // 현재 사용중인 배열의 개수
        public int Capacity { get { return data.Length; } } // 예약된 배열의 개수
        public T this[int index]
        {
            get { return data[index]; }
            set { data[index] = value; }
        } // 리스트 인덱스 접근하는 프로퍼티

        public void Add(T item)
        {
            // 공간이 충분히 남아 있는지 확인
            if (Count >= Capacity)
            {
                // 공간이 없다면 늘려서 확보
                T[] newArray = new T[Count * 2];
                for (int i = 0; i < Count; i++)
                {
                    newArray[i] = data[i];
                }
                data = newArray;
            }

            // 공간에 데이터를 넣기
            data[Count] = item;
            Count++;
        } // 리스트에 요소를 추가하는 함수
        public void RemoveAt(int index)
        {
            // index 뒤의 리스트 요소들을 모두 앞으로 이동시킴
            for (int i = index; i < Count - 1; i++)
            {
                data[i] = data[i + 1];
            }

            // index의 리스트 요소를 기본값으로 변경
            data[Count - 1] = default(T);

            // 리스트의 Count를 감소시킴
            Count--;
        } // 인자로 인덱스를 받아 요소를 제거하는 함수
    }
}