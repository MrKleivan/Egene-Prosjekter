#include <iostream>

void pp(int & a)
{
    std::cout << &a << " " << a << " " << sizeof(a) << "\n";
}

template <typename T>
class DynamicArray
{
    size_t m_size;
    T * m_arr;

public:

    DynamicArray(size_t size)
        : m_size (size)
        , m_arr (new T[size]())
    {
        std::cout << "Array Constructor\n";
    }

    ~DynamicArray()
    {
        delete [] m_arr;
        std::cout << "Array Destructor\n";
    }

    T get(size_t index) const
    {
        return m_arr[index];
    }

    void set(size_t index, T val)
    {
        m_arr[index] = val;
    }

    void print() const
    {
        for (size_t i = 0; i < m_size; i++)
        {
            std::cout << i << " " << m_arr[i] << "\n";
        }
        
    }

    const T & operator [] (size_t index) const
    {
        return m_arr[index];
    }

    T & operator [] (size_t index)
    {
        return m_arr[index];
    }

};

int main(int argc, char * argv[])
{
    // int a = 10;
    // int b = 25;

    // int arr[10] = {};
    // // int * harr = new int[10];

    // pp(a);
    // pp(b);

    // for (size_t i = 0; i < 10; i++)
    // {
    //     pp(arr[i]);
    // }

    // int a = 10;
    // int b = 25;
    // int* pa = &a;
    // int* pb = &b;

    // // *pa = 17; 
    // *(&a) = 17;

    // pp(a);
    // pp(b);

    DynamicArray<float> myArray(10);

    // myArray.set(4, 7.14);
    // myArray.set(2, 134.420);

    myArray[3] = 3.14159;
    myArray[2] = 2.78;

    myArray.print();


    return 0;
}